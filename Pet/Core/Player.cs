using Pet.Scirpt;
using Pet.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Pet.Core
{
    public class Player
    {
        #region prop & field
        private string _name;
        private Size _size;
        private Body _body;
        private PlayerScript script;


        public string Name
        {
            get { return _name; }
            set { _name = value; }
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

        #endregion

        public Player()
        {
            _body = new Body();
            script = new PlayerScript(this);
            Engin.Current.AddScript(script);
        }

        public Player(Point position)
            : this()
        {
            _body.Content = new Button() { Content = "body", Width = 100, Height = 300 };
            _body.Position = position;
        }

    }
}
