namespace SWTF22_21_Handin2_ChargingStation.Lib
{
    public class Door : EventArgs, IDoor
    {
        public event EventHandler<Door> DoorMoveEvent;

        public bool Closed { get; set; }
        public bool Locked { get; set; }

        public void CloseDoor()
        {
            Closed = true;
            OnDoorStateChangedEvent(this);
        }

        public void OpenDoor()
        {
            Closed = false;
            OnDoorStateChangedEvent(this);
        }

        public void UnlockDoor()
        {
            Locked = false;
            OnDoorStateChangedEvent(this);
        }

        public void LockDoor()
        {
            Locked = true;
            OnDoorStateChangedEvent(this);
        }

        private void OnDoorStateChangedEvent(Door e)
        {
            DoorMoveEvent?.Invoke(this, e);
        }
    }
}
