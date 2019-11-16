using Automation.Helpers.Facade;
using Automation.Workflows;

namespace Automation.Helpers.Instances.BaseLine
{
    internal class WorkflowInstanceHelperBaseLine : WorkflowInstanceHelper
    {
        public WorkflowInstanceHelperBaseLine(SetupHelperBaseLine setup) : base(setup)
        {
        }

        protected override HomeWorkflow CreateHomeWorkflow()
        {
            return new HomeWorkflow(m_setup);
        }
    }
}
