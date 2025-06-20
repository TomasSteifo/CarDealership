using System;
using CarDealership.Domain.Entities;

namespace CarDealership.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Mobile { get; private set; }
        public string Address { get; private set; }
        public string Postcode { get; private set; }
        public string City { get; private set; }
        public string PasswordHash { get; private set; }

        private User() { }

        public User(string firstName, string lastName, string email,
                    string mobile, string address, string postcode,
                    string city, string passwordHash)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Mobile = mobile;
            Address = address;
            Postcode = postcode;
            City = city;
            PasswordHash = passwordHash;
        }
    }
}
