using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            UpdateItemRequest.UpdateFieldOnHomeItem("title", "new title...");

            //Console.WriteLine("Get name home item:");
            //Console.WriteLine(AuthenticatedRequest.GetHomeItemDisplayName());

            //Console.WriteLine("RSA - Get name home item:");
            //Console.WriteLine(AuthenticatedRsaRequest.GetHomeItemDisplayName());

            //for (int i = 0; i < 20; i++)
            //{
            //    Console.WriteLine(AuthenticatedRequest.GetHomeItemDisplayName() + " " + i.ToString());
            //    System.Threading.Thread.Sleep(250);
            //}

            Console.ReadLine();
        }
    }
}
