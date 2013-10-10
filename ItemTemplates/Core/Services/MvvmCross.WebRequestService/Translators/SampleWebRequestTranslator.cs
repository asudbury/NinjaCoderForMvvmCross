// -----------------------------------------------------------------------
// <summary>
//   Defines the SampleWebRequestTranslator type.
// </summary>
// -----------------------------------------------------------------------
namespace MvvmCross.WebRequestService.Translators
{
    using System.Collections.Generic;

    using MvvmCross.WebRequestService.Entities;

    /// <summary>
    /// Defines the SampleWebRequestTranslator type.
    /// </summary>
    public class SampleWebRequestTranslator : ISampleWebRequestTranslator
    {
        /// <summary>
        /// Translates the specified from.
        /// </summary>
        /// <param name="from">From.</param>
        /// <returns>A WebRequestSampleData</returns>
        public IEnumerable<WebRequestSampleData> Translate(string from)
        {
            //// TODO : we need construct the entities
            List<WebRequestSampleData> data = new List<WebRequestSampleData>();

            return data;
        }
    }
}
