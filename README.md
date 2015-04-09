# Asynchronous_TCP_Chat_Client_in_C_Sharp
A Lightweight C# Chat Client. 

This application is currently capable of being a server and client chat interface. To switch back and forth, simply comment the line in the Unity or Ninject preparation code blocks in Program.cs. 
```cs
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
```
Also, if you prefer a debugging style logger I have included one (using System.Diagnostics). If you have your own logging class, simply implement the ILoggingService Interface and inject it. 
 
```cs
    public interface ILoggingService
    {
        void Log(string message);
    }
```
