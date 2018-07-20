using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Web;

namespace Harbour.Web
{
    /// <summary>
    /// 文件下载类
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// 同步标识
        /// </summary>
        private static Object sync = new object();

        #region 文件目录操作
        /// <summary>
        /// 获得文件物理路径
        /// </summary>
        /// <returns></returns>
        public static string GetMapPath(string path)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(path);
            }
            else
            {
                return System.Web.Hosting.HostingEnvironment.MapPath(path);
            }
        }
        /// <summary>
        /// 检测文件目录是否存在
        /// </summary>
        /// <param name="path">文件目录的相对路径</param>
        /// <returns></returns>
        public static bool IsExistDirectory(string path)
        {
            return Directory.Exists(GetMapPath(path));
        }
        /// <summary>
        /// 如果文件目录不存在就创建文件目录
        /// </summary>
        /// <param name="path">文件目录的相对路径</param>
        public static void CreateDirectory(string path)
        {
            CreateDirectoryMapPath(GetMapPath(path));
        }
        /// <summary>
        /// 如果文件目录不存在就创建文件目录
        /// </summary>
        /// <param name="mapPath">文件目录绝对路径</param>
        public static void CreateDirectoryMapPath(string mapPath)
        {
            string abPath = Path.GetDirectoryName(mapPath);
            if (!Directory.Exists(abPath))
            {
                Directory.CreateDirectory(abPath);
            }
        }
        #endregion

        #region 文件操作
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="Url">文件的相对路径</param>
        /// <returns></returns>
        public static bool DeleteFile(string Url)
        {
            string path = GetMapPath(Url);
            bool bol = false;
            if (File.Exists(path))//判断文件是不是存在
            {
                File.Delete(path);//如果存在则删除
                bol = true;
            }
            return bol;
        }
        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="Url">文件的相对路径</param>
        /// <returns></returns>
        public static bool IsFileExist(string Url)
        {
            string path = GetMapPath(Url);
            return File.Exists(path);//判断文件是不是存在
        }
        /// <summary>
        /// 获取文件扩展名
        /// </summary>
        /// <param name="FileName">文件名的相对路径</param>
        /// <returns>包含指定路径的扩展名（包括“.”）的 System.String、null 或 System.String.Empty。如果 path 为 null，则GetExtension 返回 null。如果 path 不具有扩展名信息，则 GetExtension 返回 Empty。</returns>
        public static string GetFileExtension(string FileName)
        {
            return Path.GetExtension(GetMapPath(FileName));
        }

        /// <summary>
        /// 从文件相对路径中获取目录路径
        /// </summary>
        /// <param name="filePath">文件相对路径</param>
        /// <returns></returns>
        public static string GetDirectoryFrom(string filePath)
        {
            return GetDirectoryFromFileMapPath(GetMapPath(filePath));
        }
        /// <summary>
        /// 从文件绝对路径中获取目录路径
        /// </summary>
        /// <param name="fileMapPath">文件的绝对路径</param>
        public static string GetDirectoryFromFileMapPath(string fileMapPath)
        {
            //实例化文件
            FileInfo file = new FileInfo(fileMapPath);
            //获取目录信息
            DirectoryInfo directory = file.Directory;
            //返回目录路径
            return directory.FullName;
        }

        /// <summary>
        /// 根据文件相对路径得到文件流
        /// </summary>
        /// <param name="filePath">文件相对路径</param>
        public static byte[] GetFileSream(string filePath)
        {
            return GetFileSreamFileMapPath(GetMapPath(filePath));
        }
        /// <summary>
        /// 根据文件绝对路径得到文件流
        /// </summary>
        /// <param name="fileMapPath">文件的绝对路径</param>
        public static byte[] GetFileSreamFileMapPath(string fileMapPath)
        {
            byte[] buffer = null;
            using (FileStream stream = new FileInfo(fileMapPath).OpenRead())
            {
                buffer = new byte[stream.Length];
                stream.Read(buffer, 0, Convert.ToInt32(stream.Length));
            }
            return buffer;
        }
        #endregion

        #region 创建一个文件
        /// <summary>
        /// 创建一个文件
        /// </summary>
        /// <param name="filePath">文件的相对路径</param>
        public static void CreateFile(string filePath)
        {
            CreateFileMapPath(filePath);
        }
        /// <summary>
        /// 创建一个文件
        /// </summary>
        /// <param name="fileMapPath">文件的绝对路径</param>
        public static void CreateFileMapPath(string fileMapPath)
        {
            CreateFile(fileMapPath, "");
        }

        /// <summary>
        /// 创建一个文件,并将字符串写入文件
        /// 如果该文件存在，则数据被追加到该文件中
        /// 字符编码 Encoding.Default
        /// </summary>
        /// <param name="filePath">文件的相对路径</param>
        /// <param name="text">字符串数据</param>
        public static void CreateFile(string filePath, string text)
        {
            CreateFileMapPath(GetMapPath(filePath), text);
        }
        /// <summary>
        /// 创建一个文件,并将字符串写入文件
        /// 如果该文件存在，则数据被追加到该文件中
        /// 字符编码 Encoding.Default
        /// </summary>
        /// <param name="fileMapPath">文件的绝对路径</param>
        /// <param name="text">字符串数据</param>
        public static void CreateFileMapPath(string fileMapPath, string text)
        {
            CreateFileMapPath(fileMapPath, text, true, Encoding.Default);
        }
        /// <summary>
        /// 创建一个文件,并将字符串写入文件。
        /// </summary>
        /// <param name="fileMapPath">文件的绝对路径</param>
        /// <param name="text">字符串数据</param>
        /// <param name="append">确定是否将数据追加到文件。如果该文件存在，并且 append 为 false，则该文件被覆盖。如果该文件存在，并且 append 为 true，则数据被追加到该文件中。否则，将创建新文件。</param>
        /// <param name="encoding">字符编码</param>
        public static void CreateFileMapPath(string fileMapPath, string text, bool append, Encoding encoding)
        {
            try
            {
                if (!File.Exists(fileMapPath))//文件不存在
                {
                    //获取文件目录路径
                    string directoryPath = GetDirectoryFromFileMapPath(fileMapPath);
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }
                }
                if (string.IsNullOrEmpty(text))
                    return;
                lock (sync)
                {
                    using (StreamWriter writer = new StreamWriter(fileMapPath, append, encoding))
                    {
                        writer.Write(text);//写入字符串  
                        writer.Flush(); //输出
                    }
                }
            }
            catch
            {
            }
        }
        /// <summary>
        /// 创建一个文件,并将字符串写入文件
        /// 如果该文件存在，则数据被追加到该文件中
        /// 字符编码 Encoding.Default
        /// </summary>
        /// <param name="filePath">文件的相对路径</param>
        /// <param name="buffer">字符串数据</param>

        public static void CreateFile(string filePath, byte[] buffer)
        {
            CreateFileMapPath(GetMapPath(filePath), buffer);
        }
        /// <summary>
        /// 创建一个文件,并将字符串写入文件
        /// 如果该文件存在，则数据被追加到该文件中
        /// 字符编码 Encoding.Default
        /// </summary>
        /// <param name="fileMapPath">文件的绝对路径</param>
        /// <param name="buffer">字符串数据</param>
        public static void CreateFileMapPath(string fileMapPath, byte[] buffer)
        {
            CreateFileMapPath(fileMapPath, buffer, true, Encoding.Default);
        }
        /// <summary>
        /// 创建一个文件,并将字符串写入文件。
        /// </summary>
        /// <param name="fileMapPath">文件的绝对路径</param>
        /// <param name="buffer">二进制流数据</param>
        /// <param name="append">确定是否将数据追加到文件。如果该文件存在，并且 append 为 false，则该文件被覆盖。如果该文件存在，并且 append 为 true，则数据被追加到该文件中。否则，将创建新文件。</param>
        /// <param name="encoding">字符编码</param>
        public static void CreateFileMapPath(string fileMapPath, byte[] buffer, bool append, Encoding encoding)
        {
            try
            {
                if (buffer == null || buffer.Length <= 0)
                    return;
                if (!File.Exists(fileMapPath))//文件不存在
                {
                    //获取文件目录路径
                    string directoryPath = GetDirectoryFromFileMapPath(fileMapPath);
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }
                }
                lock (sync)
                {
                    Char[] ch = encoding.GetChars(buffer);
                    using (StreamWriter writer = new StreamWriter(fileMapPath, append, encoding))
                    {
                        writer.Write(ch);//写入字符串  
                        writer.Flush(); //输出
                    }
                }
            }
            catch
            {
            }
        }
        #endregion

        #region 下载

        /// <summary>
        /// 普通下载
        /// </summary>
        /// <param name="FileName">文件虚拟路径</param>
        public static void DownLoad(string FileName)
        {
            string destFileName = GetMapPath(FileName);
            if (File.Exists(destFileName))
            {
                FileInfo fi = new FileInfo(destFileName);
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Buffer = false;
                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(Path.GetFileName(destFileName), System.Text.Encoding.UTF8));
                HttpContext.Current.Response.AppendHeader("Content-Length", fi.Length.ToString());
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.WriteFile(destFileName);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }

        /// <summary>
        /// 分块下载
        /// </summary>
        /// <param name="FileName">文件虚拟路径</param>
        /// <param name="BlockSize">分块大小，默认200K</param>
        public static void DownLoad(string FileName, long BlockSize = 204800)
        {
            string filePath = GetMapPath(FileName);        //指定块大小 
            byte[] buffer = new byte[BlockSize]; //建立一个200K的缓冲区 
            long dataToRead = 0;                 //已读的字节数   
            FileStream stream = null;
            try
            {
                //打开文件   
                stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                dataToRead = stream.Length;

                //添加Http头   
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachement;filename=" + HttpUtility.UrlEncode(Path.GetFileName(filePath), System.Text.Encoding.UTF8));
                HttpContext.Current.Response.AddHeader("Content-Length", dataToRead.ToString());

                while (dataToRead > 0)
                {
                    if (HttpContext.Current.Response.IsClientConnected)
                    {
                        int length = stream.Read(buffer, 0, Convert.ToInt32(BlockSize));
                        HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);
                        HttpContext.Current.Response.Flush();
                        HttpContext.Current.Response.Clear();
                        dataToRead -= length;
                    }
                    else
                    {
                        dataToRead = -1; //防止client失去连接 
                    }
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("Error:" + ex.Message);
            }
            finally
            {
                if (stream != null) stream.Close();
                HttpContext.Current.Response.Close();
            }
        }

        /// <summary>
        ///  输出硬盘文件，提供下载 支持大文件、续传、速度限制、资源占用小
        /// </summary>
        /// <param name="Request">Page.Request对象</param>
        /// <param name="Response">Page.Response对象</param>
        /// <param name="FileName">文件虚拟路径</param>
        /// <param name="Speed">每秒允许下载的字节数</param>
        /// <returns>返回是否成功</returns>    
        public static bool ResponseFile(HttpRequest Request, HttpResponse Response, string FileName, long Speed = 100)
        {
            string filePath = GetMapPath(FileName);
            try
            {
                FileStream myFile = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(myFile);
                try
                {
                    Response.AddHeader("Accept-Ranges", "bytes");
                    Response.Buffer = false;

                    long fileLength = myFile.Length;
                    long startBytes = 0;
                    int pack = 10240;  //10K bytes
                    int sleep = (int)Math.Floor((double)(1000 * pack / Speed)) + 1;

                    if (Request.Headers["Range"] != null)
                    {
                        Response.StatusCode = 206;
                        string[] range = Request.Headers["Range"].Split(new char[] { '=', '-' });
                        startBytes = Convert.ToInt64(range[1]);
                    }
                    Response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
                    if (startBytes != 0)
                    {
                        Response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", startBytes, fileLength - 1, fileLength));
                    }

                    Response.AddHeader("Connection", "Keep-Alive");
                    Response.ContentType = "application/octet-stream";
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(Path.GetFileName(filePath), System.Text.Encoding.UTF8));

                    br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
                    int maxCount = (int)Math.Floor((double)((fileLength - startBytes) / pack)) + 1;

                    for (int i = 0; i < maxCount; i++)
                    {
                        if (Response.IsClientConnected)
                        {
                            Response.BinaryWrite(br.ReadBytes(pack));
                            Thread.Sleep(sleep);
                        }
                        else
                        {
                            i = maxCount;
                        }
                    }
                }
                catch
                {
                    return false;
                }
                finally
                {
                    br.Close();
                    myFile.Close();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        #endregion

        #region 上传

        /// <summary>
        /// 转换为字节数组
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>字节数组</returns>
        public static byte[] GetByteBuffer(string filename)
        {
            if (File.Exists(filename))
            {
                FileStream Fsm = null;
                try
                {
                    Fsm = File.OpenRead(filename);
                    return GetByteBuffer(Fsm);
                }
                catch
                {
                    return new byte[0];
                }
                finally
                {
                    Fsm.Close();
                }
            }
            else
            {
                return new byte[0];
            }
        }

        /// <summary>
        /// 流转化为字节数组
        /// </summary>
        /// <param name="theStream">流</param>
        /// <returns>字节数组</returns>
        public static byte[] GetByteBuffer(Stream theStream)
        {
            int bi;
            MemoryStream tempStream = new MemoryStream();
            try
            {
                while ((bi = theStream.ReadByte()) != -1)
                {
                    tempStream.WriteByte(((byte)bi));
                }
                return tempStream.ToArray();
            }
            catch
            {
                return new byte[0];
            }
            finally
            {
                tempStream.Close();
            }
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="byteData">字节数组</param>
        /// <param name="FileName">文件名</param>
        /// <param name="SavePath">保存路径</param>
        /// <param name="IsNewName">是否重新命名</param>
        public static string SaveFile(byte[] byteData, string FileName, string SavePath = "/Upload", bool IsNewName = true)
        {
            string dirPath = GetMapPath(SavePath);
            string newFileName = dirPath + FileName;
            if (IsNewName)
                newFileName = dirPath + GetUploadFileName(FileName);
            FileStream fileStream = null;
            MemoryStream m = new MemoryStream(byteData);
            try
            {
                if (!Directory.Exists(dirPath))
                    Directory.CreateDirectory(dirPath);
                fileStream = new FileStream(newFileName, FileMode.Create);
                m.WriteTo(fileStream);
            }
            finally
            {
                m.Close();
                fileStream.Close();
            }
            return newFileName;
        }

        /// <summary>
        /// 获取新的文件名，自动生成，不会重复
        /// </summary>
        /// <param name="FileName">旧文件名</param>
        /// <returns></returns>
        public static string GetUploadFileName(string FileName)
        {
            string extension = Path.GetExtension(FileName);
            return TrimStart(extension, ".") + DateTime.Now.ToString("yyMMddHHmmssfffffff") + extension;
        }

        /// <summary>
        /// 移除前导字符串
        /// </summary>
        /// <param name="sourceStr">源字符串</param>
        /// <param name="trimStr">移除字符串</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns></returns>
        public static string TrimStart(string sourceStr, string trimStr, bool ignoreCase = true)
        {
            if (string.IsNullOrEmpty(sourceStr))
                return string.Empty;

            if (string.IsNullOrEmpty(trimStr) || !sourceStr.StartsWith(trimStr, ignoreCase, CultureInfo.CurrentCulture))
                return sourceStr;

            return sourceStr.Remove(0, trimStr.Length);
        }

        #endregion

    }
}
