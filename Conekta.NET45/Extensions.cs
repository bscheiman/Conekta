#region
using System;
using System.ComponentModel;
using System.Linq;
using Newtonsoft.Json;

#endregion

namespace Conekta {
    public static class Extensions {
        internal static readonly DateTime Epoch = DateTime.SpecifyKind(new DateTime(1970, 1, 1, 0, 0, 0, 0), DateTimeKind.Utc);

        public static T FromJson<T>(this string str) {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentNullException("str");

            return JsonConvert.DeserializeObject<T>(str);
        }

        public static TAttribute GetAttributeOfType<TAttribute>(this Enum enumVal) where TAttribute : Attribute {
            var type = enumVal.GetType();
            var name = Enum.GetName(type, enumVal);

            return type.GetField(name).GetCustomAttributes(false).OfType<TAttribute>().SingleOrDefault();
        }

        public static string GetDescription(this Enum enumVal) {
            try {
                return enumVal.GetAttributeOfType<DescriptionAttribute>().Description;
            } catch {
                return "";
            }
        }

        public static long ToEpoch(this DateTime dt) {
            return (long) (dt - Epoch).TotalSeconds;
        }

        public static string ToJson(this object obj, JsonSerializerSettings settings = null) {
            return settings == null ? JsonConvert.SerializeObject(obj) : JsonConvert.SerializeObject(obj, settings);
        }
    }
}