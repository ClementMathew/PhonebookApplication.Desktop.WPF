using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using Phonebook_Application.Commands;
using Phonebook_Application.Models;
using Phonebook_Application.Repositories;

namespace Phonebook_Application.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase, IDataErrorInfo
    {
        private IRepository<Person> _repository;
        private List<Person> _persons;

        /// <summary>
        /// Private list to store the error results.
        /// </summary>
        private List<ValidationResult> results;

        /// <summary>
        /// Observable Collection to list elements in list view.
        /// </summary>
        public ObservableCollection<Person> PersonsToList { get; set; }

        public RelayCommand AddCommand { get; set; }
        public RelayCommand ClearCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }

        #region Properties

        private string _name;

        [Required(ErrorMessage = "Name can't empty.")]
        [MaxLength(20, ErrorMessage = "Maximum length reached.")]
        [MinLength(3, ErrorMessage = "Minimum Length not satisfied.")]

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

        [Required(ErrorMessage = "Email can't empty.")]
        [EmailAddress(ErrorMessage = "Email address is not valid.")]

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

        [Required(ErrorMessage = "Phone can't empty.")]
        [Phone(ErrorMessage = "Phone number is invalid.")]
        [MinLength(10, ErrorMessage = "Minimum Length not satisfied.")]

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

        private Person _selectedPerson;

        public Person SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                _selectedPerson = value;
                OnPropertyChanged();
            }
        }

        #endregion

        /// <summary>
        /// MainWindowViewModel Constructor
        /// -------------------------------
        /// 1. Creates JsonRepository object.
        /// 2. Initializes _persons and PersonsToList from JsonRepository.
        /// 3. Initializes relay commands add, clear and delete.
        /// </summary>
        public MainWindowViewModel()
        {
            _repository = new JsonRepository();
            _persons = new List<Person>(_repository.GetAll());

            PersonsToList = new ObservableCollection<Person>(_persons);

            AddCommand = new RelayCommand(HandleAddCommand, CanHandleAddCommand);
            ClearCommand = new RelayCommand(HandleClearCommand);
            DeleteCommand = new RelayCommand(HandleDeleteCommand);
        }

        /// <summary>
        /// Property where we get errors.
        /// </summary>
        public string Error { get; }

        /// <summary>
        /// DataErrorInfo Validation Indexer
        /// --------------------------------
        /// 1. Creates ValidationContext with called property.
        /// 2. Takes value of corresponding property.
        /// 3. Validates using TryValidateProperty.
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns>
        ///     1. returns null if validation is true.
        ///     2. returns first error when validation is false.
        /// </returns>
        public string this[string columnName]
        {
            get
            {
                ValidationContext context = new ValidationContext(this) { MemberName = columnName };
                results = new List<ValidationResult>();

                object value = GetType().GetProperty(columnName).GetValue(this);

                bool isValid = Validator.TryValidateProperty(value, context, results);
                return isValid ? null : results.First().ErrorMessage;
            }
        }

        /// <summary>
        /// HandleDeleteCommand Function
        /// ----------------------------
        /// 1. Search for the person in the PersonsToList by command parameter 'obj' and sets SelectedPerson by person object.
        /// 2. Asking confirmation to delete the SelectedPerson.
        /// 3. Removes SelectedPerson from Json Repository, _persons and PersonsToList.
        /// </summary>
        /// <param name="obj"></param>
        private void HandleDeleteCommand(object obj)
        {
            foreach (Person person in PersonsToList)
            {
                if (person.Name == (string)obj)
                {
                    SelectedPerson = person;
                }
            }
            MessageBoxResult result = MessageBox.Show("Delete Person", "Delete Person", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _repository.RemoveItem(SelectedPerson);
                _persons.Remove(SelectedPerson);
                PersonsToList.Remove(SelectedPerson);
            }
        }

        /// <summary>
        /// HandleClearCommand Function
        /// ---------------------------
        /// 1. Clear all search input.
        /// </summary>
        /// <param name="obj"></param>
        private void HandleClearCommand(object obj)
        {
            Search = string.Empty;
        }

        /// <summary>
        /// Handle Search Function
        /// ----------------------
        /// 1. Command to handle search button.
        /// </summary>
        /// <param name="obj"></param>
        private void HandleSearch()
        {
            if (!string.IsNullOrEmpty(Search))
            {
                PersonsToList.Clear();

                foreach (Person item in _persons)
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
                foreach (Person item in _persons)
                {
                    PersonsToList.Add(item);
                }
            }
        }

        /// <summary>
        /// CanHandleAddCommand Function
        /// ----------------------------
        /// 1. Checking conditions to handle add button.
        /// </summary>
        /// <param name="arg"></param>
        /// <returns> true if mail and phone is valid</returns>
        private bool CanHandleAddCommand(object arg)
        {
            return !results.Any();
        }

        /// <summary>
        /// HandleAddCommand Function
        /// -------------------------
        /// 1. Command to handle add button.
        /// </summary>
        /// <param name="obj"></param>
        private void HandleAddCommand(object obj)
        {
            if (!(string.IsNullOrEmpty(Name) && (string.IsNullOrEmpty(Email)) && (string.IsNullOrEmpty(Phone))))
            {
                Person newPerson = new Person(Name, Phone, Email);

                _persons.Add(newPerson);
                PersonsToList.Add(newPerson);

                _repository.AddItem(newPerson);
            }
        }
    }
}
