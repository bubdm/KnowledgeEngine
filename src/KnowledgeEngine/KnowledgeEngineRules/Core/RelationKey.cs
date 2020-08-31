using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeEngineRules.Core
{
    class RelationKey
    {
        #region Member Variables

        #endregion

        #region Constructors

        public RelationKey(string key)
        {
            _key = key;
        }

        #endregion

        #region Methods

        public override bool Equals(object obj)
        {
            RelationKey rk = obj as RelationKey;
            if (rk == null)
            {
                return false;
            }
            return rk == this;
        }

        public override int GetHashCode()
        {
            return _key.GetHashCode();
        }

        public static bool operator ==(RelationKey a, RelationKey b)
        {
            if (Object.ReferenceEquals(a, b))
            {
                return true;
            }
            if ((object)a == null || (object)b == null)
            {
                return false;
            }
            return string.Compare(a.Key, b.Key) == 0;
        }

        public static bool operator !=(RelationKey a, RelationKey b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return string.Format("{0}", _key);
        }

        #endregion

        #region Properties

        public string Key
        {
            get { return _key; }
        }
        private string _key;

        #endregion
    }
}
