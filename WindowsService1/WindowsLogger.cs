using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService1
{
  public  class WindowsLogger : ILogger
    {
        EventLog logger;
        public WindowsLogger(string SourceName)
        {
            if (!EventLog.SourceExists("VkAutoPosting"))
            {                
                EventLog.CreateEventSource("VkAutoPosting", "MyNewLog");
                return;
            }
            logger = new EventLog();
            logger.Source = "VkAutoPosting";
        }
        public void LogErrorMessage(string message)
        {
            logger.WriteEntry(message, EventLogEntryType.Error);
        }

        public void LogInfoMessage(string message)
        {
            logger.WriteEntry(message, EventLogEntryType.Information);
        }

        public void LogWarningMessage(string message)
        {
            logger.WriteEntry(message, EventLogEntryType.Warning);
        }

        public void LogSuccessMessage(string message)
        {
            logger.WriteEntry(message, EventLogEntryType.SuccessAudit);
        }
    }
}
