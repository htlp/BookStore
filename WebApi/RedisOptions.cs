using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi
{
    public class RedisOptions
    {
        public string ServiceName { get; set; }

        public List<RedisEndPoint> EndPoints { get; set; }
    }

    public class RedisEndPoint
    {
        public string Host { get; set; }
        public int Port { get; set; }
    }


}
