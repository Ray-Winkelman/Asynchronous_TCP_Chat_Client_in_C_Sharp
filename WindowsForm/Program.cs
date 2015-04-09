using System;
using System.Windows.Forms;
using Microsoft.Practices.Unity;
using Ninject;
using Ninject.Modules;
using Interfaces;
using Logger;
using System.Reflection;
//using w0269804.Logger;
using Chat;

namespace Assingment_2
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            /* Right now, I'm only using this locally.
             * a feature to add could be an option to provide an 
             * IP address and port at runtime. */
            const string IP_ADDRESS = "127.0.0.1";
            const int PORT = 13000;

            Bind<ChatForm>().ToSelf().WithConstructorArgument("ipaddress", IP_ADDRESS)
            .WithConstructorArgument("port", PORT);

            Bind<ILoggingService>().To<ConsoleLogger>();
            Bind<IChatService>().To<Client>();

            /* -- THE FOLLOWING FOR NINJECTING ANOTHER LOGGING DLL -- 
            Bind<ILoggingService>().To<w0269804.Logger.CustomLogger>(); */
        }
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            /* ---------- THE FOLLOWING FOR UNITY INJECTION ---------- */
            //UnityContainer container = new UnityContainer();
            //container.RegisterType<ILoggingService, TextFileLogger>();
            //container.RegisterInstance<ILoggingService>(new TextFileLogger());         
            //Application.Run(container.Resolve<ChatForm>());

            /* ---------- THE FOLLOWING FOR NINJECTION ---------- */
            IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            Application.Run(kernel.Get<ChatForm>());
        }
    }
}
