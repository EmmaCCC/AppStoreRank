using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Web;
using System.Runtime.Serialization;
using System.Xml;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Net;
using System.Text;

namespace YQH.Tourism.Common.SMS
{
    public class ChuanglanSMS
    {
        internal string sendUrl = ConfigurationManager.AppSettings["SmsUrl"].ToString();//发送地址 国内
        internal string un;      // 账号
        internal string pw;      // 密码
     

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="account">用户名</param>
        /// <param name="password">密码</param>
        public ChuanglanSMS()
        {
          
            this.un = ConfigurationManager.AppSettings["SmsId"].ToString();
            this.pw = ConfigurationManager.AppSettings["SmsPassword"].ToString();
        }


        /// <summary>
        /// 发送短信  国内
        /// </summary>
        /// <param name="dest_addr">手机号，多个号码用分号(半角)分割。请不要超过100个</param>
        /// <param name="content">HEX编码之消息内容（例如 你好--> C4E3BAC3）</param>
        /// <returns></returns>
        public bool sendMessage(string dest_addr, string msg)
        {
            string account = this.un;
            string password = this.pw;
            string mobile = dest_addr;
            string content = msg;

            string postStrTpl = "account={0}&pswd={1}&mobile={2}&msg={3}&needstatus=true&extno=";

            UTF8Encoding encoding = new UTF8Encoding();
            byte[] postData = encoding.GetBytes(string.Format(postStrTpl, account, password, mobile, content));

            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(sendUrl);
            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.ContentLength = postData.Length;

            Stream newStream = myRequest.GetRequestStream();
            // Send the data.
            newStream.Write(postData, 0, postData.Length);
            newStream.Flush();
            newStream.Close();

            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            if (myResponse.StatusCode == HttpStatusCode.OK)
            {
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                return true;
                //反序列化upfileMmsMsg.Text
                //实现自己的逻辑
            }
            else
            {
                return false;
                //访问失败
            }
        }


    }
}