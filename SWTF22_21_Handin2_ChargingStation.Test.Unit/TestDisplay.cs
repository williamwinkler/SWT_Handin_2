using NUnit.Framework;
using SWTF22_21_Handin2_ChargingStation.Lib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWTF22_21_Handin2_ChargingStation.Test.Unit
{
    [TestFixture]
    public class TestDisplay
    {
        private readonly IDisplay _display;

        public TestDisplay()
        {
            _display = new Display();
        }

        [TestCase("This is a very long message This is a very long message This is a very long message This is a very long message")]
        [TestCase("This is a normal message")]
        [TestCase("")]
        public void DisplayMessage_ConsolePrint_CorrectMessage(string message)
        {
            var sw = new StringWriter();
            Console.SetOut(sw);

            _display.DisplayMessage(message);
         
            // \r\n is because Console.WriteLine creates a new line
            Assert.AreEqual($"Display: {message}\r\n", sw.ToString());
        }
    }
}
