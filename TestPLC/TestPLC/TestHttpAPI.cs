using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TestPLC
{
    class TestHttpAPI
    {
        /// <summary>
        /// 获取json数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public  string HttpGetJsonAPI(string url)
        {
            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.Method = "GET";
                webRequest.ContentType = "application/json";
                webRequest.Accept = "application/json";
              //  webRequest.Headers.Add("Authorization", GlobalVariable.NowLoginUser.JwtKey);

                HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
                StreamReader reader = new StreamReader(webResponse.GetResponseStream(), Encoding.Default);
                String res = reader.ReadToEnd();
                reader.Close();
                //res = res.Split('=')[1];
               // int x = res.LastIndexOf(";", res.Length); //lastindexof是从最后开始取.indexof才是从前面开始
                //res = res.Substring(0, x);
                return res.Trim();
            }
            catch (Exception ex)
            {

                return null;
            }

        }

        /// <summary>
        /// json字符串转自定义对象
        /// </summary>
        /// <param name="JsonData"></param>
        /// <returns></returns>
        public TestJson2Domain GetJson2Item(string JsonData)
        {
            TestJson2Domain user = new TestJson2Domain();
            //Newtonsoft.Json.Linq.JContainer json = (Newtonsoft.Json.Linq.JContainer)Newtonsoft.Json.JsonConvert.DeserializeObject(JsonData);
            dynamic json = Newtonsoft.Json.Linq.JToken.Parse(JsonData) as dynamic;
            // JObject jo = (JObject)json[0];

            //string str = jo.GetValue("username").ToString();
             user.username = json.username;
            user.address = json.address;
            return user;
        }

    }
}
