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
        /// <returns>An instance of Visual Studio.</returns>
        public static DTE2 Activate()
        {
            DTE2 dte2;

            try
            {
                TraceService.WriteLine("VSActivatorService::Activate " + ScorchioConstants.VisualStudio2012);
                dte2 = (DTE2)System.Runtime.InteropServices.Marshal.GetActiveObject(ScorchioConstants.VisualStudio2012);

            }
            catch (Exception)
            {
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
