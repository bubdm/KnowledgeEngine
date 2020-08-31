using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace KnowledgeEngineRules.Core
{
    class TestCondition : ICondition
    {
        #region Member Variables

        private Expression _expression;

        #endregion

        #region Constructors

        public TestCondition(Rule rule, Expression expr)
            : base(rule)
        {
            _expression = expr;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if this expression returns true
        /// </summary>
        public override void Fire(LinkedListNode<ICondition> nextcondition)
        {
            Debug.Write(string.Format("C: {0} = ", _expression.ToString()));
            if (_expression.IsTrue())
            {
                Debug.WriteLine(string.Format("true", _expression.ToString()));
                FireNextCondition(nextcondition);
            }
            else
            {
                Debug.WriteLine(string.Format("false", _expression.ToString()));
            }
        }

        #endregion

        #region Properties

        #endregion
    }
}
