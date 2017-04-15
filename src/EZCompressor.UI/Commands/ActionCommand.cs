using System;
using System.Windows.Input;

namespace EZCompressor.UI.Commands
{
    public class ActionCommand : ICommand
    {
        private Action<object> _action;
        private Predicate<object> _predicate;

        public ActionCommand(Action<object> action) : 
            this(action, null)
        {

        }
        public ActionCommand(Action<object> action, Predicate<object> predicate)
        {
            if (action == null) throw new ArgumentNullException("action", "You must specify an Action<T>");

            _action = action;
            _predicate = predicate;
        }

        #region ICommand Members

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if (_predicate == null)
                return true;

            return _predicate(parameter);
        }

        public void Execute(object parameter = null)
        {
            _action(parameter);
        } 

        #endregion
    }
}
