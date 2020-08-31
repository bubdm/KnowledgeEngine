using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeEngineRules.Core
{
    class ExpressionPath
    {
        #region Member Variables

        #endregion

        #region Constructors

        public ExpressionPath(string id)
        {
            _id = id;
            _args = new List<ExpressionNode>();
            _paths = new LinkedList<ExpressionPath>();
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(_id);
            if (_isMethod)
            {
                sb.Append("(");
                int i = 0;
                foreach (ExpressionNode n in _args)
                {
                    if (i > 0)
                    {
                        sb.Append(", ");
                    }
                    sb.Append(n.ToString());
                    i += 1;
                }
                sb.Append(")");
            }
            if (_paths.Count > 0)
            {
                foreach (ExpressionPath p in _paths)
                {
                    sb.Append(".");
                    sb.Append(p.ToString());
                }
            }

            return sb.ToString();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the path ID
        /// </summary>
        public string ID
        {
            get { return _id; }
        }
        private string _id;

        /// <summary>
        /// Gets / Sets whether or not this path item is a method or property
        /// </summary>
        public bool IsMethod
        {
            get { return _isMethod; }
            set { _isMethod = value; }
        }
        private bool _isMethod;

        /// <summary>
        /// Gets / Sets whether or not this path item is a setter for a property
        /// </summary>
        public bool IsAssignment
        {
            get { return _isAssignment; }
            set { _isAssignment = value; }
        }
        private bool _isAssignment;

        /// <summary>
        /// Gets a list of arguments pass
        /// </summary>
        public List<ExpressionNode> Args
        {
            get { return _args; }
        }
        private List<ExpressionNode> _args;

        /// <summary>
        /// Gets a list of child paths
        /// </summary>
        public LinkedList<ExpressionPath> Paths
        {
            get { return _paths; }
        }
        private LinkedList<ExpressionPath> _paths;

        #endregion
    }
}
