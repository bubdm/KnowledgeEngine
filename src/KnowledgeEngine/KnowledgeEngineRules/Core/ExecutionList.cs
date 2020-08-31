using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace KnowledgeEngineRules.Core
{
    class ExecutionList
    {
        #region Member Variables

        private Stack<Rule> _executionStack;
        private InferenceEngine _ie;
        private Engine _engine;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public ExecutionList(InferenceEngine ie, Engine engine)
        {
            _ie = ie;
            _engine = engine;
            _executionStack = new Stack<Rule>();

            foreach (KeyValuePair<string, Rule> item in _ie.RuleBase)
            {
                Push(item.Value);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Pushes a rule onto the stack
        /// </summary>
        public void Push(Rule rule)
        {
            if (!_executionStack.Contains(rule))
            {
                Debug.WriteLine(string.Format("Added {0} to Execution List", rule.ID));
                _executionStack.Push(rule);
            }
        }

        /// <summary>
        /// Pops a rule from the stack
        /// </summary>
        public Rule Pop()
        {
            return _executionStack.Pop();
        }

        public override string ToString()
        {
            if (_executionStack.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (Rule r in _executionStack)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(", ");
                    }
                    sb.Append(r.ID);
                }
                return sb.ToString();
            }
            return "(Empty)";
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of items on the stack
        /// </summary>
        public int Count
        {
            get { return _executionStack.Count; }
        }

        #endregion
    }
}
