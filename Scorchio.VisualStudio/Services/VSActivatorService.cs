// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the VSActivatorService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Scorchio.VisualStudio.Services
{
    using System;

    using EnvDTE80;

    using Scorchio.VisualStudio.Extensions;

    /// <summary>
    ///  Defines the VSActivatorService type.
    /// </summary>
    public static class VSActivatorService
    {
        /// <summary>
        /// Object Name.
        /// </summary>
        private const string ObjectName = "VisualStudio.DTE.11.0";

        /// <summary>
        /// 2nd Object Name
        /// </summary>
        private const string ObjectName2 = "VisualStudio.DTE";

        /// <summary>
        /// Activates this instance.
        /// </summary>
        /// <returns>An instance of Visual Studio.</returns>
        public static DTE2 Activate()
        {
            DTE2 dte2;

            try
            {
                TraceService.WriteLine("VSActivatorService::Activate " + ObjectName);
                dte2 = (DTE2)System.Runtime.InteropServices.Marshal.GetActiveObject(ObjectName);

            }
            catch (Exception)
            {
                TraceService.WriteLine("VSActivatorService::Activate " + ObjectName2);
                dte2 = (DTE2)System.Runtime.InteropServices.Marshal.GetActiveObject(ObjectName2);
            }

            TraceService.WriteLine("VSActivatorService::Activate Register");
            MessageFilterService.Register();

            TraceService.WriteLine("VSActivatorService::Activate Activate");
            dte2.Activate();

            return dte2;
        }
    }
}
