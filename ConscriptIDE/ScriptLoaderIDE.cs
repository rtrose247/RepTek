using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using Conscript;
using Conscript.Compiler;

namespace Conscript.IDE
{
    internal class ScriptLoaderIDE
        : ScriptLoader
    {
        #region Private Methods

        private TabControl m_tabControl;
        private ScriptLoader m_scriptLoaderDefault;

        #endregion

        #region Public Methods

        public ScriptLoaderIDE(TabControl tabControl, ScriptLoader scriptLoaderDefault)
        {
            m_tabControl = tabControl;
            m_scriptLoaderDefault = scriptLoaderDefault;
        }

        public List<String> LoadScript(String strResourceName)
        {
            TabPage tabPageFound = null;
            foreach (TabPage tabPage in m_tabControl.TabPages)
            {
                if (tabPage.Text == strResourceName)
                {
                    tabPageFound = tabPage;
                    break;
                }
            }

            if (tabPageFound != null)
            {
                TextBox txtScript = (TextBox)tabPageFound.Controls[0];
                String strSource = txtScript.Text;
                strSource = strSource.Replace("\r\n","\r");
                String[] strSourceLines = strSource.Split('\r');
                List<String> listSourceLines = new List<string>();
                listSourceLines.AddRange(strSourceLines);
                return listSourceLines;
            }
            else
            {
                return m_scriptLoaderDefault.LoadScript(strResourceName);
            }
        }

        #endregion
    }
}
