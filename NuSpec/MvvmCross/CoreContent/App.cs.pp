// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the App type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$
{
    using MvvmCross.Core.ViewModels;
    using MvvmCross.Platform.IoC;
    using ViewModels;

    /// <summary>
    /// Define the App type.
    /// </summary>
    public class App : MvxApplication
    {
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize()
        {
            this.CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            //// Start the app with the Main View Model.
            this.RegisterAppStart<MainViewModel>();
        }
    }
}