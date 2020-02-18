using System.Collections.Generic;
using AutoMapper;
using Company.Contact.WebApp.HttpUtility;
using Company.Contact.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company.Contact.WebApp.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IHttpUtilities<ContactsController> _httpUtilities;
        private readonly IMapper _mapper;
        public ContactsController(IHttpUtilities<ContactsController> httpUtilities, IMapper mapper)
        {
            _httpUtilities = httpUtilities;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var result = _httpUtilities.Get();
            var contacts = _mapper.Map<List<ContactModel>>(result);
            return View(contacts);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var result = _httpUtilities.Get("/contacts/" + id);
            var contact = _mapper.Map<ContactModel>(result);            
            return View(contact);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public ActionResult Create(ContactModel contact)
        {
            if (ModelState.IsValid)
            {
                var contacts = _mapper.Map<Contacts.DomainEntities.Contact>(contact);
                var result = _httpUtilities.PostRequest("/contacts", contacts);
                if (!result)
                {
                    return View("Error", new ErrorViewModel { ControllerName = "ContactsController", ActionName = "Create" });
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(ContactModel contact)
        {
            if (ModelState.IsValid)
            {
                var contacts = _mapper.Map<Contacts.DomainEntities.Contact>(contact);
                var result = _httpUtilities.PutRequest("/contacts", contacts);
                if (!result)
                {
                    return View("Error",new ErrorViewModel { ControllerName = "ContactsController", ActionName = "Edit" });
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            if (id > 0)
            {
                var result = _httpUtilities.DeleteRequest("/contacts/", id);
                if (!result)
                {
                    return View("Error", new ErrorViewModel { ControllerName = "ContactsController", ActionName = "Delete" });
                }
            }
            return RedirectToAction("Index");
        }
    }
}