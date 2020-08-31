using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeEngineRules.Core
{
    class VariableBase : IEnumerable
    {
        #region Member Variables

        private Dictionary<string, Variable> _variables = new Dictionary<string, Variable>();

        #endregion

        #region Constructors

        #endregion

        #region Methods

        /// <summary>
        /// Adds a variable to the knowledge base
        /// </summary>
        public void Add(Set s)
        {
            foreach (SetItem item in s)
            {
                if (item.IsWildcard && !Contains(item.Variable))
                {
                    Add(item.Variable);
                }
            }
        }

        /// <summary>
        /// Adds a variable to the knowledge base
        /// </summary>
        public void Add(Variable v)
        {
            if (!_variables.ContainsKey(v.ID))
            {
                _variables.Add(v.ID, v);
            }
        }

        /// <summary>
        /// Adds a variable to the knowledge base
        /// </summary>
        public void Remove(Set s)
        {
            foreach (SetItem item in s)
            {
                if (item.IsWildcard && Contains(item.Variable))
                {
                    Remove(item.Variable);
                }
            }
        }

        /// <summary>
        /// Removes a variable to the knowledge base
        /// </summary>
        public void Remove(Variable v)
        {
            if (_variables.ContainsKey(v.ID))
            {
                _variables.Remove(v.ID);
            }
        }

        /// <summary>
        /// Does the knowledge base contain the variable
        /// </summary>
        public bool Contains(Set s)
        {
            foreach (SetItem item in s)
            {
                if (item.IsWildcard && !Contains(item.Variable))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Does the knowledge base contain the variable
        /// </summary>
        public bool Contains(Variable v)
        {
            return Contains(v.ID);
        }

        /// <summary>
        /// Does the knowledge base contain the variable
        /// </summary>
        public bool Contains(string id)
        {
            return _variables.ContainsKey(id);
        }

        /// <summary>
        /// String Respresentation
        /// </summary>
        public override string ToString()
        {
            List<string> output = new List<string>();

            foreach (KeyValuePair<string, Variable> item in _variables)
            {
                output.Add(item.Value.ToString());
            }

            if (output.Count == 0)
            {
                return "(Empty)";
            }

            return string.Join(", ", output.ToArray());
        }

        #endregion

        #region Indexer

        /// <summary>
        /// Returns an array of relationships.
        /// </summary>
        public Variable this[string id]
        {
            get
            {
                if (_variables.ContainsKey(id))
                {
                    return _variables[id];
                }
                return null;
            }
        }

        #endregion

        #region Properties

        #endregion        

        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            return _variables.GetEnumerator();
        }

        #endregion
    }
}
