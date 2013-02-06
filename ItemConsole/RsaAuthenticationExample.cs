using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ItemConsole
{
    public class RsaAuthenticationExample
    {
        public static void GetHomeItem()
        {
            // Aquire public key
            var publicKeyRequest = 
                (HttpWebRequest)WebRequest.Create("http://sitecoreukug/-/item/v1/-/actions/getpublickey");
            var publicKeyResponse = (HttpWebResponse)publicKeyRequest.GetResponse();
            var publicKey = new StreamReader(publicKeyResponse.GetResponseStream()).ReadToEnd();

            // Setup Item request
            var request = 
                (HttpWebRequest)WebRequest.Create("http://sitecoreukug/-/item/v1/sitecore/content/home");

            // Tell Sitecore that credentails are encrypted
            request.Headers.Add("X-Scitemwebapi-Encrypted", "1");

            // Username and password
            request.Headers.Add("X-Scitemwebapi-Username", RsaEncrypt("admin", publicKey));
            request.Headers.Add("X-Scitemwebapi-Password", RsaEncrypt("b", publicKey));

            var response = (HttpWebResponse)request.GetResponse();
            ConsoleExt.WriteJsonStream(response.GetResponseStream());
        }

        /// <summary>
        /// RSA Encrypts a string of data and returns the encrypted data as a Base64 string
        /// </summary>
        /// <param name="data">Clear text data to be encrypted</param>
        /// <param name="publicKey">RSA xml config</param>
        /// <returns>A Base64 encoded string representing the encrypted data</returns>
        private static string RsaEncrypt(string data, string publicKey)
        {
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.FromXmlString(publicKey);
                return Convert.ToBase64String(RSA.Encrypt(Encoding.UTF8.GetBytes(data), false));
            }
        }
    }
}
