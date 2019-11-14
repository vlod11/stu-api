using System;

namespace UniHub.WebApi.BusinessLogic.Helpers.Contract
{
    public interface IDateHelper
    {
        DateTime GetDateTimeNow();
        DateTime GetDateTimeUtcNow();
    }
}