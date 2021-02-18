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

        /* 
         * Метод проверяет выход значения цветов по каналам за пределы диапазона от 0 до 255.
         * В случае превышения - устанавливает 255.
         * В случае ухода в отрицательные значения -устанавливает 0.
         * Обязательно вызывать метод при обработке изображения до установки значения в пиксель - иначе возврат исключения.
         * Обязательно проверять наличие загруженного изображения в pictureBox. (pictureBox.Image != null) - иначе возврат необработанного исключения NullException
         * Перегрузка с одним параметром типа Image - Проверка изображения полностью. Например pictureBox1.Image = Check(pictureBox1.Image)
         * Перегрузка с одним целочисленным параметром - возвращает цвет в диапазон. Вызывать отдельно для каждого цвета.
         */
        public static Bitmap Check(Image bmpColor)             // Реализация функции проверки
        {

            Bitmap bmpCheck = new Bitmap(bmpColor);
            int redColor;
            int greenColor;
            int blueColor;
            Color pixelColor;

            for (int i = 0; i < bmpColor.Height; i++)
            {
                for (int j = 0; j < bmpColor.Width; j++)
                {
                    redColor = bmpCheck.GetPixel(i, j).R;
                    greenColor = bmpCheck.GetPixel(i, j).G;
                    blueColor = bmpCheck.GetPixel(i, j).B;

                    if (redColor > 255)
                        redColor = 255;
                    if (redColor < 0)
                        redColor = 0;
                    if (greenColor > 255)
                        greenColor = 255;
                    if (greenColor < 0)
                        greenColor = 0;
                    if (blueColor > 255)
                        blueColor = 255;
                    if (blueColor < 0)
                        blueColor = 0;
                    pixelColor = Color.FromArgb(255, redColor, greenColor, blueColor);
                    bmpCheck.SetPixel(i, j, Color.FromArgb(255, redColor, greenColor, blueColor));                   
                }
            }
            return bmpCheck;
        }

        public static int Check(int color)
        {
            if (color > 255)
                color = 255;
            if (color < 0)
                color = 0;
            return color;
        }
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

        private void button5_Click(object sender, EventArgs e)          //Реализация кнопки принудительной коррекции диапазона яркости
        {
            Bitmap bmpCheck = new Bitmap(pictureBox1.Image);                       
            int redColor;
            int greenColor;
            int blueColor;
            Color pixelColor;
            if (pictureBox1.Image != null)
            {
                for (int i = 0; i < image.Height; i++)
                {
                    for (int j = 0; j < image.Width; j++)
                    {
                        redColor = bmpCheck.GetPixel(i, j).R;
                        greenColor = bmpCheck.GetPixel(i, j).G;
                        blueColor = bmpCheck.GetPixel(i, j).B;
                        
                        if (redColor > 255)
                            redColor = 255;
                        if (redColor < 0)
                            redColor = 0;
                        if (greenColor > 255)
                            greenColor = 255;
                        if (greenColor < 0)
                            greenColor = 0;
                        if (blueColor > 255)
                            blueColor = 255;
                        if (blueColor < 0)
                            blueColor = 0;
                        pixelColor = Color.FromArgb(255, redColor, greenColor, blueColor);
                        bmpCheck.SetPixel(i, j, Color.FromArgb(255, redColor, greenColor, blueColor));
                        pictureBox1.Image = bmpCheck;
                    }
                }

                pictureBox1.Image = bmpCheck;
            }
            else
            {
                MessageBox.Show("Сначала загрузите изображение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private void label2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
