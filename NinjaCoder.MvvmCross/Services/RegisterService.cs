// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the RegisterService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using System.Reflection;

    using NinjaCoder.MvvmCross.Services.Interfaces;

    using TinyIoC;

    /// <summary>
    ///  Defines the RegisterService type.
    /// </summary>
    public class RegisterService : IRegisterService
    {
        /// <summary>
        /// Auto the register of all the assemblies.
        /// </summary>
        public void AutoRegister()
        {
        }

        /// <summary>
        /// Autos the register of the assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        public void AutoRegister(Assembly assembly)
        {
            TinyIoCContainer container = TinyIoCContainer.Current;
            container.AutoRegister(assembly);
        }

        /// <summary>
        /// Creates/replaces a container class registration with a specific, strong referenced, instance.
        /// </summary>
        /// <typeparam name="RegisterType">The type of the register type.</typeparam>
        /// <param name="instance">Instance of RegisterType to register</param>
        public void Register<RegisterType>(RegisterType instance) where RegisterType : class
        {
            TinyIoCContainer container = TinyIoCContainer.Current;
            container.Register(instance);
        }
    }
}
