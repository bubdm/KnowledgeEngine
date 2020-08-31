using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace KnowledgeEngineRules.Core
{
    class ByteCode
    {
        #region Member Variables

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public ByteCode()
            : this(OpCodes.NoOp)
        {
        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public ByteCode(OpCodes op)
        {
            _opCode = op;
        }

        #endregion

        #region Static Methods

        public static ByteCode Create(OpCodes op)
        {
            return new ByteCode(op);
        }

        public static ByteCode Create(OpCodes op, object constant)
        {
            ByteCode bc = new ByteCode(op);
            bc.Constant = constant;
            return bc;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Human readable form of this byte code
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format(
                "{0} {1}",
                Convert.ToString(OpCode).ToLower(),
                GetOpOn()
                );
        }

        /// <summary>
        /// Gets the object to be operated on
        /// </summary>
        public string GetOpOn()
        {
            switch (OpCode)
            {
                case OpCodes.LoadVariable:
                case OpCodes.InvokeMethod:
                case OpCodes.InvokeProperty:
                    return string.Format("{0}", Constant);

                case OpCodes.Push:
                case OpCodes.PushBool:
                case OpCodes.PushInt:
                case OpCodes.PushNbr:
                    return string.Format("{0}", Constant);
            }

            return "";
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the op code to execute
        /// </summary>
        public OpCodes OpCode
        {
            get { return _opCode; }
            set { _opCode = value; }
        }
        private OpCodes _opCode;

        /// <summary>
        /// Gets / Sets the constant value for this byte code instruction
        /// </summary>
        public object Constant
        {
            get { return _constant; }
            set { _constant = value; }
        }
        private object _constant;

        /// <summary>
        /// Gets / Sets the method to invoke within the virtual object
        /// </summary>
        public PropertyInfo VirtualObject
        {
            get { return _virtualObject; }
            set { _virtualObject = value; }
        }
        private PropertyInfo _virtualObject;

        /// <summary>
        /// Gets / Sets the field to invoke within the virtual object
        /// </summary>
        public FieldInfo VirtualField
        {
            get { return _virtualField; }
            set { _virtualField = value; }
        }
        private FieldInfo _virtualField;

        /// <summary>
        /// Gets / Sets the property to invoke within the virtual object
        /// </summary>
        public PropertyInfo VirtualProperty
        {
            get { return _virtualProperty; }
            set { _virtualProperty = value; }
        }
        private PropertyInfo _virtualProperty;

        /// <summary>
        /// Gets / Sets the method to invoke within the virtual object
        /// </summary>
        public MethodInfo VirtualMethod
        {
            get { return _virtualMethod; }
            set { _virtualMethod = value; }
        }
        private MethodInfo _virtualMethod;

        /// <summary>
        /// Gets a count of parameters for the virtual method
        /// </summary>
        public int VirtualMethodParemterCount
        {
            get
            {
                if (_virtualMethodParemterCount == null)
                {
                    _virtualMethodParemterCount = _virtualMethod.GetParameters().Length;
                }
                return _virtualMethodParemterCount.Value;
            }
        }
        private int? _virtualMethodParemterCount;

        #endregion
    }
}

