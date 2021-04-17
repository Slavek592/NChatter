using NChatterServer.Core.Models;
using NChatterServer.Core.Repositories;

namespace NChatterServer.Data.Repositories
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        public MessageRepository(NChatterServerContext context) : base(context) {}

        private NChatterServerContext NChatterServerContext
        {
            get { return Context as NChatterServerContext; }
        }
    }
}