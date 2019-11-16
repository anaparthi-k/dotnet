using Automation.Helpers.Facade;
using Automation.Validations;

namespace Automation.Helpers.Instances.BaseLine
{
    internal class ValidationInstanceHelperBaseLine : ValidationInstanceHelper
    {
        public ValidationInstanceHelperBaseLine(SetupHelperBaseLine setup) : base(setup)
        {
        }

        protected override HomeValidation CreateHomeValidation()
        {
            return new HomeValidation(m_setup);
        }
    }
}
