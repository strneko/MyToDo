using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.WPF.HttpClient
{
    /// <summary>  
    /// 调用api工具类  
    /// </summary>  
    internal class HttpRestClient
    {
        private readonly RestClient _restClient;
        private readonly string _baseUrl = "http://localhost:5074/api/";
        public HttpRestClient(string route)
        {
            _restClient = new RestClient(new RestClientOptions
            {
                BaseUrl = new Uri(_baseUrl+route)
            });
        }
        public APIResponse Execute(APIRequest apiRequest)
        {
            RestRequest restRequest = new RestRequest(apiRequest.Route);
            restRequest.AddHeader("Content-Type", apiRequest.ContentType);
            if (apiRequest.Parameters != null)
            {
                restRequest.AddParameter("param", JsonConvert.SerializeObject(apiRequest.Parameters), ParameterType.RequestBody);
            }

            var result = _restClient.Execute(restRequest);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var response = JsonConvert.DeserializeObject<APIResponse>(result.Content);
                return response;
            }
            else
            {
                return new APIResponse
                {
                    StatusCode = (int)result.StatusCode,
                    Message = "请求失败,请稍候",
                    Result = null
                };
            }
        }
    }
}
