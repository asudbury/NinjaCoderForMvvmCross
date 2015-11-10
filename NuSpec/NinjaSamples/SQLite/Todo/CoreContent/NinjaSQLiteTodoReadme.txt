The Ninja Coder has added the following files

DataService\ITodoSqliteDataService.cs
DataServices\TodoSqliteDataService.cs

Entities\TodoItem.cs

Repositories\ITodoRepository.cs
Repositories\TodoRepository.cs

Services\ITodoService.cs
Services\TodoService.cs

ViewModels\TodoItemViewModel.cs
ViewModels\TodoListViewModel.cs

You need to implement the SqlConnectionService.GetConnection() 
for each of the platforms you intend to implement.

One way to do this is to use the Xamrin Forms Dependency service implementation as shown below

https://github.com/xamarin/xamarin-forms-samples/tree/master/Todo/PCL
