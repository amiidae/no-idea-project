using System;

namespace Bnny.Scripts.Services.Serializer
{
    public interface ISerializer
    {
        public string Serialize<T>(T obj, bool prettify = false);
        public T Deserialize<T>(string json);
    }
}
