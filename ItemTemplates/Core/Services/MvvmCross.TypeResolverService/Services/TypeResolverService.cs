// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TypeResolverService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace MvvmCross.TypeResolverService.Services
{
    using Cirrious.CrossCore;

    /// <summary>
    ///  Defines the TypeResolverService type.
    /// </summary>
    public class TypeResolverService : ITypeResolverService
    {
        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <returns>An instance of TService.</returns>
        public TService GetService<TService>() where TService : class
        {
            return Mvx.Resolve<TService>();
        }
    }
}
