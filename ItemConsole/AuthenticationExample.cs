using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ItemConsole
{
    public class AuthenticationExample
    {
        public static void GetHomeItem()
        {
            // Setup Item request
            var request =
                (HttpWebRequest)WebRequest.Create("http://usergroup/-/item/v1/sitecore/content/home");

            // Username and password
            request.Headers.Add("X-Scitemwebapi-Username", "admin");
            request.Headers.Add("X-Scitemwebapi-Password", "b");

            var response = (HttpWebResponse)request.GetResponse();
            ConsoleExt.WriteJsonStream(response.GetResponseStream());
        }
    }
}
