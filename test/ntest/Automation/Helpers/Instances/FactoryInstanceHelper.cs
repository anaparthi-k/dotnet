using Automation.Factories;
using Automation.Helpers.Parameters;
using System;

namespace Automation.Helpers.Instances
{
    public abstract class FactoryInstanceHelper 
    {
        protected readonly SetupHelperParameter m_setup;

        protected internal FactoryInstanceHelper(SetupHelperParameter setup)
        {
            if (setup == null)
                throw new ArgumentNullException("setup");
            this.m_setup = setup;
        }

        #region Custom Type Factory

        protected CustomTypeFactory customTypeFactory;
        public virtual CustomTypeFactory Type => GetCustomTypeFactory();

        protected CustomTypeFactory GetCustomTypeFactory()
        {
            if (customTypeFactory == null)
            {
                customTypeFactory = CreateCustomTypeFactory();
            }

            return customTypeFactory;
        }

        protected abstract CustomTypeFactory CreateCustomTypeFactory();

        #endregion
    }
}
