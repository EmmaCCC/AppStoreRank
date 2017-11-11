using System.Web.Http;
using YQH.AppStoreRank.Data;

namespace YQH.AppStoreRank.WebAdmin.Controllers.Api
{
    public class EnumController : ApiController
    {
        /// <summary>
        /// 根据枚举名称获取枚举列表
        /// </summary>
        /// <param name="name">枚举名称不区分大小写</param>
        /// <returns></returns>
        public dynamic Get(string name)
        {
            return new { status = 0, result = EnumExtension.GetEnumList(name) };
        }

    }
}