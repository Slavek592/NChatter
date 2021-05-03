using System.Threading.Tasks;
using NChatterServer.Core.Services;
using NChatterServer.Data.Repositories;
using NChatterServer.Core.Repositories;

namespace NChatterServer.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NChatterServerContext _context;
        private MessageRepository _messageRepository;
        private UserRepository _userRepository;
        private ContactRepository _contactRepository;
        private GroupRepository _groupRepository;

        public UnitOfWork(NChatterServerContext context)
        {
            this._context = context;
        }

        public IMessageRepository Messages => _messageRepository = _messageRepository ?? new MessageRepository(_context);
        public IUserRepository Users => _userRepository = _userRepository ?? new UserRepository(_context);
        public IContactRepository Contacts => _contactRepository = _contactRepository ?? new ContactRepository(_context);
        public IGroupRepository Groups => _groupRepository = _groupRepository ?? new GroupRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}