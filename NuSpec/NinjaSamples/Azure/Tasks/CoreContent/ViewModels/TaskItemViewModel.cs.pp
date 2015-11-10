// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the TaskItemViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace $rootnamespace$.ViewModels
{
    using Entities;
    using Repositories;
    using Services;

    /// <summary>
    /// Defines the TaskItemViewModel type.
    /// </summary>
    public class TaskItemViewModel : BaseViewModel
    {
        /// <summary>
        /// The task service.
        /// </summary>
        private readonly ITaskService taskService;

        /// <summary>
        /// The task item.
        /// </summary>
        private TaskItem taskItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskItemViewModel" /> class.
        /// </summary>
        /// <param name="taskService">The task service.</param>
        /// <param name="taskRepository">The task repository.</param>
        public TaskItemViewModel(
            ITaskService taskService,
            ITaskRepository taskRepository)
        {
            this.taskService = taskService;
            this.TaskItem = taskRepository.SelectedTask ?? new TaskItem();
        }

        /// <summary>
        /// Gets or sets the task item.
        /// </summary>
        public TaskItem TaskItem
        {
            get { return this.taskItem; }
            set { this.SetProperty(ref this.taskItem, value); }
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {
            this.taskService.SaveTaskAsync(this.TaskItem);

            //// for MvvmCross solutions we can use the code below
            //// this.ShowViewModel<TaskListViewModel>();
        }

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        public void Delete()
        {
            this.taskService.DeleteTaskAsync(this.TaskItem);

            //// for MvvmCross solutions we can use the code below
            //// this.ShowViewModel<TaskListViewModel>();
        }

        /// <summary>
        /// Cancels this instance.
        /// </summary>
        public void Cancel()
        {
            //// for MvvmCross solutions we can use the code below
            //// this.ShowViewModel<TaskListViewModel>();
        }
    }
}
