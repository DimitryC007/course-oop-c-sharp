using Logic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ex05_Othelo
{
    public class BoardButton : Button
    {
        private eCellState _buttonState = eCellState.Free;
        private Point _coordinate;
        
        public BoardButton(Point location)
        {
            _coordinate = location;
            ButtonState = eCellState.Disabled;
        }
        public Point Coordinate => _coordinate;
        public eCellState ButtonState { get { return _buttonState; } set { SetState(value); } }

        protected override void OnClick(EventArgs e)
        {

            if (ButtonState != eCellState.Free)
                return; 

            base.OnClick(e);
        }

        private void SetState(eCellState state)
        {

            _buttonState = state;
            switch(_buttonState)
            {
                case eCellState.Disabled:
                    {
                        Enabled = false;
                        this.Image = null;
                        this.BackColor = Color.Gray;
                    }
                    break;
                case eCellState.Free:
                    {
                        Enabled = true;
                        this.Image = null;
                        this.BackColor = Color.Green;
                    }
                    break;
                case eCellState.Black:
                    {
                        Enabled = true;
                        this.Image = Image.FromFile(Environment.CurrentDirectory+"\\CoinRed.png");
                        this.BackColor = Color.Gray;
                    }
                    break;
                case eCellState.White:
                    {
                        Enabled = true;
                        this.Image = Image.FromFile(Environment.CurrentDirectory + "\\CoinYellow.png");
                        this.BackColor = Color.Gray;
                    }
                    break;
            }
        }
    }
}
