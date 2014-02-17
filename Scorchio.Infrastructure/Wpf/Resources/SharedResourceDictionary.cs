// -----------------------------------------------------------------------
// <summary>
//   Defines the SharedResourceDictionary type.
// </summary>
// -----------------------------------------------------------------------
namespace Scorchio.Infrastructure.Wpf.Resources
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    /// <summary>
    /// Defines the SharedResourceDictionary type.
    /// </summary>
    public class SharedResourceDictionary : ResourceDictionary
    {
        /// <summary>
        /// Internal cache of loaded dictionaries 
        /// </summary> 
        public static readonly Dictionary<string, ResourceDictionary> SharedDictionaries = new Dictionary<string, ResourceDictionary>();

        /// <summary>
        /// Gets or sets the uniform resource identifier (URI) to load resources from.
        /// </summary>
        /// <returns>The source location of an external resource dictionary. </returns>
        public new Uri Source
        {
            set
            {
                string key = value.OriginalString.Replace(@"..\", string.Empty);

                if (SharedDictionaries.ContainsKey(key) == false)
                {
                    base.Source = value;
                    SharedDictionaries.Add(key, this);
                }

                else
                {
                    MergedDictionaries.Add(SharedDictionaries[key]);
                }
            }
        }

    }
}
