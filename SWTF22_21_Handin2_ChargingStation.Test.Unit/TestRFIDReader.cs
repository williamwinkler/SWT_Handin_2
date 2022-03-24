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


        

    }
}
