using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace KnowledgeEngineRules.Core
{
    class ExecAssignAction : IAction
    {
        #region Member Variables

        private Expression _expr;
        private Engine _engine;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public ExecAssignAction(Rule rule, ExpressionPath path, ExpressionNode rhs, Engine engine)
            : base(rule)
        {
            _engine = engine;
            _expr = new Expression(rule);

            //  Get the setter property (since it must be the last thing pushed to the stack)
            //
            ExpressionPath last = path.Paths.Last.Value;
            last.IsAssignment = true;
            last.Args.Add(rhs);

            ExpressionNode node = new ExpressionNode();
            node.Value = path;
            _expr.Compile(node, _engine);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if this expression returns true
        /// </summary>
        public override void Fire()
        {
            _expr.Exec();
            Debug.WriteLine(string.Format("A: ExecAssign {0}", _expr.ToString()));
        }

        #endregion

        #region Properties

        #endregion
    }
}
