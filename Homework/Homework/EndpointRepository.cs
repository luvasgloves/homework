using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;



namespace Homework
{
    class EndpointRepository : IEndpointRepository

    {
        public bool Insert(Endpoint e)
        {
            bool exists = false;
            if (endpointList.Any(x => x.SerialNumber == e.SerialNumber))
                exists = true;

            if (exists == true)
            {
                Console.WriteLine("Endpoint with given serial number already exists");
                return false;
            }
            else
            {
                Console.WriteLine("Endpoint created.");
                endpointList.Add(e);
                return true;
            }
        }

        public List<Endpoint> ListAll()
        {
            return endpointList;
        }


        public bool Edit(Endpoint e)
        {

            endpointList.Where(p => p.SerialNumber == e.SerialNumber).Update(p => p.SwitchState = e.SwitchState);
            return true;
        }

        public static void Update<T>(IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
                action(item);
        }

        public Endpoint Get(string serialNumberId)
        {
            return endpointList.FirstOrDefault(x => x.SerialNumber == serialNumberId);

        }

        public string getSwitchState(int state)
        {
            string s = "S";
            if (state == 0)
                s = "Disconnected";
            else if (state == 1)
                s = "Connected";
            else if (state == 2)
                s = "Armed";
            return s;
        }

        public int setMeterModelId(string id)
        {
            int x = 0;
            if (id == "NSX1P2W")
                x = 16;
            else if (id == "NSX1P3W")
                x = 17;
            else if (id == "NSX2P3W")
                x = 18;
            else if (id == "NSX3P4W")
                x = 19;
            return x;
        }

        public string getMeterModelString(int id)
        {
            string s = "";
            if (id == 16)
                s = "NSX1P2W";
            else if (id == 17)
                s = "NSX1P3W";
            else if (id == 18)
                s = "NSX2P3W";
            else if (id == 19)
                s = "NSX3P4PW";
            return s;
        }

        public bool Delete(string serialNumberId)
        {
            if (!endpointList.Any(x => x.SerialNumber == serialNumberId))
            {
                Console.WriteLine("Endpoint with given serial number was not found");
                return false;
            }
            else
            {
                var item = endpointList.First(x => x.SerialNumber == serialNumberId);
                Console.WriteLine("Do you want do delete Endpoint with serial number " + serialNumberId + "? (Y/N) ");
                string op = Console.ReadLine();
                if (op == "Y")
                {
                    endpointList.Remove(item);
                    Console.WriteLine("Endpoint " + serialNumberId + " deleted.");
                    return true;
                }
                else
                    return false;
            }
        }

        public static List<Endpoint> endpointList = new List<Endpoint>()
    {
        new Endpoint {SerialNumber = "54658JF97", MeterModelId = 16, MeterNumber=32, MeterFirmwareVersion="1.0.2 BETA", SwitchState=0},
        new Endpoint {SerialNumber = "246G8JF93", MeterModelId = 17, MeterNumber=16, MeterFirmwareVersion="1.0.3 RELEASE", SwitchState=1},
        new Endpoint {SerialNumber = "145586F91", MeterModelId = 16, MeterNumber=8, MeterFirmwareVersion="1.0.0 BETA", SwitchState=2},
        new Endpoint {SerialNumber = "31628JG12", MeterModelId = 16, MeterNumber=6, MeterFirmwareVersion="1.0.1 RELEASE", SwitchState=0},
    };

    }


    public static class LinqUpdates
    {

        public static void Update<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
                action(item);
        }

    }

}


