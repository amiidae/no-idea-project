public interface ISerializer
{
    string Serialize<T>(T data, bool prettify = true);

    T Deserialize<T>(string data);
}
