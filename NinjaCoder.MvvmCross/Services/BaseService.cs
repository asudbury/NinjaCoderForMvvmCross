// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BaseService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using Interfaces;
    using Scorchio.VisualStudio.Services;
    using System.Collections.Generic;

    /// <summary>
    ///  Defines the BaseService type.
    /// </summary>
    public abstract class BaseService
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        protected readonly ISettingsService SettingsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService" /> class.
        /// </summary>
        protected BaseService(ISettingsService settingsService)
        {
            this.SettingsService = settingsService;
            this.Messages = new List<string>();
        }

        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        protected List<string> Messages { get; set; }

        /// <summary>
        /// Writes the debug message.
        /// </summary>
        /// <param name="message">The message.</param>
        protected void WriteDebugMessage(string message)
        {
            TraceService.WriteDebugLine(message);

            if (this.SettingsService.ExtendedLogging)
            {
                this.Messages.Add(message);
            }
        }
        
    }
}
