using System;

namespace NChatterServer.Core.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int SentFrom { get; set; }
        public string Text { get; set; }
        public DateTime SentTime { get; set; }
        public MessageStatus.Status Status { get; set; }
        
        public virtual Group Group { get; set; }
        public virtual Contact Contact { get; set; }
    }
}
