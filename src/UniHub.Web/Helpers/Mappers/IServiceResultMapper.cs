using Microsoft.AspNetCore.Mvc;
using UniHub.Model.Models;

namespace UniHub.Web.Helpers.Mappers
{
    public interface IServiceResultMapper
    {
        ContentResult ServiceResultToContentResult<T>(ServiceResult<T> result);
    }
}