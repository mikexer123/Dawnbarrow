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
            Helmet = new PictureBox();
            Chestplate = new PictureBox();
            Leggings = new PictureBox();
            MiniMap = new Label();
            InventoryPanel = new Panel();
            InventoryHint = new Label();
            InventoryConsumableButton = new Button();
            InventoryActionButton = new Button();
            InventoryList = new ListBox();
            InventoryHeader = new Label();
            ShopPanel = new Panel();
            ShopHint = new Label();
            ShopBuyButton = new Button();
            ShopList = new ListBox();
            ShopHeader = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Background).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Pickaxe).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Ladder).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TalkingCat).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BossKey).BeginInit();
            ((System.ComponentModel.ISupportInitialize)FriendshipBracelet).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Helmet).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Chestplate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Leggings).BeginInit();
            InventoryPanel.SuspendLayout();
            ShopPanel.SuspendLayout();
            SuspendLayout();
            // 
            // ConsoleOut
            // 
            ConsoleOut.AcceptsTab = true;
            ConsoleOut.BackColor = Color.Black;
            ConsoleOut.BorderStyle = BorderStyle.FixedSingle;
            ConsoleOut.EnableAutoDragDrop = true;
            ConsoleOut.Font = new Font("Consolas", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ConsoleOut.ForeColor = SystemColors.ActiveCaption;
            ConsoleOut.ImeMode = ImeMode.Off;
            ConsoleOut.Location = new Point(12, 303);
            ConsoleOut.Margin = new Padding(3, 2, 3, 2);
            ConsoleOut.Name = "ConsoleOut";
            ConsoleOut.ReadOnly = true;
            ConsoleOut.ScrollBars = RichTextBoxScrollBars.Vertical;
            ConsoleOut.Size = new Size(1579, 578);
            ConsoleOut.TabIndex = 0;
            ConsoleOut.Text = resources.GetString("ConsoleOut.Text");
            ConsoleOut.UseWaitCursor = true;
            ConsoleOut.TextChanged += ConsoleOut_TextChanged;
            // 
            // InputBox
            // 
            InputBox.Font = new Font("Consolas", 15F);
            InputBox.Location = new Point(9, 885);
            InputBox.Margin = new Padding(3, 2, 3, 2);
            InputBox.Multiline = true;
            InputBox.Name = "InputBox";
            InputBox.Size = new Size(1582, 39);
            InputBox.TabIndex = 1;
            InputBox.Text = "Click To Type Here";
            InputBox.MouseClick += InputBox_MouseClick;
            InputBox.TextChanged += InputBox_TextChanged;
            // 
            // submit_button
            // 
            submit_button.Font = new Font("Rockwell Extra Bold", 9F);
            submit_button.Location = new Point(1597, 709);
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
            pictureBox1.Location = new Point(1597, 217);
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
            PlayerHP.Location = new Point(1061, 107);
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
            XP.Location = new Point(1061, 66);
            XP.Name = "XP";
            XP.Size = new Size(120, 22);
            XP.TabIndex = 7;
            XP.Text = "Experience";
            // 
            // Background
            // 
            Background.Image = global::Dawnbarrow.Properties.Resources._11;
            Background.Location = new Point(0, 0);
            Background.Name = "Background";
            Background.Size = new Size(1060, 298);
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
            NGL.Location = new Point(1061, 9);
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
            Equip.Location = new Point(1061, 149);
            Equip.Name = "Equip";
            Equip.Size = new Size(186, 22);
            Equip.TabIndex = 12;
            Equip.Text = "Current Equipped";
            // 
            // Pickaxe
            // 
            Pickaxe.Image = global::Dawnbarrow.Properties.Resources.EmptyQuest;
            Pickaxe.Location = new Point(1606, 405);
            Pickaxe.Name = "Pickaxe";
            Pickaxe.Size = new Size(42, 38);
            Pickaxe.TabIndex = 13;
            Pickaxe.TabStop = false;
            // 
            // Ladder
            // 
            Ladder.Image = global::Dawnbarrow.Properties.Resources.EmptyQuest;
            Ladder.Location = new Point(1660, 405);
            Ladder.Name = "Ladder";
            Ladder.Size = new Size(42, 38);
            Ladder.TabIndex = 14;
            Ladder.TabStop = false;
            // 
            // TalkingCat
            // 
            TalkingCat.Image = global::Dawnbarrow.Properties.Resources.EmptyQuest;
            TalkingCat.Location = new Point(1606, 449);
            TalkingCat.Name = "TalkingCat";
            TalkingCat.Size = new Size(42, 38);
            TalkingCat.TabIndex = 15;
            TalkingCat.TabStop = false;
            // 
            // BossKey
            // 
            BossKey.Image = global::Dawnbarrow.Properties.Resources.EmptyQuest;
            BossKey.Location = new Point(1660, 449);
            BossKey.Name = "BossKey";
            BossKey.Size = new Size(42, 38);
            BossKey.TabIndex = 16;
            BossKey.TabStop = false;
            // 
            // FriendshipBracelet
            // 
            FriendshipBracelet.Image = global::Dawnbarrow.Properties.Resources.EmptyQuest;
            FriendshipBracelet.Location = new Point(1630, 493);
            FriendshipBracelet.Name = "FriendshipBracelet";
            FriendshipBracelet.Size = new Size(42, 38);
            FriendshipBracelet.TabIndex = 17;
            FriendshipBracelet.TabStop = false;
            // 
            // Helmet
            // 
            Helmet.BackColor = Color.Blue;
            Helmet.Image = global::Dawnbarrow.Properties.Resources.EHelm;
            Helmet.Location = new Point(1621, 236);
            Helmet.Name = "Helmet";
            Helmet.Size = new Size(51, 45);
            Helmet.SizeMode = PictureBoxSizeMode.StretchImage;
            Helmet.TabIndex = 18;
            Helmet.TabStop = false;
            // 
            // Chestplate
            // 
            Chestplate.BackColor = Color.Blue;
            Chestplate.Image = global::Dawnbarrow.Properties.Resources.EChest;
            Chestplate.Location = new Point(1597, 287);
            Chestplate.Name = "Chestplate";
            Chestplate.Size = new Size(105, 44);
            Chestplate.SizeMode = PictureBoxSizeMode.StretchImage;
            Chestplate.TabIndex = 19;
            Chestplate.TabStop = false;
            // 
            // Leggings
            // 
            Leggings.BackColor = Color.Blue;
            Leggings.Image = global::Dawnbarrow.Properties.Resources.ELegs__1_;
            Leggings.Location = new Point(1606, 337);
            Leggings.Name = "Leggings";
            Leggings.Size = new Size(78, 57);
            Leggings.SizeMode = PictureBoxSizeMode.StretchImage;
            Leggings.TabIndex = 20;
            Leggings.TabStop = false;
            // 
            // MiniMap
            // 
            MiniMap.BackColor = Color.Black;
            MiniMap.BorderStyle = BorderStyle.FixedSingle;
            MiniMap.Font = new Font("Consolas", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            MiniMap.ForeColor = Color.PaleGreen;
            MiniMap.Location = new Point(804, 12);
            MiniMap.Name = "MiniMap";
            MiniMap.Size = new Size(246, 272);
            MiniMap.TabIndex = 21;
            MiniMap.Text = "MiniMap";
            // 
            // InventoryPanel
            // 
            InventoryPanel.BackColor = Color.BurlyWood;
            InventoryPanel.BorderStyle = BorderStyle.FixedSingle;
            InventoryPanel.Controls.Add(InventoryHint);
            InventoryPanel.Controls.Add(InventoryConsumableButton);
            InventoryPanel.Controls.Add(InventoryActionButton);
            InventoryPanel.Controls.Add(InventoryList);
            InventoryPanel.Controls.Add(InventoryHeader);
            InventoryPanel.Location = new Point(1061, 198);
            InventoryPanel.Name = "InventoryPanel";
            InventoryPanel.Size = new Size(530, 100);
            InventoryPanel.TabIndex = 22;
            // 
            // InventoryHint
            // 
            InventoryHint.AutoSize = true;
            InventoryHint.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            InventoryHint.Location = new Point(336, 72);
            InventoryHint.Name = "InventoryHint";
            InventoryHint.Size = new Size(109, 13);
            InventoryHint.TabIndex = 4;
            InventoryHint.Text = "Double-click item";
            // 
            // InventoryConsumableButton
            // 
            InventoryConsumableButton.Font = new Font("SimSun-ExtB", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            InventoryConsumableButton.Location = new Point(336, 53);
            InventoryConsumableButton.Name = "InventoryConsumableButton";
            InventoryConsumableButton.Size = new Size(182, 29);
            InventoryConsumableButton.TabIndex = 3;
            InventoryConsumableButton.Text = "Use Consumable";
            InventoryConsumableButton.UseVisualStyleBackColor = true;
            InventoryConsumableButton.Click += InventoryConsumableButton_Click;
            // 
            // InventoryActionButton
            // 
            InventoryActionButton.Font = new Font("SimSun-ExtB", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            InventoryActionButton.Location = new Point(336, 22);
            InventoryActionButton.Name = "InventoryActionButton";
            InventoryActionButton.Size = new Size(182, 29);
            InventoryActionButton.TabIndex = 2;
            InventoryActionButton.Text = "Equip / Use";
            InventoryActionButton.UseVisualStyleBackColor = true;
            InventoryActionButton.Click += InventoryActionButton_Click;
            // 
            // InventoryList
            // 
            InventoryList.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            InventoryList.FormattingEnabled = true;
            InventoryList.ItemHeight = 15;
            InventoryList.Location = new Point(13, 31);
            InventoryList.Name = "InventoryList";
            InventoryList.Size = new Size(315, 49);
            InventoryList.TabIndex = 1;
            InventoryList.SelectedIndexChanged += InventoryList_SelectedIndexChanged;
            InventoryList.DoubleClick += InventoryList_DoubleClick;
            // 
            // InventoryHeader
            // 
            InventoryHeader.AutoSize = true;
            InventoryHeader.Font = new Font("SimSun-ExtB", 11.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            InventoryHeader.Location = new Point(9, 8);
            InventoryHeader.Name = "InventoryHeader";
            InventoryHeader.Size = new Size(79, 16);
            InventoryHeader.TabIndex = 0;
            InventoryHeader.Text = "Inventory";
            // 
            // ShopPanel
            // 
            ShopPanel.BackColor = Color.BlanchedAlmond;
            ShopPanel.BorderStyle = BorderStyle.FixedSingle;
            ShopPanel.Controls.Add(ShopHint);
            ShopPanel.Controls.Add(ShopBuyButton);
            ShopPanel.Controls.Add(ShopList);
            ShopPanel.Controls.Add(ShopHeader);
            ShopPanel.Location = new Point(1061, 303);
            ShopPanel.Name = "ShopPanel";
            ShopPanel.Size = new Size(530, 128);
            ShopPanel.TabIndex = 23;
            // 
            // ShopHint
            // 
            ShopHint.AutoSize = true;
            ShopHint.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ShopHint.Location = new Point(346, 102);
            ShopHint.Name = "ShopHint";
            ShopHint.Size = new Size(145, 13);
            ShopHint.TabIndex = 3;
            ShopHint.Text = "Travel to 3,1 to shop";
            // 
            // ShopBuyButton
            // 
            ShopBuyButton.Font = new Font("SimSun-ExtB", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ShopBuyButton.Location = new Point(346, 63);
            ShopBuyButton.Name = "ShopBuyButton";
            ShopBuyButton.Size = new Size(170, 33);
            ShopBuyButton.TabIndex = 2;
            ShopBuyButton.Text = "Buy Selected";
            ShopBuyButton.UseVisualStyleBackColor = true;
            ShopBuyButton.Click += ShopBuyButton_Click;
            // 
            // ShopList
            // 
            ShopList.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ShopList.FormattingEnabled = true;
            ShopList.ItemHeight = 14;
            ShopList.Location = new Point(13, 33);
            ShopList.Name = "ShopList";
            ShopList.Size = new Size(320, 74);
            ShopList.TabIndex = 1;
            ShopList.SelectedIndexChanged += ShopList_SelectedIndexChanged;
            ShopList.DoubleClick += ShopList_DoubleClick;
            // 
            // ShopHeader
            // 
            ShopHeader.AutoSize = true;
            ShopHeader.Font = new Font("SimSun-ExtB", 11.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ShopHeader.Location = new Point(9, 8);
            ShopHeader.Name = "ShopHeader";
            ShopHeader.Size = new Size(43, 16);
            ShopHeader.TabIndex = 0;
            ShopHeader.Text = "Shop";
            // 
            // Dawnbarrow
            // 
            AcceptButton = submit_button;
            AutoScaleDimensions = new SizeF(6F, 12F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.RosyBrown;
            ClientSize = new Size(1727, 935);
            Controls.Add(ShopPanel);
            Controls.Add(InventoryPanel);
            Controls.Add(MiniMap);
            Controls.Add(Leggings);
            Controls.Add(Chestplate);
            Controls.Add(Helmet);
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
            ((System.ComponentModel.ISupportInitialize)Helmet).EndInit();
            ((System.ComponentModel.ISupportInitialize)Chestplate).EndInit();
            ((System.ComponentModel.ISupportInitialize)Leggings).EndInit();
            InventoryPanel.ResumeLayout(false);
            InventoryPanel.PerformLayout();
            ShopPanel.ResumeLayout(false);
            ShopPanel.PerformLayout();
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
        private PictureBox Helmet;
        private PictureBox Chestplate;
        private PictureBox Leggings;
        private Label MiniMap;
        private Panel InventoryPanel;
        private Label InventoryHeader;
        private ListBox InventoryList;
        private Button InventoryActionButton;
        private Button InventoryConsumableButton;
        private Label InventoryHint;
        private Panel ShopPanel;
        private Label ShopHint;
        private Button ShopBuyButton;
        private ListBox ShopList;
        private Label ShopHeader;
    }

}
