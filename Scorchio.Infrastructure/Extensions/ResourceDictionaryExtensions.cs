// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ResourcesExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Extensions
{
    using System.Windows;

    /// <summary>
    ///  Defines the ResourcesExtensions type.
    /// </summary>
    public static class ResourceDictionaryExtensions
    {
        /// <summary>
        /// Sets the language dictionary.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="resourceDictionary">The resource dictionary.</param>
        public static void SetLanguageDictionary(
            this ResourceDictionary instance,
            ResourceDictionary resourceDictionary)
        {
            instance.MergedDictionaries.Add(resourceDictionary);
        }
    }
}
