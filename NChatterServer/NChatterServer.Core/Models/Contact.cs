using System.Collections.Generic;

namespace NChatterServer.Core.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}