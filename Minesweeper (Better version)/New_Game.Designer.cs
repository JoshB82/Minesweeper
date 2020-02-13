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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(New_Game));
            this.label1 = new System.Windows.Forms.Label();
            this.beginner_button = new System.Windows.Forms.RadioButton();
            this.intermediate_button = new System.Windows.Forms.RadioButton();
            this.expert_button = new System.Windows.Forms.RadioButton();
            this.Start_Game_Button = new System.Windows.Forms.Button();
            this.Exit_Button = new System.Windows.Forms.Button();
            this.custom_button = new System.Windows.Forms.RadioButton();
            this.board_size_label = new System.Windows.Forms.Label();
            this.mines_label = new System.Windows.Forms.Label();
            this.board_width_selection = new System.Windows.Forms.NumericUpDown();
            this.board_height_selection = new System.Windows.Forms.NumericUpDown();
            this.num_mines_selection = new System.Windows.Forms.NumericUpDown();
            this.by_label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.board_width_selection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.board_height_selection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_mines_selection)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(232, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "Welcome to Minesweeper! Please choose a difficulty:";
            // 
            // beginner_button
            // 
            this.beginner_button.AutoSize = true;
            this.beginner_button.Location = new System.Drawing.Point(16, 60);
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
            this.intermediate_button.Location = new System.Drawing.Point(16, 87);
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
            this.expert_button.Location = new System.Drawing.Point(16, 114);
            this.expert_button.Name = "expert_button";
            this.expert_button.Size = new System.Drawing.Size(69, 21);
            this.expert_button.TabIndex = 3;
            this.expert_button.TabStop = true;
            this.expert_button.Text = "Expert";
            this.expert_button.UseVisualStyleBackColor = true;
            // 
            // Start_Game_Button
            // 
            this.Start_Game_Button.Location = new System.Drawing.Point(12, 240);
            this.Start_Game_Button.Name = "Start_Game_Button";
            this.Start_Game_Button.Size = new System.Drawing.Size(111, 37);
            this.Start_Game_Button.TabIndex = 4;
            this.Start_Game_Button.Text = "Start Game!";
            this.Start_Game_Button.UseVisualStyleBackColor = true;
            this.Start_Game_Button.Click += new System.EventHandler(this.button1_Click);
            // 
            // Exit_Button
            // 
            this.Exit_Button.Location = new System.Drawing.Point(134, 240);
            this.Exit_Button.Name = "Exit_Button";
            this.Exit_Button.Size = new System.Drawing.Size(111, 37);
            this.Exit_Button.TabIndex = 5;
            this.Exit_Button.Text = "Exit";
            this.Exit_Button.UseVisualStyleBackColor = true;
            this.Exit_Button.Click += new System.EventHandler(this.button2_Click);
            // 
            // custom_button
            // 
            this.custom_button.AutoSize = true;
            this.custom_button.Location = new System.Drawing.Point(16, 142);
            this.custom_button.Name = "custom_button";
            this.custom_button.Size = new System.Drawing.Size(80, 21);
            this.custom_button.TabIndex = 6;
            this.custom_button.TabStop = true;
            this.custom_button.Text = "Custom:";
            this.custom_button.UseVisualStyleBackColor = true;
            this.custom_button.CheckedChanged += new System.EventHandler(this.custom_button_CheckedChanged);
            // 
            // board_size_label
            // 
            this.board_size_label.AutoSize = true;
            this.board_size_label.Enabled = false;
            this.board_size_label.Location = new System.Drawing.Point(13, 173);
            this.board_size_label.Name = "board_size_label";
            this.board_size_label.Size = new System.Drawing.Size(79, 17);
            this.board_size_label.TabIndex = 7;
            this.board_size_label.Text = "Board size:";
            // 
            // mines_label
            // 
            this.mines_label.AutoSize = true;
            this.mines_label.Enabled = false;
            this.mines_label.Location = new System.Drawing.Point(12, 206);
            this.mines_label.Name = "mines_label";
            this.mines_label.Size = new System.Drawing.Size(119, 17);
            this.mines_label.TabIndex = 8;
            this.mines_label.Text = "Number of mines:";
            // 
            // board_width_selection
            // 
            this.board_width_selection.Enabled = false;
            this.board_width_selection.Location = new System.Drawing.Point(99, 173);
            this.board_width_selection.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.board_width_selection.Name = "board_width_selection";
            this.board_width_selection.Size = new System.Drawing.Size(56, 22);
            this.board_width_selection.TabIndex = 9;
            this.board_width_selection.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.board_width_selection.ValueChanged += new System.EventHandler(this.board_width_selection_ValueChanged);
            // 
            // board_height_selection
            // 
            this.board_height_selection.Enabled = false;
            this.board_height_selection.Location = new System.Drawing.Point(188, 173);
            this.board_height_selection.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.board_height_selection.Name = "board_height_selection";
            this.board_height_selection.Size = new System.Drawing.Size(56, 22);
            this.board_height_selection.TabIndex = 10;
            this.board_height_selection.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.board_height_selection.ValueChanged += new System.EventHandler(this.board_height_selection_ValueChanged);
            // 
            // num_mines_selection
            // 
            this.num_mines_selection.Enabled = false;
            this.num_mines_selection.Location = new System.Drawing.Point(134, 205);
            this.num_mines_selection.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_mines_selection.Name = "num_mines_selection";
            this.num_mines_selection.Size = new System.Drawing.Size(111, 22);
            this.num_mines_selection.TabIndex = 11;
            this.num_mines_selection.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // by_label
            // 
            this.by_label.AutoSize = true;
            this.by_label.Enabled = false;
            this.by_label.Location = new System.Drawing.Point(160, 176);
            this.by_label.Name = "by_label";
            this.by_label.Size = new System.Drawing.Size(23, 17);
            this.by_label.TabIndex = 12;
            this.by_label.Text = "by";
            // 
            // New_Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(271, 325);
            this.ControlBox = false;
            this.Controls.Add(this.by_label);
            this.Controls.Add(this.num_mines_selection);
            this.Controls.Add(this.board_height_selection);
            this.Controls.Add(this.board_width_selection);
            this.Controls.Add(this.mines_label);
            this.Controls.Add(this.board_size_label);
            this.Controls.Add(this.custom_button);
            this.Controls.Add(this.Exit_Button);
            this.Controls.Add(this.Start_Game_Button);
            this.Controls.Add(this.expert_button);
            this.Controls.Add(this.intermediate_button);
            this.Controls.Add(this.beginner_button);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "New_Game";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Game";
            this.Load += new System.EventHandler(this.New_Game_Load);
            ((System.ComponentModel.ISupportInitialize)(this.board_width_selection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.board_height_selection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_mines_selection)).EndInit();
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
        private System.Windows.Forms.Label board_size_label;
        private System.Windows.Forms.Label mines_label;
        private System.Windows.Forms.Label by_label;
        public System.Windows.Forms.RadioButton custom_button;
        public System.Windows.Forms.NumericUpDown board_width_selection;
        public System.Windows.Forms.NumericUpDown board_height_selection;
        public System.Windows.Forms.NumericUpDown num_mines_selection;
    }
}