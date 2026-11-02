namespace myOpenGL
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.hScrollBar2 = new System.Windows.Forms.HScrollBar();
            this.hScrollBar3 = new System.Windows.Forms.HScrollBar();
            this.hScrollBar4 = new System.Windows.Forms.HScrollBar();
            this.hScrollBar5 = new System.Windows.Forms.HScrollBar();
            this.hScrollBar6 = new System.Windows.Forms.HScrollBar();
            this.hScrollBar7 = new System.Windows.Forms.HScrollBar();
            this.hScrollBar8 = new System.Windows.Forms.HScrollBar();
            this.hScrollBar9 = new System.Windows.Forms.HScrollBar();
            this.checkBoxPerspective = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.checkBoxAxes = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.hScrollBarLightR = new System.Windows.Forms.HScrollBar();
            this.hScrollBarLightG = new System.Windows.Forms.HScrollBar();
            this.label11 = new System.Windows.Forms.Label();
            this.hScrollBarLightB = new System.Windows.Forms.HScrollBar();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.hScrollBarAmbient = new System.Windows.Forms.HScrollBar();
            this.label14 = new System.Windows.Forms.Label();
            this.hScrollBarLightZ = new System.Windows.Forms.HScrollBar();
            this.hScrollBarFOV = new System.Windows.Forms.HScrollBar();
            this.label15 = new System.Windows.Forms.Label();
            this.hScrollBarLightX = new System.Windows.Forms.HScrollBar();
            this.label16 = new System.Windows.Forms.Label();
            this.checkBoxSpotlight = new System.Windows.Forms.CheckBox();
            this.checkBoxLight = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1040, 648);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Location = new System.Drawing.Point(1153, 15);
            this.hScrollBar1.Maximum = 200;
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(163, 25);
            this.hScrollBar1.TabIndex = 1;
            this.hScrollBar1.Tag = "";
            this.hScrollBar1.Value = 100;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar_Scroll);
            // 
            // hScrollBar2
            // 
            this.hScrollBar2.Location = new System.Drawing.Point(1153, 48);
            this.hScrollBar2.Maximum = 200;
            this.hScrollBar2.Name = "hScrollBar2";
            this.hScrollBar2.Size = new System.Drawing.Size(163, 25);
            this.hScrollBar2.TabIndex = 2;
            this.hScrollBar2.Value = 100;
            this.hScrollBar2.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar_Scroll);
            // 
            // hScrollBar3
            // 
            this.hScrollBar3.Location = new System.Drawing.Point(1153, 78);
            this.hScrollBar3.Maximum = 200;
            this.hScrollBar3.Name = "hScrollBar3";
            this.hScrollBar3.Size = new System.Drawing.Size(163, 25);
            this.hScrollBar3.TabIndex = 3;
            this.hScrollBar3.Value = 100;
            this.hScrollBar3.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar_Scroll);
            // 
            // hScrollBar4
            // 
            this.hScrollBar4.Location = new System.Drawing.Point(1153, 110);
            this.hScrollBar4.Maximum = 200;
            this.hScrollBar4.Name = "hScrollBar4";
            this.hScrollBar4.Size = new System.Drawing.Size(163, 25);
            this.hScrollBar4.TabIndex = 4;
            this.hScrollBar4.Value = 100;
            this.hScrollBar4.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar_Scroll);
            // 
            // hScrollBar5
            // 
            this.hScrollBar5.Location = new System.Drawing.Point(1153, 144);
            this.hScrollBar5.Maximum = 200;
            this.hScrollBar5.Name = "hScrollBar5";
            this.hScrollBar5.Size = new System.Drawing.Size(163, 25);
            this.hScrollBar5.TabIndex = 5;
            this.hScrollBar5.Value = 100;
            this.hScrollBar5.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar_Scroll);
            // 
            // hScrollBar6
            // 
            this.hScrollBar6.Location = new System.Drawing.Point(1153, 182);
            this.hScrollBar6.Maximum = 200;
            this.hScrollBar6.Name = "hScrollBar6";
            this.hScrollBar6.Size = new System.Drawing.Size(163, 25);
            this.hScrollBar6.TabIndex = 6;
            this.hScrollBar6.Value = 100;
            this.hScrollBar6.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar_Scroll);
            // 
            // hScrollBar7
            // 
            this.hScrollBar7.Location = new System.Drawing.Point(1153, 217);
            this.hScrollBar7.Maximum = 200;
            this.hScrollBar7.Name = "hScrollBar7";
            this.hScrollBar7.Size = new System.Drawing.Size(163, 25);
            this.hScrollBar7.TabIndex = 7;
            this.hScrollBar7.Value = 100;
            this.hScrollBar7.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar_Scroll);
            // 
            // hScrollBar8
            // 
            this.hScrollBar8.Location = new System.Drawing.Point(1153, 249);
            this.hScrollBar8.Maximum = 200;
            this.hScrollBar8.Name = "hScrollBar8";
            this.hScrollBar8.Size = new System.Drawing.Size(163, 25);
            this.hScrollBar8.TabIndex = 8;
            this.hScrollBar8.Value = 100;
            this.hScrollBar8.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar_Scroll);
            // 
            // hScrollBar9
            // 
            this.hScrollBar9.Location = new System.Drawing.Point(1153, 280);
            this.hScrollBar9.Maximum = 200;
            this.hScrollBar9.Name = "hScrollBar9";
            this.hScrollBar9.Size = new System.Drawing.Size(163, 25);
            this.hScrollBar9.TabIndex = 9;
            this.hScrollBar9.Value = 100;
            this.hScrollBar9.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar_Scroll);
            // 
            // checkBoxPerspective
            // 
            this.checkBoxPerspective.AutoSize = true;
            this.checkBoxPerspective.Location = new System.Drawing.Point(1075, 548);
            this.checkBoxPerspective.Name = "checkBoxPerspective";
            this.checkBoxPerspective.Size = new System.Drawing.Size(101, 20);
            this.checkBoxPerspective.TabIndex = 10;
            this.checkBoxPerspective.Text = "Perspective";
            this.checkBoxPerspective.UseVisualStyleBackColor = true;
            this.checkBoxPerspective.CheckedChanged += new System.EventHandler(this.checkBoxPerspective_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1072, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = "Eye X:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1072, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 16);
            this.label2.TabIndex = 12;
            this.label2.Text = "Eye Y:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1072, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 16);
            this.label3.TabIndex = 13;
            this.label3.Text = "Eye Z:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1072, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 16);
            this.label4.TabIndex = 14;
            this.label4.Text = "Center X:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1072, 153);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 16);
            this.label5.TabIndex = 15;
            this.label5.Text = "Center Y:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1072, 191);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 16);
            this.label6.TabIndex = 16;
            this.label6.Text = "Center Z:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1072, 226);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 16);
            this.label7.TabIndex = 17;
            this.label7.Text = "Up X:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1072, 258);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 16);
            this.label8.TabIndex = 18;
            this.label8.Text = "Up Y:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1072, 289);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 16);
            this.label9.TabIndex = 19;
            this.label9.Text = "Up Z:";
            // 
            // checkBoxAxes
            // 
            this.checkBoxAxes.AutoSize = true;
            this.checkBoxAxes.Location = new System.Drawing.Point(1075, 595);
            this.checkBoxAxes.Name = "checkBoxAxes";
            this.checkBoxAxes.Size = new System.Drawing.Size(95, 20);
            this.checkBoxAxes.TabIndex = 20;
            this.checkBoxAxes.Text = "Show Axes";
            this.checkBoxAxes.UseVisualStyleBackColor = true;
            this.checkBoxAxes.CheckedChanged += new System.EventHandler(this.checkBoxAxes_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(1072, 347);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 16);
            this.label10.TabIndex = 21;
            this.label10.Text = "Light R:";
            // 
            // hScrollBarLightR
            // 
            this.hScrollBarLightR.Location = new System.Drawing.Point(1153, 347);
            this.hScrollBarLightR.Maximum = 209;
            this.hScrollBarLightR.Name = "hScrollBarLightR";
            this.hScrollBarLightR.Size = new System.Drawing.Size(114, 21);
            this.hScrollBarLightR.TabIndex = 22;
            this.hScrollBarLightR.Value = 190;
            this.hScrollBarLightR.Scroll += new System.Windows.Forms.ScrollEventHandler(this.LightScrollChanged);
            // 
            // hScrollBarLightG
            // 
            this.hScrollBarLightG.Location = new System.Drawing.Point(1153, 377);
            this.hScrollBarLightG.Maximum = 209;
            this.hScrollBarLightG.Name = "hScrollBarLightG";
            this.hScrollBarLightG.Size = new System.Drawing.Size(114, 21);
            this.hScrollBarLightG.TabIndex = 24;
            this.hScrollBarLightG.Value = 190;
            this.hScrollBarLightG.Scroll += new System.Windows.Forms.ScrollEventHandler(this.LightScrollChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(1072, 377);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 16);
            this.label11.TabIndex = 23;
            this.label11.Text = "Light G:";
            // 
            // hScrollBarLightB
            // 
            this.hScrollBarLightB.Location = new System.Drawing.Point(1153, 406);
            this.hScrollBarLightB.Maximum = 209;
            this.hScrollBarLightB.Name = "hScrollBarLightB";
            this.hScrollBarLightB.Size = new System.Drawing.Size(114, 21);
            this.hScrollBarLightB.TabIndex = 26;
            this.hScrollBarLightB.Value = 190;
            this.hScrollBarLightB.Scroll += new System.Windows.Forms.ScrollEventHandler(this.LightScrollChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(1072, 406);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(50, 16);
            this.label12.TabIndex = 25;
            this.label12.Text = "Light B:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(1072, 438);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(59, 16);
            this.label13.TabIndex = 27;
            this.label13.Text = "Ambient:";
            // 
            // hScrollBarAmbient
            // 
            this.hScrollBarAmbient.Location = new System.Drawing.Point(1153, 438);
            this.hScrollBarAmbient.Maximum = 209;
            this.hScrollBarAmbient.Name = "hScrollBarAmbient";
            this.hScrollBarAmbient.Size = new System.Drawing.Size(114, 21);
            this.hScrollBarAmbient.TabIndex = 28;
            this.hScrollBarAmbient.Value = 60;
            this.hScrollBarAmbient.Scroll += new System.Windows.Forms.ScrollEventHandler(this.LightScrollChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(1072, 571);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(34, 16);
            this.label14.TabIndex = 33;
            this.label14.Text = "FOV";
            // 
            // hScrollBarLightZ
            // 
            this.hScrollBarLightZ.Location = new System.Drawing.Point(1153, 498);
            this.hScrollBarLightZ.Maximum = 209;
            this.hScrollBarLightZ.Name = "hScrollBarLightZ";
            this.hScrollBarLightZ.Size = new System.Drawing.Size(114, 21);
            this.hScrollBarLightZ.TabIndex = 32;
            this.hScrollBarLightZ.Value = 120;
            this.hScrollBarLightZ.Scroll += new System.Windows.Forms.ScrollEventHandler(this.LightScrollChanged);
            // 
            // hScrollBarFOV
            // 
            this.hScrollBarFOV.Location = new System.Drawing.Point(1153, 571);
            this.hScrollBarFOV.Maximum = 209;
            this.hScrollBarFOV.Name = "hScrollBarFOV";
            this.hScrollBarFOV.Size = new System.Drawing.Size(114, 21);
            this.hScrollBarFOV.TabIndex = 34;
            this.hScrollBarFOV.Value = 145;
            this.hScrollBarFOV.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ViewScrollChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(1072, 498);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(49, 16);
            this.label15.TabIndex = 31;
            this.label15.Text = "Light Z:";
            // 
            // hScrollBarLightX
            // 
            this.hScrollBarLightX.Location = new System.Drawing.Point(1153, 468);
            this.hScrollBarLightX.Maximum = 209;
            this.hScrollBarLightX.Name = "hScrollBarLightX";
            this.hScrollBarLightX.Size = new System.Drawing.Size(114, 21);
            this.hScrollBarLightX.TabIndex = 30;
            this.hScrollBarLightX.Value = 120;
            this.hScrollBarLightX.Scroll += new System.Windows.Forms.ScrollEventHandler(this.LightScrollChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(1072, 468);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(49, 16);
            this.label16.TabIndex = 29;
            this.label16.Text = "Light X:";
            // 
            // checkBoxSpotlight
            // 
            this.checkBoxSpotlight.AutoSize = true;
            this.checkBoxSpotlight.Location = new System.Drawing.Point(1075, 522);
            this.checkBoxSpotlight.Name = "checkBoxSpotlight";
            this.checkBoxSpotlight.Size = new System.Drawing.Size(81, 20);
            this.checkBoxSpotlight.TabIndex = 35;
            this.checkBoxSpotlight.Text = "Spotlight";
            this.checkBoxSpotlight.UseVisualStyleBackColor = true;
            this.checkBoxSpotlight.CheckedChanged += new System.EventHandler(this.checkBoxSpotlight_CheckedChanged);
            // 
            // checkBoxLight
            // 
            this.checkBoxLight.AutoSize = true;
            this.checkBoxLight.Checked = true;
            this.checkBoxLight.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxLight.Location = new System.Drawing.Point(1075, 320);
            this.checkBoxLight.Name = "checkBoxLight";
            this.checkBoxLight.Size = new System.Drawing.Size(57, 20);
            this.checkBoxLight.TabIndex = 36;
            this.checkBoxLight.Text = "Light";
            this.checkBoxLight.UseVisualStyleBackColor = true;
            this.checkBoxLight.CheckedChanged += new System.EventHandler(this.checkBoxLight_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1341, 672);
            this.Controls.Add(this.checkBoxLight);
            this.Controls.Add(this.checkBoxSpotlight);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.hScrollBarLightZ);
            this.Controls.Add(this.hScrollBarFOV);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.hScrollBarLightX);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.hScrollBarAmbient);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.hScrollBarLightG);
            this.Controls.Add(this.hScrollBarLightB);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.hScrollBarLightR);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.checkBoxAxes);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxPerspective);
            this.Controls.Add(this.hScrollBar9);
            this.Controls.Add(this.hScrollBar8);
            this.Controls.Add(this.hScrollBar7);
            this.Controls.Add(this.hScrollBar6);
            this.Controls.Add(this.hScrollBar5);
            this.Controls.Add(this.hScrollBar4);
            this.Controls.Add(this.hScrollBar3);
            this.Controls.Add(this.hScrollBar2);
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.HScrollBar hScrollBar2;
        private System.Windows.Forms.HScrollBar hScrollBar3;
        private System.Windows.Forms.HScrollBar hScrollBar4;
        private System.Windows.Forms.HScrollBar hScrollBar5;
        private System.Windows.Forms.HScrollBar hScrollBar6;
        private System.Windows.Forms.HScrollBar hScrollBar7;
        private System.Windows.Forms.HScrollBar hScrollBar8;
        private System.Windows.Forms.HScrollBar hScrollBar9;
        private System.Windows.Forms.CheckBox checkBoxPerspective;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox checkBoxAxes;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.HScrollBar hScrollBarLightB;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.HScrollBar hScrollBarLightR;
        private System.Windows.Forms.HScrollBar hScrollBarLightG;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.HScrollBar hScrollBarAmbient;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.HScrollBar hScrollBarLightZ;
        private System.Windows.Forms.HScrollBar hScrollBarFOV;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.HScrollBar hScrollBarLightX;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.CheckBox checkBoxSpotlight;
        private System.Windows.Forms.CheckBox checkBoxLight;
        private System.Windows.Forms.Timer timer1;
    }
}