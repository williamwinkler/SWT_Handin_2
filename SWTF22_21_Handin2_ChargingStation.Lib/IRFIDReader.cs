namespace SWTF22_21_Handin2_ChargingStation.Lib
{
    public interface IRFIDReader
    {
        public int CardID { get; set; }
        public bool Error { get; set; }
        event EventHandler<ScanEventArgs> ScanEvent;
        void EnterCardId(int id);

    }
}
