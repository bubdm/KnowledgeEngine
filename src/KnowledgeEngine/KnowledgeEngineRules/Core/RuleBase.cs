using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeEngineRules.Core
{
    class RuleBase : IEnumerable
    {
        #region Member Variables

        private Dictionary<string, Rule> _rules = new Dictionary<string, Rule>();

        #endregion

        #region Constructors

        #endregion

        #region Methods

        /// <summary>
        /// Adds a r to the knowledge base
        /// </summary>
        public void Add(Rule r)
        {
            if (!ContainsKey(r.ID))
            {
                _rules.Add(r.ID, r);
            }
        }

        /// <summary>
        /// Adds a r to the knowledge base
        /// </summary>
        public bool Contains(Rule r)
        {
            return ContainsKey(r.ID);
        }

        /// <summary>
        /// Adds a r to the knowledge base
        /// </summary>
        public bool ContainsKey(string id)
        {
            return _rules.ContainsKey(id);
        }

        /// <summary>
        /// Removes a r to the knowledge base
        /// </summary>
        public void Remove(Rule r)
        {
            if (_rules.ContainsKey(r.ID))
            {
                _rules.Remove(r.ID);
            }
        }

        /// <summary>
        /// String Respresentation
        /// </summary>
        public override string ToString()
        {
            List<string> output = new List<string>();

            foreach (KeyValuePair<string, Rule> item in _rules)
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
        /// Returns a rule
        /// </summary>
        public Rule this[string id]
        {
            get
            {
                if (_rules.ContainsKey(id))
                {
                    return _rules[id];
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
            return _rules.GetEnumerator();
        }

        #endregion
    }
}
