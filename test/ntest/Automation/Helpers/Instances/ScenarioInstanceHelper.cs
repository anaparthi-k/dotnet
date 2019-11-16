using Automation.Helpers.Facade;
using Automation.Scenarios;
using System;

namespace Automation.Helpers.Instances
{
    public abstract class ScenarioInstanceHelper
    {
        protected readonly SetupHelperBaseLine m_setup;

        protected internal ScenarioInstanceHelper(SetupHelperBaseLine setup)
        {
            if (setup == null)
                throw new ArgumentNullException("setup");
            this.m_setup = setup;
        }

        #region Home Scenario

        private HomeScenario homeScenario;

        public virtual HomeScenario Home => GetHomeScenario();

        protected HomeScenario GetHomeScenario()
        {
            if (homeScenario == null)
            {
                homeScenario = CreateHomeScenario();
            }

            return homeScenario;
        }

        protected abstract HomeScenario CreateHomeScenario();

        #endregion
    }
}
