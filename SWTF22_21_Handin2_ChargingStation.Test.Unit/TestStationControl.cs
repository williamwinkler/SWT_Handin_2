using NSubstitute;
using NUnit.Framework;
using SWTF22_21_Handin2_ChargingStation.Lib;

namespace SWTF22_21_Handin2_ChargingStation.Test.Unit
{
    [TestFixture]
    class TestStationController
    {
        private StationControl _uut;
        private IChargeControl _chargecontrol;
        private IDoor _door;
        private IDisplay _display;
        private IUsbCharger _usbccharge;
        private IRFIDReader _rfid;
        private ILogFile _logfile;

        [SetUp]
        public void Setup()
        {
            _chargecontrol = Substitute.For<IChargeControl>();
            _door = Substitute.For<IDoor>();
            _display = Substitute.For<IDisplay>();
            _usbccharge = Substitute.For<IUsbCharger>();
            _rfid = Substitute.For<IRFIDReader>();
            _logfile = Substitute.For<ILogFile>();

            _uut = new StationControl(_chargecontrol, _door, _display, _usbccharge, _rfid, _logfile);
        }

        [TestCase(123)]
        [TestCase(1234)]
        public void RfidDetected_stateAvailable_OldIdIsSet(int id)
        {
            _uut.State = StationControl.ChargingStationState.Available;
            _door.Closed = true;
            _rfid.ScanEvent += Raise.EventWith(new ScanEventArgs { ID = id });

            Assert.That(_uut.OldId, Is.EqualTo(id));
        }

        [TestCase(123)]
        [TestCase(1234)]
        public void RfidDetected_stateLocked_CardIdMatch_UnlockDoorCalled(int id)
        {
            _uut.State = StationControl.ChargingStationState.Locked;
            _uut.OldId = id;

            _rfid.ScanEvent += Raise.EventWith(new ScanEventArgs { ID = id });

            _door.Received(1).UnlockDoor();
        }
    }
}