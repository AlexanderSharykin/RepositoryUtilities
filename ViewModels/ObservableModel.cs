using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ViewModels
{
    public abstract class ObservableModel: INotifyPropertyChanged
    {        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
