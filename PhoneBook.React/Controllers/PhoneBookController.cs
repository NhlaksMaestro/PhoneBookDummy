using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Contracts;
using PhoneBook.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PhoneBook.React.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PhoneBookController : ControllerBase
    {
        private readonly IPhoneBookUserRepository _phoneBookUserRepository = default(IPhoneBookUserRepository);
        public PhoneBookController(IPhoneBookUserRepository phoneBookUserRepository)
        {
            _phoneBookUserRepository = phoneBookUserRepository;
        }
        // GET: api/<PhoneBookController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhoneBookUserModel>>> Get()
        {
            try
            {
                List<PhoneBookUserModel> phoneBookList = await _phoneBookUserRepository.GetAllAsync();
                return Ok(phoneBookList.OrderByDescending(ph => ph.Id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error. Something went Wrong! ${ex.Message}");
            }
        }
        // GET api/<PhoneBookController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PhoneBookUserModel>> Get(int id)
        {
            try
            {

                PhoneBookUserModel phonebook = default(PhoneBookUserModel);
                phonebook = await _phoneBookUserRepository.GetByIdAsync(id);
                return Ok(phonebook);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error. Something went Wrong! ${ex.Message}");
            }
        }

        // POST api/<PhoneBookController>
        [HttpPost]
        public async Task<ActionResult<PhoneBookUserModel>> Post([FromBody] PhoneBookUserModel phoneBookData)
        {
            try
            {
                PhoneBookUserModel phoneBook = phoneBookData;
                phoneBook.CreatedDate = DateTime.UtcNow;
                phoneBook.ModifiedDate = DateTime.UtcNow;
                phoneBook = await _phoneBookUserRepository.AddAsync(phoneBookData);
                return Created("User: ", phoneBook);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error. Something went Wrong! ${ex.Message}");
            }
        }

        // PUT api/<PhoneBookController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<PhoneBookUserModel>> Put(int id, [FromBody] PhoneBookUserModel phoneBookData)
        {
            try
            {
                phoneBookData.ModifiedDate = DateTime.UtcNow;
                await _phoneBookUserRepository.UpdateAsync(phoneBookData);
                return Ok(phoneBookData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error. Something went Wrong! ${ex.Message}");
            }
        }

        // DELETE api/<PhoneBookController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PhoneBookUserModel>> Delete(int id, [FromBody] PhoneBookUserModel phonebookUser)
        {
            try
            {
                await _phoneBookUserRepository.DeleteAsync(phonebookUser);
                return Ok(phonebookUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error. Something went Wrong! ${ex.Message}");
            }
        }
    }
}