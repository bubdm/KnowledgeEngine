using System;
using System.Collections.Generic;
using System.Text;

namespace RulesEngine
{
    public class AIEngine : KnowledgeEngineRules.Engine
    {
        #region Member Variables

        #endregion

        #region Constructors

        public AIEngine()
            : base()
        {
        }

        #endregion

        #region Methods

        #endregion

        #region Properties

        public IG.DataObjects.mbr.Ship Ship
        {
            get { return _ship; }
            set { _ship = value; }
        }
        private IG.DataObjects.mbr.Ship _ship;

        #endregion
    }
}
