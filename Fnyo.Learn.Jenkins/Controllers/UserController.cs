using AutoMapper;
using Fnyo.Learn.Jenkins.Context;
using Fnyo.Learn.Jenkins.Dto;
using Fnyo.Learn.Jenkins.Entity;
using Fnyo.Learn.Jenkins.Model;
using Fnyo.Learn.Jenkins.Toolkits.Cahce;
using Fnyo.Learn.Jenkins.Toolkits.Email;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Fnyo.Learn.Jenkins.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController:ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly TmsDbContext _context;
        private readonly MailService _mailService;
        private readonly IDatabase _redis;
        private readonly IMapper _mapper;
        public UserController(ILogger<UserController> logger,
            TmsDbContext context,
            MailService mailService,
            RedisHelper redisHelper,
            IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mailService = mailService;
            _redis = redisHelper.GetDatabase();
            _mapper = mapper;

        }


        [HttpPost("register/get-validate-code")]
        public async Task<ApiResult> GetValidateCode(User user)
        {
            ApiResult result = new ApiResult();
            result.Success = true;
            if(string.IsNullOrEmpty(user.Email))
            {
                result.Success = false;
                result.Message = "邮箱不能为空！";
            }
            Random random = new Random();
            var validCode = $"{random.Next(0, 9)}{random.Next(0, 9)}{random.Next(0, 9)}{random.Next(0, 9)}";
            var redisResult = await _redis.StringSetAsync(user.Email, validCode,TimeSpan.FromMinutes(5));
            if (redisResult)
            {
                await _mailService.SendEmailAsync(new MailInfo()
                {
                    Subject = "方周wms立库管理系统",
                    From = "825666044@qq.com",
                    Receivers = new string[] { user.Email },
                    Content = $"您的验证码是:{validCode}!"
                });
            }
            return result;
        }

        [HttpPost("register/validate-code")]
        public async Task<bool> GetValidateCode(ValidateCodeDto code)
        {
            // 从redis获取code
            var redisValue = await _redis.StringGetAsync(code.Email);
            if (redisValue.ToString().Equals(code.Code)) return true;
            else return false;
        }


        [HttpPost("login")]
        public async Task<bool> IsLogin(User user)
        {
          var count = await  _context.Users.CountAsync(usr => usr.UserName.Equals(user.UserName) && usr.Password.Equals(user.Password));
          return count > 0;
        }


        [HttpPost("register")]
        public async Task<ApiResult> Register(UserDto user)
        {
            if(string.IsNullOrEmpty(user.Email))
            {
                return new ApiResult()
                {
                    Success = false,
                    Message = "请填写邮箱！"
                };
            }

            var redisValue = await _redis.StringGetAsync(user.Email);
            if (redisValue.ToString() == null || !redisValue.ToString().Equals(user.ValidateCode)) return new ApiResult()
            {
                Success = false,
                Message = "验证码错误！"
            };
          
            var count = await _context.Users.CountAsync(usr => usr.UserName.Equals(user.UserName) && usr.Password.Equals(user.Password));
            if(count > 0)
            {
                return new ApiResult()
                {
                    Success = false,
                    Message = $"用户名:{user.UserName}已注册！"
                };
            }
            await _context.Users.AddAsync(_mapper.Map<User>(user));
            await _context.SaveChangesAsync();
            return new ApiResult()
            {
                Success = true,
                Message = $"注册成功"
            };
        }


    }
}
