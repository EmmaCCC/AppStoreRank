using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace YQH.AppStoreRank.Web.Controllers.Api
{
    [ApiAuth]
    public class FileUploadController : ApiController
    {
        //上传文件路径
        string path = "/Upload/Images/";

        //是否上传缩略图
        bool isThumb = false;

        //图片宽度
        int imgWidth = 0;

        //图片高度
        int imgHeight = 0;

        //缩略图前缀
        string thumb = "thumb_";

        /// <summary>
        /// 是否删除原图
        /// </summary>
        bool isDeletePic = false;

        /// <summary>
        /// 文件名称
        /// </summary>
        string randFileName = "";


        public dynamic Post()
        {
            try
            {
                ////给Request的值赋值
                //SetRequestValue();

                //文件完整相对路径名称
                string fileFullName = "";

                //文件完整绝对路径名称
                string absoluteFileFullName = "";

                string strPath = ConfigurationManager.AppSettings["UploadPath"] + path;
                HttpFileCollection files = HttpContext.Current.Request.Files;
                List<UploadImg> imgList = new List<UploadImg>();

                for (int i = 0; i < files.Count; i++)
                {
                    fileFullName = "";
                    absoluteFileFullName = "";
                    if (randFileName == "")
                    {
                        randFileName = Guid.NewGuid().ToString().Replace("-", "");
                    }

                    string extFileName = System.IO.Path.GetExtension(files[i].FileName);
                    fileFullName = path + randFileName + extFileName;
                    absoluteFileFullName = strPath + randFileName + extFileName;
                    files[i].SaveAs(absoluteFileFullName);

                    UploadImg img = new UploadImg();
                    img.Name = fileFullName;
                    img.DomainPre = HttpContext.Current.Request.UserHostName;
                    imgList.Add(img);
                }
                return new { errorCode = 0, result = "上传成功", list = imgList, };
            }
            catch (Exception ex)
            {
                return new { errorCode = 1, result = ex.Message };
            }
        }

        private void SetRequestValue()
        {
            path = HttpContext.Current.Request["path"].ToString();
            if (HttpContext.Current.Request["IsThumb"] != null)
            {
                isThumb = bool.Parse(HttpContext.Current.Request["IsThumb"].ToString());
            }
            if (HttpContext.Current.Request["width"] != null)
            {
                imgWidth = int.Parse(HttpContext.Current.Request["width"].ToString());
            }
            if (HttpContext.Current.Request["height"] != null)
            {
                imgHeight = int.Parse(HttpContext.Current.Request["height"].ToString());
            }

            if (HttpContext.Current.Request["IsDeletePic"] != null)
            {
                isDeletePic = bool.Parse(HttpContext.Current.Request["IsDeletePic"].ToString());
            }

            if (HttpContext.Current.Request["fileName"] != null)
            {
                randFileName = HttpContext.Current.Request["fileName"].ToString();
            }
        }
    }

    public class UploadImg
    {
        public string Name
        {
            get;
            set;
        }

        public string ThumbName
        {
            get;
            set;
        }

        public string DomainPre
        {
            get;
            set;
        }
    }
}