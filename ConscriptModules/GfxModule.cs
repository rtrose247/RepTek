using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

using Conscript.Compiler;
using Conscript.Runtime;

namespace Conscript.Modules
{
    public class GfxModule
        : HostModule
    {
        #region Private Enumerations

        private enum DrawingPrimitive
        {
            Colour,
            LineWidth,
            Line,
            DrawRectangle,
            FillRectangle,
            DrawEllipse,
            FillEllipse,
            DrawString
        }

        #endregion

        #region Private Classes

        private class GraphicsForm : Form
        {
            public GraphicsForm()
                : base()
            {
                DoubleBuffered = true;
            }
        }

        private class DrawingInstruction
        {
            public DrawingPrimitive m_drawingPrimitive;
            public int m_iValue0;
            public int m_iValue1;
            public int m_iValue2;
            public int m_iValue3;
            public String m_strValue4;
        }

        #endregion

        #region Private Static Variables

        private static ReadOnlyCollection<HostFunctionPrototype> s_listHostFunctionPrototypes;

        #endregion

        #region Private Variables

        private Form m_form;
        private List<DrawingInstruction> m_listDrawingInstructions;
        private Pen m_pen;
        private Brush m_brush;
        private Font m_font;

        #endregion

        #region Private Methods

        private void OnPaint(object objectSender, PaintEventArgs paintEventArgs)
        {
            Graphics graphics = paintEventArgs.Graphics;
            graphics.SmoothingMode
                = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            foreach (DrawingInstruction drawingInstruction
                in m_listDrawingInstructions)
            {
                switch (drawingInstruction.m_drawingPrimitive)
                {
                    case DrawingPrimitive.Colour:
                        Color color = Color.FromArgb(
                            drawingInstruction.m_iValue0,
                            drawingInstruction.m_iValue1,
                            drawingInstruction.m_iValue2);
                        m_pen.Color = color;
                        m_brush = new SolidBrush(color);
                        break;
                    case DrawingPrimitive.LineWidth:
                        m_pen.Width = drawingInstruction.m_iValue0;
                        break;
                    case DrawingPrimitive.Line:
                        graphics.DrawLine(m_pen,
                            drawingInstruction.m_iValue0, drawingInstruction.m_iValue1,
                            drawingInstruction.m_iValue2, drawingInstruction.m_iValue3);
                        break;
                    case DrawingPrimitive.DrawRectangle:
                        graphics.DrawRectangle(m_pen,
                            drawingInstruction.m_iValue0, drawingInstruction.m_iValue1,
                            drawingInstruction.m_iValue2, drawingInstruction.m_iValue3);
                        break;
                    case DrawingPrimitive.FillRectangle:
                        graphics.FillRectangle(m_brush,
                            drawingInstruction.m_iValue0, drawingInstruction.m_iValue1,
                            drawingInstruction.m_iValue2, drawingInstruction.m_iValue3);
                        break;
                    case DrawingPrimitive.DrawEllipse:
                        graphics.DrawEllipse(m_pen,
                            drawingInstruction.m_iValue0, drawingInstruction.m_iValue1,
                            drawingInstruction.m_iValue2, drawingInstruction.m_iValue3);
                        break;
                    case DrawingPrimitive.FillEllipse:
                        graphics.FillEllipse(m_brush,
                            drawingInstruction.m_iValue0, drawingInstruction.m_iValue1,
                            drawingInstruction.m_iValue2, drawingInstruction.m_iValue3);
                        break;
                    case DrawingPrimitive.DrawString:
                        graphics.DrawString(drawingInstruction.m_strValue4, m_font, m_brush,
                            drawingInstruction.m_iValue0, drawingInstruction.m_iValue1);
                        break;
                }
            }
        }

        #endregion

        #region Public Methods

        public GfxModule()
        {
            m_form = null;
            m_listDrawingInstructions = new List<DrawingInstruction>();

            if (s_listHostFunctionPrototypes != null) return;

            List<HostFunctionPrototype> listHostFunctionPrototypes
                = new List<HostFunctionPrototype>();
            HostFunctionPrototype hostFunctionPrototype
                = null;
            Type typeBool = typeof(bool);
            Type typeInt = typeof(int);
            List<Type> listFourInts = new List<Type>();
            listFourInts.Add(typeInt);
            listFourInts.Add(typeInt);
            listFourInts.Add(typeInt);
            listFourInts.Add(typeInt);

            hostFunctionPrototype = new HostFunctionPrototype(typeBool, "Gfx_Initialise", typeInt, typeInt);
            listHostFunctionPrototypes.Add(hostFunctionPrototype);
            hostFunctionPrototype = new HostFunctionPrototype(typeBool, "Gfx_Shutdown");
            listHostFunctionPrototypes.Add(hostFunctionPrototype);
            hostFunctionPrototype = new HostFunctionPrototype(typeBool, "Gfx_Clear");
            listHostFunctionPrototypes.Add(hostFunctionPrototype);
            hostFunctionPrototype = new HostFunctionPrototype(typeBool, "Gfx_SetColour", typeInt, typeInt, typeInt);
            listHostFunctionPrototypes.Add(hostFunctionPrototype);
            hostFunctionPrototype = new HostFunctionPrototype(typeBool, "Gfx_SetLineWidth", typeInt);
            listHostFunctionPrototypes.Add(hostFunctionPrototype);
            hostFunctionPrototype = new HostFunctionPrototype(typeBool, "Gfx_DrawLine", listFourInts);
            listHostFunctionPrototypes.Add(hostFunctionPrototype);
            hostFunctionPrototype = new HostFunctionPrototype(typeBool, "Gfx_DrawRectangle", listFourInts);
            listHostFunctionPrototypes.Add(hostFunctionPrototype);
            hostFunctionPrototype = new HostFunctionPrototype(typeBool, "Gfx_FillRectangle", listFourInts);
            listHostFunctionPrototypes.Add(hostFunctionPrototype);
            hostFunctionPrototype = new HostFunctionPrototype(typeBool, "Gfx_DrawEllipse", listFourInts);
            listHostFunctionPrototypes.Add(hostFunctionPrototype);
            hostFunctionPrototype = new HostFunctionPrototype(typeBool, "Gfx_FillEllipse", listFourInts);
            listHostFunctionPrototypes.Add(hostFunctionPrototype);
            hostFunctionPrototype = new HostFunctionPrototype(typeBool, "Gfx_DrawString", typeInt, typeInt, typeof(String));
            listHostFunctionPrototypes.Add(hostFunctionPrototype);

            s_listHostFunctionPrototypes = listHostFunctionPrototypes.AsReadOnly();
        }

        public object OnHostFunctionCall(String strFunctionName, List<object> listParameters)
        {
            if (strFunctionName == "Gfx_Initialise")
            {
                if (m_form != null) return false;
                int iWidth = (int) listParameters[0];
                int iHeight = (int)listParameters[1];
                if (iWidth < 16) return false;
                if (iHeight < 16) return false;
                m_form = new GraphicsForm();
                m_form.Width = iWidth;
                m_form.Height = iHeight;
                m_form.Paint += new PaintEventHandler(OnPaint);
                m_form.Show();
                m_listDrawingInstructions.Clear();
                m_pen = new Pen(Color.Black);
                m_brush = new SolidBrush(Color.Black);
                m_font = new Font(FontFamily.GenericSansSerif, 10.0f);
                m_form.Invalidate();
                return true;
            }
            else if (strFunctionName == "Gfx_Shutdown")
            {
                if (m_form == null) return true;
                m_form.Close();
                m_listDrawingInstructions.Clear();
                m_form.Invalidate();
                m_form = null;
                return true;
            }
            else if (strFunctionName == "Gfx_Clear")
            {
                if (m_form == null) return false;
                m_listDrawingInstructions.Clear();
                m_form.Invalidate();
                return true;
            }
            else if (strFunctionName == "Gfx_SetColour")
            {
                if (m_form == null) return false;

                DrawingInstruction drawingInstruction
                    = new DrawingInstruction();
                drawingInstruction.m_drawingPrimitive = DrawingPrimitive.Colour;
                drawingInstruction.m_iValue0 = (int)listParameters[0];
                drawingInstruction.m_iValue1 = (int)listParameters[1];
                drawingInstruction.m_iValue2 = (int)listParameters[2];
                m_listDrawingInstructions.Add(drawingInstruction);
                m_form.Invalidate();
                return true;
            }
            else if (strFunctionName == "Gfx_SetLineWidth")
            {
                if (m_form == null) return false;

                DrawingInstruction drawingInstruction
                    = new DrawingInstruction();
                drawingInstruction.m_drawingPrimitive = DrawingPrimitive.LineWidth;
                drawingInstruction.m_iValue0 = (int)listParameters[0];
                m_listDrawingInstructions.Add(drawingInstruction);
                m_form.Invalidate();
                return true;
            }
            else if (strFunctionName == "Gfx_DrawLine")
            {
                if (m_form == null) return false;
                DrawingInstruction drawingInstruction
                    = new DrawingInstruction();
                drawingInstruction.m_drawingPrimitive = DrawingPrimitive.Line;
                drawingInstruction.m_iValue0 = (int)listParameters[0];
                drawingInstruction.m_iValue1 = (int)listParameters[1];
                drawingInstruction.m_iValue2 = (int)listParameters[2];
                drawingInstruction.m_iValue3 = (int)listParameters[3];
                m_listDrawingInstructions.Add(drawingInstruction);
                m_form.Invalidate();
                return true;
            }
            else if (strFunctionName == "Gfx_DrawRectangle")
            {
                if (m_form == null) return false;
                DrawingInstruction drawingInstruction
                    = new DrawingInstruction();
                drawingInstruction.m_drawingPrimitive = DrawingPrimitive.DrawRectangle;
                drawingInstruction.m_iValue0 = (int)listParameters[0];
                drawingInstruction.m_iValue1 = (int)listParameters[1];
                drawingInstruction.m_iValue2 = (int)listParameters[2];
                drawingInstruction.m_iValue3 = (int)listParameters[3];
                m_listDrawingInstructions.Add(drawingInstruction);
                m_form.Invalidate();
                return true;
            }
            else if (strFunctionName == "Gfx_FillRectangle")
            {
                if (m_form == null) return false;
                DrawingInstruction drawingInstruction
                    = new DrawingInstruction();
                drawingInstruction.m_drawingPrimitive = DrawingPrimitive.FillRectangle;
                drawingInstruction.m_iValue0 = (int)listParameters[0];
                drawingInstruction.m_iValue1 = (int)listParameters[1];
                drawingInstruction.m_iValue2 = (int)listParameters[2];
                drawingInstruction.m_iValue3 = (int)listParameters[3];
                m_listDrawingInstructions.Add(drawingInstruction);
                m_form.Invalidate();
                return true;
            }
            else if (strFunctionName == "Gfx_DrawEllipse")
            {
                if (m_form == null) return false;
                DrawingInstruction drawingInstruction
                    = new DrawingInstruction();
                drawingInstruction.m_drawingPrimitive = DrawingPrimitive.DrawEllipse;
                drawingInstruction.m_iValue0 = (int)listParameters[0];
                drawingInstruction.m_iValue1 = (int)listParameters[1];
                drawingInstruction.m_iValue2 = (int)listParameters[2];
                drawingInstruction.m_iValue3 = (int)listParameters[3];
                m_listDrawingInstructions.Add(drawingInstruction);
                m_form.Invalidate();
                return true;
            }
            else if (strFunctionName == "Gfx_FillEllipse")
            {
                if (m_form == null) return false;
                DrawingInstruction drawingInstruction
                    = new DrawingInstruction();
                drawingInstruction.m_drawingPrimitive = DrawingPrimitive.FillEllipse;
                drawingInstruction.m_iValue0 = (int)listParameters[0];
                drawingInstruction.m_iValue1 = (int)listParameters[1];
                drawingInstruction.m_iValue2 = (int)listParameters[2];
                drawingInstruction.m_iValue3 = (int)listParameters[3];
                m_listDrawingInstructions.Add(drawingInstruction);
                m_form.Invalidate();
                return true;
            }
            else if (strFunctionName == "Gfx_DrawString")
            {
                if (m_form == null) return false;
                DrawingInstruction drawingInstruction
                    = new DrawingInstruction();
                drawingInstruction.m_drawingPrimitive = DrawingPrimitive.DrawString;
                drawingInstruction.m_iValue0 = (int)listParameters[0];
                drawingInstruction.m_iValue1 = (int)listParameters[1];
                drawingInstruction.m_strValue4 = (String)listParameters[2];
                m_listDrawingInstructions.Add(drawingInstruction);
                m_form.Invalidate();
                return true;
            }
            return false;
        }

        #endregion

        #region Public Properties

        public ReadOnlyCollection<HostFunctionPrototype> HostFunctionPrototypes
        {
            get
            {
                return s_listHostFunctionPrototypes;
            }
        }

        #endregion
    }
}
