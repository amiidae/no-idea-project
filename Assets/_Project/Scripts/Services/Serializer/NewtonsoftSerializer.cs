using System;
using Newtonsoft.Json;

namespace Bnny.Scripts.Services.Serializer
{
    public class NewtonsoftSerializer : ISerializer
    {
        public string Serialize<T>(T obj, bool prettify = false)
        {
            return JsonConvert.SerializeObject(
                obj,
                prettify ? Formatting.Indented : Formatting.None
            );
        }

        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
