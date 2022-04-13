using Fnyo.Learn.Jenkins.Toolkits.Cahce;
using Fnyo.Learn.Jenkins.Toolkits.Email;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace Fnyo.Learn.Jenkins.Controllers
{
    [ApiController]
    [Route("mail")]
    public class MailController:ControllerBase
    {
        private readonly MailService _mailService;
        private readonly IDatabase _redis;
        public MailController(MailService mailService,
            RedisHelper redisHelper)
        {
            _mailService = mailService;
            _redis = redisHelper.GetDatabase();
        }

        [HttpGet("test")]
        public async Task<bool> Test()
        {
            await _mailService.SendEmailAsync(new MailInfo()
            {
                Subject = "测试主题",
                From = "825666044@qq.com",
                FromName = "方耀",
                Receivers = new string[] { "iruingfang@magicargo.com" },
                Content = $"您的验证码是8922"
            });
            return true;
        }

        [HttpGet("redis/set-value")]
        public async Task<bool> RedisSetValue()
        {
            return await _redis.StringSetAsync("hello", "world",TimeSpan.FromSeconds(120));
        }
        [HttpGet("redis/get-value")]
        public async Task<string> RedisGetValue()
        {
            var redisValue= await _redis.StringGetAsync("hello");
            return redisValue.ToString();
        }
    }
}
