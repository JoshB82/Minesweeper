using System;
using System.Windows.Forms;

namespace Minesweeper__Better_version_
{
    public partial class New_Game : Form
    {
        public New_Game()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void New_Game_Load(object sender, EventArgs e)
        {
            num_mines_selection.Maximum = board_width_selection.Value * board_height_selection.Value;
        }

        private void board_width_selection_ValueChanged(object sender, EventArgs e)
        {
            num_mines_selection.Maximum = board_width_selection.Value * board_height_selection.Value;
        }

        private void board_height_selection_ValueChanged(object sender, EventArgs e)
        {
            num_mines_selection.Maximum = board_width_selection.Value * board_height_selection.Value;
        }

        private void custom_button_CheckedChanged(object sender, EventArgs e)
        {
            if (custom_button.Checked == true)
            {
                Custom_Enable(true);
            }
            else
            {
                Custom_Enable(false);
            }
        }

        private void Custom_Enable(bool setting)
        {
            board_size_label.Enabled = setting;
            mines_label.Enabled = setting;
            by_label.Enabled = setting;
            board_width_selection.Enabled = setting;
            board_height_selection.Enabled = setting;
            num_mines_selection.Enabled = setting;
        }
    }
}
