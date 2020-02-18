using System.Collections.Generic;
using System.Linq;
using Company.Contacts.RepositoryServices;
using Microsoft.AspNetCore.Mvc;

namespace Company.Contact.WebAPI.Controllers
{
    [Route("v1/Contacts")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactRepositoryService _repositoryService;

        public ContactsController(IContactRepositoryService repositoryService)
        {
            _repositoryService = repositoryService;
        }

        [HttpGet]
        [Route("~/v1/Contacts")]
        public ActionResult<IEnumerable<Contacts.DomainEntities.Contact>> GetContacts()
        {
            var result = _repositoryService.GetContacts().ToList();
            if (result.Any())
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("~/v1/Contacts/{id}")]
        public ActionResult<Contacts.DomainEntities.Contact> Get(int id)
        {
            var result = _repositoryService.GetContact(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("~/v1/Contacts")]
        public ActionResult Post([FromBody]Contacts.DomainEntities.Contact contact)
        {
            if (contact == null)
            {
                return BadRequest();
            }
            var result = _repositoryService.AddContact(contact);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("~/v1/Contacts")]
        public ActionResult Put([FromBody]Contacts.DomainEntities.Contact contact)
        {
            if (contact == null)
            {
                return BadRequest();
            }
            var result = _repositoryService.UpdateContact(contact);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("~/v1/Contacts/{id}")]
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var result = _repositoryService.DeleteContact(id);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}