// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the WizardStepManager type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Wpf.ViewModels.Wizard
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the WizardStepManager type.
    /// </summary>
    public class WizardStepManager
    {
        /// <summary>
        /// The reconfiguring route.
        /// </summary>
        private bool reconfiguringRoute;

        /// <summary>
        /// The current linked list step.
        /// </summary>
        private LinkedListNode<WizardStepViewModel> currentLinkedListStep;

        /// <summary>
        /// The steps.
        /// </summary>
        private List<WizardStepViewModel> steps;

        /// <summary>
        /// Gets the steps.
        /// </summary>
        public LinkedList<WizardStepViewModel> LinkedSteps { get; private set; }

        /// <summary>
        /// Gets or sets the first step.
        /// </summary>
        public LinkedListNode<WizardStepViewModel> FirstStep
        {
            get { return this.LinkedSteps == null ? null : this.LinkedSteps.First; }
        }

        /// <summary>
        /// Gets the current step.
        /// </summary>
        public LinkedListNode<WizardStepViewModel> CurrentLinkedListStep
        {
            get
            {
                return this.currentLinkedListStep;
            }

            set
            {
                this.currentLinkedListStep = value;

                if ((this.LinkedSteps.First == this.currentLinkedListStep) && !reconfiguringRoute)
                {
                    ResetRoute();
                }
            }
        }

        /// <summary>
        /// Provides the steps.
        /// </summary>
        /// <param name="wizardSteps">The wizard steps.</param>
        public void ProvideSteps(List<WizardStepViewModel> wizardSteps)
        {
            this.steps = wizardSteps;
            this.LinkedSteps = new LinkedList<WizardStepViewModel>(steps);
            this.CurrentLinkedListStep = this.LinkedSteps.First;
        }

        /// <summary>
        /// Reworks the list based on.
        /// </summary>
        /// <param name="routeModifier">The route modifier.</param>
        public void ReworkListBasedOn(RouteModifier routeModifier)
        {
            if (routeModifier == null)
            {
                return;
            }

            this.reconfiguringRoute = true;

            this.ReorganizeLinkedList(routeModifier);
            this.ResetListRelevancy();

            this.reconfiguringRoute = false;
        }

        /// <summary>
        /// Each step in the wizard may modify the route, but it's assumed that if the user goes back to step one, the route initializes back to the way it
        /// was when it was created.
        /// </summary>
        private void ResetRoute()
        {
            List<Type> allStepViewTypes = this.LinkedSteps.ToList().ConvertAll(s => s.ViewType);
            ReworkListBasedOn(new RouteModifier() { IncludeViewTypes = allStepViewTypes });
        }

        /// <summary>
        /// Re-create the linked list to reflect the new "workflow."
        /// </summary>
        /// <param name="routeModifier">The route modifier.</param>
        private void ReorganizeLinkedList(RouteModifier routeModifier)
        {
            WizardStepViewModel cacheCurrentStep = this.CurrentLinkedListStep.Value;
            IEnumerable<WizardStepViewModel> newSubList = this.CreateNewStepList(routeModifier);

            this.LinkedSteps = new LinkedList<WizardStepViewModel>(newSubList);
            this.ResetCurrentLinkedListStepTo(cacheCurrentStep);
        }

        /// <summary>
        /// At this point, if a step is in the linked list, it's relevant; if not, it's not.
        /// </summary>
        private void ResetListRelevancy()
        {
            LinkedListNode<WizardStepViewModel> linkedStep = this.LinkedSteps.First;

            while (linkedStep != null)
            {
                linkedStep = linkedStep.Next;
            }
        }


        /// <summary>
        /// Creates the new step list.
        /// </summary>
        /// <param name="routeModifier">The route modifier.</param>
        /// <returns></returns>
        private IEnumerable<WizardStepViewModel> CreateNewStepList(RouteModifier routeModifier)
        {
            List<WizardStepViewModel> result = new List<WizardStepViewModel>(this.steps);

            this.EnsureNotModifyingCurrentStep(routeModifier);

            if (routeModifier.ExcludeViewTypes != null)
            {
                routeModifier.ExcludeViewTypes.ForEach(t => result.RemoveAll(step => step.ViewType == t));
            }
            if (routeModifier.IncludeViewTypes != null)
            {
                this.AddBack(result, routeModifier.IncludeViewTypes);
            }

            return result;
        }

        /// <summary>
        /// Ensures the not modifying current step.
        /// </summary>
        /// <param name="routeModifier">The route modifier.</param>
        private void EnsureNotModifyingCurrentStep(RouteModifier routeModifier)
        {
            Func<Type, bool> currentStepCondition = t => t == this.CurrentLinkedListStep.Value.ViewType;

            if (routeModifier.ExcludeViewTypes != null)
            {
               routeModifier.ExcludeViewTypes.FirstOrDefault(currentStepCondition);
            }

            if (routeModifier.IncludeViewTypes != null)
            {
                routeModifier.IncludeViewTypes.FirstOrDefault(currentStepCondition);
            }
        }

        /// <summary>
        /// Must maintain the current step reference (this re-creating of the linked list happens when the user makes a selection on
        /// the current step).
        /// After recreating the list, our CurrentLinkedListStep reference would be referring to an item in the old linked list.
        /// </summary>
        /// <param name="cacheCurrentStep"></param>
        private void ResetCurrentLinkedListStepTo(WizardStepViewModel cacheCurrentStep)
        {
            this.CurrentLinkedListStep = this.LinkedSteps.First;

            while (this.CurrentLinkedListStep.Value != cacheCurrentStep)
            {
                if (this.CurrentLinkedListStep.Next != null)
                {
                    this.CurrentLinkedListStep = this.CurrentLinkedListStep.Next;
                }
            }
        }

        /// <summary>
        /// OMG, if the user chooses an option that changes the route through the wizard, then goes back and chooses a different option,
        /// we need to add the appropriate step(s) back into the workflow.
        /// </summary>
        /// <param name="workingStepList"></param>
        /// <param name="viewTypes"></param>
        private void AddBack(
            List<WizardStepViewModel> workingStepList, 
            IEnumerable<Type> viewTypes)
        {
            foreach (Type vt in viewTypes)
            {
                //// Find the step to add back in the main list of steps.

                WizardStepViewModel stepToAddBack = this.steps.FirstOrDefault(s => s.ViewType == vt);
                
                if (!workingStepList.Contains(stepToAddBack))
                {
                    //// Re-insert the step into our working list (which will become the wizard's new linked list).

                    if (stepToAddBack != null)
                    {
                        int indexOfStepToAddBack = this.steps.IndexOf(stepToAddBack);

                        //// If it belongs at the head of the list, add it there.

                        if (indexOfStepToAddBack == 0)
                        {
                            workingStepList.Insert(0, stepToAddBack);
                        }
                        else
                        {
                            //// Otherwise we have to find the previous step in the main list, find that step in our working list and add in
                            //// the step after that step.
 
                            bool stepReinserted = false;
                            int countOfStepsToPreviousFoundStep = 1;

                            while (!stepReinserted)
                            {
                                WizardStepViewModel previousStep = this.steps[indexOfStepToAddBack - countOfStepsToPreviousFoundStep];

                                for (int i = 0; i < workingStepList.Count; i++)
                                {
                                    if (workingStepList[i].ViewType == previousStep.ViewType)
                                    {
                                        workingStepList.Insert(i + 1, stepToAddBack);
                                        stepReinserted = true;
                                    }
                                }

                                //// The previous step wasn't found; continue to the next previous step.

                                countOfStepsToPreviousFoundStep++;
                            }
                        }
                    }
                }
            }
        }
    }
}
