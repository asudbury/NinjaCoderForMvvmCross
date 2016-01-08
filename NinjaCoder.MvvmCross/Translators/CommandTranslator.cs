// --------------------------------- -----------------------------------------------------------------------------------
// <summary>
//    Defines the CommandTranslator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Translators
{
    using System.Data;

    using Scorchio.Infrastructure.Extensions;
    using Scorchio.Infrastructure.Translators;
    using System.Xml.Linq;
    using Scorchio.VisualStudio.Entities;

    /// <summary>
    /// Defines the CommandTranslator type.
    /// </summary>
    public class CommandTranslator : ITranslator<XElement, StudioCommand>
    {
        /// <summary>
        /// Translates the specified from.
        /// </summary>
        /// <param name="from">From.</param>
        /// <returns></returns>
        public StudioCommand Translate(XElement @from)
        {
            return new StudioCommand
            {
                PlatForm = @from.GetSafeAttributeStringValue("Platform"),
                CommandType = @from.GetSafeAttributeStringValue("Type"),
                Name = @from.GetSafeAttributeStringValue("Name")
            };
        }
    }
}
