using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Timers;

namespace ProgrammingAssignement2017
{
    public class PSManager
    {
        

        private bool exit = false;
        public bool Exit
        {
            get { return exit; }
            set { exit = value; }
        }

        private bool keepGoing = true;
        public bool KeepGoing
        {
            get { return keepGoing; }
            set { keepGoing = value; }
        }

        private bool pause = true;
        public bool Pause
        {
            get { return pause; }
            set { pause = value; }
        }

        PumpManager pm = new PumpManager(); //instantiation of PumpManager object
        public static Random rnd = new Random(); //create a new random object, will be used later on
        public List<Vehicle> activeVeh = new List<Vehicle>(); //this list contains all the vehicles that are active on a given time

        private List<Vehicle> myVehQ = new List<Vehicle>(); //this list contains all the vehicles created

        private List<Vehicle> myListOfTransactions = new List<Vehicle>(); //list of all the transactions completed

        public List<Pump> myListOfPumps = new List<Pump>();  // list of all the pumps of the station

        private List<Pump> availablePumps = new List<Pump>(); // list of all available pumps of the station

        private static PSManager instance; //only one PSManager is going to be used

        public List<Vehicle> MyVehQ
        {
            get { return myVehQ; }
            set { myVehQ = value; }
        } 

        public List<Vehicle> MyListOfTransactions
        {
            get { return myListOfTransactions; }
            set { myListOfTransactions = value; }
        }

        public static PSManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PSManager();
                }
                return instance;
            }
        } //accessor for the instance

        public PSManager() // this is the actual costructor - private, so no objects of this class can be created outside
        {
            FillmyListOfPumps();
        }

        private void FillmyListOfPumps()
        {

            for (int i = 0; i < 9; i++)
            {
                Pump pump = new Pump(i);
                myListOfPumps.Add(pump);
                availablePumps.Add(pump);
            }
        }//filling up the pumps list, just by creating new pumps with state set to true and throwing in a list
        
        //In the method bellow, start looking from the first lane(max pump id is 3 for the first), 
        //if no pump available go to the second lane, then third. 
        //If no pump is found we return null. We stop looking either when chosenPump is not empty 
        //or when we are out of lanes to check
        //(In reverse, we need lanes <=9 AND the boolean to be false to keep looping)
        public Pump GetChosenPump(List<Pump> myListOfPumps) 
        {
            Pump chosenPump = null;
            int laneCounter = 2;
            bool foundPump = false;
            do
            {
                chosenPump = checkLane(laneCounter, myListOfPumps);

                if (chosenPump == null)
                {
                    laneCounter = laneCounter + 3;
                    foundPump = false;

                }
                if (chosenPump != null)
                {
                    foundPump = true;
                }
            }
            while (foundPump == false && laneCounter <= 9);

            return chosenPump;







        } // in this method I loop through the 3 lanes, going for the 1rst to the 3rd. I return a pump obj, which in case no pump is free, returns null

        public static Pump checkLane(int pumpNum, List<Pump> listOfPumps)
        {
            bool keepgoing = true;
            Pump chosenPump = null;
            int laneMaxStart = pumpNum;
            int counter = 0;
            do
            {
                if (listOfPumps[pumpNum].PumpState == true)
                {
                    
                        if (counter == 0)
                        {
                            if (listOfPumps[pumpNum - 1].PumpState && listOfPumps[pumpNum - 2].PumpState )
                            {
                                chosenPump = listOfPumps[pumpNum];
                                keepgoing = false;
                            }
                        }
                        else if (counter == 1)
                        {
                            if (listOfPumps[pumpNum - 1].PumpState)
                            {
                                chosenPump = listOfPumps[pumpNum];
                                keepgoing = false;
                            }

                        }
                        else if (counter == 2)
                        {

                            chosenPump = listOfPumps[pumpNum];
                            keepgoing = false;
                        }
                    

                }
                pumpNum -= 1;
                counter += 1;
            }
            while (counter < 3 && keepgoing == true);
            return chosenPump;
        } //in this method i loop inside a specific lane and check for availability. I check starting from the 3rd pump per lane to avoid conjunction between the cars, while keeping a counter of how many times I looped throught the lane. Depending on the loop counter, i check the pumps in front of every every available pump i find. If a pump is available and all the other pumps in the same lane, with lower ID are also available, then I save the pump in the chosen pump vessel 

        public void ManagePumps(float time)//this is where I update my pumps by assigning cars to free pumps, removing the cars assigned from the active list and more
        {
            VehGenerator.Update(time, activeVeh); //call the vehicle generator update method
            Pump chosenPump = GetChosenPump(myListOfPumps); //call the method that finds the correct pump that needs to be assigned to a vehicle.
            if (activeVeh.Count > 0) //this only happens if cars are already in the queue waiting to be serviced
            {
                if (chosenPump != null) //check if chosenPump method found a pump ready
                {
                    //Assign the first vehicle of the activeVeh list of veh, to the chosen pump.
                    myListOfPumps[chosenPump.PumpID].CurVehServed = activeVeh[0];
                    myListOfPumps[chosenPump.PumpID].CurVehServed.getFuelingTimer(chosenPump);//set vehicles fueling timer
                    //set vehicle's properties to match its current state
                    myListOfPumps[chosenPump.PumpID].CurVehServed.IsVehBeingServed = true;
                    myListOfPumps[chosenPump.PumpID].PumpState = false;
                    myListOfPumps[chosenPump.PumpID].CurVehServed.MyPump = chosenPump;//assign the chosen pump to the vehicle
                    activeVeh.Remove(activeVeh[0]); //remove the vehicle from the list of activeVehicles
                    chosenPump = null;//empty the chosen pump vessel 

                }
            }
        }

        public void Update(float time) //Update method for the PSManager class
        {


            pm.Update(time, myListOfPumps); //this is where PumpManager update function is called
            

        }

    }       
}


