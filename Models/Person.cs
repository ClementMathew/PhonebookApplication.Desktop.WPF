﻿namespace Phonebook_Application.Models
{
    internal class Person
    {
        public string Name { get; set; }
        public string Phone { get; }
        public string Email { get; set; }

        public Person(string name, string phone, string email)
        {
            Name = name;
            Phone = phone;
            Email = email;
        }
    }
}
