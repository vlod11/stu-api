using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UniHub.Model.Models;

namespace UniHub.Web.Helpers.Mappers
{
    public class ServiceResultMapper : IServiceResultMapper
    {
        private readonly IMapper _mapper;

        public ServiceResultMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ContentResult ServiceResultToContentResult<T>(ServiceResult<T> serviceResult)
        {
            var contentResult = new ContentResult();
            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            //TODO: make different error exceptions depending on service result type
            if (serviceResult.IsSuccess)
            {
                contentResult.ContentType = "application/json";
                contentResult.Content = JsonConvert.SerializeObject(serviceResult.Result, serializerSettings);
                contentResult.StatusCode = (int)HttpStatusCode.OK;
            }
            else
            {
                contentResult.ContentType = "text/plain";
                contentResult.Content = serviceResult.ErrorMessage;
                contentResult.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            return contentResult;
        }
    }
}