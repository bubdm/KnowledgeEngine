using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using KnowledgeEngineRules.Assembler;
using KnowledgeEngineRules.Core;

namespace KnowledgeEngineRules
{
    public class Engine
    {
        #region Member Variables

        /// <summary>
        /// Fires for each compile error found
        /// </summary>
        public event CompilerErrorEventHandler CompileError;

        private InferenceEngine _engine;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Engine()
        {
        }

        #endregion

        #region Compile Methods

        /// <summary>
        /// Compiles a script
        /// </summary>
        public void Compile(string code)
        {
            Compile(new StringReader(code));
        }

        /// <summary>
        /// Compiles a script
        /// </summary>
        public void Compile(StringReader sr)
        {
            Compile((TextReader)sr);
        }

        /// <summary>
        /// Compiles a script
        /// </summary>
        public void Compile(StreamReader sr)
        {
            Compile((TextReader)sr);
        }

        /// <summary>
        /// Compiles a script
        /// </summary>
        public void Compile(TextReader tr)
        {
            Compiler c = new Compiler(tr);
            c.Error += new CompilerErrorEventHandler(Compile_Error);
            c.Compile(this);
            if (_compileErrors < 1)
            {
                _engine = new InferenceEngine(
                    c.KnowledgeBase,
                    c.RuleBase
                    );
                _hasCompiledEngine = true;
            }
            else
            {
                CompilerErrorEventArgs e = new CompilerErrorEventArgs(string.Format(
                    "{0} error{1} found during compilation",
                    _compileErrors,
                    _compileErrors > 1 ? "s were" : " was"
                    ));
                Compile_Error(this, e);
            }
        }

        private void Compile_Error(object sender, CompilerErrorEventArgs e)
        {
            OnCompileError(e);
        }

        protected void OnCompileError(CompilerErrorEventArgs e)
        {
            _compileErrors += 1;
            if (CompileError != null)
            {
                CompileError(this, e);
            }
            else
            {
                throw new Exception(string.Format(
                    "Compile Error: {0}",
                    e.Message
                    ));
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Run the engine
        /// </summary>
        public void Run()
        {
            if (_compileErrors < 1)
            {
                _engine.Infer(this);
            }
            else
            {
                throw new Exception(string.Format(
                    "{0} errors were found during compilation, this Rule Engine cannot be run.",
                    _compileErrors
                    ));
            }
        }

        /// <summary>
        /// Gets the knowledge base
        /// </summary>
        /// <returns></returns>
        public void ClearKnowledgeItems()
        {
            _engine.KnowledgeBase.Clear();
        }

        /// <summary>
        /// Gets the knowledge base
        /// </summary>
        /// <returns></returns>
        public List<KnowledgeItem> GetKnowledgeItems()
        {
            return _engine.KnowledgeBase.ToKnowledgeItems();
        }

        /// <summary>
        /// Add some knowledge to the knowledge base
        /// </summary>
        public void AddRangeOfKnowledge(List<KnowledgeItem> items)
        {
            foreach (KnowledgeItem item in items)
            {
                AddKnowledge(item);
            }
        }

        /// <summary>
        /// Add some knowledge to the knowledge base
        /// </summary>
        public void AddKnowledge(KnowledgeItem item)
        {
            if (!_hasCompiledEngine)
            {
                throw new Exception("An engine must be compiled prior to adding facts to its knowledge base.");
            }

            Relationship r = new Relationship(item.ID, _engine.KnowledgeBase);
            foreach (string s in item.Set)
            {
                r.Set.Add(new SetItem(s, _engine.KnowledgeBase));
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of compile errors encountered
        /// </summary>
        public int CompileErrors
        {
            get { return _compileErrors; }
        }
        private int _compileErrors;

        /// <summary>
        /// Gets whether or not the engine was successfully compiled
        /// </summary>
        public bool HasCompiledEngine
        {
            get { return _hasCompiledEngine; }
        }
        private bool _hasCompiledEngine;

        /// <summary>
        /// Gets whether or not the engine was stopped by the script
        /// </summary>
        public bool IsStopped
        {
            get { return _isStopped; }
            set { _isStopped = value; }
        }
        private bool _isStopped;

        #endregion
    }
}
