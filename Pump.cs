using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingAssignement2017
{
    public class Pump
    {
        protected int pumpID; //id of the pumps
        protected bool pumpState; //true if the pump is empty/ready to be assigned
        protected float dispenceCap; //flow of fuel. Even though it is stable at 1.5 according to the assignement specifications, having it as a field makes the software more expandable.
        private Vehicle curVehServed; //pumps can be assigned the cars that they are serving


        public Vehicle CurVehServed
        {
            get { return curVehServed; }
            set { curVehServed = value; }
        }
        
        public bool PumpState
        {
            get { return pumpState; }
            set { pumpState = value; }
        }
        public int PumpID
        {
            get { return pumpID; }
            set { pumpID = value; }
        }
        public float DispenceCap
        {
            get { return dispenceCap; }
            set { dispenceCap = value; }
        }

        public Pump(int ID, bool state = true) //Constructor. needs ID as parameter. State is true at on creation.
        {
            pumpID = ID;
            pumpState = state;
            dispenceCap = 1.5f; //right now dispence cap is always 1.5 according to assignement specs.
            curVehServed = null; // car vessel starts as null
        }

        

        
    }
}
