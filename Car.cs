using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingAssignement2017
{
    public class Car : Vehicle
    {

        public Car(float spawnTime) //Car constructor. Takes time as an argument.
        {
            FillingTimer = 0; //set filling timer original value
            getAgTimer(); //call method that gets the Agitation timer of the new car
            vehType = "Car"; //set vehicle type to Car
            this.spawnTime = spawnTime; //save the time that vehicle was created for potential use later
            int random = PSManager.rnd.Next(1, 4); //get a random number between 1 and 3 that will be used to get the fuel type
            if (random == 1)
            {
                this.FuelType = Fuel.Unleaded;
            }
            else if (random == 2)
            {
                this.FuelType = Fuel.Diesel;
            }
            else if (random == 3)
            {
                this.FuelType = Fuel.LPG;
            }
            else
            {
                Console.WriteLine("Wrong random fuel number"); //this is here to check for potential errors
            }
            this.TankMax = PSManager.rnd.Next(30, 41); //assign a random maximum tank capacity
            this.TankOnArrival = PSManager.rnd.Next(0, (int)this.TankMax/4); //assign a random initial tank level
                        
        }
    }
}
