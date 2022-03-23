namespace SWTF22_21_Handin2_ChargingStation.Lib
{
    public interface IChargeControl
    {
        bool IsConnected();
        void StartCharging();
        void StopCharging();
    }
}
