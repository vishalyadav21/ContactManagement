using Company.Contacts.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Company.Contacts.RepositoryServices
{
    public interface IContactRepositoryService
    {
        IEnumerable<Contact> GetContacts();

        Contact GetContact(int id);

        bool AddContact(Contact contact);

        bool UpdateContact(Contact contact);

        bool DeleteContact(int id);
    }

    public class ContactRepositoryService : IContactRepositoryService
    {
        private readonly ContactDbContext _dbContext;

        public ContactRepositoryService(ContactDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AddContact(Contact contact)
        {
            if (contact != null)
            {
                try
                {
                    _dbContext.Contacts.Add(contact);
                    _dbContext.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        public bool DeleteContact(int id)
        {
            if (id > 0)
            {
                try
                {
                    var contact = _dbContext.Contacts.Find(id);
                    contact.IsDeleted = true;
                    _dbContext.Contacts.Update(contact);
                    _dbContext.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        public Contact GetContact(int id)
        {
            return _dbContext.Contacts.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Contact> GetContacts()
        {
            try
            {
                return _dbContext.Contacts.Where(x => x.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                return new List<Contact>();
            }

        }

        public bool UpdateContact(Contact contact)
        {
            if (contact != null)
            {
                try
                {
                    _dbContext.Contacts.Update(contact);
                    _dbContext.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }
    }
}
