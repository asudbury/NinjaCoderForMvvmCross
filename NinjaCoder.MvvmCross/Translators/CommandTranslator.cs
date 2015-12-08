// --------------------------------- -----------------------------------------------------------------------------------
// <summary>
//    Defines the CommandTranslator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Translators
{
    using NinjaCoder.MvvmCross.Entities;
    using Scorchio.Infrastructure.Extensions;
    using Scorchio.Infrastructure.Translators;
    using System.Xml.Linq;

    /// <summary>
    /// Defines the CommandTranslator type.
    /// </summary>
    public class CommandTranslator : ITranslator<XElement, Command>
    {
        /// <summary>
        /// Translates the specified from.
        /// </summary>
        /// <param name="from">From.</param>
        /// <returns></returns>
        public Command Translate(XElement @from)
        {
            return new Command
            {
                PlatForm = @from.GetSafeAttributeStringValue("Platform"),
                CommandType = @from.GetSafeAttributeStringValue("Type"),
                Name = @from.GetSafeAttributeStringValue("Name")
            };
        }
    }
}
