using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeEngineRules.Core
{
    class Variable
    {
        #region Member Variables

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Variable(string id)
        {
            _id = id;
        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Variable(string id, object value)
            : this(id)
        {
            _value = value;
        }

        #endregion

        #region Methods

        public Variable Clone(string id)
        {
            Variable r = new Variable(id, Value);
            return r;
        }

        #endregion

        #region Overridden Methods

        public override bool Equals(object obj)
        {
            Variable r = obj as Variable;
            if (r == null)
            {
                return false;
            }
            return this == r;
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }

        public static bool operator ==(Variable a, Variable b)
        {
            if (Object.ReferenceEquals(a, b))
            {
                return true;
            }
            if ((object)a == null || (object)b == null)
            {
                return false;
            }

            return Comparer.Default.Compare(a.Value, b.Value) == 0;
        }

        public static bool operator !=(Variable a, Variable b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return string.Format("{0}={1}", _id, _value == null ? "<null>" : _value);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the identifier of this relationship
        /// </summary>
        public string ID
        {
            get { return _id; }
        }
        private string _id;

        /// <summary>
        /// Gets the object value contained in this relationship
        /// </summary>
        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }
        private object _value;

        #endregion
    }
}
