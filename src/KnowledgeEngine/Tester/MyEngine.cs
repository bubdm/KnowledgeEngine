using System;
using System.Collections.Generic;
using System.Text;

namespace RulesEngine
{
    class MyEngine : KnowledgeEngineRules.Engine
    {
        #region Member Variables

        public class InternalShip
        {
            public int Crew
            {
                get { return _crew; }
                set { _crew = value; }
            }
            private int _crew;

            public int CrewMax
            {
                get { return _crewMax; }
                set { _crewMax = value; }
            }
            private int _crewMax;

            public int CrewAssigned
            {
                get { return _crewAssigned; }
                set { _crewAssigned = value; }
            }
            private int _crewAssigned;
        }

        #endregion

        #region Constructors

        public MyEngine()
            : base()
        {
            _ship = new InternalShip();
            _ship.Crew = 100;
            _ship.CrewAssigned = 100;
            _ship.CrewMax = 1000;
        }

        #endregion

        #region Methods

        #endregion

        #region Properties

        public InternalShip Ship
        {
            get { return _ship; }
        }
        private InternalShip _ship;

        public string Name
        {
            get { return null; }
        }

        #endregion
    }
}
