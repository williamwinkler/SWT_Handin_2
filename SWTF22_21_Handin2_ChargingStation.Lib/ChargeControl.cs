namespace SWTF22_21_Handin2_ChargingStation.Lib
{
    public class ChargeControl : IChargeControl
    {
        private readonly IUsbCharger _usbCharger;

        public double ChargeCurrent { get; set; }

        public ChargeControl(IUsbCharger usbCharger)
        {
            _usbCharger = usbCharger;
            _usbCharger.CurrentValueEvent += ChargingValueChangedHandler;
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
            _usbCharger.StopCharge();
        }

        private void ChargingValueChangedHandler(object sender, CurrentEventArgs currentEvent)
        {
            ChargeCurrent = currentEvent.Current;
            if (ChargeCurrent <= 5 && ChargeCurrent > 0)
            {
                StopCharging();
            }
            else if (ChargeCurrent > 500)
            {
                StopCharging();
            }
        }
    }
}
