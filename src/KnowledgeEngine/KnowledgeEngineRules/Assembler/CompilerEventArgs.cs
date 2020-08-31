using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KnowledgeEngineRules.Assembler
{
    public delegate void CompilerErrorEventHandler(object sender, CompilerErrorEventArgs e);

    public class CompilerErrorEventArgs : EventArgs
    {
        #region Member Variables

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public CompilerErrorEventArgs(string message)
        {
            _message = message;
        }

        #endregion

        #region Methods

        #endregion

        #region Properties

        public string Message
        {
            get { return _message; }
        }
        private string _message;

        #endregion
    }
}
