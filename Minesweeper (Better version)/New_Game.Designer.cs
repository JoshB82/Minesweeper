namespace Minesweeper__Better_version_
{
    partial class New_Game
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
            this.label1 = new System.Windows.Forms.Label();
            this.beginner_button = new System.Windows.Forms.RadioButton();
            this.intermediate_button = new System.Windows.Forms.RadioButton();
            this.expert_button = new System.Windows.Forms.RadioButton();
            this.Start_Game_Button = new System.Windows.Forms.Button();
            this.Exit_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(340, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Welcome to Minesweeper! Please choose a difficulty:";
            // 
            // beginner_button
            // 
            this.beginner_button.AutoSize = true;
            this.beginner_button.Location = new System.Drawing.Point(16, 53);
            this.beginner_button.Name = "beginner_button";
            this.beginner_button.Size = new System.Drawing.Size(86, 21);
            this.beginner_button.TabIndex = 1;
            this.beginner_button.TabStop = true;
            this.beginner_button.Text = "Beginner";
            this.beginner_button.UseVisualStyleBackColor = true;
            // 
            // intermediate_button
            // 
            this.intermediate_button.AutoSize = true;
            this.intermediate_button.Location = new System.Drawing.Point(16, 80);
            this.intermediate_button.Name = "intermediate_button";
            this.intermediate_button.Size = new System.Drawing.Size(107, 21);
            this.intermediate_button.TabIndex = 2;
            this.intermediate_button.TabStop = true;
            this.intermediate_button.Text = "Intermediate";
            this.intermediate_button.UseVisualStyleBackColor = true;
            // 
            // expert_button
            // 
            this.expert_button.AutoSize = true;
            this.expert_button.Location = new System.Drawing.Point(16, 107);
            this.expert_button.Name = "expert_button";
            this.expert_button.Size = new System.Drawing.Size(69, 21);
            this.expert_button.TabIndex = 3;
            this.expert_button.TabStop = true;
            this.expert_button.Text = "Expert";
            this.expert_button.UseVisualStyleBackColor = true;
            // 
            // Start_Game_Button
            // 
            this.Start_Game_Button.Location = new System.Drawing.Point(16, 145);
            this.Start_Game_Button.Name = "Start_Game_Button";
            this.Start_Game_Button.Size = new System.Drawing.Size(111, 38);
            this.Start_Game_Button.TabIndex = 4;
            this.Start_Game_Button.Text = "Start Game!";
            this.Start_Game_Button.UseVisualStyleBackColor = true;
            this.Start_Game_Button.Click += new System.EventHandler(this.button1_Click);
            // 
            // Exit_Button
            // 
            this.Exit_Button.Location = new System.Drawing.Point(134, 145);
            this.Exit_Button.Name = "Exit_Button";
            this.Exit_Button.Size = new System.Drawing.Size(111, 38);
            this.Exit_Button.TabIndex = 5;
            this.Exit_Button.Text = "Exit";
            this.Exit_Button.UseVisualStyleBackColor = true;
            this.Exit_Button.Click += new System.EventHandler(this.button2_Click);
            // 
            // New_Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 201);
            this.Controls.Add(this.Exit_Button);
            this.Controls.Add(this.Start_Game_Button);
            this.Controls.Add(this.expert_button);
            this.Controls.Add(this.intermediate_button);
            this.Controls.Add(this.beginner_button);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "New_Game";
            this.Text = "New Game";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Start_Game_Button;
        private System.Windows.Forms.Button Exit_Button;
        public System.Windows.Forms.RadioButton beginner_button;
        public System.Windows.Forms.RadioButton intermediate_button;
        public System.Windows.Forms.RadioButton expert_button;
    }
}