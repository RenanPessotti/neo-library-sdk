namespace Neo.Extensions.Redis
{
    public class RedisSettings
    {
        public int ConnectRetry { get; set; }
        public int ConnectTimeout { get; set; }
        public int DefaultDatabase { get; set; }
        public string ClientName { get; set; }
        public string Password { get; set; }
        public string Hostname { get; set; }
        public int LinearRetry { get; set; }
        public int Port { get; set; }

        public string Hashtable { get; set; }
    }
}
