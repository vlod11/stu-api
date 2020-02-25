using System;

namespace UniHub.Common.Helpers.Contract
{
    public interface IEnumHelper
    {
        Array GetEnumValues<T>(Type enumType)
            where T : Enum;
    }
}