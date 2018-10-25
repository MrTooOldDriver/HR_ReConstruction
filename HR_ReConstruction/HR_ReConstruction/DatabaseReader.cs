using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HR_ReConstruction.Properties;


namespace HR_ReConstruction
{

    class DatabaseReader
    {
        public long imageStreamLocation { get; set; }
        public long lableStreamLocation { get; set; }
        public int pixNumber { get; set; }

        public byte[] readBytes()
        {
            string imagePath = @"train-images.idx3-ubyte";
            string lablePath = @"train-labels.idx1-ubyte";
            FileStream imageStream = new FileStream(imagePath, FileMode.Open);
            FileStream lableStream = new FileStream(lablePath, FileMode.Open);
            BinaryReader imageReader = new BinaryReader(imageStream);
            BinaryReader lableReader = new BinaryReader(lableStream);

            byte[] pixInfo = new byte[0];

            if (imageStreamLocation == 0)
            {
                int magNumber = imageReader.ReadInt32();
                int imageNumber = imageReader.ReadInt32();
                int rowNumber = imageReader.ReadInt32();
                int colNumber = imageReader.ReadInt32();

                magNumber = convertByte(magNumber);
                imageNumber = convertByte(imageNumber);
                rowNumber = convertByte(rowNumber);
                colNumber = convertByte(colNumber);

                pixNumber = rowNumber * colNumber;
                MessageBox.Show(pixNumber.ToString());

                pixInfo = new byte[pixNumber + 1];
                for (int i = 0; i < pixNumber; i++)
                {
                    pixInfo[i] = imageReader.ReadByte();
                }

                imageStreamLocation = imageStream.Position;
            }
            else
            {
                imageStream.Seek(imageStreamLocation, 0);
                pixInfo = new byte[pixNumber + 1];
                for (int i = 0; i < pixNumber; i++)
                {
                    pixInfo[i] = imageReader.ReadByte();
                }

                imageStreamLocation = imageStream.Position;
            }

            imageStream.Close();

            if (lableStreamLocation == 0)
            {
                int magNumber = lableReader.ReadInt32();
                int itemNumber = lableReader.ReadInt32();

                magNumber = convertByte(magNumber);
                itemNumber = convertByte(itemNumber);

                pixInfo[pixNumber] = lableReader.ReadByte();
                lableStreamLocation = lableStream.Position;
            }
            else
            {
                lableStream.Seek(lableStreamLocation, 0);
                pixInfo[pixNumber] = lableReader.ReadByte();
                lableStreamLocation = lableStream.Position;
            }

            lableStream.Close();
            return pixInfo;
        }

        public int convertByte(int inputInt)
        {
            byte[] inputByte = BitConverter.GetBytes(inputInt);
            Array.Reverse(inputByte);
            return BitConverter.ToInt32(inputByte, 0);
        }

    }
}
