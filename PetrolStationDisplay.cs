using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ProgrammingAssignement2017
{
    public partial class PetrolStationDisplay : Form
    {
        //Loading images from the file
        Image pumpImage = Image.FromFile("pump.png");
        Image carImage = Image.FromFile("car.png");
        Image vanImage = Image.FromFile("van.png");
        Image hgvImage = Image.FromFile("hgv.png");
        Image playImage = Image.FromFile("play.png");
        Image stopImage = Image.FromFile("stop.png");
        Image pauseImage = Image.FromFile("pause.png");



        public PetrolStationDisplay()
        {
            InitializeComponent();

            //Filling up the list of pump labels
            myPumpLabels.Add(pump0Label);
            myPumpLabels.Add(pump1Label);
            myPumpLabels.Add(pump2Label);
            myPumpLabels.Add(pump3Label);
            myPumpLabels.Add(pump4Label);
            myPumpLabels.Add(pump5Label);
            myPumpLabels.Add(pump6Label);
            myPumpLabels.Add(pump7Label);
            myPumpLabels.Add(pump8Label);

            //Filling up the list of vehicle's in the queue labels
            myVehQLabels.Add(qspot0);
            myVehQLabels.Add(qspot1);
            myVehQLabels.Add(qspot2);
            myVehQLabels.Add(qspot3);
            myVehQLabels.Add(qspot4);

            //Filling up the list of vehicle's in the queue pictures
            myVehPictures.Add(q0pic);
            myVehPictures.Add(q1pic);
            myVehPictures.Add(q2pic);
            myVehPictures.Add(q3pic);
            myVehPictures.Add(q4pic);

            //Filling up the list of pump pictures
            myPumpPictures.Add(p0pic);
            myPumpPictures.Add(p1pic);
            myPumpPictures.Add(p2pic);
            myPumpPictures.Add(p3pic);
            myPumpPictures.Add(p4pic);
            myPumpPictures.Add(p5pic);
            myPumpPictures.Add(p6pic);
            myPumpPictures.Add(p7pic);
            myPumpPictures.Add(p8pic);

            //Filling up the list of vehicle's in the pumps pictures
            myVehOnPumpsPictures.Add(pumpVeh0);
            myVehOnPumpsPictures.Add(pumpVeh1);
            myVehOnPumpsPictures.Add(pumpVeh2);
            myVehOnPumpsPictures.Add(pumpVeh3);
            myVehOnPumpsPictures.Add(pumpVeh4);
            myVehOnPumpsPictures.Add(pumpVeh5);
            myVehOnPumpsPictures.Add(pumpVeh6);
            myVehOnPumpsPictures.Add(pumpVeh7);
            myVehOnPumpsPictures.Add(pumpVeh8);


            for (int i = 0; i < 9; i++) // this loop goes through all pumps and assigns the pump image to the picture boxes
            {
                myPumpPictures[i].Image = pumpImage; //assign pump Images
                myPumpPictures[i].Size = myPumpPictures[i].Image.Size; //resize the picturebox to make it have the same size as the image
            }


        }//Constructor
        private List<PictureBox> myVehOnPumpsPictures = new List<PictureBox>(); //list of pictureboxes for the vehicles on the pumps
        public List<PictureBox> MyVehOnPumpsPictures
        {
            get { return myVehOnPumpsPictures; }
            set { myVehOnPumpsPictures = value; }
        }


        private List<PictureBox> myVehPictures = new List<PictureBox>();//list of pictureboxes for the vehicles in the queue
        public List<PictureBox> MyVehPictures
        {
            get { return myVehPictures; }
            set { myVehPictures = value; }
        }

        private List<PictureBox> myPumpPictures = new List<PictureBox>();//list of pictureboxes for the pumps
        public List<PictureBox> MyPumpPictures
        {
            get { return myPumpPictures; }
            set { myPumpPictures = value; }
        }

        private List<Label> myPumpLabels = new List<Label>(); //list of labels for the pumps
        public List<Label> MyPumpLabels
        {
            get { return myPumpLabels; }
            set { myPumpLabels = value; }
        }

        private List<Label> myVehQLabels = new List<Label>();//list of labels for the vehicles in the queue
        public List<Label> MyVehQLabels
        {
            get { return myVehQLabels; }
            set { myVehQLabels = value; }
        }

        public void UpdateForm(float time)
        {
            timeLabel.Text = time.ToString("0.00"); //software running time
            totalMoney.Text = "Total Money Earned \n" + PumpManager.TotalEarnings.ToString("0.00");
            dieselMoney.Text = "Total Money Earned from diesel \n" + PumpManager.DieselEarnings.ToString("0.00");
            unleadedMoney.Text = "Total Money Earned from unleaded \n" + PumpManager.UnleadedEarnings.ToString("0.00");
            lpgMoney.Text = "Total Money Earned from LPG \n" + PumpManager.LPGEarnings.ToString("0.00");

            totalLitresDispenced.Text = "Total Litres dispenced \n" + PumpManager.FuelDispenced.ToString("0.00");
            dieselLitres.Text = "Total Litres of diesel dispenced \n" + PumpManager.DieselTotal.ToString("0.00");
            unleadedLitres.Text = "Total Litres of unleaded dispenced \n" + PumpManager.UnleadedTotal.ToString("0.00");
            lpgLitres.Text = "Total Litres of LPG dispenced \n" + PumpManager.LPGTotal.ToString("0.00");


            commissionMoney.Text = "Total Commission of fuel dispenced \n" + (PumpManager.TotalEarnings * 0.01f).ToString("0.00");
            totalPayroll.Text = "Fuel Attendant Running Wage \n" + PumpManager.FuelAttendantPayroll.ToString("0.00");
            numberOfVehServed.Text = "Total Number of Vehicles Served \n" + PumpManager.TotalNumberOfVehServed.ToString();

            numberOfAgitatedVeh.Text = "Total Number of Agitated Vehicles \n" + VehGenerator.deadVeh.Count.ToString();

            for (int i = 0; i < 9; i++) //fill the labels for the pumps
            {

                if (PSManager.Instance.myListOfPumps[i].CurVehServed != null) //If the pump has a vehicle assigned to it
                {
                    myPumpLabels[i].Text = ("Filling Timer \n" + PSManager.Instance.myListOfPumps[i].CurVehServed.FillingTimer.ToString("0"));
                    //The counterpart label's text is modified
                    //Then check if the vehicle is Car, Van or HGV and set it's picturebox and tooltip accordingly
                    if (PSManager.Instance.myListOfPumps[i].CurVehServed is Car)
                    {
                        myVehOnPumpsPictures[i].Image = carImage;
                        myVehOnPumpsPictures[i].Size = myVehOnPumpsPictures[i].Image.Size;
                        toolTip.SetToolTip(myVehOnPumpsPictures[i], "Car " + PSManager.Instance.myListOfPumps[i].CurVehServed.FuelType);
                    }
                    else if (PSManager.Instance.myListOfPumps[i].CurVehServed is Van)
                    {
                        myVehOnPumpsPictures[i].Image = vanImage;
                        myVehOnPumpsPictures[i].Size = myVehOnPumpsPictures[i].Image.Size;
                        toolTip.SetToolTip(myVehOnPumpsPictures[i], "Van " + PSManager.Instance.myListOfPumps[i].CurVehServed.FuelType);
                    }
                    else if (PSManager.Instance.myListOfPumps[i].CurVehServed is HGV)
                    {
                        myVehOnPumpsPictures[i].Image = hgvImage;
                        myVehOnPumpsPictures[i].Size = myVehOnPumpsPictures[i].Image.Size;
                        toolTip.SetToolTip(myVehOnPumpsPictures[i], "HGV " + PSManager.Instance.myListOfPumps[i].CurVehServed.FuelType);
                    }
                    else //this exists for error catching purposes only
                    {
                        Console.WriteLine("Error with reading vehicle type");
                    }

                }
                else//if the pumps has no vehicle assigned to it
                {
                    myPumpLabels[i].Text = ("Pump number" + i + "\n is empty"); //set pumps label text
                    myVehOnPumpsPictures[i].Image = null;   //empty the corresponding vehicle picturebox
                    toolTip.SetToolTip(myVehOnPumpsPictures[i], null); //empty the corresponding tooltip

                }

            }

            if (PSManager.Instance.activeVeh.Count >= 1) //fills the labels of vehicles in the queue(if there is at list one veh in the queue)
            {
                for (int i = 0; i < PSManager.Instance.activeVeh.Count; i++)//loops though all the active cars in the queue
                {
                    //modifies labels and pictureboxes according to the type of the vehicle.
                    myVehQLabels[i].Text = ("AgitationTimer \n" + PSManager.Instance.activeVeh[i].AgTimer.ToString("0"));
                    if (PSManager.Instance.activeVeh[i] is Car)
                    {
                        myVehPictures[i].Image = carImage;
                        myVehPictures[i].Size = myVehPictures[i].Image.Size;
                        toolTip.SetToolTip(myVehPictures[i], "Car " + PSManager.Instance.activeVeh[i].FuelType);

                    }
                    else if (PSManager.Instance.activeVeh[i] is Van)
                    {
                        myVehPictures[i].Image = vanImage;
                        myVehPictures[i].Size = myVehPictures[i].Image.Size;
                        toolTip.SetToolTip(myVehPictures[i], "Van " + PSManager.Instance.activeVeh[i].FuelType);

                    }
                    else if (PSManager.Instance.activeVeh[i] is HGV)
                    {
                        myVehPictures[i].Image = hgvImage;
                        myVehPictures[i].Size = myVehPictures[i].Image.Size;
                        toolTip.SetToolTip(myVehPictures[i], "HGV " + PSManager.Instance.activeVeh[i].FuelType);

                    }
                    else //this is here for error catching purposes only
                    {
                        Console.WriteLine("Error with reading vehicle type");
                    }

                    //the loop bellow goes through all empty spots in the queue and sets the image and tooltip to null
                    for (int counter = PSManager.Instance.activeVeh.Count; counter < 5; counter++)
                    {
                        myVehPictures[counter].Image = null;
                        toolTip.SetToolTip(myVehPictures[i], null);
                    }



                }

            }
            else//if there are no vehicles in the queue
            {
                foreach (Label l in myVehQLabels)//every label in the list of labels for vehicles in the queue  
                {
                    l.Text = "Empty"; //becomes "empty"
                }
            }
        }

        private void PetrolStationDisplay_Load(object sender, EventArgs e)
        {

        }
        //When showMoreButton is clicked, the size of the form changes and all the counters are made visible.
        private void showMoreButton_Click(object sender, EventArgs e)
        {
            PetrolStationDisplay.ActiveForm.Size = new System.Drawing.Size(this.Size.Width, 896);
            showMoreButton.Visible = false;
            showLessButton.Visible = true;
            totalMoney.Visible = true;
            totalLitresDispenced.Visible = true;
            unleadedLitres.Visible = true;
            unleadedMoney.Visible = true;
            dieselLitres.Visible = true;
            dieselMoney.Visible = true;
            lpgLitres.Visible = true;
            lpgMoney.Visible = true;
            totalPayroll.Visible = true;
            commissionMoney.Visible = true;
            
            

        }
        //When showLessButton is clicked, the showMoreButton click actions are reversed.
        private void showLessButton_Click(object sender, EventArgs e)
        {
            PetrolStationDisplay.ActiveForm.Size = new System.Drawing.Size(this.Size.Width, 584);
            showMoreButton.Visible = true;
            showLessButton.Visible = false;
            totalMoney.Visible = false;
            totalLitresDispenced.Visible = false;
            unleadedLitres.Visible = false;
            unleadedMoney.Visible = false;
            dieselLitres.Visible = false;
            dieselMoney.Visible = false;
            lpgLitres.Visible = false;
            lpgMoney.Visible = false;
            commissionMoney.Visible = false;
            
            
            totalPayroll.Visible = false;
        }
        //When playButton is clicked the pause bool used in Main is becoming false, unpausing the application.
        private void playButton_Click(object sender, EventArgs e)
        {
            playButton.Enabled = false;
            PSManager.Instance.Pause = false;
            pauseButton.Enabled = true;
            stopButton.Enabled = true;
        }
        //When pauseButton is clicked the pause bool used in Main is becoming true, pausing the application.
        private void pauseButton_Click(object sender, EventArgs e)
        {
            playButton.Enabled = true;
            PSManager.Instance.Pause = true;
            pauseButton.Enabled = false;
            stopButton.Enabled = true;
        }
        //When stopButton is clicked the keepGoing bool used in Main is becoming false, stopping the application. The Exit button is enabled and becomes visible
        private void stopButton_Click(object sender, EventArgs e)
        {
            playButton.Enabled = false;
            pauseButton.Enabled = false;
            stopButton.Enabled = false;

            PetrolStationDisplay.ActiveForm.Size = new System.Drawing.Size(1650, this.Size.Height);

            listOfTransactionsLabel.Visible = true;

            string text = "List of all completed transactions \n";
            text += "(Vehicle type-Fuel type-Ltr) \n \n";
            foreach (Vehicle v in PSManager.Instance.MyListOfTransactions)
            {
                text += v.vehType + "   ";
                text += v.FuelType + "   ";
                text += (v.TankMax - v.TankOnArrival) + "   ";
                text += "\n";
            }

            listOfTransactionsLabel.Text = text;
            PSManager.Instance.KeepGoing = false;         
        }

        private void helpButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"Documentation\UserGuide.htm");

        }

        
    }       
}

