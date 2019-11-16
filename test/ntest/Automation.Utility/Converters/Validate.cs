namespace Automation.Utility.Common
{
    internal static class Validate
    {
        internal static bool IsJson(string input)
        {
            input = (input ?? "").Trim();
            return input.StartsWith("{") && input.EndsWith("}")
                   || input.StartsWith("[") && input.EndsWith("]");
        }
    }
}
