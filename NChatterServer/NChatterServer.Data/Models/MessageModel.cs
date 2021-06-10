using System;
using NChatterServer.Core.Models;

namespace NChatterServer.Data.Models
{
    public class MessageModel
    {
        public int Id { get; set; }
        public int SentFrom { get; set; }
        public string Text { get; set; }
        public string SentTime { get; set; }
        public string Status { get; set; }
    }
}