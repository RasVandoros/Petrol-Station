using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ProgrammingAssignement2017
{
    
    public abstract class Vehicle //no need to create new vehicle objects. New objects need to be cars, vans or HGVs
    {
        Random rnd = new Random();
        public enum Fuel { Unleaded, Diesel, LPG}; //enum for the different fuel types
        private bool carWasServed = false; //this bool help with defining the state of each vehicle. Was the car successfully served or not.
        private Pump myPump; //each vehicle can have a pump where it getting served.
        private Fuel fuelType;
        private float tankMax; //the maximum capacity of the vehicles tank
        private float tankOnArrival; //the initial level of the tank of the vehicle on arrival
        private bool didItLeaveQ = false; //this turns to true if the aggitation timer runs out before the car reaches a pump
        private float agTimer; // ag timer is a number that is randomly assigned to each vehicle on spawn, that will be used as a counter before they leave the queue due to irritation

        private bool isVehBeingServed; //true if the vehicle is on a pump and is currently getting served, but its not finished yet
        private bool doneWaiting = false; //when the ag timer reaches 0, this will be changed to true, indicating that the vehicle is done waiting and will leave the queue.
        private float fillingTimer; //this will hold the timer that represents the time left before the car is done using the pump.
        protected float spawnTime = 0; //initial time of spawn value is set to 0. It is set to the correct value in the constructor.

        public bool DidItLeaveQ
        {
            get { return didItLeaveQ; }
            set { didItLeaveQ = value; }
        }
        public bool CarWasServed
        {
            get { return carWasServed; }
            set { carWasServed = value; }
        }
        public float FillingTimer
        {
            get { return fillingTimer; }
            set { fillingTimer = value; }
        }
        public Pump MyPump
        {
            get { return myPump; }
            set { myPump = value; }
        }
        public string vehType;
        public Fuel FuelType
        {
            get { return fuelType; }
            set { fuelType = value; }
        }
        public bool DoneWaiting
        {
            get { return doneWaiting; }
        }
        public float TankMax
        {
            get { return tankMax; }
            set { tankMax = value; }
        }
        public float TankOnArrival
        {
            get { return tankOnArrival; }
            set { tankOnArrival = value; }
        }
        public float AgTimer
        {
            get { return agTimer; }
            set { agTimer = value; }
        }
        public bool IsVehBeingServed
        {
            get { return isVehBeingServed; }
            set { isVehBeingServed = value; }
        }
        public void getAgTimer()
        {
            AgTimer = rnd.Next(2, 10);
        } //returns agitation timer, random between to numbers
        public void getFuelingTimer(Pump p) //this methods returns the fueling timer of a vehicle
        {
            this.fillingTimer = (TankMax - TankOnArrival) / p.DispenceCap;
        }

        public void Update(float time) // if the car is not being served and the time surpasses the patience counter of the car, then doneWaiting turns to true
        {

            if (!isVehBeingServed) //if the vehicle is not being served
            {
                this.AgTimer = this.agTimer - PumpManager.interval; //agtimer of the car counts down by 1sec
                if (this.AgTimer <= 0) //if agitation timer reaches 0
                {
                    doneWaiting = true; //the vehicle is done waiting
                    
                    
                }
            }
        }
    }

    
}

