using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.WPF.HttpClient
{
    /// <summary>
    /// 请求模型
    /// </summary>
    internal class APIRequest
    {
        public string Route { get; set; }
        public string Method { get; set; }
        public object Parameters { get; set; }

        public string ContentType { get; set; }= "application/json";
    }
}
