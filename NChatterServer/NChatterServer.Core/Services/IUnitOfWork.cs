using System;
using System.Threading.Tasks;
using NChatterServer.Core.Repositories;

namespace NChatterServer.Core.Services
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IMessageRepository Messages { get; }
        IGroupRepository Groups { get; }
        IContactRepository Contacts { get; }
        Task<int> CommitAsync();
    }
}