using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Collections;

namespace ServiceInterfaceFramework.WebReq
{
    public abstract class SignOnMockerBase
    {
        #region Properties
        private CookieContainer cc = new CookieContainer();
        private bool isSigned = false;
        private string realTimeUrl = string.Empty;

        /// <summary>
        /// 实时的Response.Uri
        /// </summary>
        public string RealTimeUrl
        {
            get { return realTimeUrl; }
        }

        /// <summary>
        /// 是否已经登录
        /// </summary>
        public bool IsLogin { get { return isSigned; } set { isSigned = value; } }

        /// <summary>
        /// 页面编码类型
        /// </summary>
        public string EncodingType { get; set; }

        private int timeOut = 5000;
        /// <summary>
        /// 请求超时时间
        /// </summary>
        public int TimeOut
        {
            get { return timeOut; }
            set { timeOut = value; }
        }

        #endregion

        #region 登录模拟段
        /// <summary>
        /// 模拟登录
        /// 1)请求登录页面地址；
        /// 2)构造PostString，用于回发
        /// </summary>
        /// <param name="loginHtml">登陆地址</param>
        public virtual string MockLogin(string html, string userName, string passWord, string validCode)
        {
            var requestPost = RequestMethodPost(realTimeUrl, cc);
            var postaDatas = Encoding.GetEncoding(EncodingType).GetBytes(LoginStringToBePosted(html, userName, passWord, validCode));
            requestPost.ContentLength = postaDatas.Length;
            requestPost.GetRequestStream().Write(postaDatas, 0, postaDatas.Length);

            var response = requestPost.GetResponse() as HttpWebResponse;

            html = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(EncodingType)).ReadToEnd();

            realTimeUrl = response.ResponseUri.ToString();
            realTimeUrl = realTimeUrl.Substring(0, realTimeUrl.IndexOf("?") + 1);

            isSigned = true;

            return html;
        }

        protected virtual string LoginStringToBePosted(string html, string userName, string passWord)
        {
            return string.Empty;
        }

        protected virtual string LoginStringToBePosted(string html, string userName, string passWord, string validCode)
        {
            return string.Empty;
        }

        #endregion

        public void AddCookie(string name, string value)
        {
            var cookie = new Cookie();

            var tmpCookie = GetAnyCookie(this.cc);
            if (tmpCookie != null)
            {
                cookie.Domain = tmpCookie.Domain;
            }
            cookie.Name = name;
            cookie.Value = value;

            this.cc.Add(cookie);
        }

        public static Cookie GetAnyCookie(CookieContainer cc)
        {
            List<Cookie> lstCookies = new List<Cookie>();
            Hashtable table = (Hashtable)cc.GetType().InvokeMember("m_domainTable",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField |
                System.Reflection.BindingFlags.Instance, null, cc, new object[] { });

            foreach (object pathList in table.Values)
            {
                SortedList lstCookieCol = (SortedList)pathList.GetType().InvokeMember("m_list",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField
                    | System.Reflection.BindingFlags.Instance, null, pathList, new object[] { });
                foreach (CookieCollection colCookies in lstCookieCol.Values)
                    foreach (Cookie c in colCookies)
                        return c;
            }
            return null;
        }

        public static List<Cookie> GetAllCookies(CookieContainer cc)
        {
            List<Cookie> lstCookies = new List<Cookie>();
            Hashtable table = (Hashtable)cc.GetType().InvokeMember("m_domainTable",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField |
                System.Reflection.BindingFlags.Instance, null, cc, new object[] { });

            foreach (object pathList in table.Values)
            {
                SortedList lstCookieCol = (SortedList)pathList.GetType().InvokeMember("m_list",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField
                    | System.Reflection.BindingFlags.Instance, null, pathList, new object[] { });
                foreach (CookieCollection colCookies in lstCookieCol.Values)
                    foreach (Cookie c in colCookies) lstCookies.Add(c);
            }
            return lstCookies;
        }

        #region 请求和回发
        public virtual string Post(string expectedUrl, string postString)
        {
            var requestPost = RequestMethodPost(expectedUrl, cc);
            requestPost.Timeout = TimeOut;

            var postaDatas = Encoding.GetEncoding(EncodingType).GetBytes(postString);
            requestPost.ContentLength = postaDatas.Length;
            requestPost.GetRequestStream().Write(postaDatas, 0, postaDatas.Length);

            var response = requestPost.GetResponse() as HttpWebResponse;

            string result = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(EncodingType)).ReadToEnd();

            realTimeUrl = response.ResponseUri.ToString();

            return result;
        }

        public string Post(string expectedUrl, string html, IPostString postString)
        {
            return Post(expectedUrl, postString.GetConstructPostString(html));
        }

        public virtual string Get(string expectedUrl)
        {
            var requestGet = RequestMethodGet(expectedUrl, cc);
            requestGet.Timeout = TimeOut;

            var response = requestGet.GetResponse() as HttpWebResponse;
            var htmlResult = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(EncodingType)).ReadToEnd();
            response.Cookies = requestGet.CookieContainer.GetCookies(requestGet.RequestUri);
            realTimeUrl = response.ResponseUri.ToString();
            return htmlResult;
        }

        /// <summary>
        /// 下载图片（读取验证码），使保持在同一个CookieContainer内
        /// </summary>
        /// <param name="imgUrl"></param>
        /// <returns></returns>
        public Stream GetImage(string imgUrl)
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(imgUrl);
            //设置前面请求时保存下来的cookie，以保证两个请求是相同的，才不会出现验证码不同步的问题
            request.CookieContainer = cc;

            var response = (HttpWebResponse)request.GetResponse();
            return response.GetResponseStream();
        }


        #endregion

        #region 私有方法

        //得到一个HttpRequest
        private static HttpWebRequest CreateRequest(string url)
        {
            HttpWebRequest myRequest = WebRequest.Create(url) as HttpWebRequest;
            myRequest.AllowAutoRedirect = true;
            myRequest.MaximumAutomaticRedirections = 3;
            myRequest.KeepAlive = true;
            myRequest.Timeout = 10000;
            myRequest.Accept = "html/text";
            myRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.2; Trident/4.0; .NET CLR 1.1.4322; .NET CLR 2.0.50727; InfoPath.2; .NET4.0C; .NET4.0E; .NET CLR 3.0.4506.2152)";
            return myRequest;
        }

        //具有GET方法的HttpRequest
        private HttpWebRequest RequestMethodGet(string url, CookieContainer cc)
        {
            HttpWebRequest request;

            request = CreateRequest(url);
            request.Method = "GET";
            request.CookieContainer = cc;
            return request;
        }

        //具有POST方法的HttpRequest
        private HttpWebRequest RequestMethodPost(string url, CookieContainer cc)
        {
            HttpWebRequest request;

            request = CreateRequest(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.CookieContainer = cc;
            return request;


        }
        #endregion
    }
}
