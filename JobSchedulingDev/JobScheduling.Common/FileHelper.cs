using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;

namespace JobScheduling.Common
{
    public class FileHelper
    {
        /// <summary>
        /// 创建文件夹路径
        /// </summary>
        /// <param name="rootPath"></param>
        public static bool CreateDictionary(string rootPath)
        {
            try
            {
                if (!Directory.Exists(rootPath))
                {
                    Directory.CreateDirectory(rootPath);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool FileIsExists(string filePath)
        {
            return File.Exists(filePath);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath"></param>
        public static void DeleteFile(string filePath)
        {
            if (FileIsExists(filePath))
                File.Delete(filePath);
        }

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destPath"></param>
        public static void CopyFile(string sourcePath, string destPath)
        {
            if (FileIsExists(sourcePath))
                File.Copy(sourcePath, destPath);
        }

        /// <summary>
        /// 获取文件扩展名
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetFileExtension(string filePath)
        {
            if (FileIsExists(filePath))
                return Path.GetExtension(filePath).Replace(".","");
            else
                return null;
        }
    }
}
