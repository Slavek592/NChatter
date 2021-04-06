using NChatterServer.Data.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace NChatterServer.Data.Repositories
{
    public class MessageRepository : Repository<Message>
    {
        public MessageRepository(NChatterServerContext context) : base(context) {}

        private NChatterServerContext NChatterServerContext
        {
            get { return Context as NChatterServerContext; }
        }
    }
}