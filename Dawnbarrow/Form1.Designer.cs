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
            Background = new PictureBox();
            label1 = new Label();
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
            InputBox.Size = new Size(947, 19);
            InputBox.TabIndex = 1;
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
            // Background
            // 
            Background.BackColor = Color.LavenderBlush;
            Background.Location = new Point(-563, -14);
            Background.Margin = new Padding(3, 2, 3, 2);
            Background.Name = "Background";
            Background.Size = new Size(1644, 442);
            Background.TabIndex = 3;
            Background.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Sitka Banner", 14.2499981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(9, 7);
            label1.Name = "label1";
            label1.Size = new Size(164, 28);
            label1.TabIndex = 4;
            label1.Text = "CurrentCoordinates";
            label1.Click += label1_Click;
            // 
            // Dawnbarrow
            // 
            AutoScaleDimensions = new SizeF(6F, 12F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(1082, 601);
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
            ((System.ComponentModel.ISupportInitialize)Background).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox ConsoleOut;
        private TextBox InputBox;
        private Button submit_button;
        private PictureBox Background;
        private Label label1;
    }
}
