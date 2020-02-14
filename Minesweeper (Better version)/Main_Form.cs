using System;
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

        private const int square_width = 20;
        private const int square_height = 20;

        private const int board_offset_horizontal = 10;
        private int board_offset_vertical = 10;

        private const int beginner_num_mines = 10;
        private const int intermediate_num_mines = 75;
        private const int expert_num_mines = 250;

        // Current settings
        private int number_squares_horizontal;
        private int number_squares_vertical;
        private int num_mines;
        private int num_mines_left;
        private int num_squares_left;
        private int time_elapsed;

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
            MessageBox.Show("Minesweeper - Programmed by Josh Bryant.","About",
                MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void Canvas_Panel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(canvas,Point.Empty);
        }

        private void Main_Form_Load(object sender, EventArgs e)
        {
            Start();
        }

        private void Start()
        {
            using (New_Game new_game = new New_Game())
            {
                new_game.ShowDialog();
                if (new_game.beginner_button.Checked) NewGame("Beginner", new_game);
                if (new_game.intermediate_button.Checked) NewGame("Intermediate", new_game);
                if (new_game.expert_button.Checked) NewGame("Expert", new_game);
                if (new_game.custom_button.Checked) NewGame("Custom", new_game);
            }
        }

        public void NewGame(string difficulty, New_Game new_game)
        {
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
                case "Custom":
                    number_squares_horizontal = (int)new_game.board_width_selection.Value;
                    number_squares_vertical = (int)new_game.board_height_selection.Value;
                    num_mines = (int)new_game.num_mines_selection.Value;
                    break;
            }
            
            PopulateSquares(number_squares_horizontal, number_squares_vertical);
            DrawGrid(number_squares_horizontal, number_squares_vertical);

            num_mines_left = num_mines;
            num_squares_left = number_squares_horizontal * number_squares_vertical;
            Update_Status_Bar();

            time_elapsed = 0;
            timer_label.Text = "Time elapsed: " + time_elapsed + " seconds";
            timer1.Start();
        }

        private void DrawGrid(int num_tiles_horizontal, int num_tiles_vertical)
        {
            this.canvas_width = board_offset_horizontal * 2 + square_width * num_tiles_horizontal;
            this.canvas_height = board_offset_vertical * 2 + square_height * num_tiles_vertical;

            this.ClientSize = new Size(canvas_width, canvas_height + menuStrip1.Height + toolStrip1.Height + statusStrip1.Height);

            Bitmap temp = new Bitmap(canvas_width,canvas_height);

            using (Graphics g = Graphics.FromImage(temp))
            {
                for (int i = 0; i < num_tiles_horizontal; i++)
                {
                    for (int j = 0; j < num_tiles_vertical; j++)
                    {
                        g.DrawImage(
                            Properties.Resources.minesweeper_tiles,
                            new Rectangle(i * square_width+board_offset_horizontal, j * square_height + board_offset_vertical, square_width, square_height),
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

            for (int i = 1; i <= num_mines; i++)
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
                if (horizontal_rnd == num_tiles_horizontal - 1)
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
                            Update_Status_Bar();
                            if (square_values[square_x,square_y] == 0) {
                                Bitmap temp = new Bitmap(canvas_width, canvas_height);
                                using (Graphics g = Graphics.FromImage(temp))
                                {
                                    g.DrawImage(canvas, Point.Empty);
                                    showAdjacentSquares(square_x, square_y, g);
                                }
                                canvas = temp;
                                Canvas_Panel.Invalidate();
                            }
                        }
                        break;
                    case MouseButtons.Right:
                        switch (square_states[square_x, square_y])
                        {
                            case 0:
                                square_states[square_x, square_y] = 2;
                                DrawSquare(square_x, square_y);
                                num_mines_left--;
                                Update_Status_Bar();
                                break;
                            case 2:
                                square_states[square_x, square_y] = 0;
                                DrawSquare(square_x, square_y);
                                num_mines_left++;
                                Update_Status_Bar();
                                break;
                        }
                        break;
                }

                if (square_states[square_x, square_y] == 1 && square_values[square_x, square_y] == 9) Game_Loss();
                if (num_squares_left == num_mines) Game_Win();
            }
        }

        private void showAdjacentSquares(int x, int y, Graphics g)
        {
            // Corner squares

            // Bottom left
            if (x == 0 && y == 0)
            {
                Check_Square(0, 0, 0, 1, g);
                Check_Square(0, 0, 1, 0, g);
                Check_Square(0, 0, 1, 1, g);
                return;
            }

            // Bottom right
            if (x == number_squares_horizontal - 1 && y == 0)
            {
                Check_Square(number_squares_horizontal - 1, 0, -1, 0, g);
                Check_Square(number_squares_horizontal - 1, 0, -1, 1, g);
                Check_Square(number_squares_horizontal - 1, 0, 0, 1, g);
                return;
            }

            // Top left
            if (x == 0 && y == number_squares_vertical - 1)
            {
                Check_Square(0, number_squares_vertical - 1, 0, -1, g);
                Check_Square(0, number_squares_vertical - 1, 1, -1, g);
                Check_Square(0, number_squares_vertical - 1, 1, 0, g);
                return;
            }

            // Top right
            if (x == number_squares_horizontal - 1 && y == number_squares_vertical - 1)
            {
                Check_Square(number_squares_horizontal - 1, number_squares_vertical - 1, -1, 0, g);
                Check_Square(number_squares_horizontal - 1, number_squares_vertical - 1, -1, -1, g);
                Check_Square(number_squares_horizontal - 1, number_squares_vertical - 1, 0, -1, g);
                return;
            }

            // Side squares

            // Left side
            if (x == 0)
            {
                Check_Square(x, y, 0, -1, g);
                Check_Square(x, y, 1, -1, g);
                Check_Square(x, y, 1, 0, g);
                Check_Square(x, y, 1, 1, g);
                Check_Square(x, y, 0, 1, g);
                return;
            }

            // Bottom side
            if (y == 0)
            {
                Check_Square(x, y, -1, 0, g);
                Check_Square(x, y, -1, 1, g);
                Check_Square(x, y, 0, 1, g);
                Check_Square(x, y, 1, 1, g);
                Check_Square(x, y, 1, 0, g);
                return;
            }

            // Right side
            if (x == number_squares_horizontal - 1)
            {
                Check_Square(x, y, 0, 1, g);
                Check_Square(x, y, -1, 1, g);
                Check_Square(x, y, -1, 0, g);
                Check_Square(x, y, -1, -1, g);
                Check_Square(x, y, 0, -1, g);
                return;
            }

            // Top side
            if (y == number_squares_vertical - 1)
            {
                Check_Square(x, y, -1, 0, g);
                Check_Square(x, y, -1, -1, g);
                Check_Square(x, y, 0, -1, g);
                Check_Square(x, y, 1, -1, g);
                Check_Square(x, y, 1, 0, g);
                return;
            }

            // Regular square
            for (int i = -1; i <= 1; i++)
            {
                Check_Square(x, y, -1, i, g);
                Check_Square(x, y, 1, i, g);
            }
            Check_Square(x, y, 0, - 1, g);
            Check_Square(x, y, 0, + 1, g);
        }

        private void Check_Square(int x, int y, int offset_x, int offset_y, Graphics g)
        {
            if (square_states[x + offset_x, y + offset_y] == 0)
            {
                square_states[x + offset_x, y + offset_y] = 1;
                g.DrawImage(
                    Properties.Resources.minesweeper_tiles,
                    new Rectangle((x + offset_x) * square_width + board_offset_horizontal, (y + offset_y) * square_height + board_offset_vertical, square_width, square_height),
                    number_images[square_values[x + offset_x, y + offset_y]],
                    GraphicsUnit.Pixel
                );
                num_squares_left--;
                Update_Status_Bar();
                if (square_values[x + offset_x, y + offset_y] == 0) showAdjacentSquares(x + offset_x, y + offset_y, g);
            }
        }

        private void Update_Status_Bar()
        {
            toolStripStatusLabel1.Text = "No. of mines/squares left: " +
                ((num_mines_left < 0) ? 0 : num_mines_left) + 
                "/" + num_squares_left;
        }

        private void Game_Win()
        {
            timer1.Stop();
            MessageBox.Show("You found all the mines and cleared the board! Congratulations!\n" +
                "You completed the board in " + time_elapsed + " seconds.", "Game over!");
            Start();
        }

        private void Game_Loss()
        {
            timer1.Stop();
            DrawEntireBoard();
            MessageBox.Show("You clicked on a mine! Game over!\n" +
                "You played for " + time_elapsed + " seconds.", "Game over!");
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

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time_elapsed += 1;
            timer_label.Text = "Time elapsed: " + time_elapsed + " seconds";
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string help = "The objective of minesweeper is to locate all the mines on the grid by clicking " +
                "to reveal the number clues. Each number indicates how many mines are adjacent to that square, " +
                "including diagonals. Right click on an unrevealed square to mark it with a flag that indicates " +
                "that a mine is present in that square. Right click the square again to remove the flag.";
            MessageBox.Show(help,"Help",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
    }
}