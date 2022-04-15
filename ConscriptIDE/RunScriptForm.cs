using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Conscript.Collections;
using Conscript.Compiler;

namespace Conscript.IDE
{
    public partial class RunScriptForm : Form
    {
        #region Private Methods

        private Script m_script;
        private ScriptFunction m_scriptFunction;
        private Dictionary<String, object> m_dictParameters;

        #endregion

        #region Private Methods

        private void UpdateParameterControl(ScriptFunction scriptFunction)
        {
            m_dictParameters.Clear();
            m_dgvParameters.Rows.Clear();
            foreach (String strParameter in scriptFunction.Parameters)
            {
                m_dictParameters[strParameter] = null;

                DataGridViewRow dataGridViewRow = new DataGridViewRow();
                dataGridViewRow.CreateCells(m_dgvParameters);
                dataGridViewRow.Cells[0].Value = strParameter;
                dataGridViewRow.Cells[1].Value = "null";
                m_dgvParameters.Rows.Add(dataGridViewRow);
            }
        }

        private object ParseValue(String strValue)
        {
            strValue = strValue.Trim();

            // null
            if (strValue == "null")
                return null;

            // true or false
            if (strValue.ToUpper() == "TRUE")
                return true;
            if (strValue.ToUpper() == "FALSE")
                return false;

            // integer literal
            try
            {
                int iValue = int.Parse(strValue);
                return iValue;
            }
            catch (Exception)
            {
            }

            // float literal
            try
            {
                float fValue = float.Parse(strValue);
                return fValue;
            }
            catch (Exception)
            {
            }

            // must be string
            if (strValue.Length < 2)
                throw new Exception("Invalid literal.");
            if (!strValue.StartsWith("\""))
                throw new Exception("Invalid literal.");
            if (!strValue.EndsWith("\""))
                throw new Exception("Invalid literal.");
            strValue = strValue.Substring(1, strValue.Length - 2);
            String strTemp = strValue;
            strValue = strValue.Replace("\\r", "\r");
            strValue = strValue.Replace("\\n", "\n");
            strValue = strValue.Replace("\\t", "\t");
            strValue = strValue.Replace("\\\"", "\"");
            strValue = strValue.Replace("\\\\", "\\");

            strTemp = strTemp.Replace("\\r", "");
            strTemp = strTemp.Replace("\\n", "");
            strTemp = strTemp.Replace("\\t", "");
            strTemp = strTemp.Replace("\\\"", "");
            strTemp = strTemp.Replace("\\\\", "");

            if (strTemp.IndexOf("\\") >= 0)
                throw new Exception("Invalid literal.");
            if (strTemp.IndexOf("\"") >= 0)
                throw new Exception("Invalid literal.");

            return strValue;
        }

        private void RunScriptForm_Load(object objectSender, EventArgs eventArgs)
        {
            foreach (String strFunctionName in m_script.Functions.Keys)
                m_cmbFunction.Items.Add(strFunctionName);
            foreach (object objectItem in m_cmbFunction.Items)
                if (objectItem.ToString() == "main")
                    m_cmbFunction.SelectedItem = objectItem;
            if (m_cmbFunction.SelectedIndex < 0)
                m_cmbFunction.SelectedIndex = 0;

            m_scriptFunction
                = m_script.Functions[m_cmbFunction.SelectedItem.ToString()];
            UpdateParameterControl(m_scriptFunction);
        }

        private void m_cmbFunction_SelectedIndexChanged(
            object objectSender, EventArgs eventArgs)
        {
            m_scriptFunction
                = m_script.Functions[m_cmbFunction.SelectedItem.ToString()];
            UpdateParameterControl(m_scriptFunction);
        }

        private void m_dgvParameters_CellValidating(object objectSender,
            DataGridViewCellValidatingEventArgs dataGridViewCellValidatingEventArgs)
        {
            if (dataGridViewCellValidatingEventArgs.ColumnIndex != 1) return;

            String strValue
                = dataGridViewCellValidatingEventArgs.FormattedValue.ToString();

            try
            {
                object objectValue = ParseValue(strValue);
            }
            catch (Exception)
            {
                dataGridViewCellValidatingEventArgs.Cancel = true;
                m_erpDialog.SetError(m_dgvParameters, "Invalid literal value");
            }
        }

        private void m_dgvParameters_CellValidated(object objectSender,
            DataGridViewCellEventArgs dataGridViewCellEventArgs)
        {
            int iColumn = dataGridViewCellEventArgs.ColumnIndex;
            if (iColumn != 1) return;

            int iRow = dataGridViewCellEventArgs.RowIndex;
            String strValue
                = m_dgvParameters[iColumn, iRow].Value.ToString();
            object objectValue = ParseValue(strValue);

            if (objectValue == null)
                strValue = "null";
            else if (objectValue.GetType() != typeof(String))
                strValue = objectValue.ToString();
            else
                strValue = strValue.Trim();

            String strParameterName
                = m_dgvParameters[0, iRow].Value.ToString();
            m_dictParameters[strParameterName] = objectValue;

            m_dgvParameters[iColumn, iRow].Value = strValue;
            m_erpDialog.SetError(m_dgvParameters, "");
        }

        private void m_btnOk_Click(object objectSender, EventArgs eventArgs)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion

        #region Public Methods

        public RunScriptForm(Script script)
        {
            InitializeComponent();

            m_script = script;
            m_scriptFunction = null;
            m_dictParameters = new Dictionary<string, object>();
        }

        #endregion

        #region Public Properties

        public ScriptFunction ScriptFunction
        {
            get { return m_scriptFunction; }
        }

        public ReadOnlyDictionary<String, object> Parameters
        {
            get { return new ReadOnlyDictionary<String, object>(m_dictParameters); }
        }

        #endregion
    }
}