using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Phonebook_Application.ViewModels
{
    internal abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// OnPropertyChanged Function
        /// --------------------------
        /// 1. Invokes PropertyChanged by propertyName from CallerMemberName.
        /// </summary>
        /// <param name="propertyName"></param>
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
