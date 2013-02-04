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
    public class AuthenticatedRequest
    {
        public static string GetHomeItemDisplayName()
        {
            // Setup Item request
            var request = 
                (HttpWebRequest)WebRequest.Create("http://sitecoreukug/-/item/v1/sitecore/content/home");

            // Username and password
            request.Headers.Add("X-Scitemwebapi-Username", "admin");
            request.Headers.Add("X-Scitemwebapi-Password", "b");

            var response = (HttpWebResponse)request.GetResponse();

            var name = "";
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                // Parse JSON and fish out item name
                dynamic json = JObject.Parse(result);
                if (json.status != "OK") throw new Exception(json.ToString());

                name = json.result.items[0].DisplayName;
            }
            return name;
        }
    }
}
