using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper__Better_version_
{
    public partial class Main_Form : Form
    {
        // Constants
        private const int beginner_num_squares_horizontal = 10;
        private const int beginner_num_squares_vertical = 10;
        private const int intermediate_num_squares_horizontal = 25;
        private const int intermediate_num_squares_vertical = 25;
        private const int expert_num_squares_horizontal = 50;
        private const int expert_num_squares_vertical = 40;

        private const int square_width = 16;
        private const int square_height = 16;

        private const int board_offset_horizontal = 10;
        private const int board_offset_vertical = 10;

        private const int beginner_num_mines = 8;
        private const int intermediate_num_mines = 15;
        private const int expert_num_mines = 45;

        // Current settings
        private int number_squares_horizontal;
        private int number_squares_vertical;
        private int num_mines;
        private int num_squares_left;

        private string board_difficulty;

        private int canvas_width, canvas_height;

        // Arrays
        /* Square values
         * 0 - Empty
         * 1 to 8 - Numbers
         * 9 - Mine
        */
        private int[,] square_values;

        /* Square states-
         * 0 - Unrevealed square
         * 1 - Revealed square
         * 2 - Flagged square
        */
        private int[,] square_states;

        // Canvas properties
        private Bitmap canvas;
        private Rectangle[] number_images = new Rectangle[10]
        {
            new Rectangle(384, 0, 128, 128),
            new Rectangle(0, 128, 128, 128),
            new Rectangle(128, 128, 128, 128),
            new Rectangle(256, 128, 128, 128),
            new Rectangle(384, 128, 128, 128),
            new Rectangle(0, 256, 128, 128),
            new Rectangle(128, 256, 128, 128),
            new Rectangle(256, 256, 128, 128),
            new Rectangle(384, 256, 128, 128),
            new Rectangle(256, 0, 128, 128)
        };

        public Main_Form()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Minesweeper - Programmed by Josh Bryant.","About");
        }

        private void Canvas_Panel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(canvas,Point.Empty);
        }

        private void Main_Form_Load(object sender, EventArgs e)
        {
            Start();
        }

        public void Start()
        {
            using (New_Game new_game = new New_Game())
            {
                new_game.ShowDialog();
                if (new_game.beginner_button.Checked) NewGame("Beginner");
                if (new_game.intermediate_button.Checked) NewGame("Intermediate");
                if (new_game.expert_button.Checked) NewGame("Expert");
            }
        }

        public void NewGame(string difficulty)
        {
            board_difficulty = difficulty;
            
            switch (difficulty)
            {
                case "Beginner":
                    number_squares_horizontal = beginner_num_squares_horizontal;
                    number_squares_vertical = beginner_num_squares_vertical;
                    num_mines = beginner_num_mines;
                    break;
                case "Intermediate":
                    number_squares_horizontal = intermediate_num_squares_horizontal;
                    number_squares_vertical = intermediate_num_squares_vertical;
                    num_mines = intermediate_num_mines;
                    break;
                case "Expert":
                    number_squares_horizontal = expert_num_squares_horizontal;
                    number_squares_vertical = expert_num_squares_vertical;
                    num_mines = expert_num_mines;
                    break;
            }
            
            PopulateSquares(number_squares_horizontal, number_squares_vertical);
            DrawGrid(number_squares_horizontal, number_squares_vertical);

            num_squares_left = number_squares_horizontal * number_squares_vertical;
        }

        private void DrawGrid(int num_tiles_horizontal, int num_tiles_vertical)
        {

            this.canvas_width = board_offset_horizontal*2 + square_width * num_tiles_horizontal;
            this.canvas_height = board_offset_vertical*2 + square_height * num_tiles_vertical;

            this.ClientSize = new Size(canvas_width, canvas_height+menuStrip1.Height);

            Bitmap temp = new Bitmap(canvas_width,canvas_height);

            using (Graphics g = Graphics.FromImage(temp))
            {
                for (int i = 0; i < num_tiles_horizontal; i++)
                {
                    for (int j = 0; j < num_tiles_vertical; j++)
                    {
                        g.DrawImage(
                            Properties.Resources.minesweeper_tiles,
                            new Rectangle(i*square_width+board_offset_horizontal,j*square_height+board_offset_vertical,square_width,square_height),
                            new Rectangle(0, 0, 128, 128),
                            GraphicsUnit.Pixel
                        );
                    }
                }
            }

            canvas = temp;
            Canvas_Panel.Invalidate();
        }

        private void PopulateSquares(int num_tiles_horizontal, int num_tiles_vertical)
        {
            square_values = new int[num_tiles_horizontal, num_tiles_vertical];
            square_states = new int[num_tiles_horizontal, num_tiles_vertical];

            Random rnd = new Random();
            int horizontal_rnd, vertical_rnd;

            for (int i = 0; i < num_mines; i++)
            {
                do
                {
                    horizontal_rnd = rnd.Next(0, num_tiles_horizontal - 1);
                    vertical_rnd = rnd.Next(0, num_tiles_vertical - 1);
                } while (square_values[horizontal_rnd, vertical_rnd] == 9);

                square_values[horizontal_rnd, vertical_rnd] = 9;

                // Corner squares

                // Bottom left
                if (horizontal_rnd == 0 && vertical_rnd == 0)
                {
                    IncrementSquare(0, 0, 0, 1);
                    IncrementSquare(0, 0, 1, 0);
                    IncrementSquare(0, 0, 1, 1);
                    continue;
                }

                // Bottom right
                if (horizontal_rnd == num_tiles_horizontal - 1 && vertical_rnd == 0)
                {
                    IncrementSquare(num_tiles_horizontal - 1, 0, -1, 0);
                    IncrementSquare(num_tiles_horizontal - 1, 0, -1, 1);
                    IncrementSquare(num_tiles_horizontal - 1, 0, 0, 1);
                    continue;
                }

                // Top left
                if (horizontal_rnd == 0 && vertical_rnd == num_tiles_vertical - 1)
                {
                    IncrementSquare(0, num_tiles_vertical - 1, 0, -1);
                    IncrementSquare(0, num_tiles_vertical - 1, 1, -1);
                    IncrementSquare(0, num_tiles_vertical - 1, 1, 0);
                    continue;
                }

                // Top right
                if (horizontal_rnd == num_tiles_horizontal - 1 && vertical_rnd == num_tiles_vertical - 1)
                {
                    IncrementSquare(num_tiles_horizontal - 1, num_tiles_vertical - 1, -1, 0);
                    IncrementSquare(num_tiles_horizontal - 1, num_tiles_vertical - 1, -1, -1);
                    IncrementSquare(num_tiles_horizontal - 1, num_tiles_vertical - 1, 0, -1);
                    continue;
                }

                // Side squares

                // Left side
                if (horizontal_rnd == 0)
                {
                    IncrementSquare(horizontal_rnd, vertical_rnd, 0, -1);
                    IncrementSquare(horizontal_rnd, vertical_rnd, 1, -1);
                    IncrementSquare(horizontal_rnd, vertical_rnd, 1, 0);
                    IncrementSquare(horizontal_rnd, vertical_rnd, 1, 1);
                    IncrementSquare(horizontal_rnd, vertical_rnd, 0, 1);
                    continue;
                }

                // Bottom side
                if (vertical_rnd == 0)
                {
                    IncrementSquare(horizontal_rnd, vertical_rnd, -1, 0);
                    IncrementSquare(horizontal_rnd, vertical_rnd, -1, 1);
                    IncrementSquare(horizontal_rnd, vertical_rnd, 0, 1);
                    IncrementSquare(horizontal_rnd, vertical_rnd, 1, 1);
                    IncrementSquare(horizontal_rnd, vertical_rnd, 1, 0);
                    continue;
                }

                // Right side
                if (horizontal_rnd == num_tiles_vertical - 1)
                {
                    IncrementSquare(horizontal_rnd, vertical_rnd, 0, 1);
                    IncrementSquare(horizontal_rnd, vertical_rnd, -1, 1);
                    IncrementSquare(horizontal_rnd, vertical_rnd, -1, 0);
                    IncrementSquare(horizontal_rnd, vertical_rnd, -1, -1);
                    IncrementSquare(horizontal_rnd, vertical_rnd, 0, -1);
                    continue;
                }

                // Top side
                if (vertical_rnd == num_tiles_vertical - 1)
                {
                    IncrementSquare(horizontal_rnd, vertical_rnd, -1, 0);
                    IncrementSquare(horizontal_rnd, vertical_rnd, -1, -1);
                    IncrementSquare(horizontal_rnd, vertical_rnd, 0, -1);
                    IncrementSquare(horizontal_rnd, vertical_rnd, 1, -1);
                    IncrementSquare(horizontal_rnd, vertical_rnd, 1, 0);
                    continue;
                }

                // Regular square
                for (int j = -1; j <= 1; j++)
                {
                    IncrementSquare(horizontal_rnd, vertical_rnd, -1, j);
                    IncrementSquare(horizontal_rnd, vertical_rnd, 1, j);
                }
                IncrementSquare(horizontal_rnd, vertical_rnd, 0, -1);
                IncrementSquare(horizontal_rnd, vertical_rnd, 0, 1);
            }
        }

        private void IncrementSquare(int x, int y, int offset_x, int offset_y)
        {
            if (square_values[x + offset_x, y + offset_y] != 9) square_values[x + offset_x, y + offset_y] += 1;
        }

        private void DrawSquare(int square_horizontal, int square_vertical)
        {
            Rectangle sub_picture_dimensions = new Rectangle();

            switch (square_states[square_horizontal,square_vertical])
            {
                case 0:
                    sub_picture_dimensions = new Rectangle(0, 0, 128, 128);
                    break;
                case 1:
                    sub_picture_dimensions = number_images[square_values[square_horizontal, square_vertical]];
                    break;
                case 2:
                    sub_picture_dimensions = new Rectangle(128, 0, 128, 128);
                    break;
            }

            Bitmap temp = new Bitmap(this.canvas_width,this.canvas_height);

            using (Graphics g = Graphics.FromImage(temp))
            {
                g.DrawImage(canvas, Point.Empty);
                g.DrawImage(
                    Properties.Resources.minesweeper_tiles,
                    new Rectangle(square_horizontal * square_width + board_offset_horizontal, square_vertical * square_height + board_offset_vertical, square_width, square_height),
                    sub_picture_dimensions,
                    GraphicsUnit.Pixel
                );
            }

            canvas = temp;
            Canvas_Panel.Invalidate();
        }

        private void Canvas_Panel_MouseClick(object sender, MouseEventArgs e)
        {
            int click_x = e.X;
            int click_y = e.Y;
            int square_x, square_y;

            if (click_x > board_offset_horizontal && click_x < this.ClientSize.Width - board_offset_horizontal &&
                click_y > board_offset_vertical && click_y < this.ClientSize.Height - board_offset_vertical)
            {
                // Translate mouse click co-ordinates into board co-ordinates
                click_x -= board_offset_horizontal;
                click_y -= board_offset_vertical;
                square_x = click_x / square_width;
                square_y = click_y / square_height;

                switch (e.Button)
                {
                    case MouseButtons.Left:
                        if (square_states[square_x,square_y] != 1)
                        {
                            square_states[square_x, square_y] = 1;
                            DrawSquare(square_x, square_y);
                            num_squares_left--;
                        }
                        break;
                    case MouseButtons.Right:
                        switch (square_states[square_x, square_y])
                        {
                            case 0:
                                square_states[square_x, square_y] = 2;
                                DrawSquare(square_x, square_y);
                                break;
                            case 2:
                                square_states[square_x, square_y] = 0;
                                DrawSquare(square_x, square_y);
                                break;
                        }
                        break;
                }

                if (square_states[square_x, square_y] == 1 && square_values[square_x, square_y] == 9) Game_Loss();
                if (num_squares_left == num_mines) Game_Win();
            }
        }

        private void Game_Win()
        {
            MessageBox.Show("You found all the mines! Congratulations!", "Game over!");
            Start();
        }

        private void Game_Loss()
        {
            DrawEntireBoard();
            MessageBox.Show("You clicked on a mine!", "Game over!");
            Start();
        }

        private void DrawEntireBoard()
        {
            Bitmap temp = new Bitmap(this.canvas_width, this.canvas_height);

            using (Graphics g = Graphics.FromImage(temp))
            {
                g.DrawImage(canvas, Point.Empty);
                for (int i = 0; i < number_squares_horizontal; i++)
                {
                    for (int j = 0; j < number_squares_vertical; j++)
                    {
                        g.DrawImage(
                            Properties.Resources.minesweeper_tiles,
                            new Rectangle(i * square_width + board_offset_horizontal, j * square_height + board_offset_vertical, square_width, square_height),
                            number_images[square_values[i, j]],
                            GraphicsUnit.Pixel
                        );
                    }
                }
            }

            canvas = temp;
            Canvas_Panel.Invalidate();
        }
    }
}