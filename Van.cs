using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingAssignement2017
{
    class Van : Vehicle
    {

        public Van(float spawnTime)
        {
            
            getAgTimer();//call method that gives the agitation timer of the HGV
            vehType = "Van";//set Van as vehicle type
            this.spawnTime = spawnTime;//save spawn time of the new hgv
            int random = PSManager.rnd.Next(1, 3);
            //set fuel type of the new Van to depending on the random number we generated
            if (random == 1)
            {
                this.FuelType = Fuel.LPG;
            }
            else if (random == 2)
            {
                this.FuelType = Fuel.Diesel;
            }
            else
            {
                Console.WriteLine("Wrong random fuel number");
            }
            this.TankMax = PSManager.rnd.Next(70, 81);//assign a random maximum tank capacity
            this.TankOnArrival = PSManager.rnd.Next(0, (int)this.TankMax/4);//assign a random initial tank level

        }
    }
}
