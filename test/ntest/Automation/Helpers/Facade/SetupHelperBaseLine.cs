
using Automation.Configurations;
using Automation.Helpers.Instances;
using Automation.Helpers.Instances.BaseLine;
using Automation.Helpers.Parameters;

namespace Automation.Helpers.Facade
{
    public class SetupHelperBaseLine 
    {
        public SetupHelperBaseLine(SetupHelperParameter setup) 
        {

        }

        protected TestConfigurationManager CreateConfiguration(string configFilePath)
        {
            return new TestConfigurationManager(configFilePath);
        }

        protected FactoryInstanceHelper CreateFactoryInstance(SetupHelperParameter setup)
        {
            return new FactoryInstanceHelperBaseLine(setup);
        }

        protected PageInstanceHelper CreatePageInstance(SetupHelperParameter setup)
        {
            return new PageInstanceHelperBaseLine(setup);
        }

        protected ScenarioInstanceHelper CreateScenarioInstance()
        {
            return new ScenarioInstanceHelperBaseLine(this);
        }

        protected ValidationInstanceHelper CreateValidationInstance()
        {
            return new ValidationInstanceHelperBaseLine(this);
        }

        protected WorkflowInstanceHelper CreateWorkflowInstance()
        {
            return new WorkflowInstanceHelperBaseLine(this);
        }
    }
}
