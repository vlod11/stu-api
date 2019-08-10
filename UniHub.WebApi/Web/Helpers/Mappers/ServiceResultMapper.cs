using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UniHub.WebApi.ModelLayer.Models;

namespace UniHub.WebApi.Helpers.Mappers
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
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            if (serviceResult.IsSuccess)
            {
                contentResult.ContentType = "application/json";
                contentResult.Content = JsonConvert.SerializeObject(serviceResult.Result, serializerSettings);
                contentResult.StatusCode = (int)HttpStatusCode.OK;
            }
            else //TODO: make different error exeptions depending on service result type
            {
                contentResult.ContentType = "text/plain";
                contentResult.Content = serviceResult.ErrorMessage;
                contentResult.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            return contentResult;
        }
    }
}