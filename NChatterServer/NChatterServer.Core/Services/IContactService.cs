using System.Threading.Tasks;
using NChatterServer.Core.Models;
using System.Collections.Generic;

namespace NChatterServer.Core.Services
{
    public interface IContactService
    {
        Task<Contact> ReadContact(int id);
        Task CreateContact(Contact contact);
        Task UpdateContact(Contact contact, int id);
        Task DeleteContact(int id);
        Task<IEnumerable<Contact>> GetUserContacts(int id);
        Task<IEnumerable<User>> GetContactMembers(int id);
    }
}