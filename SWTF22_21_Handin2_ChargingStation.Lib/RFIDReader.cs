namespace SWTF22_21_Handin2_ChargingStation.Lib
{
    public class RFIDReader : IRFIDReader
    {
        public void EnterCardId(int id)
        {
            this.CardID = id;
        }
        protected virtual void OnScanEvent(ScanEventArgs eventArgs)
        {
            ScanEvent?.Invoke(this, eventArgs);
        }

        private int cardID;
        private bool error;
        public bool Error { 
            get { return error; }
            set { error = value; } }
        public int CardID
        {
            get { return cardID; }

            set
            {
                if (0 <= value)
                {
                    Error = false;
                    OnScanEvent(new ScanEventArgs { ID = value });
                    cardID = value;
                }
                else
                {
                    Error = true;
                    Console.WriteLine("CardID not positive! Please enter postive CardID");
                }
            }

        }


        // test
        public event EventHandler<ScanEventArgs> ScanEvent;
    }

    public class ScanEventArgs : EventArgs
    {
        public int ID { get; set; }
    }
}


