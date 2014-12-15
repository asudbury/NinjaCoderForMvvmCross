// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IRegisterService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.IoC
{
    using System.Reflection;

    /// <summary>
    ///  Defines the IRegisterService type.
    /// </summary>
    public interface IRegisterService
    {
        /// <summary>
        /// Auto the register of all the assemblies.
        /// </summary>
        void AutoRegister();

        /// <summary>
        /// Autos the register of the assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        void AutoRegister(Assembly assembly);

        /// <summary>
        /// Creates/replaces a container class registration with a specific, strong referenced, instance.
        /// </summary>
        /// <typeparam name="TRegisterType">The type of the register type.</typeparam>
        /// <param name="instance">Instance of RegisterType to register</param>
        void Register<TRegisterType>(TRegisterType instance) where TRegisterType : class;
    }
}