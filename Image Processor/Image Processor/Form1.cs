﻿using System;
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
        public Form1()
        {
            InitializeComponent();
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        static Bitmap image;                //Рабочее изображение
        static Bitmap startImage;                  // Хранение стартового изображения   
        int fMax, fMin, gMax, gMin;         //  Глобальные переменные для
        double alfa, power;                 //  для рассчета распределений
                
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

        public static double Frequency(int[,] colorArray, int color)    //Метод расчета частости
        {
            int count = 0;
            for (int i = 0; i < colorArray.GetLength(0); i++)
            {
                for (int j = 0; j < colorArray.GetLength(1); j++)
                {
                    if (colorArray[i, j] == color)
                    {
                        count++;
                    }
                }
            }

            return color / count;
        }


        /* 
         * Метод проверяет выход значения цветов по каналам за пределы диапазона от 0 до 255.
         * В случае превышения - устанавливает 255.
         * В случае ухода в отрицательные значения - устанавливает 0.
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

        public static int Check(int color)                     // Перегрузка функции проверки
        {
            if (color > 255)
                color = 255;
            if (color < 0)
                color = 0;
            return color;
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
        
        private void button3_Click(object sender, EventArgs e)          //Реализация кнопки построения гистограммы
        {
            try
            {
                Bitmap bmpGist = new Bitmap(pictureBox1.Image);
                int redColor;
                int greenColor;
                int blueColor;
                int[,] brightnesPixel = new int[bmpGist.Width, bmpGist.Height];         //Массив яркости 
                int[,] RedPixel = new int[bmpGist.Width, bmpGist.Height];               //Массив красного цвета 
                int[,] GreenPixel = new int[bmpGist.Width, bmpGist.Height];             //Массив зеленого цвета 
                int[,] BluePixel = new int[bmpGist.Width, bmpGist.Height];              //Массив синего цвета 
                int[] masY = new int[bmpGist.Width * bmpGist.Height];                   //Массив оси Y для гистограммы яркости
                int[] masX = new int[bmpGist.Width * bmpGist.Height];                   //Массив оси X 
                int[] masYRed = new int[bmpGist.Width * bmpGist.Height];                //Массив оси X для красного цвета
                int[] masYGreen = new int[bmpGist.Width * bmpGist.Height];              //Массив оси X для зеленого цвета
                int[] masYBlue = new int[bmpGist.Width * bmpGist.Height];               //Массив оси X для синего цвета


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

                        RedPixel[i, j] = redColor;                              //Заполняем массивы цветов 
                        GreenPixel[i, j] = greenColor;
                        BluePixel[i, j] = blueColor;

                        brightnesPixel[i, j] = (int)(0.299 * redColor + 0.587 * greenColor + 0.114 * blueColor);                //Расчет яркости - формула взята отсюда https://ru.wikipedia.org
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
                        masYRed[count] = RedPixel[i, j];
                        masYGreen[count] = GreenPixel[i, j];
                        masYBlue[count] = BluePixel[i, j];

                        count++;
                    }
                }

                this.chart1.Series["Series1"].Points.DataBindXY(masX, masY);    // Построение гистограммы яркости
                this.chart2.Series["Series1"].Points.DataBindXY(masX, masYRed);    // Построение гистограммы красного цвета
                this.chart3.Series["Series1"].Points.DataBindXY(masX, masYGreen);    // Построение гистограммы зеленого цвета
                this.chart4.Series["Series1"].Points.DataBindXY(masX, masYBlue);    // Построение гистограммы синего цвета
            }
            catch
            {
                MessageBox.Show("Загрузите изображение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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

        private void button15_Click(object sender, EventArgs e)         //Кнопка ввода исходных данных 
        {
            try
            {                
                fMin = Convert.ToInt32(maskedTextBox1.Text);
                fMax = Convert.ToInt32(maskedTextBox2.Text);
                gMin = Convert.ToInt32(maskedTextBox4.Text);
                gMax = Convert.ToInt32(maskedTextBox5.Text);
                alfa = Convert.ToDouble(maskedTextBox3.Text);
                power = Convert.ToDouble(maskedTextBox6.Text);
            }
            catch
            {
                MessageBox.Show("Корректно заполните поля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        

        private void button6_Click(object sender, EventArgs e)          //Реализация инверсии
        {
                       

            if (pictureBox1.Image != null)
            {
                Bitmap invPixel = new Bitmap(pictureBox1.Image);
                int redColor;
                int greenColor;
                int blueColor;

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
            if (pictureBox1.Image != null)
            {
                Bitmap invPixel = new Bitmap(pictureBox1.Image);
                int redColor;
                int greenColor;
                int blueColor;
                int binPixel;
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
                    }
                }

                pictureBox1.Image = invPixel;
            }
            else
            {
                MessageBox.Show("Сначала загрузите изображение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        

        private void button8_Click(object sender, EventArgs e)          //Расcчет сверток
        {
            try
            {
                string[,] stringConvolution = { { MT11.Text, MT12.Text, MT13.Text }, { MT21.Text, MT22.Text, MT23.Text }, { MT31.Text, MT32.Text, MT33.Text } };
                int[,] convolution = new int[3, 3];

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

                    for (int i = 0; i < conColors.Width; i++)           //Заполнение центральной части массива
                    {
                        for (int j = 0; j < conColors.Height; j++)
                        {
                            redColorPlus[i + 1, j + 1] = redColor[i, j];
                            greenColorPlus[i + 1, j + 1] = greenColor[i, j];
                            blueColorPlus[i + 1, j + 1] = blueColor[i, j];
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

                    for (int i = 1; i <= conColors.Width; i++) //Значение conColor.Width = 256, если оставить i < conColor.Width+3 то на 257 шаге ругнется что индекс за пределами  
                    {
                        for (int j = 1; j <= conColors.Height; j++) //Значение conColor.Height = 256, если оставить i < conColor.Height+2 то на 257 шаге ругнется что индекс за пределами    
                        {
                            redColorPlus[i, j] = (redColorPlus[i - 1, j - 1] * convolution[0, 0]            //Проход сверткой по красному диапазону
                                                + redColorPlus[i - 1, j] * convolution[0, 1]                // Значения в массиве convolution ничинаются с 0, тоесть от [0,0] до [2,2]
                                                + redColorPlus[i - 1, j + 1] * convolution[0, 2]
                                                + redColorPlus[i, j - 1] * convolution[1, 0]
                                                + redColorPlus[i, j] * convolution[1, 1]
                                                + redColorPlus[i, j + 1] * convolution[1, 2]
                                                + redColorPlus[i + 1, j - 1] * convolution[2, 0]
                                                + redColorPlus[i + 1, j] * convolution[2, 1]
                                                + redColorPlus[i + 1, j + 1] * convolution[2, 2]) / 9;

                            greenColorPlus[i, j] = (greenColorPlus[i - 1, j - 1] * convolution[0, 0]            //Проход сверткой по зеленому диапазону
                                               + greenColorPlus[i - 1, j] * convolution[0, 1]
                                               + greenColorPlus[i - 1, j + 1] * convolution[0, 2]
                                               + greenColorPlus[i, j - 1] * convolution[1, 0]
                                               + greenColorPlus[i, j] * convolution[1, 1]
                                               + greenColorPlus[i, j + 1] * convolution[1, 2]
                                               + greenColorPlus[i + 1, j - 1] * convolution[2, 0]
                                               + greenColorPlus[i + 1, j] * convolution[2, 1]
                                               + greenColorPlus[i + 1, j + 1] * convolution[2, 2]) / 9;

                            blueColorPlus[i, j] = (blueColorPlus[i - 1, j - 1] * convolution[0, 0]          //Проход сверткой по синему диапазону
                                               + blueColorPlus[i - 1, j] * convolution[0, 1]
                                               + blueColorPlus[i - 1, j + 1] * convolution[0, 2]
                                               + blueColorPlus[i, j - 1] * convolution[1, 0]
                                               + blueColorPlus[i, j] * convolution[1, 1]
                                               + blueColorPlus[i, j + 1] * convolution[1, 2]
                                               + blueColorPlus[i + 1, j - 1] * convolution[2, 0]
                                               + blueColorPlus[i + 1, j] * convolution[2, 1]
                                               + blueColorPlus[i + 1, j + 1] * convolution[2, 2]) / 9;

                            redColorPlus[i, j] = Check(redColorPlus[i, j]);                                 //Проверка диапазонов
                            greenColorPlus[i, j] = Check(greenColorPlus[i, j]);
                            blueColorPlus[i, j] = Check(blueColorPlus[i, j]);
                        }
                    }

                    for (int i = 0; i < conColors.Width; i++) //Значение conColor.Width = 256, если оставить i < conColor.Width+2 то на 257 шаге ругнется что индекс за пределами          
                    {
                        for (int j = 0; j < conColors.Height; j++) //Значение conColor.Height = 256, если оставить i < conColor.Height+2 то на 257 шаге ругнется что индекс за пределами          
                        {
                            redColor[i, j] = redColorPlus[i + 1, j + 1];            //Возврат значений в массивы
                            greenColor[i, j] = greenColorPlus[i + 1, j + 1];
                            blueColor[i, j] = blueColorPlus[i + 1, j + 1];
                        }
                    }

                    for (int i = 0; i < conColors.Width; i++)                           //Возврат цветов в экземпляр класса Bitmap
                    {
                        for (int j = 0; j < conColors.Height; j++)
                        {
                            conColors.SetPixel(i, j, Color.FromArgb(255, redColor[i, j], greenColor[i, j], blueColor[i, j]));
                        }
                    }

                    pictureBox1.Image = conColors;
                }
                else
                {
                    MessageBox.Show("Сначала загрузите изображение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Не заполнены поля свертки или не загружено изображение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void button9_Click(object sender, EventArgs e)              //Рассчет равномерного распределения(Even distribution)
        {
            try
            {
                Bitmap Freq = new Bitmap(pictureBox1.Image);

                int[,] redArray = new int[Freq.Height, Freq.Width];
                int[,] greenArray = new int[Freq.Height, Freq.Width];
                int[,] blueArray = new int[Freq.Height, Freq.Width];
                                
                for (int i = 0; i < Freq.Height; i++)
                {
                    for (int j = 0; j < Freq.Width; j++)
                    {
                        redArray[i, j] = Freq.GetPixel(i, j).R;
                        greenArray[i, j] = Freq.GetPixel(i, j).G;
                        blueArray[i, j] = Freq.GetPixel(i, j).B;

                        redArray[i, j] = (gMax - gMin) * Convert.ToInt32(Frequency(redArray, redArray[i, j])) * gMin;
                        greenArray[i, j] = (gMax - gMin) * Convert.ToInt32(Frequency(greenArray, greenArray[i, j])) * gMin;
                        blueArray[i, j] = (gMax - gMin) * Convert.ToInt32(Frequency(blueArray, blueArray[i, j])) * gMin;

                        redArray[i, j] = Check(redArray[i, j]);
                        greenArray[i, j] = Check(greenArray[i, j]);
                        blueArray[i, j] = Check(blueArray[i, j]);

                        Freq.SetPixel(i, j, Color.FromArgb(255, redArray[i, j], greenArray[i, j], blueArray[i, j]));                        
                    }
                }
                pictureBox1.Image = Freq;
            }
            catch
            {
                MessageBox.Show("Заполните поля и нажмите ОК, загрузите изображение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
               

        private void button10_Click(object sender, EventArgs e)             //Экспоненциальное распределение
        {
            try
            {
                Bitmap Freq = new Bitmap(pictureBox1.Image);

                int[,] redArray = new int[Freq.Height, Freq.Width];
                int[,] greenArray = new int[Freq.Height, Freq.Width];
                int[,] blueArray = new int[Freq.Height, Freq.Width];

                for (int i = 0; i < Freq.Height; i++)
                {
                    for (int j = 0; j < Freq.Width; j++)
                    {
                        redArray[i, j] = Freq.GetPixel(i, j).R;
                        greenArray[i, j] = Freq.GetPixel(i, j).G;
                        blueArray[i, j] = Freq.GetPixel(i, j).B;

                        redArray[i, j] = Convert.ToInt32(gMin - 1 / alfa * Math.Log10(1 - Frequency(redArray, redArray[i, j])));
                        greenArray[i, j] = Convert.ToInt32(gMin - 1 / alfa * Math.Log10(1 - Frequency(greenArray, greenArray[i, j])));
                        blueArray[i, j] = Convert.ToInt32(gMin - 1 / alfa * Math.Log10(1 - Frequency(blueArray, blueArray[i, j])));

                        redArray[i, j] = Check(redArray[i, j]);
                        greenArray[i, j] = Check(greenArray[i, j]);
                        blueArray[i, j] = Check(blueArray[i, j]);

                        Freq.SetPixel(i, j, Color.FromArgb(255, redArray[i, j], greenArray[i, j], blueArray[i, j]));
                    }
                }
                pictureBox1.Image = Freq;
            }
            catch
            {
                MessageBox.Show("Заполните поля и нажмите ОК, загрузите изображение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button11_Click(object sender, EventArgs e)                 // Распределение Рэлея
        {
            try
            {
                Bitmap Freq = new Bitmap(pictureBox1.Image);

                int[,] redArray = new int[Freq.Height, Freq.Width];
                int[,] greenArray = new int[Freq.Height, Freq.Width];
                int[,] blueArray = new int[Freq.Height, Freq.Width];

                for (int i = 0; i < Freq.Height; i++)
                {
                    for (int j = 0; j < Freq.Width; j++)
                    {
                        redArray[i, j] = Freq.GetPixel(i, j).R;
                        greenArray[i, j] = Freq.GetPixel(i, j).G;
                        blueArray[i, j] = Freq.GetPixel(i, j).B;

                        redArray[i, j] = Convert.ToInt32(gMin + Math.Pow(2 * alfa * alfa * Math.Log10(1 / (1 - Frequency(redArray, redArray[i, j]))), 0.5));
                        greenArray[i, j] = Convert.ToInt32(gMin + Math.Pow(2 * alfa * alfa * Math.Log10(1 / (1 - Frequency(greenArray, greenArray[i, j]))), 0.5));
                        blueArray[i, j] = Convert.ToInt32(gMin + Math.Pow(2 * alfa * alfa * Math.Log10(1 / (1 - Frequency(blueArray, blueArray[i, j]))), 0.5));

                        redArray[i, j] = Check(redArray[i, j]);
                        greenArray[i, j] = Check(greenArray[i, j]);
                        blueArray[i, j] = Check(blueArray[i, j]);

                        Freq.SetPixel(i, j, Color.FromArgb(255, redArray[i, j], greenArray[i, j], blueArray[i, j]));
                    }
                }
                pictureBox1.Image = Freq;
            }
            catch
            {
                MessageBox.Show("Заполните поля и нажмите ОК, загрузите изображение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button14_Click(object sender, EventArgs e)             // Распределение степени 2/3
        {
            try
            {
                Bitmap Freq = new Bitmap(pictureBox1.Image);

                int[,] redArray = new int[Freq.Height, Freq.Width];
                int[,] greenArray = new int[Freq.Height, Freq.Width];
                int[,] blueArray = new int[Freq.Height, Freq.Width];

                for (int i = 0; i < Freq.Height; i++)
                {
                    for (int j = 0; j < Freq.Width; j++)
                    {
                        redArray[i, j] = Freq.GetPixel(i, j).R;
                        greenArray[i, j] = Freq.GetPixel(i, j).G;
                        blueArray[i, j] = Freq.GetPixel(i, j).B;

                        redArray[i, j] = Convert.ToInt32(Math.Pow(Math.Pow(gMax, 1 / 3) - Math.Pow(gMin, 1 /3) * Frequency(redArray, redArray[i, j]) + Math.Pow(gMin, 1 / 3), 3));
                        greenArray[i, j] = Convert.ToInt32(Math.Pow(Math.Pow(gMax, 1 / 3) - Math.Pow(gMin, 1 / 3) * Frequency(greenArray, greenArray[i, j]) + Math.Pow(gMin, 1 / 3), 3));
                        blueArray[i, j] = Convert.ToInt32(Math.Pow(Math.Pow(gMax, 1 / 3) - Math.Pow(gMin, 1 / 3) * Frequency(blueArray, blueArray[i, j]) + Math.Pow(gMin, 1 / 3), 3));

                        redArray[i, j] = Check(redArray[i, j]);
                        greenArray[i, j] = Check(greenArray[i, j]);
                        blueArray[i, j] = Check(blueArray[i, j]);

                        Freq.SetPixel(i, j, Color.FromArgb(255, redArray[i, j], greenArray[i, j], blueArray[i, j]));
                    }
                }
                pictureBox1.Image = Freq;
            }
            catch
            {
                MessageBox.Show("Заполните поля и нажмите ОК, загрузите изображение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button13_Click(object sender, EventArgs e)             // Гиперболическое распределение
        {
            try
            {
                Bitmap Freq = new Bitmap(pictureBox1.Image);

                int[,] redArray = new int[Freq.Height, Freq.Width];
                int[,] greenArray = new int[Freq.Height, Freq.Width];
                int[,] blueArray = new int[Freq.Height, Freq.Width];

                for (int i = 0; i < Freq.Height; i++)
                {
                    for (int j = 0; j < Freq.Width; j++)
                    {
                        redArray[i, j] = Freq.GetPixel(i, j).R;
                        greenArray[i, j] = Freq.GetPixel(i, j).G;
                        blueArray[i, j] = Freq.GetPixel(i, j).B;

                        redArray[i, j] = Convert.ToInt32(gMin * Math.Pow(gMax / gMin, Frequency(redArray, redArray[i, j])));
                        greenArray[i, j] = Convert.ToInt32(gMin * Math.Pow(gMax / gMin, Frequency(redArray, redArray[i, j])));
                        blueArray[i, j] = Convert.ToInt32(gMin * Math.Pow(gMax / gMin, Frequency(redArray, redArray[i, j])));

                        redArray[i, j] = Check(redArray[i, j]);
                        greenArray[i, j] = Check(greenArray[i, j]);
                        blueArray[i, j] = Check(blueArray[i, j]);

                        Freq.SetPixel(i, j, Color.FromArgb(255, redArray[i, j], greenArray[i, j], blueArray[i, j]));
                    }
                }
                pictureBox1.Image = Freq;
            }
            catch
            {
                MessageBox.Show("Заполните поля и нажмите ОК, загрузите изображение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button12_Click(object sender, EventArgs e)                 //Степенная интенсификация
        {
            try
            {
                Bitmap Freq = new Bitmap(pictureBox1.Image);

                int[,] redArray = new int[Freq.Height, Freq.Width];
                int[,] greenArray = new int[Freq.Height, Freq.Width];
                int[,] blueArray = new int[Freq.Height, Freq.Width];

                for (int i = 0; i < Freq.Height; i++)
                {
                    for (int j = 0; j < Freq.Width; j++)
                    {
                        redArray[i, j] = Freq.GetPixel(i, j).R;
                        greenArray[i, j] = Freq.GetPixel(i, j).G;
                        blueArray[i, j] = Freq.GetPixel(i, j).B;

                        redArray[i, j] = Convert.ToInt32((((gMax - gMin) * Frequency(redArray, redArray[i, j])) / Frequency(redArray, redArray[i, j])) + gMin);
                        greenArray[i, j] = Convert.ToInt32((((gMax - gMin) * Frequency(greenArray, greenArray[i, j])) / Frequency(greenArray, greenArray[i, j])) + gMin);
                        blueArray[i, j] = Convert.ToInt32((((gMax - gMin) * Frequency(blueArray, blueArray[i, j])) / Frequency(blueArray, blueArray[i, j])) + gMin);

                        redArray[i, j] = Check(redArray[i, j]);
                        greenArray[i, j] = Check(greenArray[i, j]);
                        blueArray[i, j] = Check(blueArray[i, j]);

                        Freq.SetPixel(i, j, Color.FromArgb(255, redArray[i, j], greenArray[i, j], blueArray[i, j]));
                    }
                }
                pictureBox1.Image = Freq;
            }
            catch
            {
                MessageBox.Show("Заполните поля и нажмите ОК, загрузите изображение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
//Не забудь обработать исключение при пустых масках
//Не забудь делать проверку диапазонов
