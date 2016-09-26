//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace Web.App_Start
//{
//    ///<summary>
//    /// Redis
//    /// </summary>
//    public static class RedisConfig
//    {
//         public static void Init()
//        {
//            RedisManager.GetInstance.Client = new RedisClient();
//            //todo 读取配置连接Redis
//            RedisManager.GetInstance.Client.Connect("127.0.0.1", 6379);
//        }
//    }
//}