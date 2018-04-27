using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace AliexpressHelper
{
    public class HttpHelper
    {
        //after login ,save user cookie
        public static CookieContainer cookiecontainer;

        public static string post(string postData, string uriStr)
        {

            HttpWebRequest requestScore = (HttpWebRequest)WebRequest.Create(uriStr);
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(postData);
            requestScore.Method = "Post";
            requestScore.ContentType = "application/x-www-form-urlencoded";
            requestScore.ContentLength = data.Length;
            requestScore.KeepAlive = true;
            Stream stream = requestScore.GetRequestStream();
            stream.Write(data, 0, data.Length);
            stream.Close();
            HttpWebResponse responseSorce = (HttpWebResponse)requestScore.GetResponse();
            StreamReader reader = new StreamReader(responseSorce.GetResponseStream(), Encoding.UTF8);
            string content = reader.ReadToEnd();
            requestScore = null;
            responseSorce.Close();
            responseSorce = null;
            reader = null;
            stream = null;

            return content;
        }

        public static string get(string url)
        {
            //HttpWebRequest requestScore;
            //requestScore = (HttpWebRequest)WebRequest.Create(url);
            //HttpWebResponse responseSorce = (HttpWebResponse)requestScore.GetResponse();
            //StreamReader reader = new StreamReader(responseSorce.GetResponseStream());
            //string html = reader.ReadToEnd();
            //reader.Close();
            //responseSorce.Close();
            //return html;
            WebClient client = new WebClient();
            return client.DownloadString(url);
        }
    }
}
