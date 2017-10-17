using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Diagnostics;
using System.Windows.Forms;


namespace ProgrammingAssignement2017
{
    class Program
    {
        public static PetrolStationDisplay psd = new PetrolStationDisplay(); //Instantiation of the form
        public static Stopwatch sw = new Stopwatch();//create a stopwatch
        public static float time = 0;
        static void Main(string[] args)
        {  
            Application.EnableVisualStyles();
            psd.Show();
            psd.FormClosing += Psd_FormClosing;
            psd.FormClosed += Psd_FormClosed;
            Loops();
        }

        /// <summary>
        /// This loop updates everything
        /// </summary>
        public static void Loops()
        {
            do 
            {
                Application.DoEvents();
                time = sw.ElapsedMilliseconds / 1000f; //keeping time in seconds, so time is always the elapsed Milliseconds divided by 1000
                PSManager.Instance.Update(time); // this is the main method that updates everything behind the scenes in the software
                psd.UpdateForm(time); // this method updates the form
                //this loop bellow is used to represent a pause. The functionality of the form is retained, but time does not flow.
                //PSManager object is not updating either. When the user unpauses, the timer resumes and the original loop continues normally.
                if (PSManager.Instance.Pause)
                {
                    sw.Stop();//stops the timer when the user presses pause.
                    do
                    {
                        Application.DoEvents();
                        psd.UpdateForm(time); // this method updates the form
                    }
                    while (PSManager.Instance.Pause);
                    sw.Start(); //by starting the timer here I enable myself to set the initial state of the application to pause mode. 
                                //Therefore expecting the user to press play.
                }

            }
            while (PSManager.Instance.KeepGoing); //when the user presses the stop button keepGoing becomes false.
            //I use the following loop in a similar way to how Console.ReadLine() would be used at the end of a console app.
            //I allow time to the user to look at all the information on the application screen
            //An exit button is presented to the user to break this loop and terminate the software.
            while (PSManager.Instance.Exit == false) //exit turns true when the user presses the exit button.
            {
                Application.DoEvents();
                psd.UpdateForm(time); // this method updates the form
            }
        }

        private static void Psd_FormClosed(object sender, FormClosedEventArgs e)
        {
            MessageBox.Show("Thank you for using the software. \n \n" + "Gerasimos Vandoros", "Author's note");
        }

        private static void Psd_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("Do you want to save before exiting?", "Attention!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                PSManager.Instance.Pause = false; //make sure that all the loops stop
                PSManager.Instance.KeepGoing = false;
                PSManager.Instance.Exit = true;
                SaveToFile.Save();

            }
            else if (result == DialogResult.No)
            {
                PSManager.Instance.Pause = false; //make sure that all the loops stop
                PSManager.Instance.KeepGoing = false;
                PSManager.Instance.Exit = true;
            }
            else
            {
                PSManager.Instance.Pause = false;
                PSManager.Instance.KeepGoing = true;
                PSManager.Instance.Exit = false;

                Loops(); //get back in the original loop


            }
        }


    }
}

