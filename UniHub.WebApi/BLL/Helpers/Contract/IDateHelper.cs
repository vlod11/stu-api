using System;

namespace UniHub.WebApi.BLL.Helpers.Contract
{
    public interface IDateHelper
    {
        DateTime GetDateTimeNow();
        DateTime GetDateTimeUtcNow();
    }
}