using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public String fileSignature;
        public Int32 fileSize;
        public Int16 reserved1;
        public Int16 reserved2;
        public Int32 locationOfData;
        public Int32 sizeHeader;
        public Int32 imgWidth;
        public Int32 imgHeight;
        public Int16 numberPlanes;
        public Int16 bitPixel;
        public Int32 bfCompress;
        public Int32 sizeRasterArray;
        public Int32 imgHorizontalResolution;
        public Int32 imgVerticalResolution;
        public Int32 numberColors;
        public Int32 numberMainColors;


        public Form1()
        {
            InitializeComponent();
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            this.openFileDialog.Filter = "bmp |*.bmp";
            this.openFileDialog.ShowDialog();

            BinaryReader bReader = new BinaryReader(File.Open(openFileDialog.FileName, FileMode.Open));
            fileSignature = new string(bReader.ReadChars(2));
            fileSize = bReader.ReadInt32();
            reserved1 = bReader.ReadInt16();
            reserved2 = bReader.ReadInt16();
            locationOfData = bReader.ReadInt32();
            sizeHeader = bReader.ReadInt32();
            imgWidth = bReader.ReadInt32();
            imgHeight = bReader.ReadInt32();
            numberPlanes = bReader.ReadInt16();
            bitPixel = bReader.ReadInt16();
            bfCompress = bReader.ReadInt32();
            sizeRasterArray = bReader.ReadInt32();
            imgHorizontalResolution = bReader.ReadInt32();
            imgVerticalResolution = bReader.ReadInt32();
            numberColors = bReader.ReadInt32();
            numberMainColors = bReader.ReadInt32();


            bReader.Close();

            String compressType = 0.ToString();
            if (bfCompress == 0 || bfCompress == 3 || bfCompress == 6)
                compressType = "Без сжатия";
            else if (bfCompress == 1 || bfCompress == 2)
                compressType = "RLE";
            else if (bfCompress == 4)
                compressType = "JPEG";
            else if (bfCompress == 5)
                compressType = "PNG";

            Bitmap original_image = new Bitmap(openFileDialog.FileName);
            this.pictureBox.Width = original_image.Width;
            this.pictureBox.Height = original_image.Height;
            this.pictureBox.Image = original_image;
            this.Location = new Point(0, 0);
            this.Size = new Size(original_image.Width + 400, original_image.Height + 150);
            this.pictureBox.Show();


            String message = "Сигнатура файла: " + fileSignature + "\n Размер файла: " + fileSize.ToString() +
                             "\n Местонахождение данных растрового массива: " + locationOfData.ToString() +
                             "\n Длина заголовка растрового массива: " + sizeHeader.ToString() +
                             "\n Ширина изображения: " + imgWidth.ToString() + "\n Высота изображения: " +
                             imgHeight.ToString() + "\n Число цевтовых плоскостей: " + numberPlanes +
                             "\n Бит/пиксел: " + bitPixel + "\n Метод сжатия: " + compressType +
                             "\n Длина растрового массива: " + sizeRasterArray + "\n Горизонтальное разрешение: " +
                             imgHorizontalResolution + "\n Вертикальное разрешение: " + imgVerticalResolution +
                             "\n Количество цветов изображения: " + numberColors + "\n Количество основных цветов: " +
                             numberMainColors;

            MessageBox.Show(message);
        }
    }
}
