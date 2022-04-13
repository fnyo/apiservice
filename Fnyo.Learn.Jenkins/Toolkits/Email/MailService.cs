using Fnyo.Learn.Service;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Fnyo.Learn.Jenkins.Toolkits.Email
{
    public class MailService:ISingleton
    {

        private MailHostOption _mailHostOption;
        private readonly ILogger<MailService> _logger;
        public MailService(IOptions<MailHostOption> mailHostOption,
            ILogger<MailService> logger)
        {
            _mailHostOption = mailHostOption.Value;
            _logger = logger;   
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mailInfo"></param>
        /// <returns></returns>
        public async Task<bool> SendEmailAsync(MailInfo mailInfo)
        {
            MailMessage mailMsg = new MailMessage();//实例化对象
            mailMsg.From = new MailAddress(mailInfo.From, mailInfo.FromName);//源邮件地址和发件人
            mailInfo.Receivers.ToList().ForEach(receiver => mailMsg.To.Add(new MailAddress(receiver)));
            mailMsg.Subject = mailInfo.Subject;//发送邮件的标题
           
            mailMsg.Body = mailInfo.Content;//发送邮件的内容
            //指定smtp服务地址（根据发件人邮箱指定对应SMTP服务器地址）
            SmtpClient client = new SmtpClient();//格式：smtp.126.com  smtp.164.com
            client.Host = _mailHostOption.Host;
            //要用587端口
            client.Port = _mailHostOption.Port;//端口
            //加密
            client.EnableSsl = _mailHostOption.EnableSsl;
            //通过用户名和密码验证发件人身份
            client.Credentials = new NetworkCredential(_mailHostOption.UserName, _mailHostOption.AuthCode); // 
            //发送邮件
            try
            {
               await client.SendMailAsync(mailMsg);
            }
            catch (SmtpException ex)
            {
                _logger.LogError($"邮件发送失败，异常消息为：{ex.Message}");
            }
            Console.WriteLine("邮件已发送，请注意查收！");
            //Console.ReadKey();
            return true;
        }
    }
}
