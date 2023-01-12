using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSI_MFLD.Basis.Xml
{
    public class FileHelper
    {

        private bool _alreadyDispose;

        ~FileHelper()
        {
            this.Dispose();
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (this._alreadyDispose)
            {
                return;
            }
            this._alreadyDispose = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 获取文件的扩展名
        /// </summary>
        /// <param name="filename">完整文件名</param>
        /// <returns>返回扩展名</returns>
		public static string GetPostfixStr(string filename)
        {
            int num = filename.LastIndexOf(".");
            int length = filename.Length;
            return filename.Substring(num, length - num);
        }

        /// <summary>
        /// 判断一组文件是否都存在
        /// </summary>
        /// <param name="filePathList">文件路径List</param>
        /// <returns>文件是否全部存在</returns>
        public static bool IsFilesExist(List<string> filePathList)
        {
            bool isAllExist = true;
            foreach (string filePath in filePathList)
            {
                if (!File.Exists(filePath))
                {
                    isAllExist = false;
                }
            }
            return isAllExist;
        }

        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool IsFileExist(string filePath)
        {
            bool isAllExist = true;

            if (!File.Exists(filePath))
            {
                isAllExist = false;
            }

            return isAllExist;
        }

        /// <summary>
        /// 得某文件夹下指定后缀名的文件List
        /// </summary>
        /// <param name="directory">文件夹名称</param>
        /// <param name="pattern">后缀名模式</param>
        /// <returns></returns>
        public static List<string> GetFileListWithExtend(DirectoryInfo directory, string pattern)
        {
            List<string> pathList = new List<string>();
            string result = String.Empty;
            if (directory.Exists || pattern.Trim() != string.Empty)
            {

                foreach (FileInfo info in directory.GetFiles(pattern))
                {
                    result = info.FullName.ToString();
                    pathList.Add(result);
                }
            }
            return pathList;

        }
        /// <summary>
        /// 向指定文件写入内容
        /// </summary>
        /// <param name="path">要写入内容的文件完整路径</param>
        /// <param name="content">要写入的内容</param>
		public static void WriteFile(string path, string content)
        {
            try
            {
                object obj = new object();
                if (!System.IO.File.Exists(path))
                {
                    System.IO.FileStream fileStream = System.IO.File.Create(path);
                    fileStream.Close();
                }
                lock (obj)
                {
                    using (System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(path, false, System.Text.Encoding.Default))
                    {
                        streamWriter.WriteLine(content);
                        streamWriter.Close();
                        streamWriter.Dispose();
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 向指定文件写入内容
        /// </summary>
        /// <param name="path">要写入内容的文件完整路径</param>
        /// <param name="content">要写入的内容</param>
        /// <param name="encoding">编码格式</param>
        public static void WriteFile(string path, string content, System.Text.Encoding encoding)
        {
            try
            {
                object obj = new object();
                if (!System.IO.File.Exists(path))
                {
                    System.IO.FileStream fileStream = System.IO.File.Create(path);
                    fileStream.Close();
                }
                lock (obj)
                {
                    using (System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(path, false, encoding))
                    {
                        streamWriter.WriteLine(content);
                        streamWriter.Close();
                        streamWriter.Dispose();
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="path">要读取的文件路径</param>
        /// <returns>返回文件内容</returns>
		public static string ReadFile(string path)
        {
            string result;
            if (!System.IO.File.Exists(path))
            {
                result = "不存在相应的目录";
            }
            else
            {
                System.IO.FileStream stream = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite);
                System.IO.StreamReader streamReader = new System.IO.StreamReader(stream, System.Text.Encoding.Default);
                result = streamReader.ReadToEnd();
                streamReader.Close();
                streamReader.Dispose();
            }
            return result;
        }

        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="path">要读取的文件路径</param>
        /// <param name="encoding">编码格式</param>
        /// <returns>返回文件内容</returns>
        public static string ReadFile(string path, System.Text.Encoding encoding)
        {
            string result;
            if (!System.IO.File.Exists(path))
            {
                result = "不存在相应的目录";
            }
            else
            {
                System.IO.FileStream stream = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite);
                System.IO.StreamReader streamReader = new System.IO.StreamReader(stream, encoding);
                result = streamReader.ReadToEnd();
                streamReader.Close();
                streamReader.Dispose();
            }
            return result;
        }

        /// <summary>
        /// 返回文件的完整路径
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>返回文件的完整路径</returns>
		public static string getAppFileFullName(string fileName)
        {
            return System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, fileName);
        }

        /// <summary>
        /// 从文件中读取第一行内容
        /// </summary>
        /// <param name="path">文件完整路径</param>
        /// <returns>返回文件第一行内容</returns>
		public static string GetFirstLine(string path)
        {
            string result = string.Empty;
            if (!System.IO.File.Exists(path))
            {
                result = "不存在相应的目录";
            }
            else
            {
                System.IO.FileStream stream = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite);
                System.IO.StreamReader streamReader = new System.IO.StreamReader(stream, System.Text.Encoding.Default);
                result = streamReader.ReadLine();
                streamReader.Close();
            }
            return result;
        }

        /// <summary>
        /// 读取文件第二行内容
        /// </summary>
        /// <param name="path">文件完整路径</param>
        /// <returns>返回读取的内容</returns>
        public static string GetSecondLine(string path)
        {
            string result = string.Empty;
            if (!System.IO.File.Exists(path))
            {
                result = "不存在相应的目录";
            }
            else
            {
                System.IO.FileStream stream = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite);
                System.IO.StreamReader streamReader = new System.IO.StreamReader(stream, System.Text.Encoding.Default);
                if (!string.IsNullOrEmpty(streamReader.ReadLine()))
                {
                    result = streamReader.ReadLine();
                }
                else
                {
                    result = string.Empty;
                }
                streamReader.Close();
            }
            return result;
        }

        /// <summary>
        /// 读取文件中指定位置的值
        /// </summary>
        /// <param name="path">文件完整路径</param>
        /// <param name="row">要读取的第几行</param>
        /// <param name="col">要读取的第几列</param>
        /// <returns>返回读取的内容</returns>
		public static string GetValue(string path, int row, int col)
        {
            string text = string.Empty;
            if (!System.IO.File.Exists(path))
            {
                return "不存在相应的目录";
            }
            string[] array;
            if (row == 1)
            {
                text = FileHelper.GetFirstLine(path);
                array = text.ToString().Split(new char[]
                {
                    ':'
                });
            }
            else
            {
                text = FileHelper.GetSecondLine(path);
                array = text.ToString().Split(new char[]
                {
                    ':'
                });
            }
            return array[col];
        }

        /// <summary>
        /// 在文件末尾添加内容
        /// </summary>
        /// <param name="path">文件完整路径</param>
        /// <param name="content">要添加的内容</param>
		public static void FileAdd(string path, string content)
        {
            try
            {
                object obj = new object();
                lock (obj)
                {
                    System.IO.StreamWriter streamWriter = System.IO.File.AppendText(path);
                    streamWriter.Write(content);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 文件复制
        /// </summary>
        /// <param name="orignFile">源文件完整路径</param>
        /// <param name="newFile">目标文件完整路径</param>
		public static void FileCoppy(string orignFile, string newFile)
        {
            System.IO.File.Copy(orignFile, newFile, true);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path">要删除的文件的完整路径</param>
		public static void FileDel(string path)
        {
            System.IO.File.Delete(path);
        }

        /// <summary>
        /// 文件移动（剪贴->粘贴）
        /// </summary>
        /// <param name="orignFile">源文件的完整路径</param>
        /// <param name="newFile">目标文件完整路径</param>
		public static void FileMove(string orignFile, string newFile)
        {
            System.IO.File.Move(orignFile, newFile);
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="orignFolder">当前目录</param>
        /// <param name="newFloder">要创建的目录名</param>
		public static void FolderCreate(string orignFolder, string newFloder)
        {
            System.IO.Directory.SetCurrentDirectory(orignFolder);
            System.IO.Directory.CreateDirectory(newFloder);
        }

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="dir">要删除的目录</param>
		public static void DeleteFolder(string dir)
        {
            if (System.IO.Directory.Exists(dir))
            {
                string[] fileSystemEntries = System.IO.Directory.GetFileSystemEntries(dir);
                for (int i = 0; i < fileSystemEntries.Length; i++)
                {
                    string text = fileSystemEntries[i];
                    if (System.IO.File.Exists(text))
                    {
                        System.IO.File.Delete(text);
                    }
                    else
                    {
                        FileHelper.DeleteFolder(text);
                    }
                }
                System.IO.Directory.Delete(dir);
            }
        }

        /// <summary>
        /// 目录内容复制
        /// </summary>
        /// <param name="srcPath">源目录</param>
        /// <param name="aimPath">目标目录</param>
		public static void CopyDir(string srcPath, string aimPath)
        {
            try
            {
                if (aimPath[aimPath.Length - 1] != System.IO.Path.DirectorySeparatorChar)
                {
                    aimPath += System.IO.Path.DirectorySeparatorChar;
                }
                if (!System.IO.Directory.Exists(aimPath))
                {
                    System.IO.Directory.CreateDirectory(aimPath);
                }
                string[] fileSystemEntries = System.IO.Directory.GetFileSystemEntries(srcPath);
                string[] array = fileSystemEntries;
                for (int i = 0; i < array.Length; i++)
                {
                    string text = array[i];
                    if (System.IO.Directory.Exists(text))
                    {
                        FileHelper.CopyDir(text, aimPath + System.IO.Path.GetFileName(text));
                    }
                    else
                    {
                        System.IO.File.Copy(text, aimPath + System.IO.Path.GetFileName(text), true);
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.ToString());
            }
        }

        /// <summary>
        /// 获取文件所在目录列表
        /// </summary>
        /// <param name="list">返回文件所在目录列表</param>
        /// <returns>返回文件所在目录列表</returns>
        public static List<string> GetFileInfoPathString(List<FileInfo> list)
        {
            List<string> resultList = new List<string>();
            if (list != null && list.Count > 0)
            {
                foreach (FileInfo fi in list)
                {
                    resultList.Add(fi.DirectoryName);
                }
            }
            return resultList;
        }

        /// <summary>
        /// 搜索指定关键词的文件列表
        /// </summary>
        /// <param name="searchPath">要搜索的路径</param>
        /// <param name="searchKey">搜索关键词</param>
        /// <param name="extFilter">要过滤的文件扩展名</param>
        /// <returns>返回搜索后的文件列表</returns>
        public static List<FileInfo> SearchFiles(string searchPath, string searchKey, string extFilter)
        {
            List<FileInfo> resultList = new List<FileInfo>();
            resultList = RecursiveSearchFiles(searchPath, searchKey, extFilter, resultList);
            return resultList;
        }

        /// <summary>
        /// 递归搜索指定关键词的文件列表
        /// </summary>
        /// <param name="searchPath">要搜索的路径</param>
        /// <param name="searchKey">搜索关键词</param>
        /// <param name="extFilter">要过滤的文件扩展名</param>
        /// <param name="list">搜索结果列表</param>
        /// <returns>返回搜索后的文件列表</returns>
        private static List<FileInfo> RecursiveSearchFiles(string searchPath, string searchKey, string extFilter, List<FileInfo> list)
        {
            if (System.IO.Directory.Exists(searchPath))
            {
                try
                {
                    System.IO.DirectoryInfo di = new DirectoryInfo(searchPath);
                    FileInfo[] files = di.GetFiles();
                    foreach (FileInfo f in files)
                    {
                        if (!String.IsNullOrEmpty(extFilter))
                        {
                            if (f.Extension.ToLower() != extFilter.ToLower())
                            {
                                continue;
                            }
                        }
                        if (f.Name.ToLower().Contains(searchKey.ToLower()))
                        {
                            list.Add(f);
                        }
                    }
                    DirectoryInfo[] dirs = di.GetDirectories();
                    foreach (DirectoryInfo d in dirs)
                    {
                        RecursiveSearchFiles(d.FullName, searchKey, extFilter, list);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("RecursiveSearchFiles exception : " + ex.Message);
                }
            }
            return list;
        }
    }
}
