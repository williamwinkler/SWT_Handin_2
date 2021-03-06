using NSubstitute;
using NSubstitute.ReceivedExtensions;
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


        //Rfid handler tests: state Available
        [Test]
        public void StationControl_DoorOpened_SendNoMessageToDisplay()
        {
            _door.DoorMoveEvent += Raise.EventWith(new Door { Closed = false });

            _display.Received(0).DisplayMessage(Arg.Any<string>());
        }


        [Test]
        public void StationControl_DoorClosedAndNotCharging_MessageSendtToDisplay()
        {
            _door.DoorMoveEvent += Raise.EventWith(new Door { Closed = true });

            _display.Received(1).DisplayMessage(Arg.Any<string>());
        }

        [Test]
        public void StationControl_DoorClosedAndCharging_MessageSendtToDisplay()
        {
            _usbCharger.Connected = true;     
            _door.DoorMoveEvent += Raise.EventWith(new Door { Closed = true });
            _display.Received(1).DisplayMessage(Arg.Any<string>());
        }

        [Test]
        public void StationControl_DoorClosedAndCharging_WriteToLog()
        {
            _usbCharger.Connected = true;
            _door.DoorMoveEvent += Raise.EventWith(new Door { Closed = true });
            _logfile.Received(1).WriteToLog(Arg.Any<string>(), Arg.Any<DateTime>());
        }

        [Test]
        public void StationControl_DoorClosedAndCharging_LockDoorReceived()
        {
            _usbCharger.Connected.Returns(true);
            _door.Closed.Returns(true);
            _door.DoorMoveEvent += Raise.EventWith(new Door { Closed = true });

            _rfid.ScanEvent += Raise.EventWith(new ScanEventArgs { ID = 123 });

            _door.Received(1).LockDoor();            
        }

        [Test]
        public void StationControl_DoorClosedAndRFIDEntered_StartCharging()
        {
            _usbCharger.Connected.Returns(true);
            _door.Closed.Returns(true);
            _door.DoorMoveEvent += Raise.EventWith(new Door { Closed = true });

            _rfid.ScanEvent += Raise.EventWith(new ScanEventArgs { ID = 123 });

            _chargecontrol.Received(1).StartCharging();
        }

        [Test]
        public void StationControl_ChargingFinished_StopCharging()
        {
            _usbCharger.Connected.Returns(true);
            _door.Closed.Returns(true);
            _door.DoorMoveEvent += Raise.EventWith(new Door { Closed = true });
            
            // strat charging
            _rfid.ScanEvent += Raise.EventWith(new ScanEventArgs { ID = 123 });
            
            // Enter code to open door thus stopping to charge
            _rfid.ScanEvent += Raise.EventWith(new ScanEventArgs { ID = 123 });

            _chargecontrol.Received(1).StopCharging();
        }

        [Test]
        public void StationControl_RFIDEntered_DoorUnlocked()
        {
            _usbCharger.Connected.Returns(true);
            _door.Closed.Returns(true);
            _door.DoorMoveEvent += Raise.EventWith(new Door { Closed = true });

            // strat charging
            _rfid.ScanEvent += Raise.EventWith(new ScanEventArgs { ID = 123 });

            // Enters code to open door
            _rfid.ScanEvent += Raise.EventWith(new ScanEventArgs { ID = 123 });

            _door.Received(1).UnlockDoor();
        }

        [Test]
        public void StationControl_ChargeWattChange_MessageToDisplay()
        {
            _usbCharger.Connected.Returns(true);
            _door.Closed.Returns(true);
            _door.DoorMoveEvent += Raise.EventWith(new Door { Closed = true });

            // strat charging
            _rfid.ScanEvent += Raise.EventWith(new ScanEventArgs { ID = 123 });

            // door closed displayMessage + charging message = 2
            _display.Received(2).DisplayMessage(Arg.Any<string>());
        }

        //Rfid handler tests: state Available
        [TestCase(123)]
        [TestCase(1234)]
        [TestCase(-1)]
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

            _display.Received(1).DisplayMessage("Charging station is locked and your phone is charging. Use your RFID tag to unlock.");
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

        [TestCase(123)]
        public void RfidDetected_stateAvailable_LockDoorIsCalled(int id)
        {
            _uut.State = StationControl.ChargingStationState.Available;
            _door.Closed = true;
            _usbCharger.Connected = true;

            _rfid.ScanEvent += Raise.EventWith(new ScanEventArgs { ID = id });

            _door.Received(1).LockDoor();
        }

        [TestCase(123)]
        public void RfidDetected_stateAvailable_WriteToLogDoorIsCalled(int id)
        {
            _uut.State = StationControl.ChargingStationState.Available;
            _door.Closed = true;
            _usbCharger.Connected = true;

            _rfid.ScanEvent += Raise.EventWith(new ScanEventArgs { ID = id });

            _logfile.Received(1).WriteToLog("Charging station locked with RFID: " + id, Arg.Any<DateTime>());
        }

        [TestCase(123)]
        public void RfidDetected_stateAvailable_StartChargingIsCalled(int id)
        {
            _uut.State = StationControl.ChargingStationState.Available;
            _door.Closed = true;
            _usbCharger.Connected = true;

            _rfid.ScanEvent += Raise.EventWith(new ScanEventArgs { ID = id });

            _chargecontrol.Received(1).StartCharging();
        }

        //Rfid handler tests: state Locked
        [TestCase(123)]
        public void RfidDetected_stateLocked_CardIdMatch_UnlockDoorCalled(int id)
        {
            _uut.State = StationControl.ChargingStationState.Locked;
            _uut.OldId = id;

            _rfid.ScanEvent += Raise.EventWith(new ScanEventArgs { ID = id });

            _door.Received(1).UnlockDoor();
        }

        [TestCase(50)]
        public void RfidDetected_stateLocked_CardIdMatch_DisplayCorrectMessage(int id)
        {
            _uut.State = StationControl.ChargingStationState.Locked;
            _uut.OldId = id;

            _rfid.ScanEvent += Raise.EventWith(new ScanEventArgs { ID = id });

            _display.Received(1).DisplayMessage("Remove your phone and close the door.");
        }

        [TestCase(50)]
        public void RfidDetected_stateLocked_CardIdMatch_WriteToLogIsCalled(int id)
        {
            _uut.State = StationControl.ChargingStationState.Locked;
            _uut.OldId = id;

            _rfid.ScanEvent += Raise.EventWith(new ScanEventArgs { ID = id });

            _logfile.WriteToLog(Arg.Any<string>(), Arg.Any<DateTime>());
        }

        [TestCase(50)]
        public void RfidDetected_stateLocked_CardIdNoMatch_DisplayCorrectMessage(int id)
        {
            _uut.State = StationControl.ChargingStationState.Locked;

            _rfid.ScanEvent += Raise.EventWith(new ScanEventArgs { ID = id });

            _display.Received(1).DisplayMessage("Wrong RFID tag");
        }

        //Rfid handler tests: state Wrong
        [TestCase(50)]
        public void RfidDetected_stateWrong_default(int id)
        {
            _uut.State = (StationControl.ChargingStationState)200;
            _rfid.ScanEvent += Raise.EventWith(new ScanEventArgs { ID = id });
            _display.Received(1).DisplayMessage("Phone not connected");

        }

        //Door handler tests
        [Test]
        public void DoorClosed_Message_CorrectMessage()
        {
            _door.DoorMoveEvent += Raise.EventWith(new Door { Closed = true });
            _display.Received(1).DisplayMessage("Door Closed");
            _logfile.Received(1).WriteToLog("Door Closed", Arg.Any<DateTime>());
        }


        [Test]
        public void OpenDoor_InAvailableState_StateChangedToOpened()
        {
            _door.Closed = false;

            _door.DoorMoveEvent += Raise.EventWith(this, new Door() { Closed = false });

            Assert.That(_uut.State, Is.EqualTo(StationControl.ChargingStationState.DoorOpen));
        }

        //Charge handler tests
        [TestCase(2)]
        public void ChargeChanged_CurrentUnderFiveShowMessage_DisplayAndLogCalledWithCorrectMessage(int cur)
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = cur });
            _display.Received(1).DisplayMessage("Phone charged");
        }

        [TestCase(2)]
        public void ChargeChanged_CurrentUnderFiveShowMessage_WriteLog(int cur)
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = cur });
            _logfile.Received(1).WriteToLog("Phone charged", Arg.Any<DateTime>());
        }

        [TestCase(1000)]
        [TestCase(502)]
        [TestCase(501)]
        [TestCase(-1)]
        [TestCase(-2)]
        [TestCase(-1000)]
        public void ChargeChanged_CurrentOverFiveHundredShowMessage_ShowMessage(int cur)
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = cur });
            _display.Received(1).DisplayMessage("ERROR! Something wrong with charger");
        }
        
        [TestCase(1000)]
        [TestCase(502)]
        [TestCase(501)]
        [TestCase(-1)]
        [TestCase(-2)]
        [TestCase(-1000)]
        public void ChargeChanged_CurrentOverFiveHundredShowMessage_WriteLog(int cur)
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = cur });
            _logfile.Received(1).WriteToLog("ERROR! Something wrong with charger", Arg.Any<DateTime>());
        }
    }
}