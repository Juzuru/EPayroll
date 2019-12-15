using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EPayroll.Extension
{
    public class GridExtension : Grid
    {
        public event EventHandler Tapped;

        public GridExtension()
        {
            TapGestureRecognizer tgr = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
            tgr.Tapped += Grid_Tapped;
            GestureRecognizers.Add(tgr);
        }

        private void Grid_Tapped(object sender, EventArgs e)
        {
            Tapped?.Invoke(sender, e);
        }
    }
}
