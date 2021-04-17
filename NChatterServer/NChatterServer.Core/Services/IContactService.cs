using System.Threading.Tasks;
using NChatterServer.Core.Models;

namespace NChatterServer.Core.Services
{
    public interface IContactService
    {
        Task<Contact> ReadContact(int id);
        Task CreateContact(Contact contact);
        void UpdateContact(Contact contact, Contact contactToUpdate);
        Task DeleteContact(Contact contact);
    }
}