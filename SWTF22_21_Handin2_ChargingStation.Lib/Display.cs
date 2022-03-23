namespace SWTF22_21_Handin2_ChargingStation.Lib
{
    public class Display : IDisplay
    {
        public void DisplayMessage(string message)
        {
            Console.WriteLine($"Display: {message}");
        }
    }
}
