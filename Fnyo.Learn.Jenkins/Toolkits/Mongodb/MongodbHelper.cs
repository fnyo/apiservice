using MongoDB.Driver;

namespace Fnyo.Learn.Jenkins.Toolkits.Mongodb
{
    public class MongodbHelper
    {
         // mongodb配置
         readonly string _connectionString =  @$"mongodb://{AppSettings.MongodbConfig.Uid}:{AppSettings.MongodbConfig.Pwd}@{
                AppSettings.MongodbConfig.Host}:{AppSettings.MongodbConfig.Port}";

        private MongoClient _mongoClient; 


        public MongodbHelper()
        {
             InitClient();
        }



        /// <summary>
        /// 获取mongodb数据库
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public IMongoDatabase GetDatabase(string db = "")
        {
            if(_mongoClient == null)
            {
                _mongoClient = new MongoClient(_connectionString);
            }
            if (string.IsNullOrEmpty(db))
            {
                return _mongoClient.GetDatabase(AppSettings.MongodbConfig.DefaultDb); 
            }
            return _mongoClient.GetDatabase(db);
        }


        /// <summary>
        /// 获取mongodb集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tableName"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public IMongoCollection<T> GetCollection<T> (string tableName ,string db ="")
        {
            var database = GetDatabase(db);
            return database.GetCollection<T>(tableName);
        }

        /// <summary>
        /// 初始化mongodb client
        /// </summary>
        private void InitClient()
        {         
            _mongoClient = new MongoClient(_connectionString);        
        }
    }
}
