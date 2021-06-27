namespace Neo.Extensions.Api
{
    public class Response
    {
        public Response(object data = null, bool success = false, object notifications = null)
        {
            Data = data;
            Success = success;
            Notifications = notifications;
        }

        public bool Success { get; }
        public object Data { get; }
        public object Notifications { get; set; }
    }
}
