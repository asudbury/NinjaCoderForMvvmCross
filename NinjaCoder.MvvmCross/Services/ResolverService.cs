// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ResolverService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using Interfaces;
    using Scorchio.VisualStudio.Services;
    using TinyIoC;

    /// <summary>
    ///  Defines the ResolverService type.
    /// </summary>
    public class ResolverService : IResolverService
    {
        /// <summary>
        /// Attempts to resolve a type.
        /// </summary>
        /// <typeparam name="TResolveType">Type to resolve</typeparam>
        /// <returns>Instance of type</returns>
        public TResolveType Resolve<TResolveType>()
            where TResolveType : class
        {
            TraceService.WriteDebugLine("ResolverService::Resolve type=" + typeof(TResolveType));
            TinyIoCContainer container = TinyIoCContainer.Current;
            return container.Resolve<TResolveType>();
        }
    }
}
