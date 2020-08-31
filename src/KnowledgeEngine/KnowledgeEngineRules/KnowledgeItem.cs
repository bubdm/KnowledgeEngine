using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeEngineRules
{
    public class KnowledgeItem
    {
        #region Member Variables

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public KnowledgeItem(string id)
        {
            _id = id;
            _set = new List<string>();
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (string item in _set)
            {
                if (sb.Length > 0)
                {
                    sb.Append(", ");
                }
                sb.Append(item);
            }

            return string.Format("{0} {{{1}}}", _id, sb.ToString());
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the knowledge key
        /// </summary>
        public string ID
        {
            get { return _id; }
        }
        private string _id;

        /// <summary>
        /// Gets the knowledge set
        /// </summary>
        public List<string> Set
        {
            get { return _set; }
        }
        private List<string> _set;

        #endregion
    }
}
