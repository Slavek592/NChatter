using NChatterServer.Data.Models;
using System.Threading.Tasks;
using NChatterServer.Data;

namespace NChatterServer.Services
{
    public class ContactService
    {
        private readonly UnitOfWork _unitOfWork;

        public ContactService(UnitOfWork unitOfWork)
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