using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM_Simple_Book
{
    public class DelegateCommand : ICommand
    {
        // создаем событие возможности выполнения команды
        Action<object> execute;
        Func<object, bool> canExecute;


        public event EventHandler CanExecuteChanged
        {           
            // подписываемся и отписываемся в менеджере команд
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if (canExecute != null)
            {
                return canExecute(parameter);
            }
            return true;
        }

        public void Execute(object parameter)
        {
            if (execute != null)
            execute (parameter);
        }

        public DelegateCommand(Action<object> executeAction) : this (executeAction, null)
        { }

        public DelegateCommand(Action<object> executeAction, Func<object, bool> canExecuteFunc)
        {
            canExecute = canExecuteFunc;
            execute = executeAction;
        }
    }
}
