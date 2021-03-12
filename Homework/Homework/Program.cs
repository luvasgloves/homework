using System;
using System.Collections.Generic;

namespace Homework
{
    class Program
    {
        public static IEndpointRepository endpointRepository = new EndpointRepository();

        public void ListAll()
        {
            Console.WriteLine(new string('*', 20));
            List<Endpoint> endpoints = endpointRepository.ListAll();
            foreach (var e in endpoints)
            {

                Console.WriteLine("\nEndpoint Serial Number: " + e.SerialNumber);
                Console.WriteLine("Endpoint Meter Model Id: " + endpointRepository.getMeterModelString(e.MeterModelId));
                Console.WriteLine("Endpoint Meter Number: " + e.MeterNumber);
                Console.WriteLine("Endpoint Meter Firmware Version: " + e.MeterFirmwareVersion);
                Console.WriteLine("Endpoint Switch State: " + endpointRepository.getSwitchState(e.SwitchState));
            }
        }
        public void Insert()
        {
            Console.WriteLine(new string('*', 20));
            Console.WriteLine("Enter Serial Number, Meter Model Id, Meter Number, Meter Firmware Version And Switch State");

            Console.Write("\nSerial Number: ");
            string serialNumber = Console.ReadLine();

            Console.Write("Meter Model Id: ");
            string meterModelId = Console.ReadLine();
            int intMeterModelId;
            intMeterModelId = endpointRepository.setMeterModelId(meterModelId);
            bool success = false;
            if (intMeterModelId != 0)
                success = true;
            while (!success)
            {
                Console.WriteLine("\nInvalid Meter Model Id. It should be one one the following:");
                Console.WriteLine("NSX1P2W");
                Console.WriteLine("NSX1P3W");
                Console.WriteLine("NSX2P3W");
                Console.WriteLine("NSX3P4W");
                Console.Write("\nMeter Model Id: ");
                meterModelId = Console.ReadLine();
                intMeterModelId = endpointRepository.setMeterModelId(meterModelId);
                if (intMeterModelId != 0)
                    success = true;
            }

            Console.Write("Meter Number : ");
            string meterNumber = Console.ReadLine();
            int intMeterNumber;
            bool success2 = int.TryParse(meterNumber, out intMeterNumber);
            while (!success2)
            {
                Console.WriteLine("Invalid Integer. Try again...");
                Console.Write("Please enter an integer: ");
                meterNumber = Console.ReadLine();
                success2 = int.TryParse(meterNumber, out intMeterNumber);
            }

            Console.Write("Meter Firmware Version : ");
            string meterFirmwareVersion = Console.ReadLine();

            Console.Write("Switch State (0 for Disconnected, 1 for Connected, 2 for Armed): ");
            int switchState = Convert.ToInt32(Console.ReadLine());

            bool success5 = false;
            if (switchState == 0 || switchState == 1 || switchState == 2)
                success5 = true;
            while (!success5)
            {
                Console.WriteLine("Invalid Switch State. It should be 0, 1 or 2.");
                Console.Write("Switch State: ");
                switchState = Convert.ToInt32(Console.ReadLine());
                if (switchState == 0 || switchState == 1 || switchState == 2)
                    success5 = true;
            }
            Endpoint e = new Endpoint
            {
                SerialNumber = serialNumber,
                MeterModelId = intMeterModelId,
                MeterNumber = intMeterNumber,
                MeterFirmwareVersion = meterFirmwareVersion,
                SwitchState = switchState
            };
            endpointRepository.Insert(e);

        }

        public void Edit()
        {
            Console.WriteLine(new string('*', 20));
            //Updating
            Console.WriteLine("What Serial Number do you want to edit? ");
            string serialNumber = Console.ReadLine();

            Endpoint e = endpointRepository.Get(serialNumber);

            if (e == null)
            {
                Console.WriteLine("Endpoint with given serial number was not found! ");
                return;
            }

            Console.WriteLine("Type a Switch State (0 for Disconnected, 1 for Connected, 2 for Armed): ");
            int switchState = Convert.ToInt32(Console.ReadLine());
            bool success = false;
            if (switchState == 0 || switchState == 1 || switchState == 2)
                success = true;
            while (!success)
            {
                Console.WriteLine("Invalid Switch State. It should be 0, 1 or 2.");
                Console.Write("Switch State: ");
                switchState = Convert.ToInt32(Console.ReadLine());
                if (switchState == 0 || switchState == 1 || switchState == 2)
                    success = true;
            }
            e.SwitchState = switchState;
            endpointRepository.Edit(e);
            Console.WriteLine("Endpoint Updated.");
        }

        public void Delete()
        {
            Console.WriteLine(new string('*', 20));

            Console.Write("What Serial Number do you want to delete? ");
            string serialNumber = Console.ReadLine();
            endpointRepository.Delete(serialNumber);
        }

        public void Get()
        {
            Console.WriteLine(new string('*', 20));

            Console.Write("What Serial Number do you want to find? ");
            string serialNumber = Console.ReadLine();
            Endpoint e = endpointRepository.Get(serialNumber);
            Console.WriteLine("");
            if (e == null || e.SerialNumber != serialNumber)
            {
                Console.WriteLine("Endpoint with given serial number was not found!");
                return;
            }
            else if (e.SerialNumber == serialNumber)
            {
                Console.WriteLine("Endpoint Serial Number: " + e.SerialNumber);
                Console.WriteLine("Endpoint Meter Model Id: " + e.MeterModelId);
                Console.WriteLine("Endpoint Meter Number: " + e.MeterNumber);
                Console.WriteLine("Endpoint Meter Firmware Version: " + e.MeterFirmwareVersion);
                Console.WriteLine("Endpoint Switch State: " + endpointRepository.getSwitchState(e.SwitchState));
            }
        }


        public void Menu()
        {
            bool go = true;

            while (go)
            {
                Console.WriteLine(new string('*', 20));

                Console.WriteLine("Hello!");
                Console.WriteLine(new string('*', 20));
                Console.WriteLine("What do you want to do?");
                Console.WriteLine("Insert an Endpoint? Type 1!");
                Console.WriteLine("Edit an Endpoint? Type 2!");
                Console.WriteLine("Delete a Endpoint? Type 3!");
                Console.WriteLine("List All Endpoints? Type 4!");
                Console.WriteLine("Find an Endpoint by Serial Number? Type 5!");
                Console.WriteLine("Exit? Type 6!");
                Console.WriteLine();
                Console.Write("Your Selection :  ");
                int selection = Convert.ToInt32(Console.ReadLine());
                switch (selection)
                {
                    case 1:
                        Insert();
                        break;
                    case 2:
                        Edit();
                        break;
                    case 3:
                        Delete();
                        break;
                    case 4:
                        ListAll();
                        break;
                    case 5:
                        Get();
                        break;
                    case 6:
                        go = false;
                        break;
                    default:
                        throw new InvalidOperationException("Unknown item type");
                }

                Console.WriteLine(new string('*', 20));
            }
        }

        static void Main(string[] args)
        {

            Program p = new Program();
            p.Menu();

            Console.Write("Exiting... ");
            return;
        }
    }
}
