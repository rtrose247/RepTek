using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Conscript;
using Conscript.Compiler;
using Conscript.Runtime;

namespace ConscriptDemo
{
    internal partial class MainForm
        : Form
        , ScriptLoader
        , HostFunctionHandler
    {
        #region Private Variables

        // script manager
        ScriptManager m_scriptManager;

        // reference to default script loader
        ScriptLoader m_scriptLoaderDefault;

        #endregion

        #region Private Methods

        private void MainForm_Load(object objectSender, EventArgs eventArgs)
        {
            // initialise script manager
            m_scriptManager = new ScriptManager();

            // keep reference to default loader for include's
            m_scriptLoaderDefault = m_scriptManager.Loader;

            // assign self as script loader
            m_scriptManager.Loader = this;

            // define Print host function for script output
            HostFunctionPrototype hostFunctionPrototype
                = new HostFunctionPrototype((Type)null, "Print", (Type)null);

            // register host function
            m_scriptManager.RegisterHostFunction(hostFunctionPrototype, this);
        }

        private void m_btnRunScript_Click(object objectSender, EventArgs eventArgs)
        {
            m_txtScriptOutput.Text = "";

            Script script = null;
            try
            {
                // attempt to compile script object
                script = new Script(m_scriptManager, null);

                // build successful
                m_txtBuildOutput.Text = "Build succeeded\r\n";

                // display byte code
                foreach (ScriptInstruction scriptInstruction
                    in script.Executable.Instructions)
                    m_txtBuildOutput.Text += "\r\n" + scriptInstruction;
            }
            catch (Exception exception)
            {
                // build failed
                m_txtBuildOutput.Text = "Build failed\r\n";
                m_txtBuildOutput.Text += exception;

                // stop here
                return;
            }

            try
            {
                // create execution context (assuming main function with 
                // no parameters defined)
                ScriptContext scriptContext = new ScriptContext(script);

                // execute script context till termination
                while (!scriptContext.Terminated)
                    scriptContext.Execute();

                // display termination message
                m_txtScriptOutput.Text
                    += "\r\nScript terminated";

            }
            catch (Exception exception)
            {
                // context setup or execution error
                m_txtScriptOutput.Text
                    += "\r\nRuntime error: " + exception;
            }
        }

        #endregion

        #region Public Methods

        public MainForm()
        {
            InitializeComponent();
        }

        public List<String> LoadScript(String strResourceName)
        {
            // if resource name specified, use default disk script loader
            if (strResourceName != null)
                return m_scriptLoaderDefault.LoadScript(strResourceName);

            // null specified - load from text box
            String strSource = m_txtSource.Text;
            strSource = strSource.Replace("\r\n", "\r");
            List<String> listSourceLines = new List<string>();
            listSourceLines.AddRange(strSource.Split('\r'));
            return listSourceLines;
        }

        // implements Print host function
        public object OnHostFunctionCall(String strFunctionName, List<object> listParameters)
        {
            // note: function name can be ignored as only one defined

            // write output to script output text box
            m_txtScriptOutput.Text += "\r\n" + listParameters[0];
            m_txtScriptOutput.Refresh();
            return null;
        }

        #endregion
    }
}