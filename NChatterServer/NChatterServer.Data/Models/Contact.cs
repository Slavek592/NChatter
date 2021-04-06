using System.Collections.Generic;

namespace NChatterServer.Data.Models
{
    public class Contact
    {
        public int Id { get; set; }
        
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}