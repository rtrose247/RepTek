using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Conscript.IDE
{
    internal partial class ByteCodeListBox : ListBox
    {
        #region Private Variables

        private List<bool> m_listBreakpoints;
        private int m_iNextInstruction;

        #endregion

        #region Private Methods

        private void ByteCodeListBox_DrawItem(object objectSender, DrawItemEventArgs drawItemEventArgs)
        {
            if (m_listBreakpoints.Count != Items.Count)
                ClearBreakpoints();

            DrawMode = DrawMode.OwnerDrawFixed;

            Graphics graphics = drawItemEventArgs.Graphics;

            if (drawItemEventArgs.Index < 0 || drawItemEventArgs.Index >= Items.Count)
            {
                graphics.DrawString("Compilation required.", drawItemEventArgs.Font,
                    Brushes.Gray, drawItemEventArgs.Bounds);
                return;
            }

            bool bBreakpoint = m_listBreakpoints[drawItemEventArgs.Index];
            bool bSelected = drawItemEventArgs.Index == SelectedIndex;

            Brush brushBack = null;
            Brush brushFont = null;

            if (bBreakpoint && bSelected)
            {
                brushBack = Brushes.DarkMagenta;
                brushFont = Brushes.White;
            }
            else if (bBreakpoint)
            {
                brushBack = Brushes.DarkRed;
                brushFont = Brushes.White;
            }
            else if (bSelected)
            {
                brushBack = Brushes.SkyBlue;
                brushFont = Brushes.White;
            }
            else
            {
                if (drawItemEventArgs.Index == m_iNextInstruction)
                    brushBack = Brushes.Yellow;
                else
                    brushBack = new SolidBrush(drawItemEventArgs.BackColor);
                brushFont = new SolidBrush(drawItemEventArgs.ForeColor);
            }

            graphics.FillRectangle(brushBack, drawItemEventArgs.Bounds);

            graphics.DrawString(Items[drawItemEventArgs.Index].ToString(), drawItemEventArgs.Font,
                brushFont, drawItemEventArgs.Bounds);
        }

        private void ByteCodeListBox_SelectedIndexChanged(object objectSender, EventArgs eventArgs)
        {
            Invalidate();
        }

        private void ByteCodeListBox_DoubleClick(object objectSender, EventArgs eventArgs)
        {
            if (m_listBreakpoints.Count != Items.Count)
                ClearBreakpoints();

            if (SelectedIndex < 0) return;

            ToggleBreakpoint();
        }

        #endregion

        #region Public Methods

        public ByteCodeListBox()
        {
            InitializeComponent();

            m_listBreakpoints = new List<bool>();

            DoubleBuffered = true;
            DrawMode = DrawMode.OwnerDrawFixed;
            BorderStyle = BorderStyle.None;
        }

        public ByteCodeListBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            m_listBreakpoints = new List<bool>();
            for (int iIndex = 0; iIndex < Items.Count; iIndex++)
                m_listBreakpoints.Add(false);

            DoubleBuffered = true;
            DrawMode = DrawMode.OwnerDrawFixed;
            BorderStyle = BorderStyle.None;
        }

        public bool HasBreakPoint(int iInstructionIndex)
        {
            if (iInstructionIndex < 0) return false;
            if (iInstructionIndex >= m_listBreakpoints.Count) return false;
            return m_listBreakpoints[iInstructionIndex];
        }

        public void ClearBreakpoints()
        {
            while (m_listBreakpoints.Count > Items.Count)
                m_listBreakpoints.RemoveAt(Items.Count);

            for (int iIndex = 0; iIndex < m_listBreakpoints.Count; iIndex++)
                m_listBreakpoints[iIndex] = false;

            while (m_listBreakpoints.Count < Items.Count)
                m_listBreakpoints.Add(false);

            m_iNextInstruction = -1;

            Invalidate();
        }

        public void ToggleBreakpoint()
        {
            if (m_listBreakpoints.Count != Items.Count)
                ClearBreakpoints();

            if (SelectedIndex < 0) return;

            m_listBreakpoints[SelectedIndex]
                = !m_listBreakpoints[SelectedIndex];

            Invalidate();
        }

        #endregion

        #region Public Properties

        public int NextInstruction
        {
            get { return m_iNextInstruction; }
            set
            {
                m_iNextInstruction = value;
                Invalidate();
            }
        }

        #endregion
    }
}
