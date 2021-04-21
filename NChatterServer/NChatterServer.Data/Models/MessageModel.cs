using System;
using NChatterServer.Core.Models;

namespace NChatterServer.Data.Models
{
    public class MessageModel
    {
        public int Id { get; set; }
        public int SentFrom { get; set; }
        public string Text { get; set; }
        public DateTime SentTime { get; set; }
        public MessageStatus.Status Status { get; set; }
    }
}