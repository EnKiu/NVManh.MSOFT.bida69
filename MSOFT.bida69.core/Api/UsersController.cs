using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MSOFT.bida69.Services;
using MSOFT.bida69.Models;
using MSOFT.bida69.core.Properties;
using MSOFT.Entities;
using System;

namespace MSOFT.bida69.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Đăng ký tài khoản mới
        /// </summary>
        /// <param name="user">Thông tin tài khoản</param>
        /// <returns></returns>
        /// CreateBy: NVMANH (20/02/2020)
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _userService.Register(user);
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new { message = Resources.ExceptionErroMsg });
                }
            }
            catch (Exception ex)
            {

                return Ok(ex);
            }


        }

        [HttpPut("logout")]
        public IActionResult LogOut()
        {
            try
            {

                var result = _userService.LogOut();
                return Ok(result);
            }
            catch (Exception ex)
            {

                return NotFound(ex);
            }


        }

        /// <summary>
        /// Xác thực thông tin người dùng
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// CreateBy: NVMANH (20/02/2020)
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Sai tên đăng nhập hoặc mật khẩu" });
            return Ok(user);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            // only allow admins to access other user records
            var currentUserId = Guid.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole(Role.Admin))
                return Forbid();

            var user = _userService.GetById(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}
