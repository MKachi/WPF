using System;
using System.Windows.Input;

namespace MVVM
{
    public class RelayCommand : BaseCommand, ICommand
    {
        private readonly Action _action;

        public RelayCommand(Action action)
            : this(action, null) { }
        public RelayCommand(Action action, Func<bool> canExecute)
            : base(canExecute)
        {
            if (ReferenceEquals(action, null))
            {
                throw new ArgumentNullException("action");
            }
            _action = action;
        }

        public void Execute(object paramter)
        {
            _action();
        }
    }
}