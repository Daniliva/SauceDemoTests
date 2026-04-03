using Serilog;

namespace SauceDemoTests.Utilities
{
    public class LoggerManager
    {
        private static readonly object Lock = new object();
        private static LoggerManager? instance;

        private LoggerManager()
        {
            this.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .CreateLogger();
        }

        public static LoggerManager? Instance
        {
            get
            {
                lock (Lock)
                {
                    instance ??= new LoggerManager();
                    return instance;
                }
            }
        }

        public ILogger Logger { get; }
    }
}