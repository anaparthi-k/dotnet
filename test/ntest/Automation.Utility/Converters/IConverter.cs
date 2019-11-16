namespace Automation.Utility.Converters
{
    public interface IConverter
    {
        string ConvertTo(string value);
        T Deserialize<T>(string input) where T : class;
        T Deserialize<T>(string input,params object[] settings) where T : class;
        string Serialize<T>(T objectToSerialize);
    }
}
