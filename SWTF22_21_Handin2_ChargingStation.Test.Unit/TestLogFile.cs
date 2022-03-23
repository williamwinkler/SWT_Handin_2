using System;
using NUnit.Framework;
using SWTF22_21_Handin2_ChargingStation.Lib;
using System.IO;

namespace SWTF22_21_Handin2_ChargingStation.Test.Unit
{
    public class TestLogFile
    {
        private LogFile _uut;
        private DateTime _firstDateTime;
        private DateTime _secondDataTime;

        [SetUp]
        public void setup()
        {
            _uut = new LogFile();
            _firstDateTime = DateTime.Now;
            _secondDataTime = new DateTime(2022, 1, 1);
            File.Delete("logfile.txt");
        }

        [TestCase("Testing logging")]
        public void WriteToLog_Insert1Log_LogFileContains1Line(string logMessage)
        {
            _uut.WriteToLog(logMessage, _firstDateTime);

            int count = File.ReadAllLines("logfile.txt").Length;

            Assert.That(count, Is.EqualTo(1));
        }

        [TestCase("Testing logging line 1", "Testing loggin line 2")]
        public void WriteToLog_Insert2Logs_LogFileContains2Lines(string logMessage1, string logMessage2)
        {
            // remove logs from previous tests
            File.Delete("logfile.txt");

            _uut.WriteToLog(logMessage1, _firstDateTime);
            _uut.WriteToLog(logMessage2, _secondDataTime);


            var count = File.ReadAllLines("logfile.txt").Length;
            
            Assert.That(count, Is.EqualTo(2));
        }

        [TestCase("Testing logging line 1", "Testing loggin line 2")]
        public void WriteToLog_Insert2Logs_LogFileContainsCorrectLgogs(string logMessage1, string logMessage2)
        {
           
            File.Delete("logfile.txt");  // remove logs inserted in previous tests

            _uut.WriteToLog(logMessage1, _firstDateTime);
            _uut.WriteToLog(logMessage2, _secondDataTime);

            var stringsInLogFile = File.ReadAllLines("logfile.txt");

            Assert.Multiple(() =>
            {
                Assert.IsTrue(stringsInLogFile[0].ToString() == (string)(_firstDateTime + ": " + logMessage1));
                Assert.IsTrue(stringsInLogFile[1].ToString() == (string)(_secondDataTime + ": " + logMessage2));
            });
        }
        
        


    }
}
