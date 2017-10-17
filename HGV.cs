using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingAssignement2017
{
    class HGV : Vehicle
    {

        public HGV(float spawnTime)
        {
            getAgTimer(); //call method that gives the agitation timer of the HGV
            vehType = "HGV"; //set HGV as vehicle type
            this.spawnTime = spawnTime; //save spawn time of the new hgv
            FuelType = Fuel.Diesel; //set fuel type of the new HGV to Diesel
            this.TankMax = PSManager.rnd.Next(140, 151); //assign a random maximum tank capacity
            this.TankOnArrival = PSManager.rnd.Next(0, (int)this.TankMax/4); //assign a random initial tank level
        }
    }
}
