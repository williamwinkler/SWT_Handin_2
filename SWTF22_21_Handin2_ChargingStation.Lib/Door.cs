namespace SWTF22_21_Handin2_ChargingStation.Lib
{
    public class Door : EventArgs, IDoor
    {
        public event EventHandler<Door> DoorMoveEvent;

        public bool Closed { get; set; }
        public bool Locked { get; set; }

        public Door()
        {
            Locked = false;
            Closed = true;
        }

        public void CloseDoor()
        {
            if (!Closed)
            {
                Closed = true;
                Console.WriteLine("Door is now closed");
                OnDoorStateChangedEvent(this);
            }
            else
            {
                Console.WriteLine("Door is already closed");
            }
        }

        public void OpenDoor()
        {
            if (Locked)
            {
                Console.WriteLine("Door is locked. Please unlock it first"); ;
            }
            else if (!Closed)
            {
                Console.WriteLine("Door is already open"); ;
            }
            else
            {
                Closed = false;
                Console.WriteLine("Door is now open");
                OnDoorStateChangedEvent(this);
            }
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
