using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using Conscript;
using Conscript.Compiler;
using Conscript.Runtime;

namespace Conscript.IDE
{
    public partial class HostEnvironmentForm : Form
    {
        #region Private Variables

        ScriptManager m_scriptManager;

        #endregion

        #region Private Methods

        private void UpdateModuleControl()
        {
            m_dgvHostFunctions.Rows.Clear();
            foreach (HostFunctionPrototype hostFunctionPrototype
                in m_scriptManager.HostFunctions.Values)
            {
                DataGridViewRow dataGridViewRow = new DataGridViewRow();
                dataGridViewRow.CreateCells(m_dgvHostFunctions);
                dataGridViewRow.Cells[0].Value = hostFunctionPrototype.ToString();
                HostFunctionHandler hostFunctionHandler
                    = hostFunctionPrototype.Handler;
                dataGridViewRow.Cells[1].Value
                    = hostFunctionHandler.GetType().Name;
                dataGridViewRow.Cells[2].Value
                    = hostFunctionHandler.GetType().Assembly.FullName;

                m_dgvHostFunctions.Rows.Add(dataGridViewRow);
            }
        }

        private void HostEnvironmentForm_Load(object objectSender, EventArgs eventArgs)
        {
            UpdateModuleControl();
        }

        private void m_btnRegisterModule_Click(object objectSender, EventArgs eventArgs)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Register Module";
            openFileDialog.Filter
                = "Assemblies (*.exe, *.dll)|*.exe;*.dll|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.Cancel) return;

            Assembly assembly = null;
            try
            {
                assembly = System.Reflection.Assembly.LoadFile(
                    openFileDialog.FileName);
            }
            catch (Exception exception)
            {
                MessageBox.Show(this,
                    "Error while loading assembly. Reason: " + exception,
                    "Register module",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Type[] arrayTypes = assembly.GetExportedTypes();
            foreach (Type type in arrayTypes)
            {
                if (!typeof(HostModule).IsAssignableFrom(type))
                    continue;

                ConstructorInfo constructorInfo = null;
                try
                {
                    constructorInfo
                        = type.GetConstructor(new Type[0]);
                }
                catch (Exception)
                {
                    continue;
                }

                object objectHostModule = constructorInfo.Invoke(new object[0]);
                HostModule hostModule = (HostModule)objectHostModule;
                m_scriptManager.RegisterHostModule(hostModule);
            }

            UpdateModuleControl();
        }

        #endregion

        #region Public Methods

        public HostEnvironmentForm(ScriptManager scriptManager)
        {
            InitializeComponent();

            m_scriptManager = scriptManager;
        }

        #endregion
    }
}