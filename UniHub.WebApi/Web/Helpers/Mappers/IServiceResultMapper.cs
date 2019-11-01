using Microsoft.AspNetCore.Mvc;
using UniHub.WebApi.ModelLayer.Models;

namespace UniHub.WebApi.Web.Helpers.Mappers
{
    public interface IServiceResultMapper
    {
        ContentResult ServiceResultToContentResult<T>(ServiceResult<T> result);
    }
}