using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Pet.Engin
{
    interface IRender
    {
        void Render(UserControl canvas, DateTime dateTime);

        UserControl GetBody();
    }
}
