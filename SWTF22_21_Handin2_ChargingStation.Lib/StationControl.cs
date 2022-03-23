namespace SWTF22_21_Handin2_ChargingStation.Lib
{
    public class StationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        public enum ChargingStationState
        {
            Available,
            Locked,
            DoorOpen
        };

        public ChargingStationState State { get; set; }

        // Her mangler flere member variable
        private ChargingStationState _state;
        private IChargeControl _charging;
        private int _oldId;
        private IDoor _door;
        private IDisplay _display;
        private IUsbCharger _usbCharger;
        private IRFIDReader _rfid;
        private ILogFile _logFile;

        // Her mangler constructor
        public StationControl(IChargeControl charging, IDoor door, IDisplay display, IUsbCharger usbCharger, IRFIDReader rfid, ILogFile logFile)
        {
            _charging = charging;
            _door = door;
            _display = display;
            _usbCharger = usbCharger;
            _rfid = rfid;
            _logFile = logFile;

            State = ChargingStationState.Available;
            _usbCharger.Connected = false;
            _oldId = -1;

        }


        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(Object o, ScanEventArgs e)
        {
            switch (_state)
            {
                case ChargingStationState.Available:
                    // Check for ladeforbindelse
                    if (_charging.IsConnected())
                    {
                        _door.LockDoor();
                        _charging.StartCharging();
                        _oldId = e.ID;
                        _logFile.WriteToLog("Skab låst med RFID: " + e.ID, DateTime.Now);

                        Console.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                        _state = ChargingStationState.Locked;
                    }
                    else
                    {
                        Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                    }
                    break;

                case ChargingStationState.DoorOpen:
                    // Ignore
                    break;

                case ChargingStationState.Locked:
                    // Check for correct ID
                    if (e.ID == _oldId)
                    {
                        _charging.StopCharging();
                        _door.UnlockDoor();
                        _logFile.WriteToLog("Skab låst op med RFID: " + e.ID, DateTime.Now);

                        Console.WriteLine("Tag din telefon ud af skabet og luk døren");
                        _state = ChargingStationState.Available;
                    }
                    else
                    {
                        Console.WriteLine("Forkert RFID tag");
                    }
                    break;
            }
        }

        // Her mangler de andre trigger handlere
    }
}
