 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.WPF.HttpClient
{
    /// <summary>
    /// 接收到的API响应模型
    /// </summary>
    internal class APIResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
    }
}
