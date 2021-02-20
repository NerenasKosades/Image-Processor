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
                        pictureBox1.Image.Save(savedialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    catch
                    {
                        MessageBox.Show("Сначала загрузите изображение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                Bitmap bmpGist = new Bitmap(pictureBox1.Image);
                int redColor;
                int greenColor;
                int blueColor;
                int[,] brightnesPixel = new int[bmpGist.Width, bmpGist.Height];
                int[] masY = new int[bmpGist.Width * bmpGist.Height];
                int[] masX = new int[bmpGist.Width * bmpGist.Height];

                for (int i = 0; i < bmpGist.Height; i++)
                {
                    for (int j = 0; j < bmpGist.Width; j++)
                    {
                        redColor = bmpGist.GetPixel(i, j).R;                    //Получаем цвета
                        greenColor = bmpGist.GetPixel(i, j).G;                  
                        blueColor = bmpGist.GetPixel(i, j).B;                   
                        redColor = Check(redColor);                             //Проверка диапазона
                        greenColor = Check(greenColor);
                        blueColor = Check(blueColor);

                        brightnesPixel[i, j] = (int)(0.299 * redColor + 0.587 * greenColor + 0.114 * blueColor);                //Расчет яркости - формула взята отсюда https://ru.wikipedia.org/wiki/%D0%9E%D1%82%D1%82%D0%B5%D0%BD%D0%BA%D0%B8_%D1%81%D0%B5%D1%80%D0%BE%D0%B3%D0%BE
                    }
                }
                for (int i = 0; i < bmpGist.Width * bmpGist.Height; i++)           // Заполняем ось X
                {
                    masX[i] = i;
                }

                int count = 0;                          //Заполняем ось Y
                for (int i = 0; i < image.Height; i++)
                {
                    for (int j = 0; j < image.Width; j++)
                    {
                        masY[count] = brightnesPixel[i, j];
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
            //Color pixelColor;
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
                        //pixelColor = Color.FromArgb(255, redColor, greenColor, blueColor);
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

        private void button6_Click(object sender, EventArgs e)          //Реализация инверсии
        {
            Bitmap invPixel = new Bitmap(pictureBox1.Image);
            int redColor;
            int greenColor;
            int blueColor;            

            if (pictureBox1.Image != null)
            {
                for (int i = 0; i < image.Height; i++)
                {
                    for (int j = 0; j < image.Width; j++)
                    {
                        redColor = invPixel.GetPixel(i, j).R;
                        greenColor = invPixel.GetPixel(i, j).G;
                        blueColor = invPixel.GetPixel(i, j).B;
                        redColor = Check(redColor);
                        greenColor = Check(greenColor);
                        blueColor = Check(blueColor);

                        redColor = 255 - redColor;
                        greenColor = 255 - greenColor;
                        blueColor = 255 - blueColor;
                        
                        invPixel.SetPixel(i, j, Color.FromArgb(255, redColor, greenColor, blueColor));
                        pictureBox1.Image = invPixel;
                    }
                }

                pictureBox1.Image = invPixel;
            }
            else
            {
                MessageBox.Show("Сначала загрузите изображение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button7_Click(object sender, EventArgs e)          //Реализация бинаризации
        {
            Bitmap invPixel = new Bitmap(pictureBox1.Image);
            int redColor;
            int greenColor;
            int blueColor;
            int binPixel;

            if (pictureBox1.Image != null)
            {
                for (int i = 0; i < image.Height; i++)
                {
                    for (int j = 0; j < image.Width; j++)
                    {
                        redColor = invPixel.GetPixel(i, j).R;
                        greenColor = invPixel.GetPixel(i, j).G;
                        blueColor = invPixel.GetPixel(i, j).B;

                        binPixel = (int)((redColor + greenColor + blueColor) / 3);                       

                        redColor = binPixel;
                        greenColor = binPixel;
                        blueColor = binPixel;

                        redColor = Check(redColor);
                        greenColor = Check(greenColor);
                        blueColor = Check(blueColor);


                        invPixel.SetPixel(i, j, Color.FromArgb(255, redColor, greenColor, blueColor));
                        pictureBox1.Image = invPixel;
                    }
                }

                pictureBox1.Image = invPixel;
            }
            else
            {
                MessageBox.Show("Сначала загрузите изображение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button8_Click(object sender, EventArgs e)          //Расчет сверток
        {
            string[,] stringConvolution = { { MT11.Text, MT12.Text, MT13.Text }, { MT21.Text, MT22.Text, MT23.Text }, { MT31.Text, MT32.Text, MT33.Text } };
            int[,] convolution = new int[3, 3];
            //try
            //{
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        convolution[i, j] = Convert.ToInt32(stringConvolution[i, j]);
                    }
                }
                if (pictureBox1.Image != null)
                {
                    for (int i = 0; i < 3; i++)         //Заполнение массива свертки
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            convolution[i, j] = Convert.ToInt32(stringConvolution[i, j]);
                        }
                    }

                    Bitmap conColors = new Bitmap(pictureBox1.Image);

                    int[,] redColor = new int[conColors.Width, conColors.Height];           //Создание массивов для получения цвета
                    int[,] greenColor = new int[conColors.Width, conColors.Height];
                    int[,] blueColor = new int[conColors.Width, conColors.Height];

                    int[,] redColorPlus = new int[conColors.Width + 2, conColors.Height + 2];           //Создание массивов с увеличенным размером
                    int[,] greenColorPlus = new int[conColors.Width + 2, conColors.Height + 2];
                    int[,] blueColorPlus = new int[conColors.Width + 2, conColors.Height + 2];

                    for (int i = 0; i < conColors.Width; i++)
                    {
                        for (int j = 0; j < conColors.Height; j++)
                        {
                            redColor[i, j] = conColors.GetPixel(i, j).R;            //Получение цветов и запись в массивы
                            greenColor[i, j] = conColors.GetPixel(i, j).G;
                            blueColor[i, j] = conColors.GetPixel(i, j).B;

                            redColor[i, j] = Check(redColor[i, j]);                 //Проверка диапазонов
                            greenColor[i, j] = Check(greenColor[i, j]);
                            blueColor[i, j] = Check(blueColor[i, j]);
                        }
                    }
                    int countW = 0;
                    int countH = 0;
                    for (int i = 1; i < conColors.Width + 2; i++)           //Заполнение центральной части массива
                    {
                        for (int j = 1; j < conColors.Height + 2; j++)
                        {
                            redColorPlus[i, j] = redColor[countW, countH];
                            greenColorPlus[i, j] = greenColor[countW, countH];
                            blueColorPlus[i, j] = blueColor[countW, countH];
                            countW++;
                            countH++;
                        }
                    }

                    for (int i = 1; i < conColors.Width - 1; i++)
                    {
                        redColorPlus[1, i] = redColorPlus[2, i];            // Заполнение верхней строки
                        greenColorPlus[1, i] = greenColorPlus[2, i];
                        blueColorPlus[1, i] = blueColorPlus[2, i];

                        redColorPlus[conColors.Height, i] = redColorPlus[conColors.Height - 1, i];          //Заполнение нижней строки
                        greenColorPlus[conColors.Height, i] = greenColorPlus[conColors.Height - 1, i];
                        blueColorPlus[conColors.Height, i] = blueColorPlus[conColors.Height - 1, i];
                    }

                    for (int j = 0; j < conColors.Height; j++)
                    {
                        redColorPlus[j, 1] = redColorPlus[j, conColors.Width + 1];          //Заполнение левого столбца
                        greenColorPlus[j, 1] = greenColorPlus[j, conColors.Width + 1];
                        blueColorPlus[j, 1] = blueColorPlus[j, conColors.Width + 1];

                        redColorPlus[j, conColors.Width] = redColorPlus[j, conColors.Width - 1];
                        greenColorPlus[j, conColors.Width] = greenColorPlus[j, conColors.Width - 1];
                        blueColorPlus[j, conColors.Width] = blueColorPlus[j, conColors.Width - 1];
                    }

                    for (int i = 1; i < conColors.Width + 1; i++)
                    {
                        for (int j = 1; j < conColors.Height + 1; j++)
                        {
                            redColorPlus[i, j] = (redColorPlus[i - 1, j - 1] * convolution[1, 1]            //Проход сверткой по красному диапазону
                                                + redColorPlus[i - 1, j] * convolution[1, 2]
                                                + redColorPlus[i - 1, j + 1] * convolution[1, 3]
                                                + redColorPlus[i, j - 1] * convolution[2, 1]
                                                + redColorPlus[i, j] * convolution[2, 2]
                                                + redColorPlus[i, j + 1] * convolution[2, 3]
                                                + redColorPlus[i + 1, j - 1] * convolution[3, 1]
                                                + redColorPlus[i + 1, j] * convolution[3, 2]
                                                + redColorPlus[i + 1, j + 1] * convolution[3, 3]) / 9;

                            greenColorPlus[i, j] = (greenColorPlus[i - 1, j - 1] * convolution[1, 1]            //Проход сверткой по зеленому диапазону
                                               + greenColorPlus[i - 1, j] * convolution[1, 2]
                                               + greenColorPlus[i - 1, j + 1] * convolution[1, 3]
                                               + greenColorPlus[i, j - 1] * convolution[2, 1]
                                               + greenColorPlus[i, j] * convolution[2, 2]
                                               + greenColorPlus[i, j + 1] * convolution[2, 3]
                                               + greenColorPlus[i + 1, j - 1] * convolution[3, 1]
                                               + greenColorPlus[i + 1, j] * convolution[3, 2]
                                               + greenColorPlus[i + 1, j + 1] * convolution[3, 3]) / 9;

                            blueColorPlus[i, j] = (blueColorPlus[i - 1, j - 1] * convolution[1, 1]          //Проход сверткой по синему диапазону
                                               + blueColorPlus[i - 1, j] * convolution[1, 2]
                                               + blueColorPlus[i - 1, j + 1] * convolution[1, 3]
                                               + blueColorPlus[i, j - 1] * convolution[2, 1]
                                               + blueColorPlus[i, j] * convolution[2, 2]
                                               + blueColorPlus[i, j + 1] * convolution[2, 3]
                                               + blueColorPlus[i + 1, j - 1] * convolution[3, 1]
                                               + blueColorPlus[i + 1, j] * convolution[3, 2]
                                               + blueColorPlus[i + 1, j + 1] * convolution[3, 3]) / 9;

                            redColorPlus[i, j] = Check(redColorPlus[i, j]);                                 //Проверка диапазонов
                            greenColorPlus[i, j] = Check(greenColorPlus[i, j]);
                            blueColorPlus[i, j] = Check(blueColorPlus[i, j]);
                        }
                    }

                    for (int i = 1; i < conColors.Width + 1; i++)                           //Возврат цветов в экземпляр класса Bitmap
                    {
                        for (int j = 1; j < conColors.Height + 1; j++)
                        {
                            conColors.SetPixel(i, j, Color.FromArgb(255, redColorPlus[i, j], greenColorPlus[i, j], blueColorPlus[i, j]));
                        }
                    }

                    pictureBox1.Image = conColors;
                }
                else
                {
                    MessageBox.Show("Сначала загрузите изображение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            //}
            //catch
            //{
            //    MessageBox.Show("Заполните все поля свертки корректно", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
    }
}
