// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

using System;
using System.Globalization;

namespace OmniKassa
{
    internal static class EnumHelper
    {
        public static TEnum Parse<TEnum>(int value, TEnum defaultValue)
         where TEnum : struct, IConvertible
        {
            foreach (TEnum enumValue in Enum.GetValues(typeof(TEnum)))
            {
                if (value == enumValue.ToInt32(CultureInfo.InvariantCulture))
                    return enumValue;
            }

            return defaultValue;
        }

        public static TEnum Parse<TEnum>(string value, TEnum defaultValue)
          where TEnum : struct, IConvertible
        {
            foreach (string name in Enum.GetNames(typeof(TEnum)))
            {
                if (name.Equals(value, StringComparison.OrdinalIgnoreCase))
                    return (TEnum)Enum.Parse(typeof(TEnum), name);
            }

            return defaultValue;
        }
    }
}
