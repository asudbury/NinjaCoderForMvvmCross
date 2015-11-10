// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the VSActivatorService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.VisualStudio.Services
{
    using System;
    using EnvDTE80;
    using Extensions;

    /// <summary>
    ///  Defines the VSActivatorService type.
    /// </summary>
    public static class VSActivatorService
    {
        /// <summary>
        /// Activates this instance.
        /// </summary>
        /// <param name="objectName">Name of the object.</param>
        /// <returns>An instance of Visual Studio.</returns>
        public static DTE2 Activate(string objectName)
        {
            DTE2 dte2;

            try
            {
                TraceService.WriteLine("VSActivatorService::Activate " + objectName);
                dte2 = (DTE2)System.Runtime.InteropServices.Marshal.GetActiveObject(objectName);
            }
            catch (Exception exception)
            {
                TraceService.WriteError(exception.Message);

                TraceService.WriteLine("VSActivatorService::Activate " + ScorchioConstants.VisualStudio);
                dte2 = (DTE2)System.Runtime.InteropServices.Marshal.GetActiveObject(ScorchioConstants.VisualStudio);
            }

            TraceService.WriteLine("VSActivatorService::Activate Register");
            MessageFilterService.Register();

            TraceService.WriteLine("VSActivatorService::Activate Activate");
            dte2.Activate();

            return dte2;
        }
    }
}
