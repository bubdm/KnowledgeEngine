using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace KnowledgeEngineRules.Core
{
    abstract class IAction
    {
        #region Member Variables

        protected KnowledgeBase _knowledge;
        protected VariableBase _variables;
        protected Rule _rule;

        #endregion

        #region Constructors

        public IAction(Rule rule)
        {
            _rule = rule;
            _knowledge = _rule.KnowledgeBase;
            _variables = _knowledge.Variables;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if this expression returns true
        /// </summary>
        public abstract void Fire();

        #endregion

        #region Properties

        #endregion
    }
}
