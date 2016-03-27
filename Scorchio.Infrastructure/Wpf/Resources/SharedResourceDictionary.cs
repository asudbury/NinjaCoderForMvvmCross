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
        /// Sets the uniform resource identifier (URI) to load resources from.
        /// </summary>
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
                    this.MergedDictionaries.Add(SharedDictionaries[key]);
                }
            }
        }
    }
}
