using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Conscript.IDE
{
    public partial class BuildSettingsForm : Form
    {
        #region Private Methods

        private void m_btnOk_Click(object objectSender, EventArgs eventArgs)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion

        #region Public Methods

        public BuildSettingsForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Properties

        public bool DebugMode
        {
            get { return m_chkDebugMode.Checked; }
            set { m_chkDebugMode.Checked = value; }
        }

        public bool OptimiseCode
        {
            get { return m_chkOptimiseCode.Checked; }
            set { m_chkOptimiseCode.Checked = value; }
        }

        #endregion;
    }
}