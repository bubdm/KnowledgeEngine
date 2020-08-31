using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace KnowledgeEngineRules.Core
{
    class UnBindAction : IAction
    {
        #region Member Variables

        private Variable _variable;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public UnBindAction(Rule rule, Variable v)
            : base(rule)
        {
            if (!_variables.Contains(v))
            {
                _variables.Add(v);
            }
            _variable = _variables[v.ID];
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if this expression returns true
        /// </summary>
        public override void Fire()
        {
            Debug.WriteLine(string.Format("A: UnBind {0}",_variable.ID));
            _variable.Value = null;
        }

        #endregion

        #region Properties

        #endregion
    }
}
