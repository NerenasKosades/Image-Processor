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
            Color pixelColor;

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

                        pixelColor = Color.FromArgb(255, redColor, greenColor, blueColor);
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
    }
}
