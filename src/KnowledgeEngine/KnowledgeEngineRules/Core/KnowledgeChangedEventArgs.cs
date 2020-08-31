using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeEngineRules.Core
{
    delegate void KnowledgeChangedEventHandler(object sender, KnowledgeChangedEventArgs e);

    class KnowledgeChangedEventArgs : EventArgs
    {
        #region Member Variables

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public KnowledgeChangedEventArgs(RelationKey key, List<Rule> rules)
        {
            _key = key;
            _rules = rules;
        }

        #endregion

        #region Methods

        #endregion

        #region Properties

        /// <summary>
        /// The key to the relationship that changed
        /// </summary>
        public RelationKey RelationKey
        {
            get { return _key; }
        }
        private RelationKey _key;

        /// <summary>
        /// The list of rules associated with this key
        /// </summary>
        public List<Rule> Rules
        {
            get { return _rules; }
        }
        private List<Rule> _rules;

        #endregion
    }
}
