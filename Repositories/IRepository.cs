using System.Collections.Generic;
using Phonebook_Application.Models;

namespace Phonebook_Application.Repositories
{
    internal interface IRepository<T>
    {
        /// <summary>
        /// AddItem Method
        /// --------------
        /// 1. Add single item to the repository.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="email"></param>
        void AddItem(Person person);

        /// <summary>
        /// RemoveItem Method
        /// -----------------
        /// 1. Remove single item from the repository.
        /// </summary>
        /// <param name="person"></param>
        void RemoveItem(Person person);

        /// <summary>
        /// GetAll Method
        /// -------------
        /// 1. returns all items from repository as IEnumerable.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();
    }
}
