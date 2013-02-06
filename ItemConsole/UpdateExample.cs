using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ItemConsole
{
    public class UpdateExample
    {
        public static void UpdateFieldOnChildrenOfHome(string fieldName, string fieldValue)
        {
            // Setup Item request
            var request = WebRequest.CreateHttp("http://usergroup/-/item/v1/sitecore/content/home?scope=c");
            // Note: The scope is set to children ->                                              ^^^^^^^

            // Set username and password headers
            request.Headers.Add("X-Scitemwebapi-Username", "admin");
            request.Headers.Add("X-Scitemwebapi-Password", "b");
            request.Method = "PUT";

            // Create a query string formatted key value list with fields to update
            var data = Encoding.UTF8.GetBytes(string.Format("{0}={1}", HttpUtility.UrlEncode(fieldName), HttpUtility.UrlEncode(fieldValue)));
            request.ContentLength = data.Length;
            request.ContentType = "application/x-www-form-urlencoded";

            // Write post data to request stream
            var requestStream = request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            requestStream.Close();

            var response = (HttpWebResponse)request.GetResponse();
            ConsoleExt.WriteJsonStream(response.GetResponseStream());
        }
    }
}
