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
            else if (!Locked)
            {
                Console.WriteLine("Cannot unlock the door, as it is not locked");
            }
            else if (!Closed)
            {
                Console.WriteLine("Cannot unlock the door as it is not closed");
            }
            else
            {
                Locked = false;
                Console.WriteLine("Door unlocked");
            }
        }

        public void LockDoor()
        {
            if (Locked)
            {
                Console.WriteLine("Door is already locked");
            }
            else if (!Closed)
            {
                Console.WriteLine("Door is not closed. Please close it before trying to lock it");
            }
            else
            {
                Locked = true;
                Console.WriteLine("Door Locked");
            }
        }

        private void OnDoorStateChangedEvent(Door e)
        {
            DoorMoveEvent?.Invoke(this, e);
        }
    }
}
