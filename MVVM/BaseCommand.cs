using System;

namespace MVVM
{
    public class BaseCommand
    {
        protected readonly Func<bool> _canExecute;
        public event EventHandler CanExecuteChanged;

        protected BaseCommand(Func<bool> canExecute)
        {
            _canExecute = canExecute;
        }

        public bool CanExecute(object arg)
        {
            if (ReferenceEquals(_canExecute, null))
            {
                return true;
            }
            return _canExecute();
        }
        public void RaiseCanExecuteChanged()
        {
            if (ReferenceEquals(CanExecuteChanged, null))
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}