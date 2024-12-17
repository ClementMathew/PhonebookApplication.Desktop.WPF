using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phonebook_Application.Models;

namespace Phonebook_Application.Repositories
{
    internal class JsonRepository : IRepository<Person>
    {
        /// <summary>
        /// Private list to store initial values. 
        /// </summary>
        private List<Person> _persons = new List<Person>
        {
            new Person("Clement","8156819141","clement@gmail.com"),
            new Person("Mathew","9961145435","mathew@gmail.com"),
            new Person("Sheba","7406004048","sheba@gmail.com"),
        };

        public void AddItem(string name, string phone, string email)
        {
            _persons.Add(new Person(name, phone, email));
        }

        public IEnumerable<Person> GetAll()
        {
            return _persons;
        }
    }
}
