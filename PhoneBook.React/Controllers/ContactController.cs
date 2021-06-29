using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Contracts;
using PhoneBook.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.React.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {

        private readonly IContactRepository _contactsRepository = default(IContactRepository);
        public ContactController(IContactRepository contactsRepository)
        {
            _contactsRepository = contactsRepository;
        }
        // GET: api/<ContactController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactModel>>> Get()
        {
            try
            {
                List<ContactModel> phoneBookList = await _contactsRepository.GetAllAsync();
                return Ok(phoneBookList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error. Something went Wrong! ${ex.Message}");
            }
        }
        // GET api/<ContactController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactModel>> Get(int id)
        {
            try
            {

                ContactModel phonebook = default(ContactModel);
                phonebook = await _contactsRepository.GetByIdAsync(id);
                return Ok(phonebook);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error. Something went Wrong! ${ex.Message}");
            }
        }

        // POST api/<ContactController>
        [HttpPost]
        public async Task<ActionResult<ContactModel>> Post([FromBody] ContactModel contact)
        {
            try
            {
                ContactModel phoneBook = new ContactModel() { 
                    CreatedDate = DateTime.UtcNow,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    ModifiedDate  = DateTime.UtcNow,
                    PhoneNumber = contact.PhoneNumber,
                    PhoneBookUserId = contact.PhoneBookUserId
                };
                phoneBook = await _contactsRepository.AddAsync(phoneBook);
                return Created("Contact: ", phoneBook);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error. Something went Wrong! ${ex.Message}");
            }
        }

        // PUT api/<ContactController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ContactModel>> Put(int id, [FromBody] ContactModel contact)
        {
            try
            {
                contact.ModifiedDate = DateTime.UtcNow;
                await _contactsRepository.UpdateAsync(contact);
                return Ok(contact);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error. Something went Wrong! ${ex.Message}");
            }
        }

        // DELETE api/<ContactController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ContactModel>> Delete(int id, [FromBody] ContactModel contact)
        {
            try
            {
                await _contactsRepository.DeleteAsync(contact);
                return Ok(contact);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error. Something went Wrong! ${ex.Message}");
            }
        }
    }
}
