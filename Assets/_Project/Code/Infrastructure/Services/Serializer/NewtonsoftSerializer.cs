using Newtonsoft.Json;

public class NewtonsoftSerializer : ISerializer
{
    public string Serialize<T>(T data, bool prettify = true)
    {
        string json = JsonConvert.SerializeObject(data, prettify ? Formatting.Indented : Formatting.None);
        return json;
    }

    public T Deserialize<T>(string data)
    {
        T value = JsonConvert.DeserializeObject<T>(data);
        return value;
    }
}
