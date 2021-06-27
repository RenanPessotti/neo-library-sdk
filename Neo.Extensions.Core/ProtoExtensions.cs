using System.IO;
using ProtoBuf;

namespace Neo.Extensions.Core
{
    public static class ProtoExtensions
    {
        public static byte[] ToProtoBufByteArray(this object obj)
        {
            using (var st = new MemoryStream())
            {
                Serializer.Serialize(st, obj);
                return st.ToArray();
            }
        }
    }
}
