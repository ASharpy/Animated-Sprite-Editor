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
using System.Drawing.Imaging;

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

        public static bool deleteFile = false;

        MagickImage MagickSprite = new MagickImage();

        PictureBox GifPicBox = null;

        Animation animation = null;

        Image SpriteGif = null;

        MagickImageCollection SpriteCollection = new MagickImageCollection();

        int index = 0;

        private List<PictureBox> SpriteList = new List<PictureBox>();

        public Form1()
        {
            InitializeComponent();
        }

        /* 
           Loads an image from a file only accepts BMP,JPG,GIF and PNG's. Sets the picture box's image to the image loaded in 
           no returns      
        */

        // calls the add sprite sheet function when the add menu button is clicked
        private void AddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSpriteSheet();
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

        private void flowLayoutPanel1_DragDrop(object sender, DragEventArgs e)
        {
            PictureBox spritepic = new PictureBox();

            SpriteList.Add(spritepic);

            flowLayoutPanel1.Controls.Add(SpriteList[index]);

            Bitmap sprite = (Bitmap)e.Data.GetData(DataFormats.Bitmap);

            Bitmap resizeSprite = resizeImage(sprite, new Size(50, 50));

            SpriteList[index].Width = resizeSprite.Size.Width;

            SpriteList[index].Height = resizeSprite.Size.Height;

            SpriteList[index].Image = resizeSprite;

            SpriteList[index].MouseDown += new MouseEventHandler(sprite_MouseDown);

            MagickSprite = new MagickImage(sprite);

            SpriteCollection.Add(MagickSprite);

           // sprite = resizeSprite;

            sprite.Dispose();
           // resizeSprite.Dispose();

            index++;
        }

        private void sprite_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                PictureBox sprite = (PictureBox)sender;


                int picnum = SpriteList.IndexOf(sprite);



                SpriteCollection.Remove(SpriteCollection[picnum]);



                SpriteList.Remove(SpriteList[picnum]);



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

                GifPicBox.Width = animation.Width;
                GifPicBox.Height = animation.Height;


                for (int i = 0; i < SpriteCollection.Count; i++)
                {
                    SpriteCollection[i].AnimationDelay = 10;
                }

                SpriteCollection.Write(@".\megaman.gif");



                animation.Controls.Add(GifPicBox);



              SpriteGif = Image.FromFile(@".\megaman.gif"); 


                GifPicBox.Image = SpriteGif;

                GifPicBox.Width = GifPicBox.Image.Width;

                GifPicBox.Height = GifPicBox.Image.Height;

                animation.Width = GifPicBox.Image.Width;

                animation.Height = GifPicBox.Image.Height;

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



        }

        private void SpriteListDelete_Click(object sender, EventArgs e)
        {
            deleteSpriteList();
        }

        private void saveASGIFToolStripMenuItem_Click(object sender, EventArgs e)
        {

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

            }
        }

        private void saveImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileDialog SaveFile = new SaveFileDialog();
            SaveFile.Filter = "Image Files (*.jpg,*.png,*.bmp)|*.jpg,*.png,*.bmp|All files (*.*)|*.*";
            SaveFile.DefaultExt = "png";

            if (SaveFile.ShowDialog() == DialogResult.OK)
            {
                string pathName = Path.GetFileNameWithoutExtension(SaveFile.FileName);

                for (int i = 0; i < SpriteList.Count; i++)
                {
                    string filePath = Path.Combine(Path.GetDirectoryName(SaveFile.FileName), (pathName + i + "." + SaveFile.DefaultExt));

                    SpriteCollection[i].Write(filePath);
                }
            }
        }

        private void openXmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

            List<Image> GifImages = LoadFromGif();

            if (SpriteList.Count > 0)
            {
                deleteSpriteList();
            }
            for (int i = 0; i < GifImages.Count; i++)
            {
                SpriteList.Add(new PictureBox());

                Bitmap gifResize = resizeImage((Bitmap)GifImages[i], new Size(50, 50));

                SpriteList[i].Image = gifResize;


                SpriteList[index].MouseDown += new MouseEventHandler(sprite_MouseDown);

                flowLayoutPanel1.Controls.Add(SpriteList[i]);

               // Bitmap resizeSprite = resizeImage((Bitmap)SpriteList[i].Image, new Size(200, 200));

                MagickSprite = new MagickImage((Bitmap)GifImages[i]);

                SpriteCollection.Add(MagickSprite);

                index++;

            }


            flowLayoutPanel1.Refresh();

        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpriteSheet.Image.Dispose();
            SpriteSheet.Image = null;
            SpriteSheet.Refresh();

        }

        private List<Image> LoadFromGif()
        {

            FileDialog gifile = new OpenFileDialog();

            gifile.Filter = "Sprite Gif (*.gif)|*.gif";

            if (gifile.ShowDialog() == DialogResult.OK)
            {
                Image gifImage = Image.FromFile(gifile.FileName);
                FrameDimension frames = new FrameDimension(gifImage.FrameDimensionsList[0]);

                int gifFrames = gifImage.GetFrameCount(frames);

                List<Image> gifImages = new List<Image>();


                for (int i = 0; i < gifFrames; i++)
                {
                    gifImage.SelectActiveFrame(frames, i);

                    gifImages.Add(new Bitmap(gifImage));
                }

                return gifImages;
            }
            return null;
        }

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



                }
            }
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

        private void deleteSpriteList()
        {
         
            SpriteCollection.Dispose();

            index = 0;

            for (int i = 0; i < SpriteList.Count; i++)
            {
                SpriteList[i].Dispose();
            }


            SpriteList.Clear();
            flowLayoutPanel1.Refresh();
        }

    }
}
