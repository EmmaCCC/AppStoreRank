using StackExchange.Redis;
using System;
using System.Configuration;


namespace YQH.AppStoreRank.Common
{
    /// <summary>
    /// redis相关
    /// </summary>
    public class RedisHelper : IDisposable
    {
        static string SERVER = ConfigurationManager.AppSettings["RedisServer"].ToString();
        IDatabase _db;

        private static readonly object Locker = new object();
        private static ConnectionMultiplexer _instance;
        public static ConnectionMultiplexer RedisMultiplexer {
            get
            {
                if (_instance == null)
                {
                    lock (Locker)
                    {
                        if (_instance == null || !_instance.IsConnected)
                        {
                            _instance = GetManager();
                        }
                    }
                }
                return _instance;
            }
        }


        private static ConnectionMultiplexer GetManager()
        {
            try
            {
                ConfigurationOptions options = new ConfigurationOptions();
                //options.AbortOnConnectFail = false;
                options.EndPoints.Add(SERVER);
                options.Password = ConfigurationManager.AppSettings["RedisPassword"].ToString();
                var connect = ConnectionMultiplexer.Connect(options);
                connect.PreserveAsyncOrder = false;
                return connect;
            }
            catch (Exception ex )
            {

                throw ex;
            }
        }
        public RedisHelper(int database = 4)
        {
            _db = RedisMultiplexer.GetDatabase(database);
        }


        public void SetString(string key, string value, TimeSpan? timeout = null)
        {
            _db.StringSet(key, value, timeout);

        }

        public void Remove(string key)
        {
            _db.KeyDelete(key);
        }

        public string GetString(string key)
        {
            return _db.StringGet(key);
        }
        public long SetList(string key, RedisValue[] values)
        {
            return _db.ListRightPush(key, values);
        }

        public RedisValue[] GetList(string key)
        {
            return _db.ListRange(key);
        }

        public long ListRemove(string key, RedisValue value)
        {
            return _db.ListRemove(key, value);
        }

        public bool IsKey(string key)
        {
            return _db.KeyExists(key);
        }

        public void Dispose()
        {
            RedisMultiplexer.Close();
            RedisMultiplexer.Dispose();
            GC.Collect();
        }

        public void Close()
        {
            RedisMultiplexer.Close();
        }
    }
}