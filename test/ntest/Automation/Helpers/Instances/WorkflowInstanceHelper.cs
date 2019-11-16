using Automation.Helpers.Facade;
using Automation.Workflows;
using System;

namespace Automation.Helpers.Instances
{
    public abstract class WorkflowInstanceHelper 
    {
        protected readonly SetupHelperBaseLine m_setup;

        protected internal WorkflowInstanceHelper(SetupHelperBaseLine setup)
        {
            if (setup == null)
                throw new ArgumentNullException("setup");
            this.m_setup = setup;
        }

        #region Home Workflow

        private HomeWorkflow homeWorkflow;

        public virtual HomeWorkflow Home => GetHomeWorkflow();

        protected HomeWorkflow GetHomeWorkflow()
        {
            if (homeWorkflow == null)
            {
                homeWorkflow = CreateHomeWorkflow();
            }

            return homeWorkflow;
        }

        protected abstract HomeWorkflow CreateHomeWorkflow();

        #endregion
    }
}
