using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AnimatedForm
{
    public partial class Show3 : Form
    {
        public Show3()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form s4 = new Show4();
            s4.Show();
            //Next=>
            this.Close();
        }
    }
}
