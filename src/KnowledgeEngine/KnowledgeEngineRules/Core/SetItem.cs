using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeEngineRules.Core
{
    class SetItem
    {
        #region Member Variables

        private KnowledgeBase _knowledge;
        private VariableBase _variables;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public SetItem(object value, KnowledgeBase kb)
        {
            _value = value;
            if (_value is Variable)
            {
                _isWildcard = true;
            }
            _knowledge = kb;
            _variables = _knowledge.Variables;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        public SetItem Clone()
        {
            return new SetItem(Value, _knowledge);
        }

        #endregion

        #region Overridden Methods

        public override bool Equals(object obj)
        {
            SetItem r = obj as SetItem;
            if (r == null)
            {
                return false;
            }
            return this == r;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public static bool operator ==(SetItem a, SetItem b)
        {
            if (Object.ReferenceEquals(a, b))
            {
                return true;
            }
            if ((object)a == null || (object)b == null)
            {
                return false;
            }

            string aa = Convert.ToString(a.Value);
            string bb = Convert.ToString(b.Value);

            return Comparer.Default.Compare(aa, bb) == 0;
        }

        public static bool operator !=(SetItem a, SetItem b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return string.Format("{0}", _value);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets whether or not this relationship contains a variable
        /// </summary>
        public bool IsWildcard
        {
            get { return _isWildcard; }
        }
        private bool _isWildcard;

        /// <summary>
        /// Gets a handle to the variable (if wildcard)
        /// </summary>
        public Variable Variable
        {
            get
            {
                if (_isWildcard && _variable == null)
                {
                    _variable = _value as Variable;
                }
                return _variable;
            }
        }
        private Variable _variable;

        /// <summary>
        /// Gets the object value contained in this relationship
        /// </summary>
        public object Value
        {
            get
            {
                if (_isWildcard)
                {
                    //  has this value been assigned and stored?
                    if (_variables.Contains(Variable))
                    {
                        return _variables[Variable.ID].Value;
                    }
                    return null;
                }
                return _value;
            }
        }
        private object _value;

        #endregion
    }
}
