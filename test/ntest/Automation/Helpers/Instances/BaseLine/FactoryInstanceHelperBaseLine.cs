using Automation.Factories;
using Automation.Helpers.Parameters;

namespace Automation.Helpers.Instances.BaseLine
{
    public class FactoryInstanceHelperBaseLine : FactoryInstanceHelper
    {
        protected internal FactoryInstanceHelperBaseLine(SetupHelperParameter setup) : base(setup)
        {
        }

        protected override CustomTypeFactory CreateCustomTypeFactory()
        {
            return new CustomTypeFactory(m_setup);
        }
    }
}
