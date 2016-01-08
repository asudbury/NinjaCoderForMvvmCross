// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IMvvmCrossViewFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories.Interfaces
{
    using System.Collections.ObjectModel;
    using Scorchio.Infrastructure.Entities;

    /// <summary>
    /// Defines the IMvvmCrossViewFactory type.
    /// </summary>
    public interface IMvvmCrossViewFactory
    {
        /// <summary>
        /// Gets the views.
        /// </summary>
        ObservableCollection<ImageItemWithDescription> Views { get; }
    }
}
