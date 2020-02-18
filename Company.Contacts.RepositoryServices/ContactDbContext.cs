using Company.Contacts.DomainEntities;
using Microsoft.EntityFrameworkCore;

namespace Company.Contacts.RepositoryServices
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> option) : base(option)
        {

        }

        public virtual DbSet<Contact> Contacts { get; set; }
    }
}
