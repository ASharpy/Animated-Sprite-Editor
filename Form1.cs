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

        //The boolian to check if the second form has been closed
        public static bool deleteFile = false;

        //The magick image lib image format
        MagickImage MagickSprite = new MagickImage();

        //The picture box for the second form in which the gif plays in
        PictureBox GifPicBox = null;

        //the form in which the gif is displayed in 
        Animation animation = null;

        //The gif created
        Image SpriteGif = null;

        //The collection of sprites in the magick image lib format used to create the gif
        MagickImageCollection SpriteCollection = new MagickImageCollection();

        //Number of images in the spriteList list and the sprite collection list
        int index = 0;

        // a list of picture boxes where their images are the sprites in the flow panel
        private List<PictureBox> SpriteList = new List<PictureBox>();

        //Inizialising of the form
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
                    SpriteSheet.Image = OrigSpriteSheet;
                    SpriteSheet.Refresh();



                    copyImage(rect);

                    SpriteSheet.DoDragDrop(Clipboard.GetImage(), DragDropEffects.Copy | DragDropEffects.Move);
                }


            }

        }

        //if the mouse is moving and the left mouse button is being pressed in draw a rectangle based on the starting mouse position and the current mouse position
        // got from http://csharphelper.com/blog/2014/08/use-a-rubber-band-box-to-let-the-user-select-an-area-in-a-picture-in-c/ 
        private void SpriteSheet_MouseMove(object sender, MouseEventArgs e)
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

                if (Select.Checked == true)
                {

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

        // Gets the image inside a rectangle and saves it to the clipboard
        // @param imageRect the rectangle drawn 
        // no returns 
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

        //Flow panel drop function that Adds a picturebox to the flowPanel
        // add the image droped into the panel to the "Magick Image" lib's image collection;
        //resizes the image dragged and makes it the picture boxes image and adds it the the sprite list
        private void flowLayoutPanel1_DragDrop(object sender, DragEventArgs e)
        {
            PictureBox spritepic = new PictureBox();

            SpriteList.Add(spritepic);

            flowLayoutPanel1.Controls.Add(SpriteList[index]);

            Bitmap sprite = (Bitmap)e.Data.GetData(DataFormats.Bitmap);

            Bitmap resizeSprite = resizeImage(sprite, new Size(50, 50));

            SpriteList[index].Width = resizeSprite.Width;

            SpriteList[index].Height = resizeSprite.Height;

            SpriteList[index].Image = resizeSprite;

            resizeSprite = resizeImage(sprite, new Size(200, 200));

            //Giving the Picture Boxes a mouse down event
            SpriteList[index].MouseDown += new MouseEventHandler(sprite_MouseDown);

            //Converting from image format to the MagickImages libs format
            MagickSprite = new MagickImage(resizeSprite);

            SpriteCollection.Add(MagickSprite);

            sprite.Dispose();

            //The number of sprites in the collection list and the Sprite List
            index++;
        }

        //Removes the image that was right clicked on in the flowpanel from all lists and the panel
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

        //The image is copied when it enters the flow Panel
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

        //When the play button is clicked it creates a new form and adds a picture box to the form
        //using the Magick Image lib the gif is saved to the bin file
        //Reads the gif from the file and makes it the image of the picture box
        private void play_Click(object sender, EventArgs e)
        {


            if (SpriteCollection.Count > 0 && SpriteList.Count > 0)
            {
                GifPicBox = new PictureBox();

                animation = new Animation();

                for (int i = 0; i < SpriteCollection.Count; i++)
                {
                    SpriteCollection[i].AnimationDelay = 10;
                }

                SpriteCollection.Write(@".\megaman.gif");

                animation.Controls.Add(GifPicBox);

                SpriteGif = Image.FromFile(@".\megaman.gif");

                GifPicBox.Image = SpriteGif;

                animation.Width = GifPicBox.Image.Width + 100;

                animation.Height = GifPicBox.Image.Height + 100;

                GifPicBox.Width = animation.Width;

                GifPicBox.Height = animation.Height;

                GifPicBox.Location = new Point(50, 50);

                animation.Show();

            }

        }

        //Resizes a desired image
        //@param image the image to be resized
        //@Param size the new size of the image
        //Returns and new bitmap of the resized image
        private Bitmap resizeImage(Bitmap image, Size size)
        {
            return (new Bitmap(image, size));
        }

        //Checks every frame if the second form has closed based on a bool
        //if the second form is closed then in deletes all items in the second form
        // and the gif from the file
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

        //Deletes the sprites from the flow panel once the DeleteSpite button is clicked
        //Removes the images from all their lists
        private void SpriteListDelete_Click(object sender, EventArgs e)
        {
            deleteSpriteList();
        }

        //When the saveAs Gif button is pressed it uses the Magick Image Lib function "write"
        // to save the collection of images as a gif the a user selected file location
        private void saveASGIFToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = saveFile.Filter = "gif files (*.gif)|*.gif";
            saveFile.FileName = "Sprite";
            saveFile.DefaultExt = "gif";
            saveFile.FilterIndex = 2;
            saveFile.RestoreDirectory = true;

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                string filename = saveFile.FileName;

                //Chosen file path
                string fN = Path.GetFileName(filename);

                //Magick Image Save Function
                SpriteCollection.Write(filename + ".gif");

            }
        }

        //When the Save Images button is pressed go through the Magick image Collection and 
        //Save each image to a user chosen file location
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
                    //Had to combine the file path with the name of the user chose + the image number and extension
                    string filePath = Path.Combine(Path.GetDirectoryName(SaveFile.FileName), (pathName + i + "." + SaveFile.DefaultExt));

                    SpriteCollection[i].Write(filePath);
                }
            }
        }

        //When the open gif button is pressed it gets each frame of the gif and saves them
        //Into all lists and images and added the images to the flow panel
        private void openXmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //gets the images from the frames of the gif
            List<Image> GifImages = LoadFromGif();

            if (GifImages == null)
            {
                return;
            }

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

                MagickSprite = new MagickImage((Bitmap)GifImages[i]);

                SpriteCollection.Add(MagickSprite);

                index++;

            }


            flowLayoutPanel1.Refresh();

        }

        //When the remove sprite sheet button is presse it deletes the image of the sprite sheet picture box
        // sets it to null and refreshes the picture box
        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpriteSheet.Image.Dispose();
            SpriteSheet.Image = null;
            SpriteSheet.Refresh();

        }

        //Loads a user chosen gif and gets a list of images based on the frames of the gif
        // returns a list of images which are each individual image in a gif
        // got From https://stackoverflow.com/questions/540701/access-gif-frames-with-c-sharp 
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

        // Loads in an image of the users choice and sets the sprite sheet image to the image loaded in
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

        // Makes a new rectangle 
        //@param point1 the starting point of the rectangle (ie top left corner)
        //@param point2 the end point of the rectangel (ie botton right corner)
        // Returns a new rectangle at the choosen points
        private Rectangle makeRectangle(Point Point1, Point Point2)
        {
            return new Rectangle(Math.Min(Point1.X, Point2.X), Math.Min(Point1.Y, Point2.Y),
                Math.Abs(Point1.X - Point2.X), Math.Abs(Point1.Y - Point2.Y));
        }

        //gets the mouse position relative to the picture box and not the image in the picture box
        private Point MousePos()
        {
            Point controlrelative = SpriteSheet.PointToClient(MousePosition);

            Size imageSize = SpriteSheet.Image.Size;

            Size PicBoxSize = SpriteSheet.Size;


            float X = ((float)imageSize.Width / (float)PicBoxSize.Width) * controlrelative.X;

            float Y = ((float)imageSize.Height / (float)PicBoxSize.Height) * controlrelative.Y;


            return new Point(Math.Abs((int)X), Math.Abs((int)Y));
        }

        //Deletes all sprites in the flow panel and removes them from the lists and refreshes the flow panel
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
