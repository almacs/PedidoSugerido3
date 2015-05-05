using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace TasksScheduler
{
    class Program
    {
        static void Main(string[] args)
        {

            clsBackOrder oBackOrderUpdate = new clsBackOrder();
            oBackOrderUpdate.updateBackOrder();
        }
    }
}
