using Automation.Helpers.Facade;
using Automation.Validations;
using System;

namespace Automation.Helpers.Instances
{
    public abstract class ValidationInstanceHelper
    {
        protected readonly SetupHelperBaseLine m_setup;

        protected internal ValidationInstanceHelper(SetupHelperBaseLine setup)
        {
            if (setup == null)
                throw new ArgumentNullException("setup");
            this.m_setup = setup;
        }
     
        #region Home Validation

        private HomeValidation homeValidation;

        public virtual HomeValidation Home => GetHomeValidation();

        protected HomeValidation GetHomeValidation()
        {
            if (homeValidation == null)
            {
                homeValidation = CreateHomeValidation();
            }

            return homeValidation;
        }

        protected abstract HomeValidation CreateHomeValidation();

        #endregion
    }
}
