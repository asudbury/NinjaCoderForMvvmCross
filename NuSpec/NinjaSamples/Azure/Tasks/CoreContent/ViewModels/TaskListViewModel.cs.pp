// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the TaskListViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace $rootnamespace$.ViewModels
{
    using Entities;
    using Repositories;
    using Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the TaskListViewModel type.
    /// </summary>
    public class TaskListViewModel : BaseViewModel
    {
        /// <summary>
        /// The task service.
        /// </summary>
        private readonly ITaskService taskService;

        /// <summary>
        /// The task repository.
        /// </summary>
        private readonly ITaskRepository taskRepository;

        /// <summary>
        /// The selected task.
        /// </summary>
        private TaskItem selectedTask;

        /// <summary>
        /// The items.
        /// </summary>
        private Task<List<TaskItem>> items;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskListViewModel" /> class.
        /// </summary>
        /// <param name="taskService">The task service.</param>
        /// <param name="taskRepository">The task repository.</param>
        public TaskListViewModel(
            ITaskService taskService,
            ITaskRepository taskRepository)
        {
            this.taskService = taskService;
            this.taskRepository = taskRepository;
            this.Items = this.taskService.GetTasksAsync();
        }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        Task<List<TaskItem>> Items
        {
            get { return this.items; }
            set { this.SetProperty(ref this.items, value); }
        }

        /// <summary>
        /// Gets or sets the selected task.
        /// </summary>
        TaskItem SelectedTask
        {
            get { return this.selectedTask; }
            set { this.SetProperty(ref this.selectedTask, value); }
        }

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        public void DeleteItem()
        {
            this.taskService.DeleteTaskAsync(this.SelectedTask);
            this.Items = this.taskService.GetTasksAsync();
        }

        /// <summary>
        /// Adds the item.
        /// </summary>
        public void AddItem()
        {
            //// for MvvmCross solutions we can use the code below
            //// this.ShowViewModel<TaskItemViewModel>();
        }

        /// <summary>
        /// Updates the item.
        /// </summary>
        public void UpdateItem()
        {
            this.taskRepository.SelectedTask = this.SelectedTask;

            //// for MvvmCross solutions we can use the code below
            //// this.ShowViewModel<TaskItemViewModel>();
        }
    }
}
