using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Caro
{
    public partial class CaroBoardUI2 : UserControl
    {
        #region Properties

        CaroBoard _board;
        Image _ImgX = new Bitmap(Properties.Resources.blackp);
        Image _ImgO = new Bitmap(Properties.Resources.whitep);
        const int CELL_SIZE = 35;
        bool GameOver;
        Point TopLeft = new Point(24, 18);

        #endregion

        public CaroBoardUI2()
        {
            InitializeComponent();
            NewGame();
        }
        public void NewGame()
        {
            _board = new CaroBoard(15);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!GameOver)
            {
                int i = (int)((e.Y -TopLeft.Y)/ CELL_SIZE);
                int j = (int)((e.X -TopLeft.X)/ CELL_SIZE);
                if (i >= _board.size || i < 0 || j >= _board.size || j < 0) return;
                if (_board.cells[i, j] == ' ')
                {
                    _board.PrevMove.Set(_board.CurrMove);
                    _board.CurrMove.Set(i, j);
                    _board.cells[i, j] = _board.XPlaying ? 'x' : 'o';
                    GameOver = _board.IsGameOver;
                    _board.XPlaying = !_board.XPlaying;
                }
                else _board.cells[i, j] = ' ';
                Rectangle rc = new Rectangle(_board.CurrMove.y * CELL_SIZE + TopLeft.X, _board.CurrMove.x * CELL_SIZE + TopLeft.Y, CELL_SIZE + 1, CELL_SIZE + 1);
                Invalidate(rc);
                //rc = new Rectangle( _board.PrevMove.y * CELL_SIZE + TopLeft.X,_board.PrevMove.x * CELL_SIZE + TopLeft.Y, CELL_SIZE + 1, CELL_SIZE + 1);
                //Invalidate(rc);
                //Invalidate();
            }
            base.OnMouseDown(e);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            int y = TopLeft.Y;
            int x;
            for (int i = 0; i < _board.size; i++)
            {
                x = TopLeft.X;
                for (int j = 0; j < _board.size; j++)
                {
                    if (_board.cells[i, j] == 'x')
                        e.Graphics.DrawImage(_ImgX,x,y);
                    else if (_board.cells[i, j] == 'o')
                        e.Graphics.DrawImage(_ImgO, x,y);
                    x += CELL_SIZE;
                }
                y += CELL_SIZE;
            }
            base.OnPaint(e);
        }
    }
}
