using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace KnowledgeEngineRules.Core
{
    class FireRuleAction : IAction
    {
        #region Member Variables

        private RuleBase _rules;
        private Rule _ruleToFire;
        private string _ruleId;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public FireRuleAction(Rule rule, RuleBase rules, string ruleId)
            : base(rule)
        {
            _rules = rules;
            _ruleId = ruleId;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if this expression returns true
        /// </summary>
        public override void Fire()
        {
            if (_ruleToFire == null)
            {
                if (_rules.ContainsKey(_ruleId))
                {
                    _ruleToFire = _rules[_ruleId];
                }
                else
                {
                    throw new Exception(string.Format(
                        "'{0}' Rule ID was not found",
                        _ruleId
                        ));
                }
            }
            Debug.WriteLine(string.Format("A: Fire-Rule {0}", _ruleToFire.ID));
            _ruleToFire.Fire(_rule.Engine);
        }

        #endregion

        #region Properties

        #endregion
    }
}
