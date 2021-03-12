using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;



namespace Homework
{
    interface IEndpointRepository
    {

        List<Endpoint> ListAll();
        bool Insert(Endpoint endpoint);
        Endpoint Get(string serialNumberId);
        bool Edit(Endpoint endpoint);
        bool Delete(string serialNumberId);
        string getSwitchState(int state);

        int setMeterModelId(string id);

        string getMeterModelString(int id);
    }
}
