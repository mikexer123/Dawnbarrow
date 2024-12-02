using System.DirectoryServices.ActiveDirectory;

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
            Pickaxe = new PictureBox();
            Ladder = new PictureBox();
            TalkingCat = new PictureBox();
            BossKey = new PictureBox();
            FriendshipBracelet = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Background).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Pickaxe).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Ladder).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TalkingCat).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BossKey).BeginInit();
            ((System.ComponentModel.ISupportInitialize)FriendshipBracelet).BeginInit();
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
            label1.Size = new Size(102, 28);
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
            PlayerHP.Size = new Size(98, 22);
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
            XP.Size = new Size(120, 22);
            XP.TabIndex = 7;
            XP.Text = "Experience";
            // 
            // Background
            // 
            Background.Image = Properties.Resources._11;
            Background.Location = new Point(0, 0);
            Background.Name = "Background";
            Background.Size = new Size(952, 298);
            Background.TabIndex = 8;
            Background.TabStop = false;
            Background.Click += Background_Click;
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
            NGL.Size = new Size(219, 22);
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
            Equip.Size = new Size(186, 22);
            Equip.TabIndex = 12;
            Equip.Text = "Current Equipped";
            // 
            // Pickaxe
            // 
            Pickaxe.Image = Properties.Resources.EmptyQuest;
            Pickaxe.Location = new Point(1370, 361);
            Pickaxe.Name = "Pickaxe";
            Pickaxe.Size = new Size(42, 38);
            Pickaxe.TabIndex = 13;
            Pickaxe.TabStop = false;
            // 
            // Ladder
            // 
            Ladder.Image = Properties.Resources.EmptyQuest;
            Ladder.Location = new Point(1418, 361);
            Ladder.Name = "Ladder";
            Ladder.Size = new Size(42, 38);
            Ladder.TabIndex = 14;
            Ladder.TabStop = false;
            // 
            // TalkingCat
            // 
            TalkingCat.Image = Properties.Resources.EmptyQuest;
            TalkingCat.Location = new Point(1370, 405);
            TalkingCat.Name = "TalkingCat";
            TalkingCat.Size = new Size(42, 38);
            TalkingCat.TabIndex = 15;
            TalkingCat.TabStop = false;
            // 
            // BossKey
            // 
            BossKey.Image = Properties.Resources.EmptyQuest;
            BossKey.Location = new Point(1418, 405);
            BossKey.Name = "BossKey";
            BossKey.Size = new Size(42, 38);
            BossKey.TabIndex = 16;
            BossKey.TabStop = false;
            // 
            // FriendshipBracelet
            // 
            FriendshipBracelet.Image = Properties.Resources.EmptyQuest;
            FriendshipBracelet.Location = new Point(1395, 449);
            FriendshipBracelet.Name = "FriendshipBracelet";
            FriendshipBracelet.Size = new Size(42, 38);
            FriendshipBracelet.TabIndex = 17;
            FriendshipBracelet.TabStop = false;
            // 
            // Dawnbarrow
            // 
            AcceptButton = submit_button;
            AutoScaleDimensions = new SizeF(6F, 12F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.RosyBrown;
            ClientSize = new Size(1484, 804);
            Controls.Add(FriendshipBracelet);
            Controls.Add(BossKey);
            Controls.Add(TalkingCat);
            Controls.Add(Ladder);
            Controls.Add(Pickaxe);
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
            ((System.ComponentModel.ISupportInitialize)Pickaxe).EndInit();
            ((System.ComponentModel.ISupportInitialize)Ladder).EndInit();
            ((System.ComponentModel.ISupportInitialize)TalkingCat).EndInit();
            ((System.ComponentModel.ISupportInitialize)BossKey).EndInit();
            ((System.ComponentModel.ISupportInitialize)FriendshipBracelet).EndInit();
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
        private PictureBox Pickaxe;
        private PictureBox Ladder;
        private PictureBox TalkingCat;
        private PictureBox BossKey;
        private PictureBox FriendshipBracelet;
    }

}
