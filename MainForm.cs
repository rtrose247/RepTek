using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Sanford.Multimedia.Midi;




//
namespace AnimatedForm
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
        }

        //RED
        private void button1_Click(object sender, System.EventArgs e)
        {
            //RTR 4.26.2018 - demo code
            //(new System.Threading.Thread(() => {
            //    (new Show1()).Show();
            //})).Start();
            Form s1 = new Show1();
            s1.Show();
        }

        //BLUE
        private void button2_Click(object sender, System.EventArgs e)
        {
        }
        //GREEN
        private void button3_Click(object sender, System.EventArgs e)
        {
        }
        //BLUE
        private void button4_Click(object sender, System.EventArgs e)
        {
        }

        //Animoid Row
        private void button5_Click(object sender, System.EventArgs e)
        {
            // Add event handler code here.  
            //System.Console.Write("Animoid Row");
        }
        //Tyrell+Wallace
        private void button6_Click(object sender, System.EventArgs e)
        {

        }
        //Chinatown
        private void button7_Click(object sender, System.EventArgs e)
        {

        }
        //White Dragon
        private void button8_Click(object sender, System.EventArgs e)
        {

        }
        //Police HQ
        private void button9_Click(object sender, System.EventArgs e)
        {

        }
        //Eyeworks
        private void button10_Click(object sender, System.EventArgs e)
        {

        }
        //Yukon Hotel
        private void button11_Click(object sender, System.EventArgs e)
        {

        }
        //Snake Pit
        private void button12_Click(object sender, System.EventArgs e)
        {

        }
        //Bradbury
        private void button13_Click(object sender, System.EventArgs e)
        {

        }
        //Honolulu
        private void button14_Click(object sender, System.EventArgs e)
        {

        }
        //LAX
        private void button15_Click(object sender, System.EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
