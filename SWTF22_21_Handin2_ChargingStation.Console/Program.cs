using SWTF22_21_Handin2_ChargingStation.Lib;

bool finish = false;

IDoor door = new Door();

do
{
    string input;
    System.Console.WriteLine("Indtast E, O, C, R: ");
    input = Console.ReadLine();
    if (string.IsNullOrEmpty(input)) continue;

    switch (input[0])
    {
        case 'E':
            finish = true;
            break;

        case 'O':
            door.OpenDoor();
            break;

        case 'C':
            door.CloseDoor();
            break;

        case 'R':
            System.Console.WriteLine("Indtast RFID id: ");
            string idString = System.Console.ReadLine();

            int id = Convert.ToInt32(idString);
            rfidReader.OnRfidRead(id);
            break;

        default:
            break;
    }

} while (!finish);