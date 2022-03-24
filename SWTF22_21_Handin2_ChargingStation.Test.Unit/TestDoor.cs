using NSubstitute;
using NUnit.Framework;
using SWTF22_21_Handin2_ChargingStation.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWTF22_21_Handin2_ChargingStation.Test.Unit
{
    [TestFixture]
    public class TestDoor
    {
        private readonly IDoor _door;

        public TestDoor()
        {
            _door = new Door();
        }

        [Test]
        public void DoorClosed_CloseDoor_DoorStillClosed()
        {
            _door.Closed = true;
            _door.CloseDoor();
            Assert.IsTrue(_door.Closed);
        }

        [Test]
        public void DoorOpen_OpenDoor_DoorStillOpen()
        {
            _door.Closed = false;
            _door.OpenDoor();
            Assert.IsFalse(_door.Closed);
        }

        [Test]
        public void DoorOpen_CloseDoor_DoorIsClosed()
        {
            _door.Closed = false;
            _door.CloseDoor();
            Assert.IsTrue(_door.Closed);
        }
        
        [Test]
        public void DoorClosed_OpenDoor_DoorIsOpen()
        {
            _door.Closed = true;
            _door.OpenDoor();
            Assert.IsFalse(_door.Closed);
        }

        [Test]
        public void DoorLocked_CloseAndUnlockDoor_DoorIsUnlocked()
        {
            _door.Locked = true;
            _door.CloseDoor();
            _door.UnlockDoor();
            Assert.IsFalse(_door.Locked);
        }

        [Test]
        public void DoorUnlocked_LockDoor_DoorIsLocked()
        {
            _door.Locked = false;
            _door.LockDoor();
            Assert.IsTrue(_door.Locked);
        }

        [Test]
        public void DoorLocked_OpenDoor_DoorIsLocked()
        {
            _door.Locked = true;
            _door.OpenDoor();
            Assert.IsTrue(_door.Locked);
        }

        [Test]
        public void DoorUnlocked_CloseDoor_DoorIsUnlocked()
        {
            _door.Locked = false;
            _door.CloseDoor();
            Assert.IsFalse(_door.Locked);
        }

        [Test]
        public void DoorClosed_OpenDoor_DoorOpenEvent()
        {
            IDoor idoor = Substitute.For<IDoor>();
            _door.DoorMoveEvent +=
                (o, args) =>
                {
                    idoor = args;
                };

            _door.OpenDoor();
            Assert.IsFalse(_door.Closed);
        }

        [Test]
        public void UnlockDoor_DoorIsUnlocked_DoorIsStillUnlocked()
        {
            _door.Locked = false;
            _door.UnlockDoor();
            Assert.IsFalse(_door.Locked);
        }

        [Test]
        public void UnlockDoor_DoorIsNotClosed_DoorIsStillLocked()
        {
            _door.Closed = false;
            _door.Locked = true;
            _door.UnlockDoor();
            Assert.IsTrue(_door.Locked);
        }

        [Test]
        public void LockDoor_DoorIsNotClosed_DoorIsNotLocked()
        {
            _door.UnlockDoor();
            _door.OpenDoor();
            _door.LockDoor();
            Assert.IsFalse(_door.Locked);
        }

        [Test]
        public void LockDoor_DoorIsLocked_DoorIsStillLocked()
        {
            _door.LockDoor();
            _door.LockDoor();
            Assert.IsTrue(_door.Locked);
        }
    }
}
