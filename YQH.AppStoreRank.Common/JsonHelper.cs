using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace YQH.AppStoreRank.Common
{
    public class JsonHelper
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize(object obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj);

            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }

        public static dynamic Deserialize<T>(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);

            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
     
    }
}
