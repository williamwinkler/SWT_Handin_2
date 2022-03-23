using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWTF22_21_Handin2_ChargingStation.Lib
{
    public class LogFile : ILogFile
    {
        public List<LogEntry> Logs { get; set; }

        public LogFile()
        {
            Logs = new List<LogEntry>();
        }

        public void WriteToLog(string message, DateTime timeStamp)
        {
            Logs.Add(new LogEntry { LogMessage = message, TimeStamp = timeStamp });
        }
    }
}
