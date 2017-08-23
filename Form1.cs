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
        //Starting point of rectangle being drawn
        private Point startPoint = new Point();

        //end Point to get the rectangles height and width
        private Point endPoint = new Point();

        //The image Being loaded in
        private Bitmap OrigSpriteSheet = null;

        //To see if and area is being selected or not
        private bool selectedArea = false;

        //The Image Being Selected
        private Bitmap selectedSpite = null;

        //global graphics class variable
        private Graphics selectedG = null;

        // global blank rectangle to avoid the need to create multiple rectangles
        private Rectangle rect = new Rectangle(0, 0, 0, 0);


        PictureBox picBox = new PictureBox();

        public Form1()
        {
            InitializeComponent();
            SpriteSheet.Paint += new System.Windows.Forms.PaintEventHandler(SpriteSheet_Paint);
            SpriteSheet.MouseDown += new System.Windows.Forms.MouseEventHandler(SpriteSheet_MouseDown);
        }

        /* 
           Loads an image from a file only accepts BMP,JPG,GIF and PNG's. Sets the picture box's image to the image loaded in 
           no returns      
        */
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

        // calls the add sprite sheet function when the add menu button is clicked
        private void AddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSpriteSheet();
        }

        private void SpriteSheet_Click(object sender, EventArgs e)
        {

        }

        /*
         when the left mouse button is down saves the sprite sheet to origSpriteSheet, get the starting mouse position,
         no returns        
         */
        private void SpriteSheet_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                if (SpriteSheet.Image != null)
                {



                    OrigSpriteSheet = new Bitmap(SpriteSheet.Image);
                    this.KeyPreview = true;




                    selectedArea = true;

                    startPoint = MousePos();

                    selectedSpite = new Bitmap(OrigSpriteSheet);
                    selectedG = Graphics.FromImage(selectedSpite);
                    SpriteSheet.Image = selectedSpite;
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                selectedArea = false;
                selectedSpite = null;
                selectedG = null;
                SpriteSheet.Image = OrigSpriteSheet;
                SpriteSheet.Refresh();


                copyImage(rect);

                SpriteSheet.DoDragDrop(Clipboard.GetImage(), DragDropEffects.Copy | DragDropEffects.Move);

            }

        }

        //if the mouse is moving and the left mouse button is being pressed in draw a rectangle based on the starting mouse position and the current mouse position
        private void SpriteSheet_MouseMove(object sender, MouseEventArgs e)
        {
            if (!selectedArea)
            {
                return;
            }
            if (Select.Checked == true)
            {


                if (e.Button == MouseButtons.Left)
                {
                    selectedG.DrawImage(OrigSpriteSheet, 0, 0);





                    using (Pen pen = new Pen(Color.Red))
                    {
                        pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

                        endPoint = MousePos();


                        rect = makeRectangle(startPoint, endPoint);


                        selectedG.DrawRectangle(pen, rect);

                    }

                    SpriteSheet.Refresh();
                }
            }
        }


        // resets the rectangle when the mouse is released
        private void SpriteSheet_MouseUp(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Left)
            //{
            //    if (!selectedArea)
            //    {
            ///       return;
            //  }






            //}




        }


        private void SpriteSheet_Paint(object sender, PaintEventArgs e)
        {


            if (startPoint.X > 0 && startPoint.Y > 0 && endPoint.X > 0 && endPoint.Y > 0)
            {

            }
            //  SpriteSheet.Refresh();


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


        private Rectangle makeRectangle(Point Point1, Point Point2)
        {
            return new Rectangle(Math.Min(Point1.X, Point2.X), Math.Min(Point1.Y, Point2.Y),
                Math.Abs(Point1.X - Point2.X), Math.Abs(Point1.Y - Point2.Y));
        }


      /*  private void SpriteSheet_DragDrop(object sender, DragEventArgs e)
        {
            float fgds = 45;
        }*/

        //private void panel1_DragEnter(object sender, DragEventArgs e)
        //{
        //    if (e.Data.GetDataPresent(DataFormats.Bitmap))
        //    {
        //        e.Effect = DragDropEffects.Copy;
        //    }
        //    else
        //    {
        //        e.Effect = DragDropEffects.None;
        //    }
        //}

        //private void panel1_DragDrop(object sender, DragEventArgs e)
        //{
        //    PictureBox picBox = new PictureBox();

        //    picBox.Width = 50;

        //    picBox.Height = 50;



        //    panel1.Controls.Add(picBox);

        //   picBox.Image = (Bitmap)e.Data.GetData(DataFormats.Bitmap);
        //}

        /// Gets the correct mouse position for an image that has been transformed
        /// returns a new point of the correct mouse position>
        private Point MousePos()
        {
            Point controlrelative = SpriteSheet.PointToClient(MousePosition);

            Size imageSize = SpriteSheet.Image.Size;

            Size PicBoxSize = SpriteSheet.Size;


            float X = ((float)imageSize.Width / (float)PicBoxSize.Width) * controlrelative.X;

            float Y = ((float)imageSize.Height / (float)PicBoxSize.Height) * controlrelative.Y;


            return new Point(Math.Abs((int)X), Math.Abs((int)Y));
        }

        private void flowLayoutPanel1_DragDrop(object sender, DragEventArgs e)
        {

            picBox = new PictureBox();

            //MessageBox.Show("grr");

            //picBox.Height = 50;



            flowLayoutPanel1.Controls.Add(picBox);

            picBox.Image = (Bitmap)e.Data.GetData(DataFormats.Bitmap);

            picBox.Width = picBox.Image.Size.Width;

            picBox.Height = picBox.Image.Size.Height;
           // picBox.BorderStyle = BorderStyle.Fixed3D;
        }

        private void flowLayoutPanel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }

}
