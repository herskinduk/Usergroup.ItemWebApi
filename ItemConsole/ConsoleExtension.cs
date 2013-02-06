using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ItemConsole
{
    public static class ConsoleExt
    {
        public static void WriteJsonStream(Stream stream)
        {
            using (var streamReader = new StreamReader(stream))
            {
                var result = streamReader.ReadToEnd();
                dynamic json = JObject.Parse(result);
                Console.WriteLine(json.ToString());
            }
        }
    }
}
