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
    public class CreateExample
    {
        public static void CreateItemUnderHome(string itemName, string template)
        {
            // Setup Item request
            var request = WebRequest.CreateHttp(
                string.Format(
                    "http://usergroup/-/item/v1/sitecore/content/home?name={0}&template={1}", 
                    HttpUtility.UrlEncode(itemName),
                    HttpUtility.UrlEncode(template)));

            // Set username and password headers
            request.Headers.Add("X-Scitemwebapi-Username", "admin");
            request.Headers.Add("X-Scitemwebapi-Password", "b");
            request.Method = "POST";

            request.ContentLength = 0;

            var response = (HttpWebResponse)request.GetResponse();
            ConsoleExt.WriteJsonStream(response.GetResponseStream());
        }

        #region CreateMediaItem
		public static void CreateMediaItem(string itemName, string filePath)
        {
            // Setup Item request
            var request = WebRequest.CreateHttp(
                "http://usergroup/-/item/v1/sitecore/media%20library?name=" + 
                HttpUtility.UrlEncode(itemName));

            // Create boundry marker
            var boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");

            // Set username/password and content type headers
            request.Headers.Add("X-Scitemwebapi-Username", "admin");
            request.Headers.Add("X-Scitemwebapi-Password", "b");
            request.Method = "POST";
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            request.KeepAlive = true;
            var requestStream = request.GetRequestStream();

            // Write part boundry
            byte[] boundarybytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
            requestStream.Write(boundarybytes, 0, boundarybytes.Length);

            // Write part header
            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n Content-Type: application/octet-stream\r\n\r\n";
            string header = string.Format(headerTemplate, itemName, filePath);
            byte[] headerbytes = Encoding.UTF8.GetBytes(header);
            requestStream.Write(headerbytes, 0, headerbytes.Length);

            // Write part data
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[4096];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                requestStream.Write(buffer, 0, bytesRead);
            }
            fileStream.Close();

            // Write trailing boundry
            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            requestStream.Write(trailer, 0, trailer.Length);
            requestStream.Close();

            var response = (HttpWebResponse)request.GetResponse();
            ConsoleExt.WriteJsonStream(response.GetResponseStream());
        }
        #endregion
    }
}
