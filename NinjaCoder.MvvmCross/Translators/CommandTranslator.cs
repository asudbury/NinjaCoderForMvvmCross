// --------------------------------- -----------------------------------------------------------------------------------
// <summary>
//    Defines the CommandTranslator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Translators
{
    using Scorchio.Infrastructure.Extensions;
    using Scorchio.Infrastructure.Translators;
    using Scorchio.VisualStudio.Entities;
    using System.Xml.Linq;

    /// <summary>
    /// Defines the CommandTranslator type.
    /// </summary>
    public class CommandTranslator : ITranslator<XElement, StudioCommand>
    {
        /// <summary>
        /// Translates the specified from.
        /// </summary>
        /// <param name="from">The translation source..</param>
        /// <returns>A StudioCommand.</returns>
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
