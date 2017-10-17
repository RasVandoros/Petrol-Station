using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingAssignement2017
{
     class PumpManager
    {
        public static float interval = 1f;
        static float lastTime = 0;
        private float unleadedPrice = 1f; //unleaded price per litre
        private float dieselPrice = 2f; //diesel price per litre
        private float lpgPrice = 0.5f; //lpg price per litre

        private static float fuelAttendantPayroll = 0; //fuel attendant's payroll
        private static float hourlyWage = 2.49f / 3600f; //fuel attendant's payroll solely from time passing. His hourly wage is 2.49. Dividing by 3600 to get his wage per second.

        private static float fluelDispenced = 0; //total fuel dispenced during app's lifetime.
        private static float unleadedTotal = 0; //total unleaded dispenced during app's lifetime.
        private static float dieselTotal = 0; //total diesel dispenced during app's lifetime.
        private static float lpgTotal = 0; //total lpg dispenced during app's lifetime.

        private static float totalEarnings = 0; //total money earned in app's lifetime.
        private static float unleadedEarnings = 0; //total unleaded money earned in app's lifetime.
        private static float dieselEarnings = 0; //total diesel money earned in app's lifetime.
        private static float lpgEarnings = 0; //total lpg money earned in app's lifetime.

        private static int totalNumberOfVehServed = 0; //total number of vehicles served in app's lifetime.
        
        public static int TotalNumberOfVehServed
        {
            get { return totalNumberOfVehServed; }
            set { totalNumberOfVehServed = value; }
        }

        public static float FuelDispenced
        {
            get { return fluelDispenced; }
            set { fluelDispenced = value; }
        }
        public static float UnleadedTotal
        {
            get { return unleadedTotal; }
            set { unleadedTotal = value; }
        }
        public static float DieselTotal
        {
            get { return dieselTotal; }
            set { dieselTotal = value; }
        }
        public static float LPGTotal
        {
            get { return lpgTotal; }
            set { lpgTotal = value; }
        }

        public static float TotalEarnings
        {
            get { return totalEarnings; }
            set { totalEarnings = value; }
        }
        public static float UnleadedEarnings
        {
            get { return unleadedEarnings; }
            set { unleadedEarnings = value; }
        }
        public static float DieselEarnings
        {
            get { return dieselEarnings; }
            set { dieselEarnings = value; }
        }
        public static float LPGEarnings
        {
            get { return lpgEarnings; }
            set { lpgEarnings = value; }
        }

        public static float FuelAttendantPayroll
        {
            get { return fuelAttendantPayroll; }
            set { fuelAttendantPayroll = value; }
        }
        public static float HourlyWage
        {
            get { return hourlyWage; }
            set { hourlyWage = value; }
        }


        public void Update(float time, List<Pump> myList) //Update function for the PumpManager class
        {
            if (time > lastTime + interval)//if a second passed
            {

                PSManager.Instance.ManagePumps(time);//Call the method that manages the pumps
                foreach (Pump p in myList) //For every pump in the list passed as parameter(the list of pumps)
                    {

                    lastTime = time; //update time

                        if ((object)p.CurVehServed != null) //typecast as object to avoid errors. If no vehicle is assigned to the pump
                        {
                        p.CurVehServed.FillingTimer -= 1; //countdown on the filling timer of the car assigned on the given pump
                        FuelDispenced += 1.5f; //update total fuel dispenced
                        if (p.CurVehServed.FuelType == Vehicle.Fuel.Diesel) //if the vehicle's fuel is diesel
                        {
                            DieselTotal += 1.5f;   //add to the diesel total
                        }
                        else if (p.CurVehServed.FuelType == Vehicle.Fuel.LPG)//if the vehicle's fuel is LPG
                        {
                            LPGTotal += 1.5f; //add to the LPG total
                        }
                        else if (p.CurVehServed.FuelType == Vehicle.Fuel.Unleaded)//if the vehicle's fuel is Unleaded
                        {
                            UnleadedTotal += 1.5f; //add to the unleaded total
                        }
                        if (p.CurVehServed.FillingTimer <= 0) //if the filling timer countdown reached 0
                        {
                            p.CurVehServed.IsVehBeingServed = false;
                            p.CurVehServed.CarWasServed = true;
                            PSManager.Instance.MyListOfTransactions.Add(p.CurVehServed); //add the car to the list of completed transactions.
                            p.CurVehServed = null; //get the car off the pump.
                            p.PumpState = true; //set pump state to true, therefore make pump available
                            totalNumberOfVehServed += 1; //add one to the total number of vehicles served
                            
                        }
                        

                    }
                    
                }
                //Update all the counters
                dieselEarnings = dieselTotal * dieselPrice;
                unleadedEarnings = unleadedTotal * unleadedPrice;
                lpgEarnings = lpgTotal * lpgPrice;
                totalEarnings = dieselEarnings + unleadedEarnings + lpgEarnings;
                hourlyWage += 2.49f / 3600f;
                fuelAttendantPayroll = totalEarnings * 0.01f + hourlyWage;
                                
            }
            
        }
    }
}
