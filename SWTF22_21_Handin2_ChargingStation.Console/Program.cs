using SWTF22_21_Handin2_ChargingStation.Lib;

static void Main()
{
    var Logs = new LogFile();

    Logs.WriteToLog("Test", new DateTime());

    Console.WriteLine(Logs.ToString());
}



//bool finish = false;
//do
//{
//    string input;
//    System.Console.WriteLine("Indtast E, O, C, R: ");
//    input = Console.ReadLine();
//    if (string.IsNullOrEmpty(input)) continue;

//    switch (input[0])
//    {
//        case 'E':
//            finish = true;
//            break;

//        case 'O':
//            door.OnDoorOpen();
//            break;

//        case 'C':
//            door.OnDoorClose();
//            break;

//        case 'R':
//            System.Console.WriteLine("Indtast RFID id: ");
//            string idString = System.Console.ReadLine();

//            int id = Convert.ToInt32(idString);
//            rfidReader.OnRfidRead(id);
//            break;

//        default:
//            break;
//    }

//} while (!finish);