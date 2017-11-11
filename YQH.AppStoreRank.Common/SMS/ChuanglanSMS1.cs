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
using System.Text;

namespace YQH.Tourism.Common.SMS
{
    //public class ChuanglanSMS
    //{
    //    internal string sendUrl = ConfigurationManager.AppSettings["SmsUrl"].ToString();//发送地址 国内
    //    internal string internationalServerUrl = "http://222.73.117.140:8044/mt";  //发送地址  国际
    //    internal string queryServerUrl = "http://222.73.117.138:7891/bi";//获取余额地址

    //    internal int dc = 15;   // 消息编码，默认为中文
    //    internal int rd = 1;    // 是否需要状态报告
    //    internal int rf = 2;    // 控制返回格式
    //    internal int tf = 0;    // 短信内容的传输编码

    //    internal string un;      // 账号
    //    internal string pw;      // 密码
    //    internal string prefix = "【律博园】";  // 短信内容前缀

    //    internal WebUtils webUtils;


    //    /// <summary>
    //    /// 构造函数
    //    /// </summary>
    //    /// <param name="account">用户名</param>
    //    /// <param name="password">密码</param>
    //    public ChuanglanSMS()
    //    {
    //        this.webUtils = new WebUtils();
    //        this.un = ConfigurationManager.AppSettings["SmsId"].ToString();
    //        this.pw = ConfigurationManager.AppSettings["SmsPassword"].ToString();
    //    }

    //    /// <summary>
    //    /// 设置用户名
    //    /// </summary>
    //    /// <param name="account">用户名</param>
    //    public void setAccount(string account)
    //    {
    //        this.un = account;
    //    }

    //    /// <summary>
    //    /// 设置密码
    //    /// </summary>
    //    /// <param name="password">密码</param>
    //    public void setPassword(string password)
    //    {
    //        this.pw = password;
    //    }

    //    /// <summary>
    //    /// 发送短信  国内
    //    /// </summary>
    //    /// <param name="dest_addr">手机号，多个号码用分号(半角)分割。请不要超过100个</param>
    //    /// <param name="content">HEX编码之消息内容（例如 你好--> C4E3BAC3）</param>
    //    /// <returns></returns>
    //    public bool sendMessage(string dest_addr, string content)
    //    {
    //        SmsDictionary txtParams = new SmsDictionary();
    //        txtParams.Add("un", this.un);
    //        txtParams.Add("pw", this.pw);
    //        txtParams.Add("da", dest_addr);
    //        txtParams.Add("sm", StringToHexString(prefix+content, Encoding.Default));
    //        txtParams.Add("dc", this.dc);
    //        string result = this.webUtils.DoGet(this.sendUrl, txtParams);
    //        //成功发送，返回的值包含id字符串
    //        if (result.Contains("id"))
    //            return true;
    //        else
    //            return false;
    //    }


    //    /// <summary>
    //    ///  发送短信  国际
    //    /// </summary>
    //    /// <param name="dest_addr">手机号，多个号码用分号(半角)分割。请不要超过100个</param>
    //    /// <param name="content">HEX编码之消息内容（例如 你好--> C4E3BAC3）</param>
    //    /// <returns></returns>
    //    public string sendInternationalMessage(string dest_addr, string content)
    //    {
    //        SmsDictionary txtParams = new SmsDictionary();
    //        txtParams.Add("un", this.un);
    //        txtParams.Add("pw", this.pw);
    //        txtParams.Add("da", dest_addr);
    //        txtParams.Add("sm", StringToHexString(content, Encoding.Default));
    //        txtParams.Add("dc", this.dc);
    //        return this.webUtils.DoGet(this.internationalServerUrl, txtParams);
    //    }

    //    /// <summary>
    //    /// 获取余额
    //    /// </summary>
    //    /// <returns></returns>
    //    public string queryBalance()
    //    {
    //        SmsDictionary txtParams = new SmsDictionary();
    //        txtParams.Add("un", this.un);
    //        txtParams.Add("pw", this.pw);
    //        return this.webUtils.DoGet(this.queryServerUrl, txtParams);
    //    }


    //    /// <字符转16进制>
    //    /// Text to Hex
    //    /// </summary>
    //    /// <param name="s"></param>
    //    /// <param name="encode"></param>
    //    /// <returns></returns>
    //    public string StringToHexString(string s, Encoding encode)
    //    {
    //        byte[] b = encode.GetBytes(s);//按照指定编码将string编程字节数组
    //        string result = string.Empty;
    //        for (int i = 0; i < b.Length; i++)//逐字节变为16进制字符
    //        {
    //            result += Convert.ToString(b[i], 16);
    //        }
    //        return result;
    //    }
    //}
}