namespace SWTF22_21_Handin2_ChargingStation.Lib
{
    public interface IDoor
    {
        public event EventHandler<Door> DoorMoveEvent;

        public bool Closed { get; set; }
        public bool Locked { get; set; }

        public void OpenDoor();

        public void CloseDoor();
        public void UnlockDoor();
        public void LockDoor();
    }
}
