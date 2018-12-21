using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MVVM
{
    public class ObservedObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            if (!ReferenceEquals(PropertyChanged, null))
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected bool SetProperty<T>(ref T field, T value = default(T), [CallerMemberName]string propertyName = null)
        {
            field = value;
            if (!ReferenceEquals(propertyName, null))
            {
                RaisePropertyChanged(propertyName);
                return true;
            }
            return false;
        }
    }
}