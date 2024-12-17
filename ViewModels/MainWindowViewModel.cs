using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Phonebook_Application.Commands;
using Phonebook_Application.Models;
using Phonebook_Application.Repositories;

namespace Phonebook_Application.ViewModels
{
    internal class MainWindowViewModel : VIewModelBase
    {
        private IRepository<Person> _repository;
        private IEnumerable<Person> _persons;

        public MainWindowViewModel()
        {
            Email = string.Empty;
            Name = string.Empty;
            Phone = string.Empty;

            _repository = new JsonRepository();
            _persons = _repository.GetAll();

            PersonsToList = new ObservableCollection<Person>(_persons);

            AddCommand = new RelayCommand(HandleAddCommand, CanHandleAddCommand);
            ClearCommand = new RelayCommand(HandleClearCommand);
        }
        #region Properties

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
                AddCommand?.OnCanExecuteChanged();
            }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
                AddCommand?.OnCanExecuteChanged();
            }
        }
        private string _phone;

        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                OnPropertyChanged();
                AddCommand?.OnCanExecuteChanged();
            }
        }
        private string _search;

        public string Search
        {
            get { return _search; }
            set
            {
                _search = value;
                OnPropertyChanged();
                HandleSearch();
            }
        }

        #endregion

        /// <summary>
        /// Observable Collection to list elements in list view.
        /// </summary>
        public ObservableCollection<Person> PersonsToList { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand ClearCommand { get; set; }

        /// <summary>
        /// Clear all search input.
        /// </summary>
        /// <param name="obj"></param>
        private void HandleClearCommand(object obj)
        {
            Search = string.Empty;
        }

        /// <summary>
        /// Command to handle search button.
        /// </summary>
        /// <param name="obj"></param>
        private void HandleSearch()
        {
            if (!string.IsNullOrEmpty(Search))
            {
                PersonsToList.Clear();

                foreach (var item in _persons)
                {
                    if (item.Name.Contains(Search) || item.Email.Contains(Search))
                    {
                        PersonsToList.Add(item);
                    }
                }
            }
            else
            {
                PersonsToList.Clear();
                foreach (var item in _persons)
                {
                    PersonsToList.Add(item);
                }
            }
        }

        /// <summary>
        /// Checking conditions to handle add button.
        /// </summary>
        /// <param name="arg"></param>
        /// <returns> true if mail and phone is valid</returns>
        private bool CanHandleAddCommand(object arg)
        {
            return CheckEmail() && CheckPhone();
        }

        /// <summary>
        /// Command to handle add button.
        /// </summary>
        /// <param name="obj"></param>
        private void HandleAddCommand(object obj)
        {
            if (!(string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(Phone)))
            {
                var newPerson = new Person(Name, Phone, Email);

                _persons.Add(newPerson);
                PersonsToList.Add(newPerson);
            }
        }

        /// <summary>
        /// Checking email validity.
        /// </summary>
        /// <returns></returns>
        private bool CheckEmail() => Email.Contains("@gmail") && Email.Contains(".com");

        /// <summary>
        /// Checking phone is valid or not.
        /// </summary>
        /// <returns></returns>
        private bool CheckPhone()
        {
            bool isValid = false;

            foreach (var item in Phone)
            {
                int ascii = (int)(item);

                if (ascii <= 57 && ascii >= 48)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                    break;
                }
            }
            return Phone.Length == 10 && isValid;
        }
    }
}
