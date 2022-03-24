using NSubstitute;
using NUnit.Framework;
using SWTF22_21_Handin2_ChargingStation.Lib;
using System;

namespace SWTF22_21_Handin2_ChargingStation.Test.Unit
{
    [TestFixture]
    class TestStationController
    {
        private StationControl _uut;
        private IChargeControl _chargecontrol;
        private IDoor _door;
        private IDisplay _display;
        private IUsbCharger _usbCharger;
        private IRFIDReader _rfid;
        private ILogFile _logfile;

        [SetUp]
        public void Setup()
        {
            _chargecontrol = Substitute.For<IChargeControl>();
            _door = Substitute.For<IDoor>();
            _display = Substitute.For<IDisplay>();
            _usbCharger = Substitute.For<IUsbCharger>();
            _rfid = Substitute.For<IRFIDReader>();
            _logfile = Substitute.For<ILogFile>();

            _uut = new StationControl(_chargecontrol, _door, _display, _usbCharger, _rfid, _logfile);
        }

        //Rfid handler tests
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
        public void RfidDetected_stateAvailable_DisplayMessageIsCalled(int id)
        {
            _uut.State = StationControl.ChargingStationState.Available;
            _door.Closed = true;
            _usbCharger.Connected = true;
            _rfid.ScanEvent += Raise.EventWith(new ScanEventArgs { ID = id });

            _display.Received(1).DisplayMessage(Arg.Any<string>());
        }


        [TestCase(123)]
        public void RfidDetected_stateLocked_CardIdMatch_UnlockDoorCalled(int id)
        {
            _uut.State = StationControl.ChargingStationState.Locked;
            _uut.OldId = id;

            _rfid.ScanEvent += Raise.EventWith(new ScanEventArgs { ID = id });

            _door.Received(1).UnlockDoor();
        }

        [TestCase(123)]
        public void RfidDetected_stateAvailabe_DoorClosedChargerConnected_StateChanged(int id)
        {
            _uut.State = StationControl.ChargingStationState.Available;
            _door.Closed = true;
            _usbCharger.Connected = true;

            _rfid.ScanEvent += Raise.EventWith(new ScanEventArgs { ID = id });

            Assert.That(_uut.State, Is.EqualTo(StationControl.ChargingStationState.Locked));
        }



        //Door handler tests
        [Test]
        public void DoorClosed_Message_CorrectMessage()
        {
            _door.DoorMoveEvent += Raise.EventWith(new Door { Closed = true });
            _display.Received(1).DisplayMessage("Door Closed");
            _logfile.Received(1).WriteToLog("Door Closed", Arg.Any<DateTime>());
        }
    }
}