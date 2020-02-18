﻿namespace Company.Contacts.DomainEntities
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsDeleted { get; set; }
    }

}
