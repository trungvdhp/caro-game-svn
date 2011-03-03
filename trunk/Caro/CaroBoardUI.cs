using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Caro
{
    public partial class CaroBoardUI : UserControl
    {
        //public event EventHandler CellClick;
#region properties
        const int CELL_SIZE = 25;
        Image _ImgX;
        Image _ImgO;
        CaroBoard _board;
        bool GameOver = false;
        bool StopTimer = false;
        AI1 ai;
#endregion

        public CaroBoardUI()
        {
            InitializeComponent();
            _ImgX = new Bitmap(Properties.Resources.x, CELL_SIZE, CELL_SIZE);
            _ImgO = new Bitmap(Properties.Resources.o, CELL_SIZE, CELL_SIZE);
            _board = new CaroBoard();
            this.NewGame(true);
        }
        /// <summary>
        /// Bắt đầu game mới
        /// </summary>
        /// <param name="defaultFirst">Bằng true thì người chơi đầu tiên sẽ là mặc định theo lớp CaroBoard</param>
        public void NewGame(bool defaultFirst)
        {
            GameOver = false;
            StopTimer = false;
            _board = new CaroBoard(19);
            if (!defaultFirst) _board.XPlaying = !_board.XPlaying;
            this.MaximumSize = new Size(_board.size * CELL_SIZE + 1, _board.size * CELL_SIZE + 1);
            this.MinimumSize = new Size(_board.size * CELL_SIZE + 1, _board.size * CELL_SIZE + 1);
            this.Size = new Size(_board.size * CELL_SIZE, _board.size * CELL_SIZE);
            Invalidate();
            timer1.Start();
            _board.PrevMove.Set(-1, -1);
            _board.CurrMove.Set(-1, -1);
            ai = new AI1(19);
            
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!GameOver)
            {
                int i = (int)(e.Y / CELL_SIZE);
                int j = (int)(e.X / CELL_SIZE);
                if (i >= _board.size || i < 0 || j >= _board.size || j < 0) return;
                if (_board.cells[i, j] == ' ')
                {
                    _board.PrevMove.Set(_board.CurrMove);
                    _board.CurrMove.Set(i,j);
                    _board.cells[i, j] = _board.XPlaying ? 'x' : 'o';
                    char currplayer = _board.XPlaying ? 'o' : 'x';
                    Position p = ai.Solve(ref _board, currplayer);
                    Console.WriteLine(p.x + "-" + p.y);
                    _board.cells[p.x, p.y] = currplayer;
                    GameOver = _board.IsGame0ver;
                    //_board.XPlaying = !_board.XPlaying;
                }
                else _board.cells[i, j] = ' ';
                Rectangle rc = new Rectangle(_board.CurrMove.y * CELL_SIZE, _board.CurrMove.x * CELL_SIZE, CELL_SIZE + 1, CELL_SIZE + 1);
                Invalidate(rc);
                rc = new Rectangle(_board.PrevMove.y * CELL_SIZE, _board.PrevMove.x * CELL_SIZE, CELL_SIZE + 1, CELL_SIZE + 1);
                Invalidate(rc);
                Invalidate();
            }
            base.OnMouseDown(e);
        } 
        protected override void OnPaint(PaintEventArgs e)
        {
            int y = 0,x=0;
            for (int i = 0; i < _board.size; i++)
            {
                x = 0;
                for(int j=0;j<_board.size;j++)
                {
                    
                    if (_board.cells[i, j] == 'x')
                        e.Graphics.DrawImage(_ImgX, x, y);
                    else if (_board.cells[i, j] == 'o')
                        e.Graphics.DrawImage(_ImgO, x, y);
                    e.Graphics.DrawLine(Pens.Black, 0, x, this.Height, x);
                    x += CELL_SIZE;
                }
                e.Graphics.DrawLine(Pens.Black, y, 0, y, this.Width);
                y += CELL_SIZE;
            }
            e.Graphics.DrawLine(Pens.Black, 0, x, this.Height, x);
            e.Graphics.DrawLine(Pens.Black, y, 0, y, this.Width);
            Pen p;
            if (!_board.XPlaying) p = Pens.Red;
            else p = Pens.DarkViolet;
            e.Graphics.DrawRectangle(p, new Rectangle(_board.CurrMove.y * CELL_SIZE, _board.CurrMove.x * CELL_SIZE, CELL_SIZE, CELL_SIZE));
            base.OnPaint(e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (StopTimer) return;
            if(GameOver)
            {
                StopTimer = true;
                if (_board.XPlaying) MessageBox.Show("Quân O thắng.");
                else MessageBox.Show("Quân X thắng.");
                timer1.Stop();
            }
        }
    }
}
