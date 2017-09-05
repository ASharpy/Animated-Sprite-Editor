using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageMagick;
using System.Diagnostics;
using System.IO;
using System.Security.AccessControl;
using System.Xml;
using System.Xml.Serialization;

// select index changed

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

        private Image SpriteGif = null;

        public static bool deleteFile = false;

        MagickImage MagickSprite = new MagickImage();

        PictureBox GifPicBox = null;

        Animation animation = null;

        MagickImageCollection SpriteCollection = new MagickImageCollection();

        int index = 0;

        private List<PictureBox> SpriteList = new List<PictureBox>();

        public List<Image> SerializedList = new List<Image>();

        List<Serialize> PicList;

        public Form1()
        {

            InitializeComponent();
            SpriteSheet.Paint += new System.Windows.Forms.PaintEventHandler(SpriteSheet_Paint);
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
                dlg.Filter = "Image Files(*.BMP;*.JPG;*.PNG;)|*.BMP;*.JPG;*.PNG|All files (*.*)|*.*";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    SpriteSheet.Image = Image.FromFile(dlg.FileName);
                    SpriteSheet.SizeMode = PictureBoxSizeMode.StretchImage;
                    SerializedList.Add(Image.FromFile(dlg.FileName));

                   
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
            if (selectedArea == true)
            {
                selectedSpite = null;
                selectedG = null;
                SpriteSheet.Image = OrigSpriteSheet;
                SpriteSheet.Refresh();

                // SpriteSheet.Image = null;

            }


            if (e.Button == MouseButtons.Left)
            {

                if (SpriteSheet.Image != null)
                {



                    OrigSpriteSheet = new Bitmap(SpriteSheet.Image);







                    selectedArea = false;

                    startPoint = MousePos();

                    selectedSpite = new Bitmap(OrigSpriteSheet);
                    selectedG = Graphics.FromImage(selectedSpite);
                    SpriteSheet.Image = selectedSpite;
                }
            }

            if (e.Button == MouseButtons.Right)
            {
                if (OrigSpriteSheet != null && selectedArea == true)
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

        }

        //if the mouse is moving and the left mouse button is being pressed in draw a rectangle based on the starting mouse position and the current mouse position
        private void SpriteSheet_MouseMove(object sender, MouseEventArgs e)
        {



            if (Select.Checked == true)
            {


                if (e.Button == MouseButtons.Left)
                {
                    if (OrigSpriteSheet == null)
                    {
                        return;
                    }


                    if (OrigSpriteSheet.Width > 0 && OrigSpriteSheet.Height > 0)
                    {
                        selectedG.DrawImage(OrigSpriteSheet, 0, 0);
                    }

                    selectedArea = true;


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

        }


        private void SpriteSheet_Paint(object sender, PaintEventArgs e)
        {


            if (startPoint.X > 0 && startPoint.Y > 0 && endPoint.X > 0 && endPoint.Y > 0)
            {

            }



        }

        private void panel2_Click(object sender, EventArgs e)
        {

        }

        private void Select_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void copyImage(Rectangle imageRect)
        {



            int X = Math.Abs(endPoint.X - startPoint.X);


            int Y = Math.Abs(endPoint.Y - startPoint.Y);

            Bitmap bm = new Bitmap(X, Y);

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
            PictureBox spritepic = new PictureBox();

            SpriteList.Add(spritepic);

            flowLayoutPanel1.Controls.Add(SpriteList[index]);

            //Debug.WriteLine(sender.ToString());



            Bitmap sprite = (Bitmap)e.Data.GetData(DataFormats.Bitmap);

            Bitmap resizeSprite = resizeImage(sprite, new Size(200, 200));


            SpriteList[index].Width = sprite.Size.Width;

            SpriteList[index].Height = sprite.Size.Height;

            SpriteList[index].Image = sprite;

            SpriteList[index].MouseDown += new MouseEventHandler(sprite_MouseDown);

            MagickSprite = new MagickImage(resizeSprite);

            SerializedList.Add(sprite);

            SpriteCollection.Add(MagickSprite);



            sprite = resizeSprite;

            sprite.Dispose();
            resizeSprite.Dispose();


            index++;




            // picBox.BorderStyle = BorderStyle.Fixed3D;
        }


        private void sprite_MouseDown(object sender, MouseEventArgs e)
        {


            if (e.Button == MouseButtons.Right)
            {
                PictureBox sprite = (PictureBox)sender;


                int picnum = SpriteList.IndexOf(sprite);



                SpriteCollection.Remove(SpriteCollection[picnum]);



                SpriteList.Remove(SpriteList[picnum]);

                SerializedList.Remove(SerializedList[picnum]);

                sprite.Dispose();
                index--;
            }





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

        private void play_Click(object sender, EventArgs e)
        {


            if (SpriteCollection.Count > 0 && SpriteList.Count > 0)
            {
                GifPicBox = new PictureBox();

                animation = new Animation();


                // SpriteCollection.Optimize();





                GifPicBox.Width = animation.Width;
                GifPicBox.Height = animation.Height;
                // animated.BackColor = Color.Black;

                for (int i = 0; i < SpriteCollection.Count; i++)
                {
                    SpriteCollection[i].AnimationDelay = 10;
                }

                SpriteCollection.Write(@".\megaman.gif");

                //  SpriteCollection.Optimize();

                animation.Controls.Add(GifPicBox);

                SpriteGif = Image.FromFile(@".\megaman.gif");


                GifPicBox.Image = SpriteGif;


                animation.Show();


            }
        }



        private Bitmap resizeImage(Bitmap image, Size size)
        {
            return (new Bitmap(image, size));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (deleteFile == true)
            {
                animation.Dispose();
                GifPicBox.Dispose();
                SpriteGif.Dispose();

                System.IO.File.Delete(@".\megaman.gif");
                deleteFile = false;
            }

            if (Select.Checked == true)
            {
                SpriteSheet.Cursor = Cursors.Cross;
            }
            else
            {
                SpriteSheet.Cursor = Cursors.Arrow;
            }

        }

        private void SpriteListDelete_Click(object sender, EventArgs e)
        {

            SpriteCollection.Dispose();

            index = 0;

            for (int i = 0; i < SpriteList.Count; i++)
            {
                SpriteList[i].Dispose();
            }


            for (int i = 0; i < SerializedList.Count; i++)
            {
                SerializedList[i].Dispose();
            }

            SerializedList.Clear();
            SpriteList.Clear();
            flowLayoutPanel1.Refresh();


        }

        private void saveASGIFToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //Saving the file to the user location



            FileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = saveFile.Filter = "gif files (*.gif)|*.gif|All files (*.*)|*.*";
            saveFile.FileName = "SpriteGif";
            saveFile.DefaultExt = "gif";
            saveFile.FilterIndex = 2;
            saveFile.RestoreDirectory = true;

            if (saveFile.ShowDialog() == DialogResult.OK)
            {

                string filename = saveFile.FileName;


                string fN = Path.GetFileName(filename);





                SpriteCollection.Write(filename + ".gif");


                // saveFile.Dispose();



                //File.Delete(saveFile.FileName);

                //saveFile.FileName = null;

            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {


        }

        public void Serialize()
        {
          

        }

        private void saveImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "png files (*.png)|*.png|All files (*.*)|*.*";
            saveFile.FileName = "Sprites";
            //saveFile.DefaultExt = "xml";
            saveFile.FilterIndex = 2;
            saveFile.RestoreDirectory = true;
        

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < SpriteList.Count; i++)
                {
                    string filePath = Path.Combine(saveFile.FileName, string.Format("sprite{0}.png", i));
                    SpriteList[i].Image.Save(filePath);
                }

            }
          

        //    Serialize serial = new Serialize();

          

        //    serial.serialize(SpriteList);
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void openXmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
                Serialize serial = new Serialize();

           FileDialog openfile = new OpenFileDialog();

            openfile.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";


            if (openfile.ShowDialog() == DialogResult.OK)
            {
                PicList = serial.Deserialize(openfile);
            }


            SpriteSheet.Image = PicList[0].spriteImages;
            SpriteSheet.SizeMode = PictureBoxSizeMode.StretchImage;
           // SerializedList.Add(Image.FromFile(dlg.FileName));

        }
    }



}
