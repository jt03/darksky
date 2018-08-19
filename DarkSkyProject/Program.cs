using ServiceStack;
using ServiceStack.Razor;
using System;

namespace DarkSkyProject
{
    class Program
    {
        public class AppHost : AppSelfHostBase
        {
            //Tell ServiceStack the name of your application and where to find your services
            public AppHost() : base("DarkSkyProject", typeof(DarkSkyService).Assembly)
            {

            }

            public override void Configure(Funq.Container container)
            {
                //register any dependencies your services use, e.g:
                //container.Register<ICacheClient>(new MemoryCacheClient());
                Plugins.Add(new RazorFormat());


            }
        }

        //Self hosting the web-site 
        public static void Main(string[] args)
        {
            var listeningOn = args.Length == 0 ? "http://localhost:1337/" : args[0];
            var appHost = new AppHost()
                .Init()
                .Start(listeningOn);

            Console.WriteLine("AppHost Created at {0}, listening on {1}",
                DateTime.Now, listeningOn);

            System.Diagnostics.Process.Start(listeningOn);
            Console.ReadKey();

        }
    }
}
