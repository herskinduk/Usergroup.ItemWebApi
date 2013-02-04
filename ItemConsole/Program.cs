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
            //Console.WriteLine("Get name home item:");
            //Console.WriteLine(AuthenticatedRequest.GetHomeItemName());

            //Console.WriteLine("RSA - Get name home item:");
            //Console.WriteLine(AuthenticatedRsaRequest.GetHomeItemName());

            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine(AuthenticatedRequest.GetHomeItemName() + " " + i.ToString());
                System.Threading.Thread.Sleep(250);
            }

            Console.ReadLine();
        }
    }
}
