using Harbour.Utils;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;

namespace Harbour.UtilsTest
{
    class Program
    {
        static void Main(string[] args)
        {

            //UploadFiles();

            //UploadFiles();


            //List<User> userList = new List<User>();
            //userList.IsValuable();
            //userList.TryForEach((e, i) => { i = 1; });

            var fds = default(int[]);

            User user = null;
            user.dfs(user?.Name);


            TypeParseExtTest.TryToIntArray();
            TypeParseExtTest.TryToIntArrayTest();
            TypeParseExtTest.TryToIntTest();
            TypeParseExtTest.TryToJsonTest();
            TypeParseExtTest.TryToStringArrayTest();
        }

        static void UploadFiles()
        {
            //string url = "http://localhost:8643/file/putfiles";
            string url = "http://39.155.251.32:8643/file/putfiles";

            HttpParam httpParam = new HttpParam();

            httpParam.Url = url;
            httpParam.ContentType = "multipart/form-data";

            NameValueCollection nameValues = new NameValueCollection();
            nameValues.Add("stoCode", "HStoDefault");
            nameValues.Add("domName", "harDefault");
            nameValues.Add("chiPath", "/daa/ds");
            nameValues.Add("fileTags", "ceshi,fdsafdas");
            nameValues.Add("isTempFiles", "true");
            nameValues.Add("token", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJHVUlEIjoiZWM2ZjgxODk1YTBjNGEzZThiNGYwN2ZkMDQ3YzU5MTUiLCJVc2VyTmFtZSI6ImFkbWluIiwiTmFtZSI6Iui2hee6p-euoeeQhuWRmCIsIkV4cFRpbWUiOiIyMDE5LTA1LTIyVDE5OjIxOjUwLjk5NDI5NDMrMDg6MDAiLCJHZW5kZXIiOjEsIlBob3RvIjoiLzIzNDU2IiwiQ2xhaW1zIjpbIntcIkdVSURcIjpcImVjNmY4MTg5NWEwYzRhM2U4YjRmMDdmZDA0N2M1OTE1XCIsXCJVc2VyTmFtZVwiOlwiYWRtaW5cIixcIk5hbWVcIjpcIui2hee6p-euoeeQhuWRmFwiLFwiRXhwVGltZVwiOlwiMjAxOS0wNS0yMlQxOToyMTo1MC45OTQyOTQzKzA4OjAwXCIsXCJHZW5kZXJcIjoxLFwiUGhvdG9cIjpcIi8yMzQ1NlwifSJdfQ.A9c3HQWpUmmN0sWdwgZqp-TrcsJJNncrzpidTfEBDsI");

            httpParam.RequestParameters = nameValues;

            string[] filePath = new string[] { @"E:\Harbour\150426001201-1.jpg", @"E:\Harbour\150426001201-4.jpg" };
            foreach (string fileUrl in filePath)
            {
                FileInfo fileInfo = new FileInfo(fileUrl);
                UploadFileParams uploadFileParams = new UploadFileParams();
                uploadFileParams.FileName = fileInfo.Name;
                uploadFileParams.FileStream = fileInfo.OpenRead();

                httpParam.UploadFiles.Add(uploadFileParams);
            }

            var sdfs = HttpUtils.Post(httpParam);
        }

        static void Login()
        {
            //string url = "http://localhost:8643/login";
            string url = "http://39.155.251.32:8643/login";
            string param = $"userName=admin&password=admin";

            HttpParam httpParam = new HttpParam();

            httpParam.Url = url;
            httpParam.ContentType = "multipart/form-data";

            NameValueCollection nameValues = new NameValueCollection();
            nameValues.Add("userName", "admin");
            nameValues.Add("password", "admin");

            httpParam.RequestParameters = nameValues;

            var fsds = HttpUtils.Post(httpParam);
        }
    }
    public class User
    {
        public int ID { get; set; }

        public string Name { get; set; }
    }

}
