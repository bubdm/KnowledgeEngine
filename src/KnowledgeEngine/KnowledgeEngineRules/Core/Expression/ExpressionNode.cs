using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeEngineRules.Core
{
    enum ExpressionOperators
    {
        None,
        Or,
        And,
        Xor,
        Eq,
        Ne,
        Lt,
        Le,
        Gt,
        Ge,
        Add,
        Sub,
        Mul,
        Div,
        Mod,
        Is,
        Not,
        Neg,
        Str,        //  STRING concatenation,
        IsNull      // ISNULL (expr)
    }

    class ExpressionNode
    {
        #region Member Variables

        #endregion

        #region Constructors

        public ExpressionNode()
        {
            _opr = ExpressionOperators.None;
            _right = new List<ExpressionNode>();
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return GetString(this);
        }

        private string GetString(ExpressionNode node)
        {
            StringBuilder sb = new StringBuilder();
            if (Opr != ExpressionOperators.None)
            {
                sb.Append("(");
            }
            if (Left != null)
            {
                sb.Append(Left.ToString());
            }
            if (Right.Count > 0)
            {
                foreach (ExpressionNode en in Right)
                {
                    switch (Opr)
                    {
                        case ExpressionOperators.None:
                            break;

                        //or
                        case ExpressionOperators.Or:
                            sb.Append(" OR ");
                            break;

                        //and
                        case ExpressionOperators.And:
                            sb.Append(" AND ");
                            break;

                        //xor
                        case ExpressionOperators.Xor:
                            sb.Append(" XOR ");
                            break;

                        //==
                        case ExpressionOperators.Eq:
                            sb.Append(" == ");
                            break;

                        //!=
                        case ExpressionOperators.Ne:
                            sb.Append(" != ");
                            break;

                        //<
                        case ExpressionOperators.Lt:
                            sb.Append(" < ");
                            break;

                        //>
                        case ExpressionOperators.Gt:
                            sb.Append(" > ");
                            break;

                        //<=
                        case ExpressionOperators.Le:
                            sb.Append(" <= ");
                            break;

                        //>=
                        case ExpressionOperators.Ge:
                            sb.Append(" >= ");
                            break;

                        //& = str concat
                        case ExpressionOperators.Str:
                            sb.Append(" & ");
                            break;

                        //+
                        case ExpressionOperators.Add:
                            sb.Append(" + ");
                            break;

                        //-
                        case ExpressionOperators.Sub:
                            sb.Append(" - ");
                            break;

                        //*
                        case ExpressionOperators.Mul:
                            sb.Append(" * ");
                            break;

                        //'/'
                        case ExpressionOperators.Div:
                            sb.Append(" / ");
                            break;

                        //%
                        case ExpressionOperators.Mod:
                            sb.Append(" mod ");
                            break;

                        //- (negate)
                        case ExpressionOperators.Neg:
                            sb.Append(" -");
                            break;

                        //not
                        case ExpressionOperators.Not:
                            sb.Append(" NOT ");
                            break;

                        //isnull
                        case ExpressionOperators.IsNull:
                            break;

                        default:
                            throw new Exception(string.Format(
                                "'{0}' Expression Operator has not been accounted for.",
                                Opr
                                ));
                    }
                    if (Opr != ExpressionOperators.IsNull)
                    {
                        sb.Append(en.ToString());
                    }
                    else
                    {
                        sb.Append(" ISNULL(" + en.ToString() + ")");
                    }
                }
            }
            if (Value != null)
            {
                sb.Append(Value.ToString());
            }
            if (Opr != ExpressionOperators.None)
            {
                sb.Append(")");
            }
            return sb.ToString();
        }

        #endregion

        #region Properties

        public ExpressionNode Left
        {
            get { return _left; }
            set { _left = value; }
        }
        private ExpressionNode _left;

        public List<ExpressionNode> Right
        {
            get { return _right; }
        }
        private List<ExpressionNode> _right;

        public ExpressionOperators Opr
        {
            get { return _opr; }
            set { _opr = value; }
        }
        private ExpressionOperators _opr;

        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }
        private object _value;

        #endregion
    }
}