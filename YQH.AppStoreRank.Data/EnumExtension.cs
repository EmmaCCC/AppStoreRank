using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace YQH.AppStoreRank.Data
{
    public static class EnumExtension
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }

        private static IEnumerable<EnumItem> GetEnumList(Type tEnum)
        {
            return Enum.GetValues(tEnum).OfType<Enum>()
                .Select(x => new EnumItem
                {
                    Key = Convert.ToInt32(x),
                    Value = x.ToString(),
                    Description = x.GetDescription()
                }).OrderBy(a => a.Key); 
        }

        public static IEnumerable<EnumItem> GetEnumList(string enumName)
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                var type = assembly.DefinedTypes.First(t => string.Equals(t.Name, enumName, StringComparison.CurrentCultureIgnoreCase));
                return GetEnumList(type);
            }
            catch (Exception)
            {
                return Enumerable.Empty<EnumItem>();
            }
        }

        public class EnumItem
        {
            public int Key { get; set; }
            public string Value { get; set; }
            public string Description { get; set; }
        }
    }
}
