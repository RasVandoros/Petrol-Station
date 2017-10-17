using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingAssignement2017
{
    static class VehGenerator
    {

        static float interval = 1f;
        static float lastSpawn = 0;
        static float lastTime = 0;
        public static List<Vehicle> deadVeh = new List<Vehicle>(); //this list contains all the vehicles that their agitation timer run out before they got served.

        static void SpawnVeh(float time, List<Vehicle> activeVeh) //this method is responsible for spawning vehicles when necessary
        {
            if (activeVeh.Count < 5) // if the active vehicles are less than 5
            {
                Vehicle myVeh; //create a vehicle vessel
                int myRandom =PSManager.rnd.Next(1, 4); //get a random number that we will be used to get a random type of vehicle
                if(myRandom == 1)
                {
                    myVeh= new Car(time);
                    
                    activeVeh.Add(myVeh); //add the new car in the active vehicle list
                    PSManager.Instance.MyVehQ.Add(myVeh); //add it to the list of all vehicles
                    //Console.WriteLine(myVeh.vehName + "was created");
                }
                else if (myRandom == 2)
                {
                    myVeh = new Van(time);
                    PSManager.Instance.MyVehQ.Add(myVeh);
                    activeVeh.Add(myVeh);
                    //Console.WriteLine(myVeh.vehName + "was created");
                }
                else if (myRandom == 3)
                {
                    myVeh = new HGV(time);
                    PSManager.Instance.MyVehQ.Add(myVeh);
                    activeVeh.Add(myVeh);
                    //Console.WriteLine(myVeh.vehName + "was created");
                }
                else
                {
                    Console.WriteLine("Something went wrong with the random number on the car spawner");
                    //This exists only for error catching purposes
                }

            }
        } 

        public static void Update(float time, List<Vehicle> activeVeh) // when the timer ticks, i update the lastSpawn time, and then check if car is done waiting, if yes, I drop it of the list of active cars
        {
            float myRnd = PSManager.rnd.Next(1500, 2201) / 1000;
            if (time > lastSpawn + myRnd) //Vehicles spawn every 1.5-2.2 seconds. If that time period has passed since the last vehicle spawn then do the following.
            {
                SpawnVeh(time, activeVeh); //Call method that spawns vehicles
                lastSpawn = time; //update last spawn time
            }
            if (time > lastTime + interval) //Every one second
            {

                foreach (Vehicle c in activeVeh) //check every vehicle in active vehicles list
                {
                    c.Update(time); //update every veh in the list

                    if (c.DoneWaiting) //if the agitation timer runs out
                    {
                        c.DidItLeaveQ = true; 
                        deadVeh.Add(c); //add it to the dead vehicles list
                        
                    }
                }
                
                    foreach (Vehicle c in deadVeh) //for every vehicle in the dead vehicles list
                    {

                        activeVeh.Remove(c); //remove it from the active cars list
                        
                    }
                }
                


                lastTime = time; // update the time. Last time is always the last time checked.
                
            }
        }
    }

