using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;

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
        public bool GameOver{private set;get;}
        AI ai;
        char PlayerSymbol;
        public bool processing
        {
            get{
                return PlayerSymbol != _board.CurrentPlayer&&!GameOver;
            }
        }
        #endregion
        public CaroBoardUI()
        {
            InitializeComponent();
            _ImgX = new Bitmap(Properties.Resources.x, CELL_SIZE, CELL_SIZE);
            _ImgO = new Bitmap(Properties.Resources.o, CELL_SIZE, CELL_SIZE);
            _board = new CaroBoard();
            this.NewGame(true,'o',4);
            GameOver = true;
        }
        /// <summary>
        /// Bắt đầu game mới
        /// </summary>
        /// <param name="playerFirst">Người đi trước</param>
        /// <param name="playerSymbol">Quân người chơi (x-o)</param>
        /// <param name="conputerAI">Độ sâu khi máy tính toán</param>
        public void NewGame(bool playerFirst, char playerSymbol, int computerAI)
        {
            GameOver = false;
            PlayerSymbol = playerSymbol=='x'?'x':'o';
            _board = new CaroBoard(19);
            _board.PrevMove.Set(-1, -1);
            if (playerFirst)
            {
                _board.XPlaying = playerSymbol == 'x' ? true : false;
                _board.CurrMove.Set(-1, -1);
            }
            else
            {
                _board.XPlaying = playerSymbol == 'x' ? false : true;
                _board.CurrMove.Set(_board.size / 2, _board.size / 2);
                _board.cells[_board.size / 2, _board.size / 2] = playerSymbol=='x'?'o':'x';
                SwithchPlayer();
            }
            this.MaximumSize = new Size(_board.size * CELL_SIZE + 1, _board.size * CELL_SIZE + 1);
            this.MinimumSize = new Size(_board.size * CELL_SIZE + 1, _board.size * CELL_SIZE + 1);
            this.Size = new Size(_board.size * CELL_SIZE, _board.size * CELL_SIZE);
            Invalidate();            
            ai = new AI(19, computerAI);
            
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!GameOver && _board.CurrentPlayer==PlayerSymbol)
            {
                int i = (int)(e.Y / CELL_SIZE);
                int j = (int)(e.X / CELL_SIZE);
                if (i >= _board.size || i < 0 || j >= _board.size || j < 0) return;
                if (_board.cells[i, j] == ' ')
                {
                    _board.PrevMove.Set(_board.CurrMove);
                    _board.CurrMove.Set(i, j);
                    _board.cells[i, j] = _board.XPlaying ? 'x' : 'o';
                    UpdateGraphic(_board.CurrMove);
                    UpdateGraphic(_board.PrevMove);
                    SwithchPlayer();
                }
//                 else
//                 {
//                     _board.cells[i, j] = ' ';
//                     UpdateGraphic(_board.CurrMove);
//                 }
                //Invalidate();
            }
            base.OnMouseDown(e);
        } 
        private void EchoBoard()
        {
            Console.Clear();
            for (int i = 0; i < _board.size; i++)
            {
                for (int j = 0; j < _board.size; j++)
                {
                    
                    Console.Write("{0} ",_board.cells[i, j]);
                }
                Console.WriteLine();
            }
        }
        private void UpdateGraphic(Position p)
        {
            Rectangle rc = new Rectangle(p.y * CELL_SIZE, p.x * CELL_SIZE, CELL_SIZE + 1, CELL_SIZE + 1);
            Invalidate(rc);
        }
        public void SwithchPlayer()
        {
            //EchoBoard();
            GameOver = _board.IsGame0ver;
            _board.XPlaying = !_board.XPlaying;
            if (GameOver)
            {
                if (_board.XPlaying) MessageBox.Show("Quân O thắng.");
                else MessageBox.Show("Quân X thắng.");
                return;
            }
            
            if(_board.CurrentPlayer!=PlayerSymbol)
            {
                Thread th = new Thread(Computer);
                th.Start();
            }
        }
        public void Computer()
        {
            _board.PrevMove.Set(_board.CurrMove);
            char currplayer = _board.XPlaying ? 'x' : 'o';
            Position p = ai.Solve(ref _board, currplayer);
            Console.WriteLine(p.x + "-" + p.y);
            _board.CurrMove.Set(p);
            _board.cells[p.x, p.y] = currplayer;
            UpdateGraphic(p);
            UpdateGraphic(_board.PrevMove);
            SwithchPlayer();
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
    }
}

