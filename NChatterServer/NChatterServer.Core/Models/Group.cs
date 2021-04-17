using System.Collections.Generic;

namespace NChatterServer.Core.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public virtual ICollection<User> Members { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
