namespace SWTF22_21_Handin2_ChargingStation.Lib
{
    public class LogFile : ILogFile
    {
        private readonly string filename = "logfile.txt";

        public void WriteToLog(string message, DateTime timeStamp)
        {
            using (var writer = File.AppendText(filename))
            {
                writer.WriteLine(timeStamp + ": " + message);
            }
        }
    }
}
