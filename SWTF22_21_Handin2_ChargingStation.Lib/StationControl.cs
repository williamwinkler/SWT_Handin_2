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
        public double ChargeWatt { get; set; }

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
        private void RfidDetected(object o, ScanEventArgs e)
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
                        _logFile.WriteToLog("Charging station locked with RFID: " + e.ID, DateTime.Now);

                        Console.WriteLine("Charging station is locked and your phone is charging. Use your RFID tag to unlock.");
                        _state = ChargingStationState.Locked;
                    }
                    else
                    {
                        Console.WriteLine("Your phone is not connected. Try again.");
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
                        _logFile.WriteToLog("Charging station unlocked with RFID: " + e.ID, DateTime.Now);

                        Console.WriteLine("Remove your phone and close the door.");
                        _state = ChargingStationState.Available;
                    }
                    else
                    {
                        Console.WriteLine("Wrong RFID tag");
                    }
                    break;
            }
        }

        private void DoorMoveHandler(object o, IDoor door)
        {
            if (door.Closed)
            {
                _state = ChargingStationState.Available;
                _display.DisplayMessage("Door Closed");
                _logFile.WriteToLog("Door Closed", DateTime.Now);
            }
            else
            {
                _state |= ChargingStationState.DoorOpen;
            }         
        }

        private void ChargingValueChangedHandler(object sender, CurrentEventArgs currentEvent)
        {
            ChargeWatt = currentEvent.Current;

            if (ChargeWatt > 0 && ChargeWatt <=5)
            {
                _display.DisplayMessage("Phone charged");
                _logFile.WriteToLog("Phone charged", DateTime.Now);
            }
            else if (ChargeWatt > 500)
            {
                _display.DisplayMessage("ERROR! Something wrong with charger");
                _logFile.WriteToLog("ERROR! Something wrong with charger", DateTime.Now);
            }
        }
    }
}
