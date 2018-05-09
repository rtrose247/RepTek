using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AnimatedForm
{
    public partial class Show2 : Form
    {
        public Show2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form s3 = new Show3();
            s3.Show();
            //Next=>
            this.Close();
        }
    }
}
