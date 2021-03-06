﻿using System;
using System.Windows.Input;

namespace ViewModels
{
    public class BaseCommand : ICommand
    {
        private readonly Action<object> _exec;
        private readonly Func<object, bool> _canExec;

        protected BaseCommand()
        { }

        public BaseCommand(Action<object> exec, Func<object, bool> canExec = null)
        {
            if (exec == null) 
                throw new ArgumentNullException("exec");
            _exec = exec;
            _canExec = canExec;
        }

        public virtual void Execute(object parameter)
        {
            _exec(parameter);
        }

        public virtual bool CanExecute(object parameter)
        {
            return _canExec == null || _canExec(parameter);
        }

        
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        protected virtual void OnCanExecuteChanged(EventArgs e)
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
