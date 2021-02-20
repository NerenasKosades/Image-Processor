
namespace Image_Processor
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.MT22 = new System.Windows.Forms.MaskedTextBox();
            this.MT32 = new System.Windows.Forms.MaskedTextBox();
            this.MT13 = new System.Windows.Forms.MaskedTextBox();
            this.MT23 = new System.Windows.Forms.MaskedTextBox();
            this.MT33 = new System.Windows.Forms.MaskedTextBox();
            this.MT12 = new System.Windows.Forms.MaskedTextBox();
            this.MT31 = new System.Windows.Forms.MaskedTextBox();
            this.MT21 = new System.Windows.Forms.MaskedTextBox();
            this.MT11 = new System.Windows.Forms.MaskedTextBox();
            this.button8 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Image_Processor.Properties.Resources.no_image_icon_1;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(19, 25);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(410, 403);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(19, 434);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(198, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Загрузить изображение";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(223, 434);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(207, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Сохранить изображение";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // chart1
            // 
            this.chart1.BorderlineColor = System.Drawing.Color.Black;
            this.chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(435, 25);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(426, 403);
            this.chart1.TabIndex = 3;
            this.chart1.Text = "chart1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(149, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Просмотр изображения";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(549, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(190, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Гистограмма яркости изображения";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(436, 434);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(425, 68);
            this.button3.TabIndex = 6;
            this.button3.Text = "Построить гистограмму";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(19, 463);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(198, 39);
            this.button4.TabIndex = 7;
            this.button4.Text = "Сброс на начальное изображение";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(223, 463);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(206, 39);
            this.button5.TabIndex = 8;
            this.button5.Text = "Принудительная коррекция цветового диапазона";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button7);
            this.groupBox1.Controls.Add(this.button6);
            this.groupBox1.Location = new System.Drawing.Point(867, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(403, 60);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Обработка эффектов";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(135, 22);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(117, 22);
            this.button7.TabIndex = 1;
            this.button7.Text = "Бинаризация";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(12, 22);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(117, 22);
            this.button6.TabIndex = 0;
            this.button6.Text = "Инверсия";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button8);
            this.groupBox2.Controls.Add(this.MT22);
            this.groupBox2.Controls.Add(this.MT32);
            this.groupBox2.Controls.Add(this.MT13);
            this.groupBox2.Controls.Add(this.MT23);
            this.groupBox2.Controls.Add(this.MT33);
            this.groupBox2.Controls.Add(this.MT12);
            this.groupBox2.Controls.Add(this.MT31);
            this.groupBox2.Controls.Add(this.MT21);
            this.groupBox2.Controls.Add(this.MT11);
            this.groupBox2.Location = new System.Drawing.Point(867, 91);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(402, 105);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Применение сверток";
            // 
            // MT22
            // 
            this.MT22.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.MT22.Location = new System.Drawing.Point(47, 45);
            this.MT22.Mask = "0";
            this.MT22.Name = "MT22";
            this.MT22.Size = new System.Drawing.Size(29, 20);
            this.MT22.TabIndex = 8;
            // 
            // MT32
            // 
            this.MT32.Location = new System.Drawing.Point(47, 71);
            this.MT32.Mask = "0";
            this.MT32.Name = "MT32";
            this.MT32.Size = new System.Drawing.Size(29, 20);
            this.MT32.TabIndex = 7;
            // 
            // MT13
            // 
            this.MT13.Location = new System.Drawing.Point(82, 19);
            this.MT13.Mask = "0";
            this.MT13.Name = "MT13";
            this.MT13.Size = new System.Drawing.Size(29, 20);
            this.MT13.TabIndex = 6;
            // 
            // MT23
            // 
            this.MT23.Location = new System.Drawing.Point(82, 45);
            this.MT23.Mask = "0";
            this.MT23.Name = "MT23";
            this.MT23.Size = new System.Drawing.Size(29, 20);
            this.MT23.TabIndex = 5;
            // 
            // MT33
            // 
            this.MT33.Location = new System.Drawing.Point(82, 71);
            this.MT33.Mask = "0";
            this.MT33.Name = "MT33";
            this.MT33.Size = new System.Drawing.Size(29, 20);
            this.MT33.TabIndex = 4;
            // 
            // MT12
            // 
            this.MT12.Location = new System.Drawing.Point(47, 19);
            this.MT12.Mask = "0";
            this.MT12.Name = "MT12";
            this.MT12.Size = new System.Drawing.Size(29, 20);
            this.MT12.TabIndex = 3;
            // 
            // MT31
            // 
            this.MT31.Location = new System.Drawing.Point(12, 71);
            this.MT31.Mask = "0";
            this.MT31.Name = "MT31";
            this.MT31.Size = new System.Drawing.Size(29, 20);
            this.MT31.TabIndex = 2;
            // 
            // MT21
            // 
            this.MT21.Location = new System.Drawing.Point(12, 45);
            this.MT21.Mask = "0";
            this.MT21.Name = "MT21";
            this.MT21.Size = new System.Drawing.Size(29, 20);
            this.MT21.TabIndex = 1;
            // 
            // MT11
            // 
            this.MT11.Location = new System.Drawing.Point(12, 19);
            this.MT11.Mask = "0";
            this.MT11.Name = "MT11";
            this.MT11.Size = new System.Drawing.Size(29, 20);
            this.MT11.TabIndex = 0;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(135, 19);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(99, 72);
            this.button8.TabIndex = 9;
            this.button8.Text = "Рассчитать";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Image_Processor.Properties.Resources.Metal;
            this.ClientSize = new System.Drawing.Size(1282, 549);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Image Processor";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.MaskedTextBox MT11;
        private System.Windows.Forms.MaskedTextBox MT22;
        private System.Windows.Forms.MaskedTextBox MT32;
        private System.Windows.Forms.MaskedTextBox MT13;
        private System.Windows.Forms.MaskedTextBox MT23;
        private System.Windows.Forms.MaskedTextBox MT33;
        private System.Windows.Forms.MaskedTextBox MT12;
        private System.Windows.Forms.MaskedTextBox MT31;
        private System.Windows.Forms.MaskedTextBox MT21;
        private System.Windows.Forms.Button button8;
    }
}

