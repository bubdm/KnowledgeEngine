using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

using KnowledgeEngineRules.Assembler;

namespace KnowledgeEngineRules.Core
{
    class InferenceEngine
    {
        #region Member Variables

        private ExecutionList _executionList;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public InferenceEngine(KnowledgeBase kb, RuleBase rb)
        {
            _knowledgeBase = kb;
            _ruleBase = rb;

            _knowledgeBase.Changed += new KnowledgeChangedEventHandler(KnowledgeBase_Changed);
        }

        #endregion

        #region Methods

        private void KnowledgeBase_Changed(object sender, KnowledgeChangedEventArgs e)
        {
            foreach (Rule r in e.Rules)
            {
                _executionList.Push(r);
            }
        }

        /// <summary>
        /// Run the engine
        /// </summary>
        public void Infer(Engine engine)
        {
            if (!engine.HasCompiledEngine)
            {
                throw new Exception("Inference Engine cannot run because the Main Rule Engine has not been compiled");
            }

            _executionList = new ExecutionList(this, engine);

            while (!engine.IsStopped && _executionList.Count > 0)
            {
                Rule rule = _executionList.Pop();

                Debug.WriteLine(""); 
                Debug.WriteLine(string.Format("Running Rule {0}", rule.ID));
                Debug.Indent();

                Debug.WriteLine(string.Format("List: {0}", _executionList.ToString()));
                rule.Fire(engine);

                Debug.Unindent();
                Debug.WriteLine(string.Format("End Run : {0}", rule.ID));
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a handle to the knowledge base
        /// </summary>
        public KnowledgeBase KnowledgeBase
        {
            get { return _knowledgeBase; }
        }
        private KnowledgeBase _knowledgeBase;

        /// <summary>
        /// Gets a handle to the rule base
        /// </summary>
        public RuleBase RuleBase
        {
            get { return _ruleBase; }
        }
        private RuleBase _ruleBase;

        #endregion
    }
}
