using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace KnowledgeEngineRules.Core
{
    class BindExistsCondition : ICondition
    {
        #region Member Variables

        private Variable _variable;

        #endregion

        #region Constructors

        public BindExistsCondition(Rule rule, Variable v)
            : base(rule)
        {
            _variable = v;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if this expression returns true
        /// </summary>
        public override void Fire(LinkedListNode<ICondition> nextcondition)
        {
            Debug.Write(string.Format("C: Bind Exists {0} = ", _variable));
            if (_variables.Contains(_variable))
            {
                Debug.WriteLine("true");
                FireNextCondition(nextcondition);
            }
            else
            {
                Debug.WriteLine("false");
            }
        }

        #endregion

        #region Properties

        #endregion
    }
}
