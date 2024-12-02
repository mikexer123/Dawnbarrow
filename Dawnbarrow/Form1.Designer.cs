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
            Background = new PictureBox();
            pictureBox3 = new PictureBox();
            NGL = new Label();
            Equip = new Label();
            bryant = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Background).BeginInit();
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
            ConsoleOut.ReadOnly = true;
            ConsoleOut.Size = new Size(1322, 455);
            ConsoleOut.TabIndex = 0;
            ConsoleOut.Text = resources.GetString("ConsoleOut.Text");
            ConsoleOut.TextChanged += ConsoleOut_TextChanged;
            // 
            // InputBox
            // 
            InputBox.Font = new Font("Consolas", 15F);
            InputBox.Location = new Point(12, 762);
            InputBox.Margin = new Padding(3, 2, 3, 2);
            InputBox.Multiline = true;
            InputBox.Name = "InputBox";
            InputBox.Size = new Size(1322, 39);
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
            label1.Size = new Size(130, 35);
            label1.TabIndex = 4;
            label1.Text = "CurrCoords";
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
            PlayerHP.Location = new Point(958, 122);
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
            XP.Location = new Point(958, 80);
            XP.Name = "XP";
            XP.Size = new Size(152, 27);
            XP.TabIndex = 7;
            XP.Text = "Experience";
            // 
            // Background
            // 
            Background.Image = (Image)resources.GetObject("Background.Image");
            Background.Location = new Point(0, 0);
            Background.Name = "Background";
            Background.Size = new Size(952, 298);
            Background.TabIndex = 8;
            Background.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Location = new Point(0, 0);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(100, 50);
            pictureBox3.TabIndex = 9;
            pictureBox3.TabStop = false;
            // 
            // NGL
            // 
            NGL.AutoSize = true;
            NGL.BackColor = Color.LightCoral;
            NGL.Font = new Font("SimSun-ExtB", 16F);
            NGL.Location = new Point(958, 39);
            NGL.Name = "NGL";
            NGL.Size = new Size(278, 27);
            NGL.TabIndex = 11;
            NGL.Text = "Name / Gender / Lvl";
            // 
            // Equip
            // 
            Equip.AutoSize = true;
            Equip.BackColor = Color.LightCoral;
            Equip.Font = new Font("SimSun-ExtB", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Equip.Location = new Point(958, 164);
            Equip.Name = "Equip";
            Equip.Size = new Size(236, 28);
            Equip.TabIndex = 12;
            Equip.Text = "Current Equipped";
            // 
            // bryant
            // 
            bryant.AutoSize = true;
            bryant.Location = new Point(979, 224);
            bryant.Name = "bryant";
            bryant.Size = new Size(55, 15);
            bryant.TabIndex = 13;
            bryant.Text = "bryant";
            // 
            // Dawnbarrow
            // 
            AcceptButton = submit_button;
            AutoScaleDimensions = new SizeF(8F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.RosyBrown;
            ClientSize = new Size(1484, 804);
            Controls.Add(bryant);
            Controls.Add(Equip);
            Controls.Add(NGL);
            Controls.Add(XP);
            Controls.Add(PlayerHP);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Controls.Add(submit_button);
            Controls.Add(InputBox);
            Controls.Add(ConsoleOut);
            Controls.Add(Background);
            Controls.Add(pictureBox3);
            Font = new Font("SimSun-ExtB", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            Name = "Dawnbarrow";
            Text = "Dawnbarrow";
            Load += Dawnbarrow_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)Background).EndInit();
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
        private PictureBox Background;
        private PictureBox pictureBox3;
        public Label NGL;
        public Label Equip;
        private Label bryant;
    }

}
