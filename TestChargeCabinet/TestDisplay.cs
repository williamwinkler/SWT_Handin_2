using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestChargeCabinet
{
    public class TestDisplay
    {
        private IDisplay _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new Display();
        }

        [Test]
        public void testDisplayMessage()
        {
            _uut.DisplayMessage("testDisplayMessage virker!");
        }
    }
}
