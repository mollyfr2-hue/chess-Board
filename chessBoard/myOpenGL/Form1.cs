using System;
using System.Windows.Forms;
using OpenGL;

namespace myOpenGL
{
    public partial class Form1 : Form
    {
        cOGL cGL;

        public Form1()
        {
            InitializeComponent();

            // camera scrollbar ranges
            HScrollBar[] bars = { hScrollBar1, hScrollBar2, hScrollBar3,
                                  hScrollBar4, hScrollBar5, hScrollBar6,
                                  hScrollBar7, hScrollBar8, hScrollBar9 };
            foreach (HScrollBar hb in bars)
            {
                hb.Minimum = 0;
                hb.Maximum = 209;
                hb.LargeChange = 10;
                hb.SmallChange = 1;
            }

            // light + view scrollbar ranges
            HScrollBar[] lightBars = { hScrollBarLightR, hScrollBarLightG, hScrollBarLightB,
                                       hScrollBarAmbient, hScrollBarLightX, hScrollBarLightZ,
                                       hScrollBarFOV };
            foreach (HScrollBar hb in lightBars)
            {
                hb.Minimum = 0;
                hb.Maximum = 209;
                hb.LargeChange = 10;
                hb.SmallChange = 1;
            }

            cGL = new cOGL(panel1);

            // default camera
            hScrollBar1.Value = 100;
            hScrollBar2.Value = 150;
            hScrollBar3.Value = 200;
            hScrollBar4.Value = 100;
            hScrollBar5.Value = 100;
            hScrollBar6.Value = 100;
            hScrollBar7.Value = 100;
            hScrollBar8.Value = 110;
            hScrollBar9.Value = 100;

            // default lighting
            hScrollBarLightR.Value = 190;
            hScrollBarLightG.Value = 190;
            hScrollBarLightB.Value = 190;
            hScrollBarAmbient.Value = 60;
            hScrollBarLightX.Value = 120;
            hScrollBarLightZ.Value = 120;
            hScrollBarFOV.Value = 145;

            // default checkbox states
            checkBoxLight.Checked = true;
            checkBoxSpotlight.Checked = false;
            checkBoxPerspective.Checked = false;
            checkBoxAxes.Checked = true;

            // wire light/view scrollbar events
            hScrollBarLightR.Scroll += LightScrollChanged;
            hScrollBarLightG.Scroll += LightScrollChanged;
            hScrollBarLightB.Scroll += LightScrollChanged;
            hScrollBarAmbient.Scroll += LightScrollChanged;
            hScrollBarLightX.Scroll += LightScrollChanged;
            hScrollBarLightZ.Scroll += LightScrollChanged;
            hScrollBarFOV.Scroll += ViewScrollChanged;

            // wire checkbox events
            checkBoxLight.CheckedChanged += checkBoxLight_CheckedChanged;
            checkBoxSpotlight.CheckedChanged += checkBoxSpotlight_CheckedChanged;
            checkBoxAxes.CheckedChanged += checkBoxAxes_CheckedChanged;

            // wire panel mouse click for piece selection
            panel1.MouseClick += panel1_MouseClick;

            // animation timer — fires every 16ms (~60fps)
            timer1.Interval = 16;
            timer1.Tick += timer1_Tick;
            timer1.Start();

            ApplyAllScrollbars();
            ApplyLightScrollbars();
        }

        // ── scrollbar helpers ─────────────────────────────────────────────
        void ApplyAllScrollbars()
        {
            cGL.ScrollValue[0] = (hScrollBar1.Value - 100) / 10.0f;
            cGL.ScrollValue[1] = (hScrollBar2.Value - 100) / 10.0f;
            cGL.ScrollValue[2] = (hScrollBar3.Value - 100) / 10.0f;
            cGL.ScrollValue[3] = (hScrollBar4.Value - 100) / 10.0f;
            cGL.ScrollValue[4] = (hScrollBar5.Value - 100) / 10.0f;
            cGL.ScrollValue[5] = (hScrollBar6.Value - 100) / 10.0f;
            cGL.ScrollValue[6] = (hScrollBar7.Value - 100) / 10.0f;
            cGL.ScrollValue[7] = (hScrollBar8.Value - 100) / 10.0f;
            cGL.ScrollValue[8] = (hScrollBar9.Value - 100) / 10.0f;
        }

        void ApplyLightScrollbars()
        {
            cGL.LightR = hScrollBarLightR.Value / 200.0f;
            cGL.LightG = hScrollBarLightG.Value / 200.0f;
            cGL.LightB = hScrollBarLightB.Value / 200.0f;
            cGL.Ambient = hScrollBarAmbient.Value / 200.0f;
            cGL.LightX = (hScrollBarLightX.Value - 100) / 10.0f;
            cGL.LightZ = (hScrollBarLightZ.Value - 100) / 10.0f;
            cGL.FOV = 20.0 + (hScrollBarFOV.Value / 200.0) * 80.0;
        }

        // ── panel events ──────────────────────────────────────────────────
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (cGL == null) return;
            cGL.Draw();
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            if (cGL == null) return;
            cGL.OnResize();
        }

        // ── mouse click → forward to cGL for board square detection ───────
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (cGL == null) return;
            cGL.OnBoardClick(e.X, e.Y);
        }

        // ── animation timer tick ──────────────────────────────────────────
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (cGL == null) return;
            if (cGL.IsAnimating)
                cGL.AnimationTick();
        }

        // ── camera scrollbars ─────────────────────────────────────────────
        private void hScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            cGL.intOptionC = 0;
            ApplyAllScrollbars();
            cGL.Draw();
        }

        // ── light scrollbars ──────────────────────────────────────────────
        private void LightScrollChanged(object sender, ScrollEventArgs e)
        {
            ApplyLightScrollbars();
            cGL.Draw();
        }

        private void ViewScrollChanged(object sender, ScrollEventArgs e)
        {
            ApplyLightScrollbars();
            cGL.SetProjection();
            cGL.Draw();
        }

        // ── checkboxes ────────────────────────────────────────────────────
        private void checkBoxPerspective_CheckedChanged(object sender, EventArgs e)
        {
            cGL.ToggleProjection();
        }

        private void checkBoxLight_CheckedChanged(object sender, EventArgs e)
        {
            cGL.LightEnabled = checkBoxLight.Checked;
            cGL.Draw();
        }

        private void checkBoxSpotlight_CheckedChanged(object sender, EventArgs e)
        {
            cGL.SpotlightEnabled = checkBoxSpotlight.Checked;
            cGL.Draw();
        }

        private void checkBoxAxes_CheckedChanged(object sender, EventArgs e)
        {
            cGL.ShowAxes = checkBoxAxes.Checked;
            cGL.Draw();
        }
    }
}