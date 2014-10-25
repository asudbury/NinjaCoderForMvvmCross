// --------------------------------------------------------------------------------------------------------------------
// <summary>
// 		Defines the EnumExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Scorchio.Infrastructure.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    ///	Defines the EnumExtensions type.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>Teh friendly description</returns>
        public static string GetDescription(this Enum instance)
        {
            FieldInfo fieldInfo = instance.GetType().GetField(instance.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return (attributes.Length > 0) ? attributes[0].Description : instance.ToString();
        }

        /// <summary>
        /// Gets the value from description.
        /// 
        /// example :- type.GetValueFromDescription<FrameworkType>("Xamarin Forms");
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="description">The description.</param>
        /// <returns>
        /// The enum value
        /// </returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        public static T GetValueFromDescription<T>(
            this Enum instance,
            string description)
        {
            Type type = typeof(T);

            if (type.IsEnum == false)
            {
                throw new InvalidOperationException();
            }

            foreach (FieldInfo field in type.GetFields())
            {
                DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

                if (attribute != null)
                {

                    if (attribute.Description == description)
                    {
                        return (T)field.GetValue(null);
                    }
                }
                else
                {
                    if (field.Name == description)
                    {
                        return (T)field.GetValue(null);
                    }
                }
            }

            return default(T);
        }

        /// <summary>
        /// Gets the descriptions.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>
        /// a list of enum descriptions.
        /// </returns>
        public static IEnumerable<string> GetDescriptions(this Enum instance)
        {
            Type type = instance.GetType();

            string name = Enum.GetName(type, instance);

            FieldInfo field = type.GetField(name);

            object[] fds = field.GetCustomAttributes(typeof(DescriptionAttribute), true);

            return (from DescriptionAttribute fd in fds select fd.Description).ToList();
        }
    }
}
