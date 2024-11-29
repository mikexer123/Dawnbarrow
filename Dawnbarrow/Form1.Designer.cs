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
            Background = new PictureBox();
            PlayerHP = new Label();
            XP = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Background).BeginInit();
            SuspendLayout();
            // 
            // ConsoleOut
            // 
            ConsoleOut.BackColor = Color.Black;
            ConsoleOut.Font = new Font("Sans Serif Collection", 18F, FontStyle.Regular, GraphicsUnit.Pixel, 0, true);
            ConsoleOut.ForeColor = SystemColors.ActiveCaption;
            ConsoleOut.Location = new Point(12, 432);
            ConsoleOut.Margin = new Padding(3, 2, 3, 2);
            ConsoleOut.Name = "ConsoleOut";
            ConsoleOut.Size = new Size(947, 143);
            ConsoleOut.TabIndex = 0;
            ConsoleOut.Text = resources.GetString("ConsoleOut.Text");
            ConsoleOut.TextChanged += ConsoleOut_TextChanged;
            // 
            // InputBox
            // 
            InputBox.Location = new Point(12, 579);
            InputBox.Margin = new Padding(3, 2, 3, 2);
            InputBox.Name = "InputBox";
            InputBox.Size = new Size(947, 22);
            InputBox.TabIndex = 1;
            InputBox.Text = "Click To Type Here";
            InputBox.MouseClick += InputBox_MouseClick;
            InputBox.TextChanged += InputBox_TextChanged;
            // 
            // submit_button
            // 
            submit_button.Font = new Font("Rockwell Extra Bold", 9F);
            submit_button.Location = new Point(963, 432);
            submit_button.Margin = new Padding(3, 2, 3, 2);
            submit_button.Name = "submit_button";
            submit_button.Size = new Size(118, 165);
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
            pictureBox1.Location = new Point(953, -2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(128, 430);
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // Background
            // 
            Background.AccessibleName = "Background";
            Background.BackColor = Color.LavenderBlush;
            Background.Image = (Image)resources.GetObject("Background.Image");
            Background.Location = new Point(-563, -14);
            Background.Margin = new Padding(3, 2, 3, 2);
            Background.Name = "Background";
            Background.Size = new Size(1522, 442);
            Background.TabIndex = 3;
            Background.TabStop = false;
            // 
            // PlayerHP
            // 
            PlayerHP.AutoSize = true;
            PlayerHP.BackColor = Color.LightCoral;
            PlayerHP.Location = new Point(12, 45);
            PlayerHP.Name = "PlayerHP";
            PlayerHP.Size = new Size(71, 15);
            PlayerHP.TabIndex = 6;
            PlayerHP.Text = "PlayerHP";
            // 
            // XP
            // 
            XP.AutoSize = true;
            XP.BackColor = Color.LightCoral;
            XP.Location = new Point(12, 70);
            XP.Name = "XP";
            XP.Size = new Size(87, 15);
            XP.TabIndex = 7;
            XP.Text = "Experience";
            // 
            // Dawnbarrow
            // 
            AutoScaleDimensions = new SizeF(8F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.RosyBrown;
            ClientSize = new Size(1082, 601);
            Controls.Add(XP);
            Controls.Add(PlayerHP);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Controls.Add(submit_button);
            Controls.Add(InputBox);
            Controls.Add(ConsoleOut);
            Controls.Add(Background);
            Font = new Font("SimSun-ExtB", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            Name = "Dawnbarrow";
            Text = "Dawnbarrow";
            Load += Dawnbarrow_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)Background).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox ConsoleOut;
        private TextBox InputBox;
        private Button submit_button;
        private Label label1;
        private PictureBox pictureBox1;
        private PictureBox Background;
        public Label PlayerHP;
        public Label XP;
    }

}
