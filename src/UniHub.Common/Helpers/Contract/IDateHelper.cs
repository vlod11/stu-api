using System;

namespace UniHub.Common.Helpers.Contract
{
    public interface IDateHelper
    {
        DateTime GetDateTimeNow();
        DateTime GetDateTimeUtcNow();
    }
}