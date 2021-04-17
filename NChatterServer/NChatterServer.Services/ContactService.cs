using NChatterServer.Core.Models;
using System.Threading.Tasks;
using NChatterServer.Core.Services;
using NChatterServer.Data;

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

        public void UpdateContact(Contact contact, Contact contactToUpdate)
        {}

        public async Task DeleteContact(Contact contact)
        {
            _unitOfWork.Contacts.Remove(contact);
            await _unitOfWork.CommitAsync();
        }
    }
}