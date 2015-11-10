// --------------------------------------------------------------------------------------------------------------------
//  Defines the ITodoRepository type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$.Repositories
{
    /// <summary>
    /// Defines the ITodoRepository type.
    /// </summary>
    public interface ITodoRepository
    {
        /// <summary>
        /// Gets or sets the selected item identifier.
        /// </summary>
        int? SelectedItemId { get; set; }
    }
}
