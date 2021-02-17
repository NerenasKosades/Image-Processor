using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image_Processor
{
    public partial class Form1 : Form
    {
        static Bitmap image; //Рабочее изображение
        //Bitmap im = new Bitmap(pictureBox1.image);
        
        Bitmap startImage; // Хранение стартового изображения        
        //int imW = image.Width;
        //int imH = image.Height;
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            {
                SaveFileDialog savedialog = new SaveFileDialog();
                savedialog.Title = "Сохранить картинку как...";
                savedialog.OverwritePrompt = true;
                savedialog.CheckPathExists = true;
                savedialog.Filter = "*.jpeg|*.jpeg";
                if (savedialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        image.Save(savedialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    catch
                    {
                        MessageBox.Show("Сначала загрузите изображение", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "BitMap Files()*.bmp|*.bmp";
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    image = new Bitmap(openDialog.FileName);
                    pictureBox1.Image = image;
                    pictureBox1.Invalidate();
                    startImage = image;
                }
                catch
                {
                    DialogResult rezult = MessageBox.Show("Невозможно открыть выбранный файл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap bmp = new Bitmap(pictureBox1.Image);         
                int[,] arrayBmp = new int[bmp.Width, bmp.Height];
                int[] masY = new int[bmp.Width * bmp.Height];
                int[] masX = new int[bmp.Width * bmp.Height];

                Color pixelColor;

                for (int i = 0; i < image.Height; i++)           // Получаем значения яркости и записываем в массив                
                {                    
                    for (int j = 0; j < image.Width; j++)
                    {
                    pixelColor = bmp.GetPixel(i, j);          // Получаем цвет пикселя
                    arrayBmp[i, j] = Convert.ToInt32(pixelColor.GetBrightness() * 256);         //Получаем яркость пикселя
                    //arrayBmp[i, j] = ((image.GetPixel(i, j) == Color.Red ? 0 : 256) + (image.GetPixel(i, j) == Color.Green ? 0 : 256) + (image.GetPixel(i, j) == Color.Blue ? 0 : 256)) / 3;                        
                    }
                }
               
                for (int i = 0; i < bmp.Width * bmp.Height; i++)           // Заполняем ось X
                {
                    masX[i] = i;
                }
                
                int count = 0;                          //Заполняем ось Y
                for (int i = 0; i < image.Height; i++)         
                {
                    for (int j = 0; j < image.Width; j++)
                    {
                        masY[count] = arrayBmp[i, j];
                        count++;
                    }
                }

                  this.chart1.Series["Series1"].Points.DataBindXY(masX, masY);    // Построение гистограммы
            }
            catch
            {
                MessageBox.Show("Загрузите изображение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
