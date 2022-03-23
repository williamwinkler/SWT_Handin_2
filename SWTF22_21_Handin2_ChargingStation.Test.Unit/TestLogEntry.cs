using NUnit.Framework;
using SWTF22_21_Handin2_ChargingStation.Lib;
using System;

namespace SWTF22_21_Handin2_ChargingStation.Test.Unit
{
    public class TestLogEntry
    {
        private LogEntry _uut;
      

        [SetUp]
        public void Setup()
        {
            _uut = new LogEntry();
        }

        [TestCase("Testing LogMessage", "2022-03-23")]
        public void LogEntry_AddLogMessageAndDateTime_ReturnSame(string log, DateTime d)
        {
            _uut.LogMessage = log;
            _uut.TimeStamp = d;

            Assert.Multiple(() =>
            {
                Assert.That(_uut.LogMessage, Is.EqualTo(log));
                Assert.That(_uut.TimeStamp, Is.EqualTo(d));
            });
        }
    }
}
