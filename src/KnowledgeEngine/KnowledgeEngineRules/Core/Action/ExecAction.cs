using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace KnowledgeEngineRules.Core
{
    class ExecAction : IAction
    {
        #region Member Variables

        private Expression _expr;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public ExecAction(Rule rule, Expression expr)
            : base(rule)
        {
            _expr = expr;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if this expression returns true
        /// </summary>
        public override void Fire()
        {
            Debug.WriteLine(string.Format("A: Exec {0}", _expr.ToString()));
            _expr.Exec();
        }

        #endregion

        #region Properties

        #endregion
    }
}
