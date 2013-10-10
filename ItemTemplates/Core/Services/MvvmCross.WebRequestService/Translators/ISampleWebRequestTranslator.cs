// -----------------------------------------------------------------------
// <summary>
//   Defines the ISampleWebRequestTranslator type.
// </summary>
// -----------------------------------------------------------------------
namespace MvvmCross.WebRequestService.Translators
{
    using System.Collections.Generic;

    using MvvmCross.WebRequestService.Entities;

    /// <summary>
    /// Defines the ISampleWebRequestTranslator type.
    /// </summary>
    public interface ISampleWebRequestTranslator : ITranslator<string, IEnumerable<WebRequestSampleData>>
    {
    }
}
