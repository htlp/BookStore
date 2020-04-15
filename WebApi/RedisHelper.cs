using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi
{
    public class RedisHelper
    {
        private IOptionsSnapshot<RedisOptions> options;
        private ConfigurationOptions sentinelConfig;
        private ConfigurationOptions masterConfig;
        
        public RedisHelper(IOptionsSnapshot<RedisOptions> _options,string connectionString, string instanceName, int defaultDB = 0)
        {
            options = _options;
            sentinelConfig = new ConfigurationOptions();
            //ConfigurationOptions sentinelConfig = new ConfigurationOptions();
            sentinelConfig.ServiceName = _options.Value.ServiceName;// "mymaster";
            sentinelConfig.EndPoints.Add("192.168.99.102", 26379);
            sentinelConfig.EndPoints.Add("192.168.99.102", 26380);
            sentinelConfig.EndPoints.Add("192.168.99.102", 26381);
            sentinelConfig.TieBreaker = "";
            sentinelConfig.DefaultVersion = new Version(4, 0, 11);
            sentinelConfig.CommandMap = CommandMap.Sentinel;            
            masterConfig = new ConfigurationOptions { ServiceName = _options.Value.ServiceName };
        }

        /// <summary>
        /// 获取ConnectionMultiplexer
        /// </summary>
        /// <returns></returns>
        private ConnectionMultiplexer GetMasterConnection()
        {
            ConnectionMultiplexer sentinelConnection = ConnectionMultiplexer.Connect(sentinelConfig, Console.Out);
            ConnectionMultiplexer redisMasterConnection = sentinelConnection.GetSentinelMasterConnection(masterConfig);
            return redisMasterConnection;
        }

        /// <summary>
        /// 获取数据库
        /// </summary>
        /// <param name="configName"></param>
        /// <param name="db">默认为0：优先代码的db配置，其次config中的配置</param>
        /// <returns></returns>
        public IDatabase GetDatabase()
        {
            return GetMasterConnection().GetDatabase();
        }
    }
}
