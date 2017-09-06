using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Animated_Sprite_Editor
{
    public class Serialize
    {
        public List<Serialize> sprites = new List<Serialize>();

        private Form1 form = new Form1();

        public string spriteImages;


        public Serialize(){}

        string pathName = Path.GetDirectoryName(Application.ExecutablePath);

        string filePath;
        public void serialize(List<PictureBox> spriteList)
        {
            for (int i = 0; i < spriteList.Count; i++)
            {

                filePath = Path.Combine(pathName, string.Format("image{0}.png", i));

                spriteList[i].Image.Save(filePath);

                Serialize pic = new Serialize();

                pic.spriteImages = filePath;

                sprites.Add(pic);
            }


       

            XmlSerializer mySerializer = new XmlSerializer(typeof(List<Serialize>));

            FileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            saveFile.FileName = "Sprites";
            //saveFile.DefaultExt = "xml";
            saveFile.FilterIndex = 2;
            saveFile.RestoreDirectory = true;


            if (saveFile.ShowDialog() == DialogResult.OK)
            {

                StreamWriter streamWriter = new StreamWriter(saveFile.FileName);

                mySerializer.Serialize(streamWriter, sprites);
                streamWriter.Close();
            }

          

           
        }
        

        public List<Serialize> Deserialize(FileDialog openfile)
        {
          

            XmlSerializer mySerializer = new XmlSerializer(typeof(List<Serialize>));

            StreamReader streamReader = new StreamReader(openfile.FileName);

            
                List<Serialize> spriteImages = mySerializer.Deserialize(streamReader) as List<Serialize>;

                streamReader.Close();

                return spriteImages;
            

          
        }
    }
}
