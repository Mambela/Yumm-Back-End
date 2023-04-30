using Serilog;

namespace Yumm_Back_End.Services.Logger
{
    public class LoggerService : ILoggerService
    {
        string ErrorLogsPath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), @"Logs\StdLog.txt");
        public void Error(string message)
        {
            throw new System.NotImplementedException();
        }

        public void Fatal(string message)
        {
            throw new System.NotImplementedException();
        }

        public void LogError(string message)
        {
            Log.Logger = new LoggerConfiguration().WriteTo.File(ErrorLogsPath, rollingInterval: RollingInterval.Day).CreateLogger();

            Log.Logger.Error(message);
        }

        public void Verbose(string message)
        {
            throw new System.NotImplementedException();
        }

        public void Warning(string message)
        {
            throw new System.NotImplementedException();
        }
    }
}
