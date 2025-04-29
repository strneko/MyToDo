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
        //private readonly string _baseUrl = "http://localhost:5074/api/";
        public HttpRestClient(string baseUrl)
        {
            _restClient = new RestClient(new RestClientOptions
            {
                BaseUrl = new Uri(baseUrl)
            });
        }
        public APIResponse Execute(APIRequest apiRequest)
        {
            RestRequest restRequest = new RestRequest(apiRequest.Route, apiRequest.Method);
            if (apiRequest.Parameters != null)
            {
                restRequest.AddJsonBody(apiRequest.Parameters);
            }

            var result = _restClient.Execute<APIResponse>(restRequest);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                 var response = result.Data;
              
                return response;
            }
            else if(result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return new APIResponse
                {
                    StatusCode = (int)result.StatusCode,
                    Message = "账号或密码错误",
                    Result = null
                };
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
