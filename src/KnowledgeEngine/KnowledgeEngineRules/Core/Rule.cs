using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeEngineRules.Core
{
    class Rule
    {
        #region Member Variables

        private LinkedList<ICondition> _conditionList;
        private List<IAction> _actionList;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Rule(KnowledgeBase kb)
        {
            _knowledgeBase = kb;
            _conditionList = new LinkedList<ICondition>();
            _actionList = new List<IAction>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a condition
        /// </summary>
        public void AddCondition(ICondition c)
        {
            _conditionList.AddLast(c);
        }

        /// <summary>
        /// Adds an action
        /// </summary>
        public void AddAction(IAction a)
        {
            _actionList.Add(a);
        }

        /// <summary>
        /// Adds a condition
        /// </summary>
        public void AddVariable(Variable v)
        {
            _knowledgeBase.Variables.Add(v);
        }

        /// <summary>
        /// Runs the Rule
        /// </summary>
        public void Fire(Engine engine)
        {
            _engine = engine;
            RunCondition(_conditionList.First);
        }

        private void RunCondition(LinkedListNode<ICondition> node)
        {
            node.Value.Fire(node.Next);
        }

        public void FireActions()
        {
            foreach (IAction a in _actionList)
            {
                if (_engine.IsStopped)
                {
                    break;
                }
                a.Fire();
            }
        }

        #endregion

        #region Overridden Methods

        public override bool Equals(object obj)
        {
            Rule r = obj as Rule;
            if (r == null)
            {
                return false;
            }
            return this == r;
        }

        public static bool operator ==(Rule a, Rule b)
        {
            if (Object.ReferenceEquals(a, b))
            {
                return true;
            }
            if ((object)a == null || (object)b == null)
            {
                return false;
            }

            return string.Compare(a.ID, b.ID) == 0;
        }

        public static bool operator !=(Rule a, Rule b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }

        public override string ToString()
        {
            return _id;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets / Sets the ID for the Rule
        /// </summary>
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _id;

        /// <summary>
        /// Gets a handle to the knowledge base
        /// </summary>
        public KnowledgeBase KnowledgeBase
        {
            get { return _knowledgeBase; }
        }
        private KnowledgeBase _knowledgeBase;

        /// <summary>
        /// Gets a handle to the knowledge base
        /// </summary>
        public Engine Engine
        {
            get { return _engine; }
        }
        private Engine _engine;

        #endregion
    }
}
