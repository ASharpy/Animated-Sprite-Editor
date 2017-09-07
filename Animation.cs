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

        }

        private void Animation_Load(object sender, EventArgs e)
        {

        }

        private void Animation_FormClosing(object sender, FormClosingEventArgs e)
        {

            Form1.deleteFile = true;

        }
    }
}
