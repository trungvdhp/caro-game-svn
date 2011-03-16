using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using CaroLibrary;

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
        Image _ImgThink;
        public bool GameOver{private set;get;}
        AI ai;
        public char PlayerSymbol;
        public bool processing;
        public int[] kq;
        List<Step> step;
        int t;
        int PlayerScore, ComputerScore;
        char ComputerSymbol
        {
            get{
                return PlayerSymbol == 'x' ? 'o' : 'x';
            }
        }
        public int ComputerAI
        {
            set
            {
                ai = new AI(19, value);
            }
        }
        #endregion

        public CaroBoardUI()
        {
            InitializeComponent();
            _ImgX = new Bitmap(Properties.Resources.x, CELL_SIZE, CELL_SIZE);
            _ImgO = new Bitmap(Properties.Resources.o, CELL_SIZE, CELL_SIZE);
            _ImgThink = new Bitmap(Properties.Resources.think, CELL_SIZE, CELL_SIZE);
            _board = new CaroBoard();
            this.NewGame(true,'x',3);
            t = -1;
            ResetScores();
            GameOver = true;
            GameData = new DataTable("Caro");
            GameData.Columns.Add("Step", typeof(List<Step>));
            GameData.Columns.Add("PlayerScore", typeof(int));
            GameData.Columns.Add("ComputerScore", typeof(int));
            GameData.Columns.Add("PlayedTime", typeof(int));
            GameData.Columns.Add("PlayerSymbol", typeof(char));
            UpdateMessage();
        }
        /// <summary>
        /// Đặt lại tỉ số
        /// </summary>
        public void ResetScores()
        {
            PlayerScore = ComputerScore = 0;
            CaroScore.Text = PlayerScore + ":" + ComputerScore;
        }
        /// <summary>
        /// Bắt đầu game mới
        /// </summary>
        /// <param name="playerFirst">Người đi trước</param>
        /// <param name="playerSymbol">Quân người chơi (x-o)</param>
        /// <param name="conputerAI">Độ sâu khi máy tính toán</param>
        public void NewGame(bool playerFirst, char playerSymbol, int computerAI)
        {
            ComputerAI = computerAI;
            step = new List<Step>();
            PlayerSymbol = playerSymbol=='x'?'x':'o';
            _board = new CaroBoard(19);
            _board.PrevMove.Set(-1, -1);
            kq = new int[5];
            this.MaximumSize = new Size(_board.size * CELL_SIZE + 1, (_board.size + 1) * CELL_SIZE + 1);
            this.MinimumSize = new Size(_board.size * CELL_SIZE + 1, (_board.size + 1) * CELL_SIZE + 1);
            this.Size = new Size(_board.size * CELL_SIZE + 1, (_board.size + 1) * CELL_SIZE + 1);
            CurrIndex = -1;
            t = 0;
            timer2.Start();
            GameOver = false;
            Invalidate();
            if (playerFirst)
            {
                _board.XPlaying = playerSymbol == 'x' ? true : false;
                _board.CurrMove.Set(-1, -1);
                UpdateMessage();
            }
            else
            {
                _board.XPlaying = playerSymbol == 'x' ? false : true;
                _board.CurrMove.Set(_board.size / 2, _board.size / 2);
                _board.cells[_board.size / 2, _board.size / 2] = playerSymbol=='x'?'o':'x';
                SwithchPlayer();
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!GameOver && _board.CurrentPlayer==PlayerSymbol)
            {
                int i = (int)(e.Y / CELL_SIZE)-1;
                int j = (int)(e.X / CELL_SIZE);
                //MessageBox.Show("i= " + i + ", j= " + j);
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
        protected override void  OnMouseMove(MouseEventArgs e)
        {
            int i = (int)((e.X) / CELL_SIZE)+1;
            int j = (int)((e.Y) / CELL_SIZE);
            CaroPosition.Text = "" + j + ":" + i;
            base.OnMouseMove(e);
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
            //MessageBox.Show("i = " + p.x + "j= " + p.y);
            Rectangle rc = new Rectangle(p.y * CELL_SIZE, (p.x+1) * CELL_SIZE, CELL_SIZE+1, CELL_SIZE + 1);
            Invalidate(rc);
        }
        private void UpdateGr4phic(Position p)
        {
            Rectangle rc = new Rectangle(p.y * CELL_SIZE+1, (p.x+1) * CELL_SIZE-1, CELL_SIZE-2 , CELL_SIZE-2 );
            Invalidate(rc);
        }
        private int CurrIndex;
        public void Redo()
        {
            if (CurrIndex+1 >= step.Count || GameOver || processing) return;
            UpdateGraphic(_board.CurrMove);
            CurrIndex += 1;
            _board.PrevMove.Set(step[CurrIndex].p);
            _board.cells[step[CurrIndex].p.x, step[CurrIndex].p.y] = PlayerSymbol;
            CurrIndex += 1;
            _board.cells[step[CurrIndex].p.x, step[CurrIndex].p.y] = PlayerSymbol=='x'?'o':'x';
            _board.CurrMove.Set(step[CurrIndex].p);
            UpdateGraphic(_board.PrevMove);
            UpdateGraphic(_board.CurrMove);
            UpdateMessage();
        }
        public void Undo()
        {
            if (CurrIndex < 1 || GameOver || processing) return;
            Step c = new Step(step[CurrIndex].p, step[CurrIndex].CurrentPlayer);
            Step p = new Step(step[CurrIndex - 1].p, step[CurrIndex - 1].CurrentPlayer);
            CurrIndex -= 2;
            _board.cells[c.p.x, c.p.y] = ' ';
            _board.cells[p.p.x, p.p.y] = ' ';
            if (CurrIndex>-1)_board.CurrMove.Set(step[CurrIndex].p);
            else _board.CurrMove.Set(-1,-1);
            if (CurrIndex > 0) _board.PrevMove.Set(step[CurrIndex - 1].p);
            else _board.PrevMove.Set(-1, -1);
            _board.XPlaying = PlayerSymbol == 'x' ? true : false;
            UpdateGraphic(c.p);
            UpdateGraphic(p.p);
            UpdateGraphic(_board.CurrMove);
            UpdateMessage();
        }
        void UpdateMessage()
        {
            if (step.Count == 0 && GameOver) CaroMessage.Text = "Press option to custom or new game to play!";
            else if (step.Count == 0) CaroMessage.Text = "You play first.";
            else if(!GameOver)
            {
                if (_board.CurrentPlayer == PlayerSymbol) CaroMessage.Text = "Next to player...";
                else CaroMessage.Text = "Next to computer...";
            }
            else
            {
                if(_board.CurrentPlayer==PlayerSymbol)
                    CaroMessage.Text = "You lose! Chicken, kkk...";
                else
                    CaroMessage.Text = "You win! Let's play next round!";

            }
            CaroCount.Text = (CurrIndex + 1).ToString();
            CaroCurrentMove.Text=(_board.CurrMove.x+1)+":"+(_board.CurrMove.y+1);
        }
        public void SwithchPlayer()
        {
            //EchoBoard();
            kq = _board.IsGame0ver;
            GameOver = kq[0]>=5?true:false;
            while (step.Count-1 > CurrIndex) step.RemoveAt(step.Count - 1);
            step.Add(new Step(_board.CurrMove, _board.CurrentPlayer));
            CurrIndex++;
            //MessageBox.Show(step.Count.ToString());
            _board.XPlaying = !_board.XPlaying;
            UpdateMessage();
            if (GameOver)
            {
                timer2.Stop();
                Invalidate();
                if (_board.CurrentPlayer != PlayerSymbol)
                    PlayerScore++;
                else
                    ComputerScore++;
                CaroScore.Text = PlayerScore + ":" + ComputerScore;
                UpdateMessage();
                return;
            }
            
            if(_board.CurrentPlayer!=PlayerSymbol)
            {
                processing = true;
                timer1.Start();
                Thread th = new Thread(Computer);
                th.Start();
            }
            
        }
        public void Computer()
        {
            //timer1.Start();
            _board.PrevMove.Set(_board.CurrMove);
            char currplayer = _board.XPlaying ? 'x' : 'o';
            Position p = ai.Solve(ref _board, currplayer);
            Console.WriteLine(p.x + "-" + p.y);
            _board.CurrMove.Set(p);
            _board.cells[p.x, p.y] = currplayer;
            UpdateGraphic(p);
            UpdateGraphic(_board.PrevMove);
            UpdateGraphic(ai.prevp);
            processing = false;
            timer1.Stop();
            SwithchPlayer();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            int y = CELL_SIZE, x=0;
            for (int i = 0; i < _board.size; i++)
            {
                x = 0;
                for(int j=0;j<_board.size;j++)
                {

                    if (_board.cells[i, j] == 'x')
                    {
                        //MessageBox.Show("i= "+ i + " j= " + j + "[" + x + "," + y + "]");
                        e.Graphics.DrawImage(_ImgX, x, y);
                    }
                    else if (_board.cells[i, j] == 'o')
                    {
                        //MessageBox.Show("i = " + i + "j= " + j);
                        //MessageBox.Show("i= " + i + " j= " + j + "[" + x + "," + y + "]");
                        e.Graphics.DrawImage(_ImgO, x, y);
                    }
                    //Vẽ đường nằm ngang
                    e.Graphics.DrawLine(Pens.Black, 0, x, this.Height, x);
                    x += CELL_SIZE;
                }
                //Vẽ đường thẳng đứng
                e.Graphics.DrawLine(Pens.Black, y, CELL_SIZE, y, this.Width+CELL_SIZE);
                y += CELL_SIZE;
            }
            e.Graphics.DrawLine(Pens.Black, 0, x, this.Height, x);
            e.Graphics.DrawLine(Pens.Black, 0, x+CELL_SIZE, this.Height,x+CELL_SIZE);
            e.Graphics.DrawLine(Pens.Black, y, CELL_SIZE, y, this.Width+CELL_SIZE);
            e.Graphics.DrawLine(Pens.Black, 0, CELL_SIZE, 0, this.Width + CELL_SIZE);
            Pen p=Pens.Red;
//             if (!_board.XPlaying) p = Pens.Red;
//             else p = Pens.DarkViolet;
            e.Graphics.DrawRectangle(p, new Rectangle(_board.CurrMove.y * CELL_SIZE, (_board.CurrMove.x+1) * CELL_SIZE, CELL_SIZE, CELL_SIZE));
            //e.Graphics.DrawRectangle(p, new Rectangle(_board.CurrMove.y * CELL_SIZE-1, _board.CurrMove.x * CELL_SIZE-1, CELL_SIZE+2, CELL_SIZE+2));
            if (processing) e.Graphics.DrawImage(_ImgThink, ai.currp.y * CELL_SIZE, (ai.currp.x +1)* CELL_SIZE);
            if (kq[0]>=5&&GameOver)
            {
                //MessageBox.Show(" " + kq[0]+ " " + kq[2]*CELL_SIZE + " " + kq[1]*CELL_SIZE + " " + kq[4]*CELL_SIZE + " " + kq[3]*CELL_SIZE);
                e.Graphics.DrawLine(new Pen(Color.SpringGreen,5), kq[2]*CELL_SIZE+CELL_SIZE/2, (kq[1]+1)*CELL_SIZE+CELL_SIZE/2
                    , kq[4]*CELL_SIZE+CELL_SIZE/2, (kq[3]+1)*CELL_SIZE+CELL_SIZE/2);
            }
            base.OnPaint(e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(!(ai.prevp.x==ai.currp.x&&ai.prevp.y==ai.currp.y))
            {
                UpdateGr4phic(ai.currp);
                UpdateGr4phic(ai.prevp);
                ai.prevp.Set(ai.currp);
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (t == -1) return;
            t++;
            int mi = t / 60;
            int se = t % 60;
            if(mi<10) CaroTime.Text = "0"+ mi;
            else CaroTime.Text = ""+mi;
            if(se<10) CaroTime.Text += ":0" + se;
            else CaroTime.Text += ":"+se;
        }
        private DataTable GameData;
        public void SaveGame(string FileName)
        {
            if (GameOver||processing) return;
            //if (GameOver) step.Clear();
            GameData.Rows.Clear();
            DataRow r = GameData.NewRow();
            //MessageBox.Show(step.Count.ToString());
            r["Step"] = step;
            r["PlayerScore"] = PlayerScore;
            r["ComputerScore"] = ComputerScore;
            r["PlayedTime"] = t;
            r["PlayerSymbol"] = PlayerSymbol;
            GameData.Rows.Add(r);
            GameData.WriteXml(FileName);
        }
        public void LoadGame(string FileName)
        {
            GameOver = false;
            GameData.Rows.Clear();
            GameData.ReadXml(FileName);
            step = (List<Step>)GameData.Rows[0]["Step"];
            t = (int)GameData.Rows[0]["PlayedTime"];
            PlayerScore = (int)GameData.Rows[0]["PlayerScore"];
            ComputerScore = (int)GameData.Rows[0]["ComputerScore"];
            char playerSymbol = (char)GameData.Rows[0]["PlayerSymbol"];

            CurrIndex = step.Count - 1;
            _board = new CaroBoard(19);
            for (int i = 0; i < step.Count; i++)
                _board.cells[step[i].p.x, step[i].p.y] = step[i].CurrentPlayer == playerSymbol ? PlayerSymbol : ComputerSymbol;

            _board.XPlaying = (_board.cells[step[CurrIndex].p.x, step[CurrIndex].p.y] == 'o');
            _board.CurrMove.Set(step[CurrIndex].p);
            CaroScore.Text = PlayerScore + ":" + ComputerScore;
            timer2.Start();
            UpdateMessage();
            Invalidate();
        }
    }

    //public class Step
    //{
    //    public char CurrentPlayer;
    //    public Position p;
    //    public Step()
    //    {
    //        p = new Position(-1, -1);
    //        CurrentPlayer = ' ';
    //    }
    //    public Step(Position pp, char cc)
    //    {
    //        CurrentPlayer = cc;
    //        p = new Position();
    //        p.Set(pp);
    //    }
    //}
}

