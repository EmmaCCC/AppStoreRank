using System.Collections.Generic;
using System.IO;

namespace YQH.AppStoreRank.Common
{
    public static class File
    {
        /// <summary>
        /// 获取指定目录下所有文件信息
        /// </summary>
        /// <param name="strDirectory"></param>
        /// <returns></returns>
        public static List<FileInfo> GetAllFilesInDirectory(string strDirectory)
        {
            List<FileInfo> listFiles = new List<FileInfo>(); //保存所有的文件信息  
            DirectoryInfo directory = new DirectoryInfo(strDirectory);
            DirectoryInfo[] directoryArray = directory.GetDirectories();
            FileInfo[] fileInfoArray = directory.GetFiles();
            if (fileInfoArray.Length > 0) listFiles.AddRange(fileInfoArray);
            foreach (DirectoryInfo _directoryInfo in directoryArray)
            {
                DirectoryInfo directoryA = new DirectoryInfo(_directoryInfo.FullName);
                DirectoryInfo[] directoryArrayA = directoryA.GetDirectories();
                FileInfo[] fileInfoArrayA = directoryA.GetFiles();
                if (fileInfoArrayA.Length > 0) listFiles.AddRange(fileInfoArrayA);
                GetAllFilesInDirectory(_directoryInfo.FullName);//递归遍历  
            }
            return listFiles;
        }

        /// <summary>
        /// 删除一个文件
        /// </summary>
        /// <param name="url">文件url</param>
        /// <param name="isVirtual">是否虚拟路径</param>
        public static bool DeleteFile(string url, bool isVirtual)
        {
            try
            {
                if (isVirtual)
                {
                    url = System.Web.HttpContext.Current.Server.MapPath(url);
                }
                if (System.IO.File.Exists(url))
                    System.IO.File.Delete(url);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="FolderPathName"></param>
        public static void CreateFolder(string FolderPathName, bool isVirtual)
        {
            if (FolderPathName.Trim().Length > 0)
            {
                try
                {
                    string CreatePath = "";
                    if (isVirtual)
                        CreatePath = System.Web.HttpContext.Current.Server.MapPath(FolderPathName).ToString();
                    else
                        CreatePath = FolderPathName;


                    if (!Directory.Exists(CreatePath))
                    {
                        Directory.CreateDirectory(CreatePath);                        
                    }
                }
                catch
                {
                    throw;
                }
            }

        }

        /// <summary>
        /// 创建文件并写入文件
        /// </summary>
        /// <param name="FilePathName"></param>
        public static void CreateFile(string filePathName,string content,bool isVirtual)
        {
            try
            {
                //创建文件夹 
                string[] strPath = filePathName.Split('/');
                CreateFolder(filePathName.Replace("/" + strPath[strPath.Length - 1].ToString(), ""),false); //创建文件夹 

                string fName = "";
                if (isVirtual)
                    fName = System.Web.HttpContext.Current.Server.MapPath(filePathName).ToString();
                else
                    fName = filePathName;
                
                FileInfo CreateFile = new FileInfo(fName); //创建文件 
                if (!CreateFile.Exists)
                {
                    FileStream FS = CreateFile.Create();
                    FS.Close();
                    WriteFile(fName, content);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="FilePathName"></param>
        /// <param name="content"></param>
        public static void WriteFile(string FilePathName, string content)
        {
            StreamWriter sw = new StreamWriter(FilePathName, false, System.Text.Encoding.GetEncoding("utf-8"));
            sw.WriteLine(content);
            sw.Close();
            sw.Dispose(); 
        }


    }
}
