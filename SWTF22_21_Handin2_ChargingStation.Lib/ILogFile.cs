namespace SWTF22_21_Handin2_ChargingStation.Lib
{
    public interface ILogFile
    {
        public void WriteToLog(string message, DateTime timeStamp);
    }
}
