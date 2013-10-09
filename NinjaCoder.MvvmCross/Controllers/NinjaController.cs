// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the NinjaController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Abstractions;
    using System.Reflection;
    using Entities;
    using EnvDTE;
    using EnvDTE80;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using TinyIoC;
    using Translators;

    /// <summary>
    /// Define the NinjaController type.
    /// </summary>
    public static class NinjaController
    {
        /// <summary>
        /// The initialized indicator.
        /// </summary>
        private static bool initialized;

        /// <summary>
        /// Runs the configuration controller.
        /// </summary>
        /// <param name="dte2">The dte2.</param>
        public static void RunConfigurationController(DTE2 dte2 = null)
        {
            TraceService.WriteLine("NinjaController::RunProjectsController");

            ConfigurationController controller = ResolveController<ConfigurationController>(dte2);

            controller.Run();
        }

        /// <summary>
        /// Runs the projects controller.
        /// </summary>
        /// <param name="dte2">The dte2.</param>
        public static void RunProjectsController(DTE2 dte2 = null)
        {
            TraceService.WriteLine("NinjaController::RunProjectsController");

            ProjectsController controller = ResolveController<ProjectsController>(dte2);

            controller.Run();
        }

        /// <summary>
        /// Runs the view model views controller.
        /// </summary>
        /// <param name="dte2">The dte2.</param>
        public static void RunViewModelViewsController(DTE2 dte2 = null)
        {
            TraceService.WriteLine("NinjaController::RunViewModelViewsController");

            ViewModelViewsController controller = ResolveController<ViewModelViewsController>(dte2);

            controller.Run();
        }

        /// <summary>
        /// Runs the plugins controller.
        /// </summary>
        /// <param name="dte2">The dte2.</param>
        public static void RunPluginsController(DTE2 dte2 = null)
        {
            TraceService.WriteLine("NinjaController::RunPluginsController");

            PluginsController controller = ResolveController<PluginsController>(dte2);

            controller.Run();
        }

        /// <summary>
        /// Runs the services controller.
        /// </summary>
        /// <param name="dte2">The dte2.</param>
        public static void RunServicesController(DTE2 dte2 = null)
        {
            TraceService.WriteLine("NinjaController::RunServicesController");

            ServicesController controller = ResolveController<ServicesController>(dte2);

            controller.Run();
        }

        /// <summary>
        /// Runs the converters controller.
        /// </summary>
        /// <param name="dte2">The dte2.</param>
        public static void RunConvertersController(DTE2 dte2 = null)
        {
            TraceService.WriteLine("NinjaController::RunConvertersController");

            ConvertersController controller = ResolveController<ConvertersController>(dte2);

            controller.Run();
        }

        /// <summary>
        /// Shows the options.
        /// </summary>
        /// <param name="dte2">The dte2.</param>
        public static void ShowOptions(DTE2 dte2 = null)
        {
            TraceService.WriteLine("NinjaController::ShowOptions");

            ApplicationController controller = ResolveController<ApplicationController>(dte2);

            controller.ShowOptions();
        }

        /// <summary>
        /// Shows the about box.
        /// </summary>
        /// <param name="dte2">The dte2.</param>
        public static void ShowAboutBox(DTE2 dte2 = null)
        {
            TraceService.WriteLine("NinjaController::ShowAboutBox");

            ApplicationController controller = ResolveController<ApplicationController>(dte2);

            controller.ShowAboutBox();
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <param name="dte2">The dte2.</param>
        /// <returns>A list of projects.</returns>
        public static IEnumerable<Project> GetProjects(DTE2 dte2 = null)
        {
            TraceService.WriteLine("NinjaController::GetProjects");

            ApplicationController controller = ResolveController<ApplicationController>(dte2);

            return controller.GetProjects();
        }

        /// <summary>
        /// Attempts to resolve a type using default options.
        /// </summary>
        /// <typeparam name="Type">The type of the controller</typeparam>
        /// <param name="dte2">The dte2.</param>
        /// <returns>Instance of the controller.</returns>
        internal static Type ResolveController<Type>(DTE2 dte2)
            where Type : class
        {
            TraceService.WriteLine("NinjaController::Setup");

            Initialize();

            TinyIoCContainer container = TinyIoCContainer.Current;
            
            Type type = container.Resolve<Type>();

            if (dte2 != null)
            {
                BaseController controller = type as BaseController;

                if (controller != null)
                {
                    controller.DTE2 = dte2;
                }
            }

            return type;
        }
        
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        internal static void Initialize()
        {
            TraceService.WriteLine("NinjaController::Initialize");

            if (initialized == false)
            {
                TinyIoCContainer container = TinyIoCContainer.Current;
                
                //// only auto register classes in this assembly.
                container.AutoRegister(Assembly.GetExecutingAssembly());

                TraceService.WriteLine("NinjaController::Initialize AutoRegistered IoC");

                //// register the types that aren't auto-registered by TinyIoC.
                container.Register<ITranslator<string, CodeConfig>>(new CodeConfigTranslator());
                container.Register<ITranslator<string, CodeSnippet>>(new CodeSnippetTranslator());
                container.Register<ITranslator<FileInfoBase, Plugin>>(new PluginTranslator());
                container.Register<ITranslator<DirectoryInfoBase, Plugins>>(new PluginsTranslator(new PluginTranslator()));

                container.Register<IFileSystem>(new FileSystem());

                initialized = true;
            }
        }
    }
}
