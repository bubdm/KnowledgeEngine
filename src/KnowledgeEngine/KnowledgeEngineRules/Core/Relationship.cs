using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeEngineRules.Core
{
    class Relationship
    {
        #region Member Variables

        private KnowledgeBase _knowledge;
        private VariableBase _variables;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Relationship(string id, KnowledgeBase kb)
        {
            _set = new Set();
            _id = id;
            _knowledge = kb;
            _variables = _knowledge.Variables;
        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Relationship(string id, KnowledgeBase kb, Set s)
            : this(id, kb)
        {
            _set.AddRange(s);
        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Relationship(string id, KnowledgeBase kb, params SetItem[] sets)
            : this(id, kb)
        {
            _set.AddRange(sets);
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        public void Add(SetItem item)
        {
            if (item.IsWildcard)
            {
                _isWildcard = true;
            }
            _set.Add(item);
        }

        /// <summary>
        /// 
        /// </summary>
        public Relationship Clone()
        {
            Relationship r = new Relationship(ID, _knowledge);
            foreach (SetItem item in Set)
            {
                r.Add(item.Clone());
            }
            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        public Relationship Clone(string id)
        {
            Relationship r = new Relationship(id, _knowledge);
            foreach (SetItem item in Set)
            {
                r.Add(item.Clone());
            }
            return r;
        }

        #endregion

        #region Overridden Methods

        public override bool Equals(object obj)
        {
            Relationship r = obj as Relationship;
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

        public static bool operator ==(Relationship a, Relationship b)
        {
            if (Object.ReferenceEquals(a, b))
            {
                return true;
            }
            if ((object)a == null || (object)b == null)
            {
                return false;
            }

            if (string.Compare(a.ID, b.ID) != 0)
            {
                return false;
            }

            return a.Set == b.Set;
        }

        public static bool operator !=(Relationship a, Relationship b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return string.Format("{0} {{{1}}}", _id, Set.ToString());
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
        /// Gets whether or not this relationship contains a variable
        /// </summary>
        public bool IsWildcard
        {
            get { return _isWildcard; }
        }
        private bool _isWildcard;

        /// <summary>
        /// Gets the key used for collapsing (deduction)
        /// </summary>
        public RelationKey Key
        {
            get
            {
                if (_key == null)
                {
                    _key = new RelationKey(_id);
                }
                return _key;
            }
        }
        private RelationKey _key;

        /// <summary>
        /// Gets the set used for this relationship
        /// </summary>
        public Set Set
        {
            get { return _set; }
        }
        private Set _set;

        #endregion
    }
}
