using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace KnowledgeEngineRules.Core
{
    class StopAction : IAction
    {
        #region Member Variables

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public StopAction(Rule rule)
            : base(rule)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if this expression returns true
        /// </summary>
        public override void Fire()
        {
            Debug.WriteLine("A: STOP Rule Engine");
            _rule.Engine.IsStopped = true;
        }

        #endregion

        #region Properties

        #endregion
    }
}
