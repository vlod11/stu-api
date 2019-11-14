using Microsoft.AspNetCore.Mvc;
using UniHub.WebApi.Models.Models;

namespace UniHub.WebApi.Web.Helpers.Mappers
{
    public interface IServiceResultMapper
    {
        ContentResult ServiceResultToContentResult<T>(ServiceResult<T> result);
    }
}