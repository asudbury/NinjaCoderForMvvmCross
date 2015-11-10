// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the TodoItemViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace $rootnamespace$.ViewModels
{
    using Entities;
    using Repositories;
    using Services;

    /// <summary>
    /// Defines the TodoItemViewModel type.
    /// </summary>
    public class TodoItemViewModel : BaseViewModel
    {
        /// <summary>
        /// The todo service.
        /// </summary>
        private readonly ITodoService todoService;

        /// <summary>
        /// The todo item.
        /// </summary>
        private TodoItem todoItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="TodoItemViewModel" /> class.
        /// </summary>
        /// <param name="todoService">The todo service.</param>
        /// <param name="todoRepository">The todo repository.</param>
        public TodoItemViewModel(
            ITodoService todoService,
            ITodoRepository todoRepository)
        {
            this.todoService = todoService;

            if (todoRepository.SelectedItemId.HasValue)
            {
                this.TodoItem = this.todoService.GetItem(todoRepository.SelectedItemId.Value);
            }

            if (this.TodoItem == null)
            {
                this.TodoItem = new TodoItem();
            }
        }

        /// <summary>
        /// Gets or sets the todo item.
        /// </summary>
        public TodoItem TodoItem
        {
            get { return this.todoItem; }
            set { this.SetProperty(ref this.todoItem, value); }
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {
            this.todoService.SaveItem(this.TodoItem);

            //// for MvvmCross solutions we can use the code below
            //// this.ShowViewModel<TodoListViewModel>();
        }

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        public void Delete()
        {
            this.todoService.DeleteItem(this.TodoItem.Id);

            //// for MvvmCross solutions we can use the code below
            //// this.ShowViewModel<TodoListViewModel>();
        }

        /// <summary>
        /// Cancels this instance.
        /// </summary>
        public void Cancel()
        {
            //// for MvvmCross solutions we can use the code below
            //// this.ShowViewModel<TodoListViewModel>();
        }
    }
}
