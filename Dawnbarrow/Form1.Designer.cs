namespace Dawnbarrow
{
    partial class Dawnbarrow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dawnbarrow));
            ConsoleOut = new RichTextBox();
            InputBox = new TextBox();
            submit_button = new Button();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            PlayerHP = new Label();
            XP = new Label();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            ArAt = new Label();
            NGL = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // ConsoleOut
            // 
            ConsoleOut.AcceptsTab = true;
            ConsoleOut.BackColor = Color.Black;
            ConsoleOut.EnableAutoDragDrop = true;
            ConsoleOut.Font = new Font("Consolas", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ConsoleOut.ForeColor = SystemColors.ActiveCaption;
            ConsoleOut.ImeMode = ImeMode.Off;
            ConsoleOut.Location = new Point(12, 303);
            ConsoleOut.Margin = new Padding(3, 2, 3, 2);
            ConsoleOut.Name = "ConsoleOut";
            ConsoleOut.Size = new Size(1322, 455);
            ConsoleOut.TabIndex = 0;
            ConsoleOut.Text = resources.GetString("ConsoleOut.Text");
            ConsoleOut.TextChanged += ConsoleOut_TextChanged;
            // 
            // InputBox
            // 
            InputBox.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            InputBox.Location = new Point(12, 762);
            InputBox.Margin = new Padding(3, 2, 3, 2);
            InputBox.Multiline = true;
            InputBox.Name = "InputBox";
            InputBox.Size = new Size(1322, 31);
            InputBox.TabIndex = 1;
            InputBox.Text = "Click To Type Here";
            InputBox.MouseClick += InputBox_MouseClick;
            InputBox.TextChanged += InputBox_TextChanged;
            // 
            // submit_button
            // 
            submit_button.Font = new Font("Rockwell Extra Bold", 9F);
            submit_button.Location = new Point(1354, 578);
            submit_button.Margin = new Padding(3, 2, 3, 2);
            submit_button.Name = "submit_button";
            submit_button.Size = new Size(118, 215);
            submit_button.TabIndex = 2;
            submit_button.Text = "Submit";
            submit_button.UseVisualStyleBackColor = true;
            submit_button.Click += submit_button_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.Highlight;
            label1.Font = new Font("Sitka Banner", 14.2499981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(9, 7);
            label1.Name = "label1";
            label1.Size = new Size(212, 35);
            label1.TabIndex = 4;
            label1.Text = "CurrentCoordinates";
            label1.Click += label1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Blue;
            pictureBox1.Location = new Point(1354, 144);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(118, 429);
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // PlayerHP
            // 
            PlayerHP.AutoSize = true;
            PlayerHP.BackColor = Color.LightCoral;
            PlayerHP.Font = new Font("SimSun-ExtB", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            PlayerHP.Location = new Point(958, 225);
            PlayerHP.Name = "PlayerHP";
            PlayerHP.Size = new Size(124, 28);
            PlayerHP.TabIndex = 6;
            PlayerHP.Text = "PlayerHP";
            // 
            // XP
            // 
            XP.AutoSize = true;
            XP.BackColor = Color.LightCoral;
            XP.Font = new Font("SimSun-ExtB", 16F);
            XP.Location = new Point(958, 176);
            XP.Name = "XP";
            XP.Size = new Size(152, 27);
            XP.TabIndex = 7;
            XP.Text = "Experience";
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(0, 0);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(100, 50);
            pictureBox2.TabIndex = 8;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Location = new Point(0, 0);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(100, 50);
            pictureBox3.TabIndex = 9;
            pictureBox3.TabStop = false;
            // 
            // ArAt
            // 
            ArAt.AutoSize = true;
            ArAt.BackColor = Color.LightCoral;
            ArAt.Font = new Font("SimSun-ExtB", 16F);
            ArAt.Location = new Point(958, 274);
            ArAt.Name = "ArAt";
            ArAt.Size = new Size(236, 27);
            ArAt.TabIndex = 10;
            ArAt.Text = "Armor and Attack";
            ArAt.Click += ArAt_Click;
            // 
            // NGL
            // 
            NGL.AutoSize = true;
            NGL.BackColor = Color.LightCoral;
            NGL.Font = new Font("SimSun-ExtB", 16F);
            NGL.Location = new Point(958, 128);
            NGL.Name = "NGL";
            NGL.Size = new Size(278, 27);
            NGL.TabIndex = 11;
            NGL.Text = "Name / Gender / Lvl";
            // 
            // Dawnbarrow
            // 
            AutoScaleDimensions = new SizeF(8F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.RosyBrown;
            ClientSize = new Size(1484, 804);
            Controls.Add(NGL);
            Controls.Add(ArAt);
            Controls.Add(XP);
            Controls.Add(PlayerHP);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Controls.Add(submit_button);
            Controls.Add(InputBox);
            Controls.Add(ConsoleOut);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox3);
            Font = new Font("SimSun-ExtB", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            Name = "Dawnbarrow";
            Text = "Dawnbarrow";
            Load += Dawnbarrow_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox ConsoleOut;
        private TextBox InputBox;
        private Button submit_button;
        private Label label1;
        private PictureBox pictureBox1;
        public Label PlayerHP;
        public Label XP;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        public Label ArAt;
        public Label NGL;
    }

}
