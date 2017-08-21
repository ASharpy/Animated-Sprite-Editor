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
    public partial class Form1 : Form
    {
        private Point startPoint = new Point();
        private Point endPoint = new Point();



        public Form1()
        {
            InitializeComponent();
            SpriteSheet.Paint += new System.Windows.Forms.PaintEventHandler(SpriteSheet_Paint);
            SpriteSheet.MouseDown += new System.Windows.Forms.MouseEventHandler(SpriteSheet_MouseDown);
        }

        private void AddSpriteSheet()
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Sprite Sheet";
                dlg.Filter = "Image Files(*.BMP;*.JPG;*.GIF,*.PNG;)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    SpriteSheet.Image = Image.FromFile(dlg.FileName);
                    SpriteSheet.SizeMode = PictureBoxSizeMode.StretchImage;

                }
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void AddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSpriteSheet();
        }

        private void SpriteSheet_Click(object sender, EventArgs e)
        {
        }

        private void SpriteSheet_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                startPoint = e.Location;
              
            }
        }

        private void SpriteSheet_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                endPoint = e.Location;
            }
          

        }

        private void SpriteSheet_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                endPoint = e.Location;
            
            }
            
        }

        private void SpriteSheet_Paint(object sender, PaintEventArgs e)
        {
            if (Select.Checked == true)
            {


                if (startPoint.X > 0 && startPoint.Y > 0 && endPoint.X > 0 && endPoint.Y > 0)
                {
                    Rectangle rect = new Rectangle(startPoint.X, startPoint.Y, endPoint.X - startPoint.X, endPoint.Y - startPoint.Y);

                    e.Graphics.DrawRectangle(Pens.Blue, rect);

                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            SpriteSheet.Refresh();
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            
        }

        private void Select_CheckedChanged(object sender, EventArgs e)
        {
         
        }

        private void copyImage(Rectangle imageRect)
        {
            Bitmap bm = new Bitmap(endPoint.X - startPoint.X, endPoint.Y - startPoint.Y);

            using (Graphics GR = Graphics.FromImage(bm))
            {
                Rectangle dest_rect = new Rectangle(0, 0, imageRect.Width, imageRect.Height);
                GR.DrawImage(SpriteSheet.Image, dest_rect, imageRect, GraphicsUnit.Pixel);
            }

            Clipboard.SetImage(bm);
        }

    }

}
