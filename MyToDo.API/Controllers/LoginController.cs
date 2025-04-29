using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyToDo.API.APIResponses;
using MyToDo.API.DataModel;
using MyToDo.API.DTOs;

namespace MyToDo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly MyDbContext _context;
        public LoginController(MyDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult Login(AccountInfoDTO accountInfo)
        {
            var user = _context.AccountInfos.FirstOrDefault(x => x.AccountEmail == accountInfo.AccountEmail && x.AccountPassword == accountInfo.AccountPassword);
            if (user != null)
            {
                return Ok(new APIResponse
                {
                    StatusCode = 200,
                    Message = "登录成功",
                    Result = user
                });
            }
            else
            {
                return BadRequest(new APIResponse
                {
                    StatusCode = 400,
                    Message = "邮箱或密码错误",
                    Result = null
                });
            }
        }
    }
}
