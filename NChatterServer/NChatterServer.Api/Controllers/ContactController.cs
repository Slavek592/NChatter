using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using NChatterServer.Core.Models;
using NChatterServer.Data.Models;
using NChatterServer.Core.Services;
using Microsoft.AspNetCore.Mvc;
using NChatterServer.Data;

namespace NChatterServer.Api.Controllers
{
    [ApiController]
    [Route("User/{idUser:int}/Contacts")]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IUserService _userService;
        public ContactController(IContactService contactService, IUserService userService)
        {
            _contactService = contactService;
            _userService = userService;
        }
        
        [HttpGet("")]
        public async Task<IEnumerable<ContactModel>> GetAll(int idUser)
        {
            IEnumerable<Contact> contacts = await _contactService.GetUserContacts(idUser);
            List<ContactModel> contactModels = new List<ContactModel>();
            foreach (var contact in contacts)
            {
                contactModels.Add(Mapping.ContactToModel(contact));
            }
            return contactModels;
        }

        [HttpGet("{id:int}")]
        public async Task<ContactModel> Get(int idUser, int id)
        {
            return Mapping.ContactToModel(await _contactService.ReadContact(id));
        }
        
        [HttpPost("")]
        public async Task Post(int idUser, Contact contact)
        {
            List<User> users = new List<User>();
            foreach (var user in contact.Users)
            {
                users.Add(await _userService.ReadUser(user.Id));
            }

            contact.Users = users;
            await _contactService.CreateContact(contact);
        }

        [HttpPut("{id:int}")]
        public async Task Put(int idUser, int id, Contact contact)
        {
            await _contactService.UpdateContact(contact, id);
        }

        [HttpDelete("{id:int}")]
        public async Task Delete(int idUser, int id)
        {
            await _contactService.DeleteContact(id);
        }

        [HttpGet("{id:int}/Members")]
        public async Task<IEnumerable<UserModel>> GetMembers(int idUser, int id)
        {
            IEnumerable<User> users = await _contactService.GetContactMembers(id);
            List<UserModel> userModels = new List<UserModel>();
            foreach (var user in users)
            {
                userModels.Add(Mapping.UserToModel(user));
            }

            return userModels;
        }
    }
}