using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeEngineRules.Core
{
    abstract class ICondition
    {
        #region Member Variables

        protected KnowledgeBase _knowledge;
        protected VariableBase _variables;
        protected Rule _rule;

        #endregion

        #region Constructors

        public ICondition(Rule rule)
        {
            _rule = rule;
            _knowledge = _rule.KnowledgeBase;
            _variables = _knowledge.Variables;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if this condition returns true
        /// </summary>
        public abstract void Fire(LinkedListNode<ICondition> nextcondition);

        /// <summary>
        /// Fires the next condition in the list
        /// </summary>
        protected void FireNextCondition(LinkedListNode<ICondition> nextcondition)
        {
            if (nextcondition == null)
            {
                _rule.FireActions();
            }
            else
            {
                nextcondition.Value.Fire(nextcondition.Next);
            }
        }

        #endregion

        #region Properties

        #endregion
    }
}
