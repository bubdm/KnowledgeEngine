using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace KnowledgeEngineRules.Core
{
    class ExpressionInterpreter
    {
        #region Member Variables

        private List<ByteCode> _bytes;
        private OpStack _opStack;
        private KnowledgeBase _knowledge;
        private VariableBase _variables;
        private Rule _rule;
        private Engine _engine;

        #endregion

        #region Constructors

        public ExpressionInterpreter(Rule rule)
        {
            _bytes = new List<ByteCode>();
            _opStack = new OpStack();

            _rule = rule;
            _knowledge = _rule.KnowledgeBase;
            _variables = _knowledge.Variables;
        }

        #endregion

        #region Compile Methods

        /// <summary>
        /// Determines if this expression returns true
        /// </summary>
        public void Compile(ExpressionNode node, Engine engine)
        {
            _engine = engine;
            GetBytes(node);
        }

        private void GetBytes(ExpressionNode node)
        {
            if (node.Left != null)
            {
                GetBytes(node.Left);
            }
            if (node.Right.Count > 0)
            {
                foreach (ExpressionNode en in node.Right)
                {
                    GetBytes(en);
                    switch (node.Opr)
                    {
                        case ExpressionOperators.None:
                            break;

                        case ExpressionOperators.Or:
                            _bytes.Add(ByteCode.Create(OpCodes.Or));
                            break;

                        case ExpressionOperators.And:
                            _bytes.Add(ByteCode.Create(OpCodes.And));
                            break;

                        case ExpressionOperators.Xor:
                            _bytes.Add(ByteCode.Create(OpCodes.Xor));
                            break;

                        case ExpressionOperators.Eq:
                            _bytes.Add(ByteCode.Create(OpCodes.Eq));
                            break;

                        case ExpressionOperators.Ne:
                            _bytes.Add(ByteCode.Create(OpCodes.Ne));
                            break;

                        case ExpressionOperators.Lt:
                            _bytes.Add(ByteCode.Create(OpCodes.Lt));
                            break;

                        case ExpressionOperators.Gt:
                            _bytes.Add(ByteCode.Create(OpCodes.Gt));
                            break;

                        case ExpressionOperators.Le:
                            _bytes.Add(ByteCode.Create(OpCodes.Le));
                            break;

                        case ExpressionOperators.Ge:
                            _bytes.Add(ByteCode.Create(OpCodes.Ge));
                            break;

                        case ExpressionOperators.Str:
                            _bytes.Add(ByteCode.Create(OpCodes.Str));
                            break;

                        case ExpressionOperators.Add:
                            _bytes.Add(ByteCode.Create(OpCodes.Add));
                            break;

                        case ExpressionOperators.Sub:
                            _bytes.Add(ByteCode.Create(OpCodes.Sub));
                            break;

                        case ExpressionOperators.Mul:
                            _bytes.Add(ByteCode.Create(OpCodes.Mul));
                            break;

                        case ExpressionOperators.Div:
                            _bytes.Add(ByteCode.Create(OpCodes.Div));
                            break;

                        case ExpressionOperators.Mod:
                            _bytes.Add(ByteCode.Create(OpCodes.Mod));
                            break;

                        case ExpressionOperators.Neg:
                            _bytes.Add(ByteCode.Create(OpCodes.Neg));
                            break;

                        case ExpressionOperators.Not:
                            _bytes.Add(ByteCode.Create(OpCodes.Not));
                            break;

                        case ExpressionOperators.IsNull:
                            _bytes.Add(ByteCode.Create(OpCodes.IsNull));
                            break;

                        default:
                            throw new Exception(string.Format(
                                "'{0}' Expression Operator has not been accounted for.",
                                node.Opr
                                ));
                    }
                }
            }
            if (node.Value != null)
            {
                if (node.Value is Variable)
                {
                    _bytes.Add(ByteCode.Create(OpCodes.LoadVariable, (node.Value as Variable).ID));
                }
                else if (node.Value is ExpressionPath)
                {
                    ExpressionPath path = node.Value as ExpressionPath;
                    Type parent = GetBytes(path, null);

                    LinkedListNode<ExpressionPath> pathnode = path.Paths.First;
                    while (pathnode != null)
                    {
                        parent = GetBytes(pathnode.Value, parent);
                        pathnode = pathnode.Next;
                    }
                }
                else if (node.Value is string)
                {
                    _bytes.Add(ByteCode.Create(OpCodes.PushStr, node.Value));
                }
                else if (node.Value is float || node.Value is int)
                {
                    _bytes.Add(ByteCode.Create(OpCodes.PushNbr, node.Value));
                }
                else if (node.Value is bool)
                {
                    _bytes.Add(ByteCode.Create(OpCodes.PushBool, node.Value));
                }
                else
                {
                    _bytes.Add(ByteCode.Create(OpCodes.Push, node.Value));
                }
            }
        }

        private Type GetBytes(ExpressionPath path, Type parentType)
        {
            if (path.IsMethod)
            {
                return GetMethod(path, parentType);
            }

            if (path.IsAssignment)
            {
                PropertyInfo pi = ReflectionCache.Instance.GetProperty(parentType, path.ID);

                if (pi == null)
                {
                    throw new NullReferenceException(string.Format(
                        "The property '{0}.{1}' could not be found",
                        parentType.FullName,
                        path.ID
                        ));
                }

                ByteCode bc = ByteCode.Create(OpCodes.InvokeMethod, path.ID);
                bc.VirtualMethod = pi.GetSetMethod();

                GetBytesFromArgs(path, bc, parentType);

                _bytes.Add(bc);

                return null;
            }

            return GetProperty(path, parentType);
        }

        private Type GetMethod(ExpressionPath path, Type parentType)
        {
            ByteCode bc = ByteCode.Create(OpCodes.InvokeMethod, path.ID);
            bc.VirtualMethod = ReflectionCache.Instance.GetMethod(parentType, path.ID);

            if (bc.VirtualMethod == null)
            {
                throw new NullReferenceException(string.Format(
                    "The method '{0}.{1}()' could not be found",
                    parentType.FullName,
                    path.ID
                    ));
            }

            GetBytesFromArgs(path, bc, parentType);

            _bytes.Add(bc);

            return bc.VirtualMethod.ReturnType;
        }

        private Type GetProperty(ExpressionPath path, Type parentType)
        {
            if (string.Compare(path.ID, "this") == 0)
            {
                ByteCode bc = ByteCode.Create(OpCodes.LoadObject, "Engine");
                bc.VirtualObject = ReflectionCache.Instance.GetThis();

                _bytes.Add(bc);

                return _engine.GetType();
            }

            ByteCode bcprop = ByteCode.Create(OpCodes.InvokeProperty, path.ID);
            bcprop.VirtualProperty = ReflectionCache.Instance.GetProperty(parentType, path.ID);

            if (bcprop.VirtualProperty == null)
            {
                FieldInfo fi = ReflectionCache.Instance.GetField(parentType, path.ID);
                if (fi != null)
                {
                    throw new NotSupportedException(string.Format(
                        "Fields (or public member variables) are NOT supported.  '{0}.{1}' must be wrapped by a property.",
                        parentType.FullName,
                        path.ID
                        ));
                }

                throw new NullReferenceException(string.Format(
                    "The property '{0}.{1}' could not be found",
                    parentType.FullName,
                    path.ID
                    ));
            }
            _bytes.Add(bcprop);

            bcprop.VirtualMethod = bcprop.VirtualProperty.GetGetMethod();

            return bcprop.VirtualMethod.ReturnType;
        }

        private void GetBytesFromArgs(ExpressionPath path, ByteCode bc, Type parentType)
        {
            if (path.Args.Count > 0)
            {
                if (path.Args.Count != bc.VirtualMethodParemterCount)
                {
                    throw new Exception(string.Format(
                        "Invalid number of arguments for '{0}.{1}()', expected {2} found {3}.",
                        parentType.FullName,
                        path.ID,
                        bc.VirtualMethodParemterCount,
                        path.Args.Count
                        ));
                }

                path.Args.Reverse();
                foreach (ExpressionNode node in path.Args)
                {
                    GetBytes(node);
                }
            }
        }

        #endregion

        #region Run Methods

        /// <summary>
        /// Interprets this expression (run only w/o an expected return)
        /// </summary>
        public void Exec()
        {
            Evaluate();
        }

        /// <summary>
        /// Interprets this expression
        /// </summary>
        public object Run()
        {
            Evaluate();

            return _opStack.Pop();
        }

        private void Evaluate()
        {
            _opStack.Clear();

            foreach (ByteCode bc in _bytes)
            {
                Eval(bc);
            }
        }

        /// <summary>
        /// Eval a piece of byte code
        /// </summary>
        private void Eval(ByteCode bc)
        {
            switch (bc.OpCode)
            {
                case OpCodes.LoadObject:
                    _opStack.Push(bc.VirtualObject.GetValue(_rule, null));
                    break;

                case OpCodes.InvokeMethod:
                    InvokeMethod(bc);
                    break;

                case OpCodes.InvokeProperty:
                    InvokeProperty(bc);
                    break;

                case OpCodes.LoadVariable:
                    _opStack.Push(_variables[bc.Constant as string].Value);
                    break;

                case OpCodes.Push:
                    _opStack.Push(bc.Constant);
                    break;

                case OpCodes.PushBool:
                    _opStack.PushBool(bc.Constant);
                    break;

                case OpCodes.PushNbr:
                    _opStack.PushNbr(bc.Constant);
                    break;

                case OpCodes.PushStr:
                    _opStack.PushStr(bc.Constant);
                    break;

                case OpCodes.Eq:
                    {
                        object right = _opStack.Pop();
                        object left = _opStack.Pop();
                        if (left is string || right is string)
                        {
                            _opStack.Push(
                                string.Compare(Convert.ToString(left), Convert.ToString(right), true) == 0
                                );
                        }
                        else if (left is IConvertible && right is IConvertible)
                        {
                            _opStack.Push(
                                Convert.ToSingle(left) == Convert.ToSingle(right)
                                );
                        }
                        else
                        {
                            _opStack.Push(left == right);
                        }
                    }
                    break;

                case OpCodes.Ne:
                    {
                        object right = _opStack.Pop();
                        object left = _opStack.Pop();
                        if (left is string || right is string)
                        {
                            _opStack.Push(
                                string.Compare(Convert.ToString(left), Convert.ToString(right), true) != 0
                                );
                        }
                        else if (left is IConvertible && right is IConvertible)
                        {
                            _opStack.Push(
                                Convert.ToSingle(left) != Convert.ToSingle(right)
                                );
                        }
                        else
                        {
                            _opStack.Push(left != right);
                        }
                    }
                    break;

                case OpCodes.Gt:
                    {
                        float right = _opStack.PopNbr();
                        float left = _opStack.PopNbr();
                        _opStack.Push(left > right);
                    }
                    break;

                case OpCodes.Ge:
                    {
                        float right = _opStack.PopNbr();
                        float left = _opStack.PopNbr();
                        _opStack.Push(left >= right);
                    }
                    break;

                case OpCodes.Lt:
                    {
                        float right = _opStack.PopNbr();
                        float left = _opStack.PopNbr();
                        _opStack.Push(left < right);
                    }
                    break;

                case OpCodes.Le:
                    {
                        float right = _opStack.PopNbr();
                        float left = _opStack.PopNbr();
                        _opStack.Push(left <= right);
                    }
                    break;

                case OpCodes.Str:
                    {
                        string right = _opStack.PopStr();
                        string left = _opStack.PopStr();
                        _opStack.Push(left + right);
                    }
                    break;

                case OpCodes.Add:
                    {
                        float right = _opStack.PopNbr();
                        float left = _opStack.PopNbr();
                        _opStack.Push(left + right);
                    }
                    break;

                case OpCodes.Sub:
                    {
                        float right = _opStack.PopNbr();
                        float left = _opStack.PopNbr();
                        _opStack.Push(left - right);
                    }
                    break;

                case OpCodes.Mul:
                    {
                        float right = _opStack.PopNbr();
                        float left = _opStack.PopNbr();
                        _opStack.Push(left * right);
                    }
                    break;

                case OpCodes.Div:
                    {
                        float right = _opStack.PopNbr();
                        float left = _opStack.PopNbr();
                        _opStack.Push(left / right);
                    }
                    break;

                case OpCodes.Mod:
                    {
                        int right = (int)_opStack.PopNbr();
                        int left = (int)_opStack.PopNbr();
                        _opStack.Push(left % right);
                    }
                    break;

                case OpCodes.And:
                    {
                        bool right = _opStack.PopBool();
                        bool left = _opStack.PopBool();
                        _opStack.Push(left && right);
                    }
                    break;

                case OpCodes.Or:
                    {
                        bool right = _opStack.PopBool();
                        bool left = _opStack.PopBool();
                        _opStack.Push(left || right);
                    }
                    break;

                case OpCodes.Xor:
                    {
                        bool right = _opStack.PopBool();
                        bool left = _opStack.PopBool();
                        _opStack.Push(left ^ right);
                    }
                    break;

                case OpCodes.Not:
                    {
                        bool item = _opStack.PopBool();
                        _opStack.Push(!item);
                    }
                    break;

                case OpCodes.Neg:
                    {
                        float op = _opStack.PopNbr();
                        _opStack.Push(op * -1);
                    }
                    break;

                case OpCodes.IsNull:
                    {
                        object op = _opStack.Pop();
                        _opStack.Push(op == null);
                    }
                    break;

                default:
                    throw new Exception(string.Format(
                        "WARNING: OP CODE '{0}' Not Implemented",
                        bc.OpCode
                        ));
            }
        }

        private void InvokeMethod(ByteCode bc)
        {
            //  Pop the arguments
            //
            ParameterInfo[] parameterInfo = bc.VirtualMethod.GetParameters();
            int countOfParms = parameterInfo.Length;
            object[] parms = new object[countOfParms];
            for (int i = 0; i < countOfParms; i++)
            {
                try
                {
                    if (parameterInfo[i].ParameterType == typeof(int))
                    {
                        parms[i] = (int)_opStack.PopNbr();
                    }
                    else if (parameterInfo[i].ParameterType == typeof(float))
                    {
                        parms[i] = _opStack.PopNbr();
                    }
                    else if (parameterInfo[i].ParameterType == typeof(string))
                    {
                        parms[i] = _opStack.PopStr();
                    }
                    else if (parameterInfo[i].ParameterType == typeof(IConvertible))
                    {
                        parms[i] = Convert.ChangeType(_opStack.Pop(), parameterInfo[i].ParameterType);
                    }
                    else
                    {
                        parms[i] = _opStack.Pop();
                    }
                }
                catch (Exception exp)
                {
                    Exception e = new Exception(exp.Message);
                    //e.Data.Add("Object", virtualObj.GetType().FullName);
                    e.Data.Add("Method", bc.VirtualMethod.Name);
                    e.Data.Add("ParmName", parameterInfo[i].Name);
                    e.Data.Add("ParmType", parameterInfo[i].ParameterType);
                    throw e;
                }
            }
            
            object virtualObj = _opStack.Pop();
            try
            {
                //  call the method
                //
                object returnObject = bc.VirtualMethod.Invoke(virtualObj, parms);
                if (bc.VirtualMethod.ReturnType != typeof(void))
                {
                    _opStack.Push(returnObject);
                }
            }
            catch (Exception exp)
            {
                Exception e = new Exception(exp.Message);
                e.Data.Add("Object", virtualObj.GetType().FullName);
                e.Data.Add("Method", bc.VirtualMethod.Name);
                throw e;
            }
        }

        private void InvokeProperty(ByteCode bc)
        {
            object virtualObj = _opStack.Pop();

            try
            {
                object returnObject = bc.VirtualProperty.GetValue(virtualObj, null);
                if (bc.VirtualMethod.ReturnType != typeof(void))
                {
                    _opStack.Push(returnObject);
                }
            }
            catch (Exception exp)
            {
                Exception e = new Exception(exp.Message);
                e.Data.Add("Object", virtualObj.GetType().FullName);
                e.Data.Add("Property", bc.VirtualProperty.Name);
                throw e;
            }
        }

        #endregion

        #region Properties

        #endregion
    }
}
