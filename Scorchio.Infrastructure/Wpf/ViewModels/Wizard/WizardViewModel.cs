// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the WizardViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Wpf.ViewModels.Wizard
{
    using System.Collections.Generic;
    using System.Windows.Input;

    /// <summary>
    ///  Defines the WizardViewModel type.
    /// </summary>
    public abstract class WizardViewModel : BaseViewModel, IWizardViewModel
    {
        /// <summary>
        /// The wizard step manager.
        /// </summary>
        private readonly WizardStepManager wizardStepManager;

        /// <summary>
        /// The steps.
        /// </summary>
        private List<WizardStepViewModel> steps;

        /// <summary>
        /// The move next command.
        /// </summary>
        private RelayCommand moveNextCommand;

        /// <summary>
        /// The move previous command.
        /// </summary>
        private RelayCommand movePreviousCommand;

        /// <summary>
        /// The finish command.
        /// </summary>
        private RelayCommand finishCommand;

        /// <summary>
        /// The cancel command.
        /// </summary>
        private RelayCommand cancelCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="WizardViewModel"/> class.
        /// </summary>
        protected WizardViewModel()
        {
            //// TODO : lets make wizard step manager a service that can be injected into the constructor.
            this.wizardStepManager = new WizardStepManager();
        }

        /// <summary>
        /// Gets or sets the steps.
        /// </summary>
        public List<WizardStepViewModel> Steps
        {
            get
            {
                return this.steps;
            }

            protected set
            {
                this.SetProperty(ref this.steps, value);
                this.wizardStepManager.ProvideSteps(value);

                this.ActionsOnCurrentStep(this.wizardStepManager.FirstStep);
            }
        }
        
        /// <summary>
        /// Gets the current linked list step.
        /// </summary>
        public LinkedListNode<WizardStepViewModel> CurrentLinkedListStep
        {
            get
            {
                return this.wizardStepManager.CurrentLinkedListStep;
            }

            set
            {
                this.wizardStepManager.CurrentLinkedListStep = value;

                this.ActionsOnCurrentStep(value);

                this.OnNotify("CurrentLinkedListStep");
                this.OnNotify("IsOnLastStep");
                this.OnNotify("MovePreviousCommandEnabled");
                this.OnNotify("MoveNextCommandEnabled");
                this.OnNotify("FinishCommandEnabled");
            }
        }


        /// <summary>
        /// Gets the move next command.
        /// </summary>
        public ICommand MoveNextCommand
        {
            get { return this.moveNextCommand ?? (this.moveNextCommand = new RelayCommand(this.MoveToNextStep)); }
        }

        /// <summary>
        /// Gets the move previous command.
        /// </summary>
        public ICommand MovePreviousCommand
        {
            get { return this.movePreviousCommand ?? (this.movePreviousCommand = new RelayCommand(this.MoveToPreviousStep)); }
        }

        /// <summary>
        /// Gets the finish command.
        /// </summary>
        public ICommand FinishCommand
        {
            get { return this.finishCommand ?? (this.finishCommand = new RelayCommand(this.Finish)); }
        }

        /// <summary>
        /// Gets the cancel command.
        /// </summary>
        public ICommand CancelCommand
        {
            get { return this.cancelCommand ?? (this.cancelCommand = new RelayCommand(this.Cancel)); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is on last step.
        /// </summary>
        public bool IsOnLastStep
        {
            get { return CurrentLinkedListStep.Next == null; }
        }

        /// <summary>
        /// Gets a value indicating whether [move previous command enabled].
        /// </summary>
        public bool MovePreviousCommandEnabled
        {
            get { return this.CurrentLinkedListStep.Previous != null; }        
        }

        public bool MoveNextCommandEnabled
        {
            get { return this.CurrentLinkedListStep.Next != null; }
        }

        /// <summary>
        /// Gets a value indicating whether [finish command enabled].
        /// </summary>
        public bool FinishCommandEnabled
        {
            get { return this.IsOnLastStep; }
        }

        /// <summary>
        /// Cancels this instance.
        /// </summary>
        public abstract void Cancel();

        /// <summary>
        /// Finishes this instance.
        /// </summary>
        protected abstract void Finish(); 
  
        /// <summary>
        /// Moves to next step.
        /// </summary>
        private void MoveToNextStep()
        {
            if (this.CurrentLinkedListStep.Value.ViewModel.CanMoveToNextPage())
            {
                this.wizardStepManager.ReworkListBasedOn(CurrentLinkedListStep.Value.ViewModel.OnNext());
                this.CurrentLinkedListStep = this.CurrentLinkedListStep.Next;

                if (this.CurrentLinkedListStep != null)
                {
                    this.CurrentLinkedListStep.Value.ViewModel.OnInitialize();
                }
            }
        }
        
        /// <summary>
        /// Moves to previous step.
        /// </summary>
        private void MoveToPreviousStep()
        {
            if (this.CurrentLinkedListStep.Value.ViewModel.CanMoveToPreviousPage())
            {
                this.CurrentLinkedListStep = CurrentLinkedListStep.Previous;
            }
        }
        
        /// <summary>
        /// Actionses the on current step.
        /// </summary>
        /// <param name="step">The step.</param>
        private void ActionsOnCurrentStep(LinkedListNode<WizardStepViewModel> step)
        {
            if (this.CurrentLinkedListStep != null)
            {
                this.CurrentLinkedListStep.Value.ViewModel.IsCurrentStep = false;
            }

            this.wizardStepManager.CurrentLinkedListStep = step;

            if (step != null)
            {
                step.Value.ViewModel.IsCurrentStep = true;
            }
        }
    }
}
