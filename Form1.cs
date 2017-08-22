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

        private Bitmap OrigSpriteSheet = null;
        private bool selectedArea = false;
        private Bitmap selectedSpite = null;
        private Graphics selectedG = null;
        private Rectangle rect = new Rectangle(0, 0, 0, 0);

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
                    SpriteSheet.SizeMode = PictureBoxSizeMode.Normal;

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
            if (e.Button == MouseButtons.Left)
            {
                if (SpriteSheet.Image != null)
                {

              

                    OrigSpriteSheet = new Bitmap(SpriteSheet.Image);
                    this.KeyPreview = true;

                    


                    selectedArea = true;

                        startPoint = e.Location;

                        selectedSpite = new Bitmap(OrigSpriteSheet);
                        selectedG = Graphics.FromImage(selectedSpite);
                        SpriteSheet.Image = selectedSpite;
                    } 
            }
        }

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

                        endPoint = e.Location;
                        

                        rect = makeRectangle(startPoint, endPoint);
                        

                        selectedG.DrawRectangle(pen, rect);

                    }

                    SpriteSheet.Refresh();
                }
            }
        }



        private void SpriteSheet_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!selectedArea)
                {
                    return;
                }

                selectedArea = false;
                selectedSpite = null;
                selectedG = null;
                SpriteSheet.Image = OrigSpriteSheet;
                SpriteSheet.Refresh();


                // endPoint = e.Location;

                
            }
            // Rectangle rect = makeRectangle(startPoint, endPoint);
            if ((rect.X > 0) && (rect.Y > 0))
            {
                  MessageBox.Show("Start point" + startPoint.ToString() + "End Point" + endPoint.ToString() + "Mouse pos" + e.Location.ToString() + " rectangle" + rect.ToString());
            }
          
           
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
    }

}
