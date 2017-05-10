using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfTask.Helpers
{
    public class TaskCommand : ICommand
    {
        private readonly Action _execute;
        //private readonly Action<Task> _executeTask;
        private readonly Predicate<object> _canExecute;

        public TaskCommand(Action execute)
        {
            _execute = execute;
        }

        //public TaskCommand(Action<Task> executeTask)
        //{
        //    _executeTask = executeTask;
        //}

        public TaskCommand(Action execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute();
        }

        public event EventHandler CanExecuteChanged;
    }
}
