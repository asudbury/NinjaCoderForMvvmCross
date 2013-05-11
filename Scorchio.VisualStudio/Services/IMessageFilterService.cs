// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IMessageFilterService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Scorchio.VisualStudio.Services
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    ///  Defines the IMessageFilterService type.
    /// </summary>
    [ComImport, Guid("00000016-0000-0000-C000-000000000046"),
    InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMessageFilterService
    {
        /// <summary>
        /// Handles the in coming call.
        /// </summary>
        /// <param name="dwCallType">Type of the call.</param>
        /// <param name="hTaskCaller">The task caller.</param>
        /// <param name="dwTickCount">The tick count.</param>
        /// <param name="lpInterfaceInfo">The interface info.</param>
        /// <returns></returns>
        [PreserveSig]
        int HandleInComingCall(
            int dwCallType,
            IntPtr hTaskCaller,
            int dwTickCount,
            IntPtr lpInterfaceInfo);

        /// <summary>
        /// Retries the rejected call.
        /// </summary>
        /// <param name="hTaskCallee">The task callee.</param>
        /// <param name="dwTickCount">The tick count.</param>
        /// <param name="dwRejectType">Type of the reject.</param>
        /// <returns></returns>
        [PreserveSig]
        int RetryRejectedCall(
            IntPtr hTaskCallee,
            int dwTickCount,
            int dwRejectType);

        /// <summary>
        /// Messages the pending.
        /// </summary>
        /// <param name="hTaskCallee">The task callee.</param>
        /// <param name="dwTickCount">The tick count.</param>
        /// <param name="dwPendingType">Type of the pending.</param>
        /// <returns></returns>
        [PreserveSig]
        int MessagePending(
            IntPtr hTaskCallee,
            int dwTickCount,
            int dwPendingType);
    }
}