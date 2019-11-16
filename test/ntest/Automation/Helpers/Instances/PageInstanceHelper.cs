using Automation.Helpers.Parameters;
using Automation.Pages;
using System;

namespace Automation.Helpers.Instances
{
    public abstract class PageInstanceHelper
    {
        protected readonly SetupHelperParameter m_setup;

        protected internal PageInstanceHelper(SetupHelperParameter setup)
        {
            if (setup == null)
                throw new ArgumentNullException("setup");
            this.m_setup = setup;
        }

        #region Home Page

        private HomePage homePage;

        public virtual HomePage Home => GetHomePage();

        protected HomePage GetHomePage()
        {
            if (homePage == null)
            {
                homePage = CreateHomePage();
            }

            return homePage;
        }

        protected abstract HomePage CreateHomePage();

        #endregion
    }
}
