using System;
using System.Drawing;

#if REFERENCE_WINDOWS_FORMS
using Clipboard = System.Windows.Forms.Clipboard;
#endif

namespace GLGUI
{
	public class GLLabel : GLControl
	{
        public string Text { get { return text; } set { if (value != text) { text = value; Invalidate(); } } }
        public bool Enabled { get { return enabled; } set { enabled = value; Invalidate(); } }
        public GLSkin.GLLabelSkin SkinEnabled { get { return skinEnabled; } set { skinEnabled = value; Invalidate(); } }
        public GLSkin.GLLabelSkin SkinDisabled { get { return skinDisabled; } set { skinDisabled = value; Invalidate(); } }

        public bool WordWrap = false;
        public bool Multiline = false;

		private string text = "";
		private GLFontText textProcessed = new GLFontText();
		private SizeF textSize;
		private GLSkin.GLLabelSkin skinEnabled, skinDisabled;
		private GLSkin.GLLabelSkin skin;
		private bool enabled = true;

		public GLLabel(GLGui gui) : base(gui)
		{
			Render += OnRender;

			skinEnabled = Gui.Skin.LabelEnabled;
			skinDisabled = Gui.Skin.LabelDisabled;

			outer = new Rectangle(0, 0, 0, 0);
			sizeMin = new Size(1, (int)skinEnabled.Font.LineSpacing + skinEnabled.Padding.Vertical);
			sizeMax = new Size(int.MaxValue, int.MaxValue);

            HandleMouseEvents = false;
		}

        protected override void UpdateLayout()
		{
			skin = Enabled ? skinEnabled : skinDisabled;

            textSize = skin.Font.ProcessText(textProcessed, text,
                new SizeF(WordWrap ? (AutoSize ? SizeMax.Width - skin.Padding.Horizontal : outer.Width - skin.Padding.Horizontal) : float.MaxValue, Multiline ? (AutoSize ? float.MaxValue : outer.Height - skin.Padding.Vertical) : skin.Font.LineSpacing),
                skin.TextAlign);

			if (AutoSize)
			{
				outer.Width = (int)textSize.Width + skin.Padding.Horizontal;
                outer.Height = (int)textSize.Height + skin.Padding.Vertical;
			}

            outer.Width = Math.Min(Math.Max(outer.Width, sizeMin.Width), sizeMax.Width);
            outer.Height = Math.Min(Math.Max(outer.Height, sizeMin.Height), sizeMax.Height);

			Inner = new Rectangle(skin.Padding.Left, skin.Padding.Top, outer.Width - skin.Padding.Horizontal, outer.Height - skin.Padding.Vertical);
		}

        private void OnRender(object sender, double timeDelta)
		{
            GLDraw.Fill(ref skin.BackgroundColor);
            GLDraw.Text(textProcessed, Inner, ref skin.Color);
		}
	}
}

