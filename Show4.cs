using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AnimatedForm
{
    public partial class Show4 : Form
    {
        public Show4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form s5 = new Show5();
            s5.Show();
            //Next=>
            this.Close();
        }
    }
}
