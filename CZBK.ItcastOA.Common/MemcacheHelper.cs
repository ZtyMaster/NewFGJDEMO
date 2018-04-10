using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.ItcastOA.Common
{
    public class MemcacheHelper
    {
        private static readonly MemcachedClient mc = null;
        static MemcacheHelper()
        {
            string[] serverlist = { "122.114.8.40:11211", "60.18.162.202:11211" };//一定要将地址写到Web.config文件中。
            //初始化池
            SockIOPool pool = SockIOPool.GetInstance();
            pool.SetServers(serverlist);

            pool.InitConnections = 3;
            pool.MinConnections = 3;
            pool.MaxConnections = 5;

            pool.SocketConnectTimeout = 1000;
            pool.SocketTimeout = 3000;

            pool.MaintenanceSleep = 30;
            pool.Failover = true;

            pool.Nagle = false;
            pool.Initialize();

            // 获得客户端实例
            mc = new MemcachedClient();
            mc.EnableCompression = false;
           
        }
        /// <summary>
        /// 向Memcache中写数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set(string key, object value)
        {
            mc.Set(key, value);
        }
        /// <summary>
        ///  向Memcache中写数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="time">过期时间</param>
        public static void Set(string key, object value, DateTime time)
        {
            mc.Set(key, value, time);
        }
        /// <summary>
        /// 获取Memcahce中的数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get(string key)
        {
            return mc.Get(key);
        }
        /// <summary>
        /// 删除Memcache中的数据
        /// </summary>
        /// <param name="key"></param>
        public static bool Delete(string key)
        {
            if (mc.KeyExists(key))
            {
                
                return mc.Delete(key);
            }
            return false;
        }

        public static int SetClickMCH(string Strid)
        {
            #region 创建 点击更新 sessionId
            int ret = 0;
            object obj = Get(Strid);//根据key从Memcache中获取用户的信息
            if (obj == null)
            {
                //使用Memcache代替Session解决数据在不同Web服务器之间共享的问题。
                Set(Strid, 0, DateTime.Now.AddHours(999));
                ret = 0;
            }
            else
            {
                ret = Convert.ToInt32(obj) + 1;
                if (ret > 99999)
                {
                    ret = 0;
                }
                Set(Strid, ret);               
            }
            return ret;
            #endregion
        }
    }
}

    
   
