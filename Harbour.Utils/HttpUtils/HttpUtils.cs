using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Harbour.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpUtils
    {

        #region Get Post Stream
        /// <summary>
        /// Get Stream
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static Stream GetStream(string url)
        {
            return RequestStream(new HttpParam()
            {
                Url = url,
                Method = "GET"
            });
        }
        /// <summary>
        /// Get Stream
        /// </summary>
        /// <param name="url"></param>
        /// <param name="getParam"></param>
        /// <returns></returns>
        public static Stream GetStream(string url, object getParam)
        {
            return RequestStream(new HttpParam()
            {
                Url = url,
                Method = "GET",
                GetParam = getParam
            });
        }
        /// <summary>
        /// Post Stream
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static Stream PostStream(string url)
        {
            return RequestStream(new HttpParam()
            {
                Url = url,
                Method = "POST"
            });
        }
        /// <summary>
        /// Post Stream
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postParam"></param>
        /// <returns></returns>
        public static Stream PostStream(string url, object postParam)
        {
            return RequestStream(new HttpParam()
            {
                Url = url,
                Method = "POST",
                GetParam = postParam
            });
        }
        #endregion

        #region Get请求
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="getParam"></param>
        /// <returns></returns>
        public static string Get(string url, object getParam = null)
        {
            var param = new HttpParam
            {
                Url = url,
                Method = "GET",
                GetParam = getParam
            };
            return Get(param);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string Get(HttpParam param)
        {
            param.Method = "GET";
            return RequestString(param);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="getParam"></param>
        /// <returns></returns>
        public static T Get<T>(string url, object getParam = null)
        {
            var str = Get(url, getParam);
            return JsonConvert.DeserializeObject<T>(str);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <returns></returns>
        public static T Get<T>(HttpParam param)
        {
            var str = Get(param);
            return JsonConvert.DeserializeObject<T>(str);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="getParam"></param>
        /// <returns></returns>
        public static JsonResponse<T> GetJR<T>(string url, object getParam = null)
        {
            return Get<JsonResponse<T>>(url, getParam);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <returns></returns>
        public static JsonResponse<T> GetJR<T>(HttpParam param)
        {
            return Get<JsonResponse<T>>(param);
        }
        #endregion

        #region Post 请求
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postParam"></param>
        /// <returns></returns>
        public static string Post(string url, object postParam = null)
        {
            var param = new HttpParam
            {
                Url = url,
                Method = "POST",
                PostParam = postParam
            };
            var str = Post(param);
            return str;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string Post(HttpParam param)
        {
            param.Method = "POST";
            var str = RequestString(param);
            return str;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postParam"></param>
        /// <returns></returns>
        public static T Post<T>(string url, object postParam = null)
        {
            var str = Post(url, postParam);
            return JsonConvert.DeserializeObject<T>(str);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <returns></returns>
        public static T Post<T>(HttpParam param)
        {
            param.Method = "POST";
            var str = Post(param);
            return JsonConvert.DeserializeObject<T>(str);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postParam"></param>
        /// <returns></returns>
        public static JsonResponse<T> PostJR<T>(string url, object postParam = null)
        {
            return Post<JsonResponse<T>>(url, postParam);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <returns></returns>
        public static JsonResponse<T> PostJR<T>(HttpParam param)
        {
            return Post<JsonResponse<T>>(param);
        }

        #endregion

        #region 文件上传
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <returns></returns>
        public static T PostFile<T>(HttpPostFileParam param)
        {
            var str = PostFileString(param);
            return JsonConvert.DeserializeObject<T>(str);
        }
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <returns></returns>
        public static JsonResponse<T> PostFileJR<T>(HttpPostFileParam param)
        {
            return PostFile<JsonResponse<T>>(param);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        public static string RequestString(HttpParam param)
        {
            var result = "";
            using (var reader = new StreamReader(RequestStream(param), param.Encoding))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }
        /// <summary>
        /// 获取响应流
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static Stream RequestStream(HttpParam param)
        {
            //处理地址栏参数
            string getParam = GetParamFormat(param.GetParam);
            if (!string.IsNullOrWhiteSpace(getParam))
                param.Url = $"{param.Url}?{getParam}";

            HttpWebRequest httpWebRequest = WebRequest.CreateHttp(param.Url);

            AddClientCertificate(httpWebRequest.ClientCertificates, param.CertPath, param.CertPwd);
            httpWebRequest.Timeout = param.TimeOut * 1000;
            httpWebRequest.UserAgent = param.UserAgent;
            httpWebRequest.Method = param.Method ?? "POST";
            httpWebRequest.Referer = param.Referer;
            httpWebRequest.CookieContainer = param.CookieContainer;
            httpWebRequest.ContentType = param.ContentType;

            var postParamString = PostParamFormat(param.PostParam, param.PostParamType);
            if (!string.IsNullOrWhiteSpace(postParamString))
            {
                byte[] postParamByte = param.Encoding.GetBytes(postParamString);
                httpWebRequest.ContentLength = postParamByte.Length;
                using (Stream stream = httpWebRequest.GetRequestStream())
                {
                    stream.Write(postParamByte, 0, postParamByte.Length);
                }
            }
            return httpWebRequest.GetResponse().GetResponseStream();
        }


        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string PostFileString(HttpPostFileParam param)
        {
            var result = "";
            using (var reader = new StreamReader(PostFileStream(param), param.Encoding))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }
        /// <summary>
        /// 文件上传至远程服务器
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public static Stream PostFileStream(HttpPostFileParam param)
        {
            HttpWebRequest httpWebRequest = WebRequest.CreateHttp(param.Url);

            httpWebRequest.Timeout = param.TimeOut * 1000;
            httpWebRequest.UserAgent = param.UserAgent;
            httpWebRequest.Method = "POST";

            string boundary = "----" + Guid.NewGuid().ToString().Replace("-", "");

            httpWebRequest.ContentType = "multipart/mixed;boundary=" + boundary;

            using (Stream stream = httpWebRequest.GetRequestStream())
            {
                int contentLength = 0;
                var postParamString = PostParamFormat(param.PostParam, param.PostParamType);
                if (!string.IsNullOrWhiteSpace(postParamString))
                {
                    byte[] paramByte = param.Encoding.GetBytes(postParamString);
                    stream.Write(paramByte, 0, paramByte.Length);
                    contentLength += paramByte.Length;
                }
                int index = 1;
                string baseDisposition = "Content-Disposition:form-data;name=\"{0}\";filename=\"{1}\"\r\nContent-Type:{2}\r\n\r\n";
                foreach (UploadFileParams uploadFile in param.UploadFiles)
                {
                    //分隔符
                    if (index != 1 || !string.IsNullOrWhiteSpace(postParamString))
                    {
                        byte[] boundaryParm = param.Encoding.GetBytes($"\r\n--{boundary}\r\n");
                        stream.Write(boundaryParm, 0, boundaryParm.Length);
                        contentLength += boundaryParm.Length;
                    }
                    //文件描述
                    string contentDisposition = string.Format(baseDisposition, uploadFile.Name, uploadFile.FileName, uploadFile.ContentType);
                    var disByte = Encoding.ASCII.GetBytes(contentDisposition);
                    stream.Write(disByte, 0, disByte.Length);
                    contentLength += disByte.Length;
                    //文件
                    byte[] fileByte = uploadFile.FileStream.TryToBytes();
                    stream.Write(fileByte, 0, fileByte.Length);
                    contentLength += fileByte.Length;
                    index += 1;
                }
                //结束分隔符
                if (!string.IsNullOrWhiteSpace(postParamString) || param.UploadFiles.Count > 0)
                {
                    byte[] boundaryEnd = param.Encoding.GetBytes($"\r\n--{boundary}--\r\n");
                    stream.Write(boundaryEnd, 0, boundaryEnd.Length);
                    contentLength += boundaryEnd.Length;
                }
                httpWebRequest.ContentLength = contentLength;
            }

            return httpWebRequest.GetResponse().GetResponseStream();
        }

        /// <summary>
        /// 添加安全证书
        /// </summary>
        /// <param name="certificateCollection"></param>
        /// <param name="certPath"></param>
        /// <param name="certPwd"></param>
        private static void AddClientCertificate(X509CertificateCollection certificateCollection, string certPath, string certPwd)
        {
            if (!string.IsNullOrWhiteSpace(certPath) && !string.IsNullOrWhiteSpace(certPwd))
            {
                ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;
                var cer = new X509Certificate2(certPath, certPwd, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
                certificateCollection.Add(cer);
                //暂时不要的
                //ServicePointManager.Expect100Continue = true;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                //req.ProtocolVersion = HttpVersion.Version11;
                //req.Headers.Add("x-requested-with", "XMLHttpRequest");
            }
        }
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        /// <summary>
        /// 格式化GetParam
        /// </summary>
        /// <param name="getParam"></param>
        /// <returns></returns>
        private static string GetParamFormat(object getParam)
        {
            if (getParam == null)
                return null;

            if (getParam is string)
                return getParam.ToString();

            StringBuilder getParamSb = new StringBuilder();
            PropertyInfo[] propertyInfos = getParam.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                getParamSb.AppendFormat($"{propertyInfo.Name}={propertyInfo.GetValue(getParam, null)}&");
            }
            return getParamSb.ToString().TrimEnd('&');
        }
        /// <summary>
        /// 格式化PostParam
        /// </summary>
        /// <param name="postParam"></param>
        /// <param name="paramType"></param>
        /// <returns></returns>
        private static string PostParamFormat(object postParam, HttpParamType paramType)
        {
            if (postParam == null)
                return null;

            if (postParam is string)
                return postParam.ToString();

            if (paramType != HttpParamType.Form)
                return JsonConvert.SerializeObject(postParam);

            string postParamStr = JsonConvert.SerializeObject(postParam);
            Dictionary<string, string> dicParam = JsonConvert.DeserializeObject<Dictionary<string, string>>(postParamStr);
            StringBuilder getParamSb = new StringBuilder();
            foreach (KeyValuePair<string, string> kvVal in dicParam)
            {
                getParamSb.AppendFormat($"{kvVal.Key}={kvVal.Value}&");
            }
            return getParamSb.ToString().TrimEnd('&');
        }
    }
}