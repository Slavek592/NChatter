using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using NChatterServer.Core.Models;
using NChatterServer.Data.Models;
using NChatterServer.Core.Services;
using Microsoft.AspNetCore.Mvc;
using NChatterServer.Data;
using System.Linq;

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
        public async Task<ActionResult<IEnumerable<ContactModel>>> GetAll(int idUser)
        {
            try
            {
                User test = await _userService.ReadUser(idUser);
                if (test == null)
                {
                    return NotFound("This user does not exist.");
                }
                IEnumerable<Contact> contacts = await _contactService.GetUserContacts(idUser);
                if (!contacts.Any())
                {
                    return Ok();
                }
                List<ContactModel> contactModels = new List<ContactModel>();
                foreach (var contact in contacts)
                {
                    contactModels.Add(Mapping.ContactToModel(contact));
                }
                return Ok(contactModels);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ContactModel>> Get(int idUser, int id)
        {
            try
            {
                User test = await _userService.ReadUser(idUser);
                if (test == null)
                {
                    return NotFound("This user does not exist.");
                }
                Contact contact = await _contactService.ReadContact(id);
                if (contact == null)
                {
                    return NotFound("This contact does not exist.");
                }
                if (!Authorize.UserInGroup(await _contactService.GetContactMembers(id), idUser))
                {
                    return StatusCode(405);
                }
                return Ok(Mapping.ContactToModel(contact));
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
        
        [HttpPost("")]
        public async Task<ActionResult> Post(int idUser, Contact contact)
        {
            try
            {
                User test = await _userService.ReadUser(idUser);
                if (test == null)
                {
                    return NotFound("This user does not exist.");
                }
                List<User> users = new List<User>();
                
                try
                {
                    User createdUser = await _userService.ReadUser(idUser);
                    users.Add(createdUser);
                    foreach (var user in contact.Users)
                    {
                        users.Add(await _userService.ReadUser(user.Id));
                        contact.Name = user.Username + " and " + createdUser.Username;
                    }
                }
                catch (Exception e)
                {
                    return BadRequest("Error in adding users to the group.");
                }

                contact.Users = users;
                await _contactService.CreateContact(contact);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int idUser, int id, Contact contact)
        {
            try
            {
                User user = await _userService.ReadUser(idUser);
                if (user == null)
                {
                    return NotFound("This user does not exist.");
                }
                Contact test = await _contactService.ReadContact(id);
                if (test == null)
                {
                    return NotFound("This contact does not exist.");
                }
                if (!Authorize.UserInGroup(await _contactService.GetContactMembers(id), idUser))
                {
                    return StatusCode(405);
                }
                await _contactService.UpdateContact(contact, id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int idUser, int id)
        {
            try
            {
                User user = await _userService.ReadUser(idUser);
                if (user == null)
                {
                    return NotFound("This user does not exist.");
                }
                Contact test = await _contactService.ReadContact(id);
                if (test == null)
                {
                    return NotFound("This contact does not exist.");
                }
                if (!Authorize.UserInGroup(await _contactService.GetContactMembers(id), idUser))
                {
                    return StatusCode(405);
                }
                await _contactService.DeleteContact(id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
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