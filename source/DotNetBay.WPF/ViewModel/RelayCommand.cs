using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;

namespace DotNetBay.WPF.ViewModel
{
    public class RelayCommand<TCommandParameter> : ICommand
    {
        private readonly Action<TCommandParameter> executeAction;

        private readonly Func<TCommandParameter, bool> canExecute;

        public RelayCommand(Action<TCommandParameter> executeAction, Func<TCommandParameter, bool> canExecute)
        {
            this.executeAction = executeAction;
            this.canExecute = canExecute;
        }

        public RelayCommand(Action<TCommandParameter> executeAction)
            : this(executeAction, null)
        {
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (this.canExecute != null)
            {
                return this.canExecute((TCommandParameter)parameter);
            }

            return true;
        }

        public void Execute(object parameter)
        {
            this.executeAction((TCommandParameter)parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            this.OnCanExecuteChanged();
        }

        protected virtual void OnCanExecuteChanged()
        {
            var handler = this.CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "This basically the same class, but with no TypeArguments")]
    public class RelayCommand : RelayCommand<object>
    {
        public RelayCommand(Action executeAction, Func<bool> canExecute)
            : base(o => executeAction(), o => canExecute())
        {
        }

        public RelayCommand(Action executeAction)
            : base(o => executeAction(), null)
        {
        }
    }
}
