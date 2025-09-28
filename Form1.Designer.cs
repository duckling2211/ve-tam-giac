namespace Draw
{
    partial class FormMain
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.yCord = new System.Windows.Forms.Label();
            this.xCord = new System.Windows.Forms.Label();
            this.Area = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Silver;
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Draw";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PaintMode);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button2.Location = new System.Drawing.Point(0, 25);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(60, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Select";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            this.button2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SelectMode);
            // 
            // yCord
            // 
            this.yCord.AutoSize = true;
            this.yCord.BackColor = System.Drawing.Color.Transparent;
            this.yCord.Location = new System.Drawing.Point(0, 92);
            this.yCord.Name = "yCord";
            this.yCord.Size = new System.Drawing.Size(20, 16);
            this.yCord.TabIndex = 3;
            this.yCord.Text = "y: ";
            this.yCord.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // xCord
            // 
            this.xCord.AutoSize = true;
            this.xCord.BackColor = System.Drawing.Color.Transparent;
            this.xCord.Location = new System.Drawing.Point(0, 76);
            this.xCord.Name = "xCord";
            this.xCord.Size = new System.Drawing.Size(19, 16);
            this.xCord.TabIndex = 2;
            this.xCord.Text = "x: ";
            this.xCord.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Area
            // 
            this.Area.AutoSize = true;
            this.Area.BackColor = System.Drawing.Color.Transparent;
            this.Area.Location = new System.Drawing.Point(0, 108);
            this.Area.Name = "Area";
            this.Area.Size = new System.Drawing.Size(42, 16);
            this.Area.TabIndex = 4;
            this.Area.Text = "Area: ";
            this.Area.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(65, 0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(28, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "?";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button4.Location = new System.Drawing.Point(0, 50);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(60, 23);
            this.button4.TabIndex = 6;
            this.button4.Text = "Move";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.Move_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1687, 819);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.Area);
            this.Controls.Add(this.yCord);
            this.Controls.Add(this.xCord);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "FormMain";
            this.TransparencyKey = System.Drawing.Color.Transparent;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.SizeChanged += new System.EventHandler(this.FormMain_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormMain_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormMain_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormMain_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label yCord;
        private System.Windows.Forms.Label xCord;
        private System.Windows.Forms.Label Area;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}

