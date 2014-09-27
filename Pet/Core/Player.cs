using Pet.Engin;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Pet.Core
{
    public class Player : IRender
    {
        #region prop & field
        private string _name;
        private Point _position;
        private Size _size;
        private Body _body;
        private Brain _brain;
        private Spriter _spriter;


        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public Point Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Size Size
        {
            get { return _size; }
            set { _size = value; }
        }

        public Body Body
        {
            get { return _body; }
            set { _body = value; }
        }


        public Brain Brain
        {
            get { return _brain; }
            set { _brain = value; }
        }

        public Spriter Spriter
        {
            get { return _spriter; }
            set { _spriter = value; }
        }

        #endregion


        public Player(Point position, Engin.Body body)
        {
            _position = position;
            this.Body = body;
            _body.Width = 100;
            _body.Height = 200;
            _body.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            _body.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            updateMargin();
            _brain = new Brain();
        }


        private void updateMargin()
        {
            _body.Margin = new System.Windows.Thickness(_position.X + 1, _position.Y + 1, 0, 0);
        }


        private DateTime timegap;
        public void Render(UserControl uc, DateTime SignalTime)
        {
            if (timegap == default(DateTime))
            {
                timegap = SignalTime;
            }
            //timegap = SignalTime;
            updateMargin();
        }


        public UserControl GetBody()
        {
            return _body;
        }
    }
}
