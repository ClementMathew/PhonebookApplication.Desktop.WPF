﻿using System;
using System.Windows.Input;

namespace Phonebook_Application.Commands
{
    internal class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// RelayCommand Constructor
        /// ------------------------
        /// 1. Intializes _execute and _canExecute functions.
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// CanExecute Function
        /// -------------------
        /// 1. Determines whether to execute the function 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>
        ///     1. returns true when _canExecute is null
        ///     2. returns _canExecute() Function if _canExecute is not null.
        /// </returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        /// <summary>
        /// Execute Function
        /// ----------------
        /// 1. returns _execute() Function.
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        /// <summary>
        /// OnCanExecuteChanged Function
        /// ----------------------------
        /// 1. Invokes CanExecuteChanged.
        /// </summary>
        public void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
