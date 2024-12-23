using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using Phonebook_Application.Models;

namespace Phonebook_Application.Repositories
{
    internal class JsonRepository : IRepository<Person>
    {
        private List<Person> _persons;
        private string _filePath;

        public JsonRepository()
        {
            IntializeJsonRepository();

            if (_persons == null)
            {
                _persons = new List<Person>()
                {
                    new Person("Clement","8156819141","clementmathew@gmail.com")
                };
            }
        }

        /// <summary>
        /// IntializeJsonRepository Function
        /// --------------------------------
        /// 1. Creates directory and file for storing data as Json File.
        /// 2. Write dummy data to data.json if file not exists
        /// 3. Reads data to _persons if file exists.
        /// </summary>
        private void IntializeJsonRepository()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string folderPath = Path.Combine(path, "DotNet", "Phonebook Application");

            Directory.CreateDirectory(folderPath);
            _filePath = Path.Combine(folderPath, "data.json");

            if (!File.Exists(_filePath))
            {
                JsonWrite();
            }
            else
            {
                string fromJson = File.ReadAllText(_filePath);
                _persons = JsonConvert.DeserializeObject<List<Person>>(fromJson);
            }
        }

        /// <summary>
        /// JsonWrite Function
        /// ------------------
        /// 1. Writes JsonSerializedObject to data.json file.
        /// </summary>
        private void JsonWrite()
        {
            string toJson = JsonConvert.SerializeObject(_persons, Formatting.Indented);
            File.WriteAllText(_filePath, toJson);
        }

        /// <summary>
        /// AddItem Function
        /// ----------------
        /// 1. Creates person object and adds to _persons list.
        /// 2. Write _persons to data.json by JsonWrite Function.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="email"></param>
        public void AddItem(Person person)
        {
            _persons.Add(person);

            JsonWrite();
        }

        /// <summary>
        /// GetAll Function
        /// </summary>
        /// <returns>
        ///     1. returns _persons list.
        /// </returns>
        public IEnumerable<Person> GetAll()
        {
            return _persons;
        }
    }
}
