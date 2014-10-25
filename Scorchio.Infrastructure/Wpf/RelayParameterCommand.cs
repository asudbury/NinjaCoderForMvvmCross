﻿// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the RelayParameterCommand type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Wpf
{
    using System;
    using System.Windows.Input;

    /// <summary>
    ///  Defines the RelayParameterCommand type.
    /// </summary>
    public class RelayParameterCommand<T> : ICommand
    {
        /// <summary>
        /// The action.
        /// </summary>
        private readonly Action<T> action;

        /// <summary>
        /// The can execute.
        /// </summary>
        private readonly Func<bool> canExecute;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayParameterCommand{T}"/> class.
        /// </summary>
        /// <param name="action">The action.</param>
        public RelayParameterCommand(Action<T> action)
        {
            this.action = action;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayParameterCommand{T}"/> class.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="canExecute">The can execute.</param>
        public RelayParameterCommand(
            Action<T> action,
            Func<bool> canExecute)
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute();
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(object parameter)
        {
            this.action((T)parameter);
        }
    }
}
