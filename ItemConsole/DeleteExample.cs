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
    public class DeleteExample
    {
        public static void DeleteItemsUnderHome()
        {
            // Setup Item request
            var request = WebRequest.CreateHttp("http://usergroup/-/item/v1/sitecore/content/home?scope=c");
            // Important: Children scope ->                                                          ^^^^^^^

            // Setup username and password headers
            request.Headers.Add("X-Scitemwebapi-Username", "admin");
            request.Headers.Add("X-Scitemwebapi-Password", "b");
            request.Method = "DELETE";

            var response = (HttpWebResponse)request.GetResponse();
            ConsoleExt.WriteJsonStream(response.GetResponseStream());
        }
    }
}
