using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Animated_Sprite_Editor
{
    public partial class Animation : Form
    {
        public Animation()
        {
            InitializeComponent();
        }

        private void Animation_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1.ok.Dispose();

            System.IO.File.Delete(@".\megaman.gif");
        }
    }
}
