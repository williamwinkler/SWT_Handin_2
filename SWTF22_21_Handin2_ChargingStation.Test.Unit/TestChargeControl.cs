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
    public class TestChargeControl
    {
        private IUsbCharger _usbCharger;
        private IChargeControl _uut;


        [SetUp]
        public void Setup()
        {
            _usbCharger = Substitute.For<IUsbCharger>();
            _uut = new ChargeControl(_usbCharger);
        }

        [Test]
        public void StartCharging_StartCharge_IsCalled()
        {
            _uut.StartCharging();
            _usbCharger.Received(1).StartCharge();
        }

        [TestCase(false)]
        [TestCase(true)]
        public void IsConnected_ChargerConnected_True(bool value)
        {
            _usbCharger.Connected = value;

            Assert.That(_uut.IsConnected, Is.EqualTo(value));
        }

        [TestCase(5.0001)]
        [TestCase(10)]
        [TestCase(50)]
        [TestCase(300)]
        [TestCase(450)]
        [TestCase(499.999)]
        public void CurrentChanged_WithinChargingLimits_StopChargingNotCalled(double current)
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current });
            _usbCharger.Received(0).StopCharge();
        }

        [TestCase(500.001)]
        [TestCase(505)]
        [TestCase(10000000)]
        [TestCase(1000)]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(0.00001)]
        [TestCase(4.9)]
        public void ChargeChanged_OutsideLimits_StopCharge(double current)
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current });
            _usbCharger.Received(1).StopCharge();
        }
    }
}
