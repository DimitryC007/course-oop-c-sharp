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
        private Coordinate _coordinate;
        
        public BoardButton(Coordinate location)
        {
            _coordinate = location;
            ButtonState = eCellState.Disabled;
        }
        public Coordinate Coordinate => _coordinate;
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
                        this.BackgroundImage = null;
                        this.BackColor = default(Color);
                    }
                    break;
                case eCellState.Free:
                    {
                        Enabled = true;
                        this.BackgroundImage = null;
                        this.BackColor = Color.Green;
                    }
                    break;
                case eCellState.Black:
                    {
                        Enabled = true;
                        this.BackgroundImage = Image.FromFile(Environment.CurrentDirectory+"\\CoinRed.png");
                        this.BackgroundImageLayout = ImageLayout.Stretch;
                        this.BackColor = Color.Gray;
                    }
                    break;
                case eCellState.White:
                    {
                        Enabled = true;
                        this.BackgroundImage = Image.FromFile(Environment.CurrentDirectory + "\\CoinYellow.png");
                        this.BackgroundImageLayout = ImageLayout.Stretch;
                        this.BackColor = Color.Gray;
                    }
                    break;
            }
        }
    }
}
