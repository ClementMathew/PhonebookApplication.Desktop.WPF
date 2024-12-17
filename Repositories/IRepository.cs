using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook_Application.Repositories
{
    internal interface IRepository<T>
    {
        void AddItem(string name,string phone,string email);
        IEnumerable<T> GetAll();
    }
}
