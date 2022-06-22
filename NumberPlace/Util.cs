using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NumberPlace
{
	public static partial class Util
    {
        public static readonly float DpiScale = new Form().CreateGraphics().DpiX / 96;
    }

}
