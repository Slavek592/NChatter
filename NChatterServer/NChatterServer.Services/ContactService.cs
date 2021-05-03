using NChatterServer.Core.Models;
using System.Threading.Tasks;
using NChatterServer.Core.Services;
using NChatterServer.Data;
using System.Collections.Generic;

namespace NChatterServer.Services
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContactService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        
        public async Task<Contact> ReadContact(int id)
        {
            return await _unitOfWork.Contacts.GetByIdAsync(id);
        }

        public async Task CreateContact(Contact contact)
        {
            await _unitOfWork.Contacts.AddAsync(contact);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateContact(Contact contact, int id)
        {
            Contact contactToUpdate = await _unitOfWork.Contacts.GetByIdAsync(id);
            contactToUpdate.Name = contact.Name;
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteContact(int id)
        {
            Contact contact = await _unitOfWork.Contacts.GetByIdAsync(id);
            _unitOfWork.Contacts.Remove(contact);
            await _unitOfWork.CommitAsync();
        }
        
        public async Task<IEnumerable<Contact>> GetUserContacts(int id)
        {
            User user = await _unitOfWork.Users.GetWithContactsByIdAsync(id);
            return user.Contacts;
        }

        public async Task<IEnumerable<User>> GetContactMembers(int id)
        {
            Contact contact = await _unitOfWork.Contacts.GetWithMembersByIdAsync(id);
            return contact.Users;
        }
    }
}