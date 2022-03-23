using SWTF22_21_Handin2_ChargingStation.Lib;

IUsbCharger usbCharger = new UsbChargerSimulator();
IChargeControl chargeControl = new ChargeControl(usbCharger);
IDoor door = new Door();
IDisplay display = new Display();
IRFIDReader rfidReader = new RFIDReader();
ILogFile logFile = new LogFile();

new StationControl(chargeControl, door, display, usbCharger, rfidReader, logFile);

bool finish = false;
do
{
    string input;
    System.Console.WriteLine("Indtast (E)nd, (O)pen, (C)lose, (R)ead Card, Connect (P)hone: ");
    input = Console.ReadLine();
    if (string.IsNullOrEmpty(input)) continue;

    switch (input[0].ToString().ToUpper())
    {
        case "E":
            System.Environment.Exit(1);
            break;

        case "O":
            door.OpenDoor();
            break;

        case "C":
            door.CloseDoor();
            break;

        case "R":
            Console.WriteLine("Indtast RFID kode");
            rfidReader.EnterCardId(int.Parse(Console.ReadLine()));
            break;

        case "P":
            if (door.Closed)
                Console.WriteLine("Cant connect phone when door is closed");
            else
            {
                if (usbCharger.Connected)
                {
                    Console.WriteLine("Phone is already connected");
                }
                else
                {
                    usbCharger.Connected = true;
                    Console.WriteLine("Phone connected");
                }
            }

            break;
        default:
            break;
    }

} while (!finish);