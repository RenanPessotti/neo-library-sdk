using JsonSerializer = Utf8Json.JsonSerializer;

namespace Neo.Extensions.Core
{
    public static class ObjectExtensions
    {
        public static string ToJsonString(this object obj)
        {
            return JsonSerializer.ToJsonString(obj);
        }
    }
}
