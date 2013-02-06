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
            AuthenticationExample.GetHomeItem();

            RsaAuthenticationExample.GetHomeItem();

            CreateExample.CreateItemUnderHome("Child Item", "Sample/Sample Item");

            UpdateExample.UpdateFieldOnChildrenOfHome("Title", "A new title...");
            
            DeleteExample.DeleteItemsUnderHome();

            //CreateExample.CreateMediaItem("pizza", "pizza_0000.png");

            //for (int i = 0; i < 20; i++)
            //{
            //    AuthenticationExample.GetHomeItem();
            //    Console.WriteLine(">>> " + i.ToString());
            //    System.Threading.Thread.Sleep(100);
            //}

            Console.ReadLine();
        }
    }
}
