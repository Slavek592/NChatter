namespace NChatterServer.Data
{
    public class MessageStatus
    {
        public enum Status
        {
            NotSent,
            Sent,
            Delivered,
            Read
        }
    }
}