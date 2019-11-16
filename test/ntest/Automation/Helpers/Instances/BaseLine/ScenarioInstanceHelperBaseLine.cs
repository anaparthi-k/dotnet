using Automation.Helpers.Facade;
using Automation.Scenarios;

namespace Automation.Helpers.Instances.BaseLine
{
    internal class ScenarioInstanceHelperBaseLine : ScenarioInstanceHelper
    {
        public ScenarioInstanceHelperBaseLine(SetupHelperBaseLine setup) : base(setup)
        {
        }

        protected override HomeScenario CreateHomeScenario()
        {
            return new HomeScenario(m_setup);
        }
    }
}
