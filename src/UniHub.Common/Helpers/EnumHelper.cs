using System;
using System.Collections.Generic;
using UniHub.Common.Enums;
using UniHub.Common.Helpers.Contract;

namespace UniHub.Common.Helpers
{
    public class EnumHelper : IEnumHelper
    {
        public Array GetEnumValues<T>(Type enumType)
            where T : Enum
        {
            return Enum.GetValues(enumType);
        }
    }
}