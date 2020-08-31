using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace KnowledgeEngineRules.Core
{
    class AssertAction : IAction
    {
        #region Member Variables

        private Relationship _relationship;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public AssertAction(Rule rule, Relationship relationship)
            : base(rule)
        {
            _relationship = relationship;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if this expression returns true
        /// </summary>
        public override void Fire()
        {
            Relationship r = _relationship.Clone();
            Debug.WriteLine(string.Format("A: Assert ({0})", r));
            _knowledge.Add(r);
        }

        #endregion

        #region Properties

        #endregion
    }
}
