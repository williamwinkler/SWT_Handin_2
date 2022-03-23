using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWTF22_21_Handin2_ChargingStation.Lib
{
    public interface ILogFile
    {
        public void WriteToLog(string message, DateTime timeStamp);
    }
}
