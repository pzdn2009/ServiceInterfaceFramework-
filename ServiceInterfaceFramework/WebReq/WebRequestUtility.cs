using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace ServiceInterfaceFramework.WebReq
{
    public class WebRequestUtility
    {
        public static WebRequest CreateUrl(string url, string apiToken)
        {
            var request = WebRequest.Create(url);
            if (!string.IsNullOrEmpty(apiToken))
            {
                SetHeader(request, apiToken);
            }

            request.ContentType = "text/xml; charset=utf-8";
            return request;
        }

        public static void SetHeader(WebRequest request, string apiToken)
        {
            request.ContentType = "text/xml; charset=utf-8";
            request.Headers.Add("Authorization", "basic " + apiToken);
        }

        public static string GetResponse(WebRequest request)
        {
            Func<WebResponse, string> getResponseContent = wr =>
            {
                if (wr == null)
                    return string.Empty;

                using (var reader = new StreamReader(wr.GetResponseStream(), Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            };

            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    return getResponseContent(response);
                }
            }
            catch (WebException ex)
            {
                return getResponseContent(ex.Response);
            }
        }
    }
}
