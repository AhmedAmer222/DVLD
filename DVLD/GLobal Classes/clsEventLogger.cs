using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DVLD.GLobal_Classes
{
    public static class clsEventLogger
    {
        private const string LogSource = "DVLD";
        private const string LogName = "Application";

        private static bool EnsureEventSourceExists()
        {
            try
            {
                if (!EventLog.SourceExists(LogSource))
                    EventLog.CreateEventSource(LogSource, LogName);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static void LoginSuccessfulEvent(string username)
        {
            if (!EnsureEventSourceExists())
                return;

            EventLog.WriteEntry(LogSource, $"User with username {username} logedin at {DateTime.Now}", EventLogEntryType.Information);
        }

        public static void LoginFailedEvent(string username)
        {
            if (!EnsureEventSourceExists())
                return;

            EventLog.WriteEntry(LogSource, $"User with username {username} failed to log in at {DateTime.Now}", EventLogEntryType.Warning);
        }

        public static void UserLoggedOutEvent(string username)
        {
            if (!EnsureEventSourceExists())
                return;

            EventLog.WriteEntry(LogSource, $"User with username {username} logged out at {DateTime.Now}", EventLogEntryType.Information);
        }

        public static void LogError(string errorMessage)
        {
            if (!EnsureEventSourceExists())
                return;

            EventLog.WriteEntry(LogSource, errorMessage, EventLogEntryType.Error);
        }
    }
}
