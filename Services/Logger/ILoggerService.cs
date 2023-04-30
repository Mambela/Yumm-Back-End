namespace Yumm_Back_End.Services.Logger
{
    public interface ILoggerService
    {
        void LogError(string message);
        void Verbose(string message);

        void Error(string message);

        void Fatal(string message);

        void Warning(string message);
    }
}
