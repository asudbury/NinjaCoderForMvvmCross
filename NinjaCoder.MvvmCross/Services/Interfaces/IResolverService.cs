// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IResolverService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services.Interfaces
{
    /// <summary>
    ///  Defines the IResolverService type.
    /// </summary>
    public interface IResolverService
    {
        /// <summary>
        /// Attempts to resolve a type using default options.
        /// </summary>
        /// <typeparam name="TResolveType">Type to resolve</typeparam>
        /// <returns>Instance of type</returns>
        TResolveType Resolve<TResolveType>() where TResolveType : class;
    }
}