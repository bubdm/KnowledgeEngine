using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace KnowledgeEngineRules.Core
{
    class BindAction : IAction
    {
        #region Member Variables

        private Variable _variable;
        private Expression _expr;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public BindAction(Rule rule, Variable v, Expression expr)
            : base(rule)
        {
            if (!_variables.Contains(v))
            {
                _variables.Add(v);
            }
            _variable = _variables[v.ID];
            _expr = expr;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if this expression returns true
        /// </summary>
        public override void Fire()
        {
            _variable.Value = _expr.GetValue();
            Debug.WriteLine(string.Format("A: Bind {0} {1}", _variable, _expr.ToString()));
        }

        #endregion

        #region Properties

        #endregion
    }
}
