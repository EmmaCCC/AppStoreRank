using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace YQH.AppStoreRank.Common
{
    public class NpoiHelper
    {
        public static byte[] ToExcel<T>(List<T> data, params string[] colNames)
        {

            if (data == null)
            {
                data = new List<T>();
            }


            HSSFWorkbook hssfworkbook = new HSSFWorkbook();

            ISheet sheet1 = hssfworkbook.CreateSheet("Sheet1");

            IRow rowHeader = sheet1.CreateRow(0);
            int i = 0;

            foreach (var col in colNames)
            {
                rowHeader.CreateCell(i++).SetCellValue(col);
            }

            Type t = data.Count == 0 ? typeof(string) : data[0].GetType();


            System.Reflection.PropertyInfo[] properties = t.GetProperties();

            int j = 0, k = 0;

            foreach (var d in data)
            {
                IRow excelRow = sheet1.CreateRow(++j);


                foreach (var pro in properties)
                {
                    var ptyInfo = t.GetProperty(pro.Name);

                    excelRow.CreateCell(k++).SetCellValue(Convert.ToString(ptyInfo.GetValue(d)));

                }
                k = 0;
            }

            using (MemoryStream ms = new MemoryStream())
            {
                hssfworkbook.Write(ms);
                return ms.GetBuffer();

            }

        }


        public static List<T> FromExcel<T>(Stream sr) where T : class, new()
        {
            try
            {

                var workbook = new XSSFWorkbook(sr);
                return FromExcel<T>(workbook);
            }
            catch (Exception ex)
            {
                Common.LogHelper.WriteLog(typeof(NpoiHelper), ex);
                throw;
            }

        }

        public static List<T> FromExcel<T>(string filePath) where T : class, new()
        {
            try
            {
                if (!System.IO.File.Exists(filePath))
                {
                    throw new Exception("文件未找到");
                }
                var workbook = new XSSFWorkbook(filePath);
                return FromExcel<T>(workbook);
            }
            catch (Exception ex)
            {
                Common.LogHelper.WriteLog(typeof(NpoiHelper), ex);
                throw;
            }

        }


        private static List<T> FromExcel<T>(XSSFWorkbook workbook) where T : class, new()
        {
            try
            {

                var list = new List<T>();

                if (workbook != null)
                {
                    var sheet = workbook.GetSheetAt(0);//读取第一个sheet，当然也可以循环读取每个sheet  

                    if (sheet != null)
                    {
                        int rowCount = sheet.LastRowNum;//总行数  
                        if (rowCount > 0)
                        {
                            IRow firstRow = sheet.GetRow(0);//第一行  
                            int cellCount = firstRow.LastCellNum;//列数  

                            for (int j = 1; j < rowCount + 1; j++)
                            {
                                T t = new T();
                                Type entityType = t.GetType();
                                for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                                {
                                    var firstCell = firstRow.GetCell(i);
                                    var row = sheet.GetRow(j);
                                    var cell = row.GetCell(i);
                                    if (cell != null)
                                    {

                                        PropertyInfo propertyInfo = entityType.GetProperty(firstCell.StringCellValue);
                                        propertyInfo.SetValue(t, cell.ToString(), null);

                                    }
                                }
                                PropertyInfo keyInfo = entityType.GetProperty("Id");
                                if (keyInfo != null && keyInfo.PropertyType == typeof(Guid))
                                {

                                    keyInfo.SetValue(t, Guid.NewGuid(), null);
                                }
                                PropertyInfo timeInfo = entityType.GetProperty("CreateTime");
                                if (timeInfo != null)
                                {

                                    timeInfo.SetValue(t, DateTime.Now, null);
                                }
                                list.Add(t);
                            }


                        }

                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                Common.LogHelper.WriteLog(typeof(NpoiHelper), ex);
                throw;
            }

        }
    }
}
