using System;
using System.Diagnostics;
using MailApi.Properties;
using ServiceStack.Text;

namespace MailApi
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var app = new AppHost().Init().Start(string.Format("http://*:{0}/",Settings.Default.listenPort));
            var msg = string.Format("ServiceStack SelfHost listening at http://localhost:{0}",
                Settings.Default.listenPort);
            msg.Print();
            Process.Start(string.Format("http://localhost:{0}/",Settings.Default.listenPort));
            Console.ReadLine();
        }
    }
}