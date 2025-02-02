
using Serilog;

namespace Tanks.Common.Logging
{
    public static class LoggerConfig
    {
        public static void ConfigureLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }
    }
}