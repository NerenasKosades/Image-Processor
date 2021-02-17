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
        static Bitmap image;            //Рабочее изображение
        Bitmap startImage;          // Хранение стартового изображения   
        
       
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)          //Реализация кнопки сохранения изображения
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

        private void button1_Click(object sender, EventArgs e)          //Реализация кнопки загрузки изображения
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

        private void button3_Click(object sender, EventArgs e)          //Реализация кнопки построения гистограммы
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
                        arrayBmp[i, j] = Convert.ToInt32(pixelColor.GetBrightness() * 255);         //Получаем яркость пикселя
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

        private void button4_Click(object sender, EventArgs e)          //Реализация кнопки сброса изображения к начальному
        {
           if(pictureBox1.Image != null)             
           { 
                pictureBox1.Image = startImage;
           }
           else
           {
                MessageBox.Show("Сначала загрузите изображение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
           }
        }

        private void button5_Click(object sender, EventArgs e)          //Реализация кнопки коррекции диапазона яркости
        {
            Bitmap bmpCheck = new Bitmap(pictureBox1.Image);            
            int brPixel;

            for (int i = 0; i < image.Height; i++)                       
            {
                for (int j = 0; j < image.Width; j++)
                {
                    brPixel = (int)(255 * bmpCheck.GetPixel(i, j).GetBrightness());         //Получаем яркость пикселя

                    if (brPixel > 255)          //Проверка на выход из диапазона
                    {
                        brPixel = 255;
                    }
                    else if (brPixel < 0)
                    {
                        brPixel = 0;
                    }
                }
            }










            /*           Bitmap bmpCheck = new Bitmap(pictureBox1.Image);
                        int[,] arrayCheck = new int[bmpCheck.Width, bmpCheck.Height];            

                        Color checkColor;

                        for (int i = 0; i < image.Height; i++)           // Получаем значения яркости и записываем в массив                
                        {
                            for (int j = 0; j < image.Width; j++)
                            {
                                checkColor = bmpCheck.GetPixel(i, j);          // Получаем цвет пикселя
                                arrayCheck[i, j] = Convert.ToInt32(checkColor.GetBrightness() * 255);
                                if (arrayCheck[i, j] > 255)
                                {
                                    arrayCheck[i, j] = 255;
                                }
                                else if (arrayCheck[i, j] < 0)
                                {
                                    arrayCheck[i, j] = 0;
                                }
                            }
                        }*/
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
