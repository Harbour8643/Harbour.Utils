using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;

namespace Harbour.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpUtils
    {
        #region Get请求
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string Get(HttpParam param)
        {
            param.Method = "GET";
            var result = "";
            using (var reader = new StreamReader(HttpRequestStream(param), param.Encoding))
            {
                result = reader.ReadToEnd();
            }
            return result;
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
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <returns></returns>
        public static JsonData<T> GetJR<T>(HttpParam param)
        {
            return Get<JsonData<T>>(param);
        }


        /// <summary>
        /// Get WebResponse
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static WebResponse GetResponse(string url)
        {
            return HttpRequestResponse(new HttpParam()
            {
                Url = url,
                Method = "GET"
            });
        }
        /// <summary>
        /// Get Stream
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static Stream GetStream(string url)
        {
            return HttpRequestStream(new HttpParam()
            {
                Url = url,
                Method = "GET"
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string Get(string url)
        {
            var param = new HttpParam
            {
                Url = url,
                Method = "GET",
            };
            return Get(param);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static T Get<T>(string url)
        {
            var str = Get(url);
            return JsonConvert.DeserializeObject<T>(str);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static JsonData<T> GetJR<T>(string url)
        {
            return Get<JsonData<T>>(url);
        }
        #endregion

        #region Post 请求
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string Post(HttpParam param)
        {
            param.Method = "POST";
            var result = "";
            using (var reader = new StreamReader(HttpRequestStream(param), param.Encoding))
            {
                result = reader.ReadToEnd();
            }
            return result;
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
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <returns></returns>
        public static JsonData<T> PostJR<T>(HttpParam param)
        {
            return Post<JsonData<T>>(param);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postParam"></param>
        /// <returns></returns>
        public static WebResponse PostResponse(string url, NameValueCollection postParam = null)
        {
            return HttpRequestResponse(new HttpParam()
            {
                Url = url,
                Method = "POST",
                RequestParameters = postParam
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postParam"></param>
        /// <returns></returns>
        public static Stream PostStream(string url, NameValueCollection postParam = null)
        {
            return HttpRequestStream(new HttpParam()
            {
                Url = url,
                Method = "POST",
                RequestParameters = postParam
            });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postParam"></param>
        /// <returns></returns>
        public static string Post(string url, NameValueCollection postParam = null)
        {
            var param = new HttpParam
            {
                Url = url,
                Method = "POST",
                RequestParameters = postParam
            };
            var str = Post(param);
            return str;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postParam"></param>
        /// <returns></returns>
        public static T Post<T>(string url, NameValueCollection postParam = null)
        {
            var str = Post(url, postParam);
            return JsonConvert.DeserializeObject<T>(str);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postParam"></param>
        /// <returns></returns>
        public static JsonData<T> PostJR<T>(string url, NameValueCollection postParam = null)
        {
            return Post<JsonData<T>>(url, postParam);
        }
        #endregion

        #region Http相关
        /// <summary>
        /// Http请求
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public static Stream HttpRequestStream(HttpParam param)
        {
            return HttpRequestResponse(param).GetResponseStream();
        }
        /// <summary>
        /// 获取 WebResponse
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static WebResponse HttpRequestResponse(HttpParam param)
        {
            string url = param.Url;

            //文件上传必须是Post、multipart/form-data
            if (param.UploadFiles.Count > 0)
            {
                param.Method = "POST";
                param.ContentType = "multipart/form-data";
            }

            if (string.IsNullOrEmpty(param.Method))
                param.Method = "GET";
            param.Method = param.Method.ToUpper();

            if (string.IsNullOrEmpty(param.ContentType))
                param.ContentType = "application/x-www-form-urlencoded";

            //如果是GET请求，处理请求参数
            if ("GET".Equals(param.Method) && param.RequestParameters != null && param.RequestParameters.Count > 0)
            {
                string[] nameVals = NameValueCollectionFormat(param.RequestParameters);
                string nameValStr = string.Join("&", nameVals);
                if (url.Contains("?"))
                    url += "&" + nameValStr;
                else
                    url += "?" + nameValStr;
            }

            HttpWebRequest httpWebRequest = CreatHttpWebRequest(url);

            //处理自定义请求头信息
            if (param.CustomHeaders != null && param.CustomHeaders.Count > 0)
            {
                string[] nameVals = NameValueCollectionFormat(param.RequestParameters);
                foreach (string nameVal in nameVals)
                {
                    string[] arr = nameVal.Split('=');
                    if (arr.Length == 2)
                        httpWebRequest.Headers.Add(arr[0], arr[1]);
                }
            }
            httpWebRequest.Method = param.Method;
            httpWebRequest.UserAgent = param.UserAgent;
            httpWebRequest.Accept = "*/*";
            httpWebRequest.Timeout = param.TimeOut * 1000;
            httpWebRequest.CookieContainer = param.CookieContainer;
            httpWebRequest.ContentType = $"{param.ContentType}";

            //如果是POST请求，处理请求参数
            if ("POST".Equals(param.Method))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    if (param.ContentType.Contains("multipart/form-data"))
                    {
                        //分隔符、ContentType
                        string boundary = Guid.NewGuid().ToString().Replace("-", "");
                        httpWebRequest.ContentType = $"multipart/form-data; boundary={boundary}";
                        //Post的参数
                        if (param.RequestParameters != null && param.RequestParameters.Count > 0)
                        {
                            string[] nameVals = NameValueCollectionFormat(param.RequestParameters);
                            string _textFormdataTemplate = "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\"" + "\r\n\r\n{1}";
                            string postParamStr = "";
                            foreach (string nameVal in nameVals)
                            {
                                string[] arr = nameVal.Split('=');
                                if (arr.Length != 2)
                                    continue;
                                postParamStr += string.Format(_textFormdataTemplate, arr[0], arr[1]);
                            }

                            byte[] postParams = param.Encoding.GetBytes(postParamStr);
                            memoryStream.Write(postParams, 0, postParams.Length);
                        }
                        //Post的文件
                        if (param.UploadFiles != null && param.UploadFiles.Count > 0)
                        {
                            string fileFormdataTemplate = "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"file\"; filename=\"{0}\"\r\nContent-Type: {1}\r\n\r\n";
                            foreach (UploadFileParams uploadFile in param.UploadFiles)
                            {
                                string contentType = MimeData.GetContentType(uploadFile.FileName);
                                string fileFormdata = string.Format(fileFormdataTemplate, uploadFile.FileName, contentType);
                                byte[] fileFormdataByte = param.Encoding.GetBytes(fileFormdata);
                                memoryStream.Write(fileFormdataByte, 0, fileFormdataByte.Length);

                                byte[] fileByte = uploadFile.FileStream.TryToBytes();
                                memoryStream.Write(fileByte, 0, fileByte.Length);
                            }
                        }
                        //添加结束分隔符
                        if (memoryStream.Length > 0)
                        {
                            byte[] endBoundary = param.Encoding.GetBytes($"\r\n--{boundary}--\r\n");
                            memoryStream.Write(endBoundary, 0, endBoundary.Length);
                        }
                    }
                    else
                    {
                        string[] nameVals = NameValueCollectionFormat(param.RequestParameters);
                        string nameValStr = string.Join("&", nameVals);
                        byte[] postParams = param.Encoding.GetBytes(nameValStr);
                        memoryStream.Write(postParams, 0, postParams.Length);
                    }
                    if (memoryStream.Length > 0)
                    {
                        using (Stream stream = httpWebRequest.GetRequestStream())
                        {
                            byte[] postParamByte = memoryStream.ToArray();
                            stream.Write(postParamByte, 0, postParamByte.Length);
                        }
                    }
                }
            }
            return httpWebRequest.GetResponse();
        }

        /// <summary>
        /// 创建HttpWebRequest
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static HttpWebRequest CreatHttpWebRequest(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException("url");

            HttpWebRequest request = null;
            //如果是发送HTTPS请求 
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                //ServicePointManager.SecurityProtocol = spt; //不指定,使之自动协商/适应, 避免指定的版本与服务器不一样反而连不上
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true; //总是接受
                request = WebRequest.Create(url) as HttpWebRequest;
                //request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            return request;
        }
        private static string[] NameValueCollectionFormat(NameValueCollection nameValues)
        {
            List<string> nameVals = new List<string>();
            string[] allKey = nameValues.AllKeys;
            foreach (string name in allKey)
            {
                string val = nameValues.Get(name);
                nameVals.Add($"{name}={val}");
            }
            return nameVals.ToArray();
        }
        #endregion
    }
}