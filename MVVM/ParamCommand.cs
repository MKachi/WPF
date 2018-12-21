using System;
using System.Windows.Input;

namespace MVVM
{
    public class ParamCommand : BaseCommand, ICommand
    {
        private readonly Action<object> _action;

        public ParamCommand(Action<object> action)
            : this(action, null) { }
        public ParamCommand(Action<object> action, Func<bool> canExecute)
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
            _action(paramter);
        }
    }
}