using System;
using System.Collections.Generic;
using System.Text;

namespace YQH.Tourism.Common.SMS
{
    /// <summary>
    /// 常量（全局）
    /// </summary>
    public sealed class Constants
    {

        /// <summary>
        /// 接收上行和状态报告返回XML格式
        /// </summary>
        public const string API_RECEIVE_SMS_AS_XML = "/mt";

        public const string CHARSET_UTF8 = "utf-8";

        public const string DATE_TIME_FORMAT = "yyyy-MM-dd HH:mm:ss";

        public const string CONTENT_ENCODING_GZIP = "gzip";
    }
}
