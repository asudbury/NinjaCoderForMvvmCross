// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ResolverService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using NinjaCoder.MvvmCross.Services.Interfaces;

    using TinyIoC;

    /// <summary>
    ///  Defines the ResolverService type.
    /// </summary>
    public class ResolverService : IResolverService
    {
        /// <summary>
        /// Attempts to resolve a type.
        /// </summary>
        /// <typeparam name="ResolveType">Type to resolve</typeparam>
        /// <returns>Instance of type</returns>
        public ResolveType Resolve<ResolveType>()
            where ResolveType : class
        {
            TinyIoCContainer container = TinyIoCContainer.Current;
            return container.Resolve<ResolveType>();
        }
    }
}
