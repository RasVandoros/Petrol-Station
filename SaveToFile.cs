using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingAssignement2017
{
    public static class SaveToFile
    {
        public static void Save()
        {
            CreateFolder();
            CreateFile();
        }


        public static void CreateFolder()
        {
            string folderName = @"PetrolStationData";
            if (File.Exists(folderName) == false)
            {
                Directory.CreateDirectory(folderName);
            }


        }


        public static void CreateFile()
        {
            using (System.IO.StreamWriter file =
            new StreamWriter(@"myData.txt", true))
            {
                file.WriteLine("Total Earnings: " + PumpManager.TotalEarnings);
                file.WriteLine("Total Diesel Earnings: " + PumpManager.DieselEarnings);
                file.WriteLine("Total Unleaded Earnings: " + PumpManager.UnleadedEarnings);
                file.WriteLine("Total LPG Earnings: " + PumpManager.LPGEarnings);
                file.WriteLine("Total fuel disposed: " + PumpManager.FuelDispenced);
                file.WriteLine("Total diesel disposed: " + PumpManager.DieselTotal);
                file.WriteLine("Total unleaded disposed: " + PumpManager.UnleadedTotal);
                file.WriteLine("Total lpg disposed: " + PumpManager.LPGTotal);
                file.WriteLine("");

                foreach (Vehicle v in PSManager.Instance.MyVehQ)
                {
                    
                    file.WriteLine("Vehicle Type: " + v.vehType);
                    file.WriteLine("Fuel Type: " + v.FuelType);
                    file.WriteLine("The Vehicle got Served: " + v.CarWasServed);
                    file.WriteLine("Agitation timer run out: " + v.DidItLeaveQ);
                    file.WriteLine("Is the car currently being served: " + v.IsVehBeingServed);
                    file.WriteLine("");
                }
                


            }
            System.IO.File.Move("myData.txt", "PetrolStationData/" + DateTime.Now.ToString("dd -MM-yyy-h-mm-ss") + ".txt");

        }

        
    }
        
        

            

}

