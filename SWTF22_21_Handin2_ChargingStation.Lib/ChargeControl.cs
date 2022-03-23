namespace SWTF22_21_Handin2_ChargingStation.Lib
{
    public class ChargeControl : IChargeControl
    {
        private readonly IUsbCharger _usbCharger;

        public ChargeControl(IUsbCharger usbCharger)
        {
            _usbCharger = usbCharger;
        }

        public bool IsConnected()
        {
            return _usbCharger.Connected;
        }

        public void StartCharging()
        {
            _usbCharger.StartCharge();
        }

        public void StopCharging()
        {
            _usbCharger.StartCharge();
        }
    }
}
