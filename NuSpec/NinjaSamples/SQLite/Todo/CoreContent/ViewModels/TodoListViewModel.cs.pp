// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the TodoListViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace $rootnamespace$.ViewModels
{
    using Entities;
    using Repositories;
    using Services;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the TodoListViewModel type.
    /// </summary>
    public class TodoListViewModel : BaseViewModel
    {
        /// <summary>
        /// The todo service.
        /// </summary>
        private readonly ITodoService todoService;

        /// <summary>
        /// The todo repository.
        /// </summary>
        private readonly ITodoRepository todoRepository;

        /// <summary>
        /// The items.
        /// </summary>
        private IEnumerable<TodoItem> items;

        /// <summary>
        /// The selected item.
        /// </summary>
        private TodoItem selectedItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="TodoListViewModel" /> class.
        /// </summary>
        /// <param name="todoService">The todo service.</param>
        /// <param name="todoRepository">The todo repository.</param>
        public TodoListViewModel(
            ITodoService todoService,
            ITodoRepository todoRepository)
        {
            this.todoService = todoService;
            this.todoRepository = todoRepository;
            this.Items = this.todoService.GetItems();
        }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        IEnumerable<TodoItem> Items
        {
            get { return this.items; }
            set { this.SetProperty(ref this.items, value); }
        }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        TodoItem SelectedItem
        {
            get { return this.selectedItem; }
            set { this.SetProperty(ref this.selectedItem, value); }
        }

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        public void DeleteItem()
        {
            this.todoService.DeleteItem(this.selectedItem.Id);
            this.Items = this.todoService.GetItems();
        }

        /// <summary>
        /// Adds the item.
        /// </summary>
        public void AddItem()
        {
            //// for MvvmCross solutions we can use the code below
            //// this.ShowViewModel<TodoItemViewModel>();
        }

        /// <summary>
        /// Updates the item.
        /// </summary>
        public void UpdateItem()
        {
            //// TODO : needs to be replacwe with real Selected Item Id
            this.todoRepository.SelectedItemId = 1;

            //// for MvvmCross solutions we can use the code below
            //// this.ShowViewModel<TodoItemViewModel>();
        }
    }
}
