using System;

namespace EventLogManager
{
    public class EventLog
    {
        private NLog.Logger _loger;

        public EventLog() => _loger = NLog.LogManager.GetCurrentClassLogger();

        public void LogInfo(string format, params object[] args) => _loger.Info(format, args);

        public void LogWarrning(string format, params object[] args) => _loger.Warn(format, args);

        public void LogError(Exception e, string format, params object[] args) => _loger.Error(e, format, args);

        public void LogFatal(Exception e, string format, params object[] args) => _loger.Fatal(e, format, args);
    }
}