using StackExchange.Redis;
using System;
using System.Collections.Concurrent;

namespace Fnyo.Learn.Jenkins.Toolkits.Cahce
{
    public class RedisHelper:IDisposable
    {
        private string _connectionString;
        private string _instanceName;
        private int _defaultDb;
        private ConcurrentDictionary<string, ConnectionMultiplexer> _connections;



        public RedisHelper()
        {
            _connectionString = $"{AppSettings.RedisConfig.Host}:{AppSettings.RedisConfig.Port},password={AppSettings.RedisConfig.Password}";
            _instanceName = AppSettings.RedisConfig.InstanceName;
            _defaultDb = AppSettings.RedisConfig.DefaultDb;
            _connections = new ConcurrentDictionary<string,ConnectionMultiplexer>();
        }


        private ConnectionMultiplexer GetConnect()
        {
            return _connections.GetOrAdd(_instanceName, p => ConnectionMultiplexer.Connect(_connectionString));
        }


        public IDatabase GetDatabase()
        {
            return GetConnect().GetDatabase(_defaultDb);
        }



        public IServer GetServer(string configName = null, int endPointsIndex=0)
        {
            var configOption = ConfigurationOptions.Parse(_connectionString);
            return GetConnect().GetServer(configOption.EndPoints[endPointsIndex]);
        }

        public ISubscriber GetSubscriber(string configName = null)
        {
            return GetConnect().GetSubscriber();
        }

        public void Dispose()
        {
            if (_connections != null && _connections.Count > 0)
            {
                foreach(var connection in _connections.Values)
                {
                    connection.Close();
                }
            }
        }
    }
}
