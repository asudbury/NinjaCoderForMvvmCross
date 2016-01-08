// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the NinjaController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Controllers
{
    using Entities;
    using EnvDTE;
    using EnvDTE80;
    using MahApps.Metro;
    using Scorchio.Infrastructure.Entities;
    using Scorchio.Infrastructure.Translators;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Abstractions;
    using System.Reflection;
    using System.Xml.Linq;
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
        /// Startups this instance.
        /// </summary>
        public static void Startup()
        {
            TraceService.WriteLine("NinjaController::Startup");
        }

        /// <summary>
        /// Sets the working directory.
        /// </summary>
        /// <param name="location">The location.</param>
        public static void SetWorkingDirectory(string location)
        {
            TraceService.WriteLine("NinjaController::SetWorkingDirectory " + location);

            ResolveController<ApplicationController>(null)
                .SetWorkingDirectory(location);
        }

        /// <summary>
        /// Runs the projects controller.
        /// </summary>
        /// <param name="dte2">The dte2.</param>
        public static void RunProjectsController(DTE2 dte2 = null)
        {
            TraceService.WriteLine("NinjaController::RunProjectsController");

            ResolveController<ProjectsController>(dte2)
                .Run();
        }

        /// <summary>
        /// Runs the view model views controller.
        /// </summary>
        /// <param name="dte2">The dte2.</param>
        public static void RunViewModelViewsController(DTE2 dte2 = null)
        {
            TraceService.WriteLine("NinjaController::RunViewModelViewsController");

            ResolveController<ViewModelViewsController>(dte2)
                .Run();
        }

        /// <summary>
        /// Runs the plugins controller.
        /// </summary>
        /// <param name="dte2">The dte2.</param>
        public static void RunPluginsController(DTE2 dte2 = null)
        {
            TraceService.WriteLine("NinjaController::RunPluginsController");

            ResolveController<PluginsController>(dte2)
                .Run();
        }

        /// <summary>
        /// Runs the nuget packages controller.
        /// </summary>
        /// <param name="dte2">The dte2.</param>
        public static void RunNugetPackagesController(DTE2 dte2 = null)
        {
            TraceService.WriteLine("NinjaController::RunNugetPackagesController");

            ResolveController<NugetPackagesController>(dte2)
                .Run();
        }
        
        /// <summary>
        /// Runs the dependency services controller.
        /// </summary>
        /// <param name="dte2">The dte2.</param>
        public static void RunDependencyServicesController(DTE2 dte2 = null)
        {
            TraceService.WriteLine("NinjaController::RunDependencyServicesController");

            ResolveController<DependencyServicesController>(dte2)
                .Run();
        }

        /// <summary>
        /// Runs the customer renderer controller.
        /// </summary>
        /// <param name="dte2">The dte2.</param>
        public static void RunCustomerRendererController(DTE2 dte2 = null)
        {
            TraceService.WriteLine("NinjaController::RunCustomerRendererController");

            ResolveController<CustomRendererController>(dte2)
                .Run();
        }

        /// <summary>
        /// Shows the options.
        /// </summary>
        /// <param name="dte2">The dte2.</param>
        public static void ShowOptions(DTE2 dte2 = null)
        {
            TraceService.WriteLine("NinjaController::ShowOptions");

            ResolveController<ApplicationController>(dte2)
                .ShowOptions();
        }

        /// <summary>
        /// Shows the about box.
        /// </summary>
        /// <param name="dte2">The dte2.</param>
        public static void ShowAboutBox(DTE2 dte2 = null)
        {
            TraceService.WriteLine("NinjaController::ShowAboutBox");

            ResolveController<ApplicationController>(dte2)
                .ShowAboutBox();
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <param name="dte2">The dte2.</param>
        /// <returns>A list of projects.</returns>
        public static IEnumerable<Project> GetProjects(DTE2 dte2 = null)
        {
            TraceService.WriteLine("NinjaController::GetProjects");

            return ResolveController<ApplicationController>(dte2)
                .GetProjects();
        }
        
        /// <summary>
        /// Views the log file.
        /// </summary>
        public static void ViewLogFile()
        {
            TraceService.WriteLine("NinjaController::ViewLogFile");

            ResolveController<ApplicationController>(null)
                .ViewLogFile();
        }

        /// <summary>
        /// Clears the log file.
        /// </summary>
        public static void ClearLogFile()
        {
            TraceService.WriteLine("NinjaController::ClearLogFile");

            ResolveController<ApplicationController>(null)
                .ClearLogFile();
        }

        /// <summary>
        /// Views the error log file.
        /// </summary>
        public static void ViewErrorLogFile()
        {
            TraceService.WriteLine("NinjaController::ViewErrorLogFile");

            ResolveController<ApplicationController>(null)
                .ViewErrorLogFile();
        }

        public static void ClearErrorLogFile()
        {
            TraceService.WriteLine("NinjaController::ClearErrorLogFile");

            ResolveController<ApplicationController>(null)
                .ClearErrorLogFile();
        }

        /// <summary>
        /// MVVMs the cross home page.
        /// </summary>
        public static void MvvmCrossHomePage()
        {
            TraceService.WriteLine("NinjaController::MvvmCrossHomePage");

            ResolveController<ApplicationController>(null)
                .MvvmCrossHomePage();
        }

        /// <summary>
        /// Xamarins the forms home page.
        /// </summary>
        public static void XamarinFormsHomePage()
        {
            TraceService.WriteLine("NinjaController::XamarinFormsHomePage");

            ResolveController<ApplicationController>(null)
                .XamarinFormsHomePage();
        }

        /// <summary>
        /// Attempts to resolve a type using default options.
        /// </summary>
        /// <typeparam name="T">The type of the controller</typeparam>
        /// <param name="dte2">The dte2.</param>
        /// <returns>Instance of the controller.</returns>
        internal static T ResolveController<T>(DTE2 dte2)
            where T : class
        {
            TraceService.WriteLine("NinjaController::ResolveController");

            Initialize();

            ResolverService resolverService = new ResolverService();

            T t = resolverService.Resolve<T>();

            if (dte2 != null)
            {
                BaseController controller = t as BaseController;

                if (controller != null)
                {
                    TraceService.WriteLine("**Setting DTE2**");
                    controller.SetDte2(dte2);
                }
            }

            return t;
        }
        
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        internal static void Initialize()
        {
            TraceService.WriteLine("NinjaController::Initialize");

            try
            {
                if (initialized == false)
                {
                    TraceService.WriteLine("NinjaController::Initialize AutoRegister");

                    //// only auto register classes in this assembly.
                    TinyIoCContainer container = TinyIoCContainer.Current;

                    string location = Assembly.GetExecutingAssembly().Location;

                    TraceService.WriteLine("NinjaController::Initialize Assembly Location=" + location);
                    
                    PathBase pathBase = new PathWrapper();
                    string directory = pathBase.GetDirectoryName(location);

                    TraceService.WriteLine("NinjaController::Initialize NinjaCoder.MvvmCross.dll");

                    string path = directory + @"\NinjaCoder.MvvmCross.dll";

                    if (File.Exists(path))
                    {
                        //// NinjaCoder for MvvmCross interfaces.
                        container.AutoRegister(Assembly.LoadFrom(path));
                    }
                    else
                    {
                        TraceService.WriteError(path + " does not exist");
                    }

                    //// we only want one instance of the VisualStudio class.
                    container.Register<VisualStudioService>().AsSingleton();

                    //// register the types that aren't auto-registered by TinyIoC.
                    container.Register<ITranslator<string, CodeConfig>>(new CodeConfigTranslator());
                    container.Register<ITranslator<string, CodeSnippet>>(new CodeSnippetTranslator());
                    container.Register<ITranslator<XElement, Plugin>>(new PluginTranslator());
                    container.Register<ITranslator<string, Plugins>>(new PluginsTranslator());
                    container.Register<ITranslator<string, CommandsList>>(new CommandsListTranslator());
                    container.Register<ITranslator<string, CustomRenderers>>(new CustomRenderersTranslator());

                    //// file io abstraction
                    container.Register<IFileSystem>(new FileSystem());

                    TraceService.WriteLine("NinjaController::Initialize Scorchio.Infrastructure.dll");

                    path = directory + @"\Scorchio.Infrastructure.dll";

                    if (File.Exists(path))
                    {
                        //// Scorchio.Infrastructure interfaces.
                        container.AutoRegister(Assembly.LoadFrom(path));
                    }
                    else
                    {
                        TraceService.WriteError(path + " does not exist");
                    }

                    TraceService.WriteLine("NinjaController::Initialize AccentTranslator");

                    //// register the types that aren't auto-registered by TinyIoC.
                    container.Register<ITranslator<IList<Accent>, IEnumerable<AccentColor>>>(new AccentTranslator());

                    TraceService.WriteLine("NinjaController::Initialize Scorchio.VisualStudio.dll");

                    path = directory + @"\Scorchio.VisualStudio.dll";

                    if (File.Exists(path))
                    {
                        //// Scorchio.VisualStudio interfaces.
                        container.AutoRegister(Assembly.LoadFrom(path));
                    }
                    else
                    {
                        TraceService.WriteError(path + " does not exist");
                    }

                    initialized = true;
                }

                TraceService.WriteLine("NinjaController::Initialize end");
            }
            catch (ReflectionTypeLoadException exception)
            {
                TraceService.WriteError("ReflectionTypeLoadException=" + exception);

                foreach (Exception ex in exception.LoaderExceptions)
                {
                    TraceService.WriteError("ReflectionTypeLoadException=" + ex);
                }
            }    

            catch (Exception exception)
            {
                TraceService.WriteError("Exception=" + exception);

                if (exception.InnerException != null)
                {
                    ReflectionTypeLoadException loadException = exception.InnerException as ReflectionTypeLoadException;

                    if (loadException != null)
                    {
                        foreach (Exception ex in loadException.LoaderExceptions)
                        {
                            TraceService.WriteError("ReflectionTypeLoadException=" + ex);
                        }
                    }
                }
            }
        }
    }
}
