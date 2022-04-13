using Microsoft.Extensions.Configuration;
using System.IO;

namespace Fnyo.Learn.Jenkins
{
    public class AppSettings
    {
        private static IConfigurationRoot _config;



        static AppSettings()
        {
            _config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        }


        public static RedisConfig RedisConfig => _config.GetSection("Redis").Get<RedisConfig>();

        public static MongodbConfig MongodbConfig => _config.GetSection("Mongodb").Get<MongodbConfig>();
    }

    public class RedisConfig
    {
        public string Host { get; set; }
        public int Port { get; set; }   
        public string InstanceName { get; set; }

        public string Password { get; set; }

        public int DefaultDb { get; set; }
    }


    public class MongodbConfig
    {
        public string Uid { get; set; }
        public string Pwd { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }

        public string DefaultDb { get; set; }
    }
}
