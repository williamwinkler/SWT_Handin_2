namespace SWTF22_21_Handin2_ChargingStation.Lib
{
    public class Door : IDoor
    {
        public bool Closed { get; set; }
        public bool Locked { get; set; }

        public void CloseDoor()
        {
            Closed = true;
        }

        public void OpenDoor()
        {
            Closed = false;
        }

        public void UnlockDoor()
        {
            Locked = false;
        }

        public void LockDoor()
        {
            Locked = true;
        }
    }
}
