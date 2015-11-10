// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MessageFilterService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Scorchio.VisualStudio.Services
{
    using Interfaces;
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    ///   Defines the StudioMessageFilter type.
    /// </summary>
    public class MessageFilterService : MarshalByRefObject, IDisposable, IMessageFilterService
    {
        /// <summary>
        /// Registers this instance.
        /// </summary>
        public static void Register()
        {
            IMessageFilterService newFilter = new MessageFilterService();
            IMessageFilterService oldFilter;
            CoRegisterMessageFilter(newFilter, out oldFilter);
        }

        /// <summary>
        /// Revokes this instance.
        /// </summary>
        public static void Revoke()
        {
            IMessageFilterService oldFilter;
            CoRegisterMessageFilter(null, out oldFilter);
        }

        /// <summary>
        /// IOleMessageFilter functions.
        /// Handle incoming thread requests.
        /// </summary>
        int IMessageFilterService.HandleInComingCall(
            int dwCallType,
            IntPtr hTaskCaller,
            int dwTickCount,
            IntPtr lpInterfaceInfo)
        {
            // Return the flag SERVERCALL_ISHANDLED.
            return 0;
        }

        /// <summary>
        /// Retries the rejected call.
        /// </summary>
        /// <param name="hTaskCallee">The task callee.</param>
        /// <param name="dwTickCount">The tick count.</param>
        /// <param name="dwRejectType">Type of the reject.</param>
        /// <returns></returns>
        int IMessageFilterService.RetryRejectedCall(
            IntPtr hTaskCallee,
            int dwTickCount,
            int dwRejectType)
        {
            if (dwRejectType == 2)
            {
                // Retry the thread call immediately if return >=0 & 
                // <100.
                return 99;
            }

            // Too busy; cancel call.
            return -1;
        }

        /// <summary>
        /// Messages the pending.
        /// </summary>
        /// <param name="hTaskCallee">The task callee.</param>
        /// <param name="dwTickCount">The tick count.</param>
        /// <param name="dwPendingType">Type of the pending.</param>
        /// <returns></returns>
        int IMessageFilterService.MessagePending(
            IntPtr hTaskCallee,
            int dwTickCount,
            int dwPendingType)
        {
            // Return the flag PENDINGMSG_WAITDEFPROCESS.
            return 2;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Revoke();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Implement the IOleMessageFilter interface.
        /// </summary>
        [DllImport("Ole32.dll")]
        private static extern int
          CoRegisterMessageFilter(
              IMessageFilterService newFilter,
              out IMessageFilterService oldFilter);
    }
}
