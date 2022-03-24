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

        [Test]

    }
}