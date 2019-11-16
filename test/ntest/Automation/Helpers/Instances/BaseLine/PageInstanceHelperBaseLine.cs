using Automation.Helpers.Parameters;
using Automation.Pages;

namespace Automation.Helpers.Instances.BaseLine
{
    internal class PageInstanceHelperBaseLine : PageInstanceHelper
    {
        public PageInstanceHelperBaseLine(SetupHelperParameter setup) : base(setup)
        {
        }

        protected override HomePage CreateHomePage()
        {
            return new HomePage(m_setup);
        }
    }
}
