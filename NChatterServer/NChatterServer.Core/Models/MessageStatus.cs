namespace NChatterServer.Core.Models
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