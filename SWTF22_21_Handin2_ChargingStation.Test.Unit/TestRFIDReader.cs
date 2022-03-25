using NUnit.Framework;
using SWTF22_21_Handin2_ChargingStation.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWTF22_21_Handin2_ChargingStation.Test.Unit
{
    public class TestRFIDReader
    {

        private ScanEventArgs _receivedScanEventArgs;
        private IRFIDReader _uut;

    
        [SetUp]
        public void Setup()
        {
            _uut = new RFIDReader();
            _receivedScanEventArgs = null;

            _uut.ScanEvent += (o, args) => { _receivedScanEventArgs = args; };
        }

        [Test]
        public void SetCardID_newIdAdded_EventContainsCorrectId()
        {
            _uut.CardID = 69; // B-)

            Assert.That(_receivedScanEventArgs.ID, Is.EqualTo(69));
        }

        [Test]
        public void CardIDPropertySetGet_ValueIsSet()
        {
            _uut.CardID = 69; // B-)

            Assert.That(_uut.CardID, Is.EqualTo(69));
        }

        [Test]
        public void SetCardID_newIdAdded_EventTriggeredAndNotNull()
        {
            _uut.CardID = 69; // B-)

            Assert.That(_receivedScanEventArgs.ID, Is.Not.Null);
        }

        [Test]
        public void CardIDPropertySetToNegativeValue_idErrorCalled()
        {
            _uut.CardID = -69; // :(

            Assert.IsTrue(_uut.Error);
        }

        [TestCase(0)]
        [TestCase(2)]
        [TestCase(99)]
        [TestCase(1342598)]
        public void EnterCardId_enterId_IdChanged(int id)
        {
            _uut.EnterCardId(id);

            Assert.That(_uut.CardID, Is.EqualTo(id));
        }

        [TestCase(-1)]
        [TestCase(-10)]
        public void EnterCardId_enterNegativeId_IdChanged(int id)
        {
            _uut.EnterCardId(69);
            _uut.EnterCardId(id);

            Assert.That(_uut.CardID, Is.EqualTo(69));
        }

    }
}
