using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab1
{


    public partial class Form1 : Form
    {

        private Bitmap originalImage;
        private Bitmap grayImage;
        private Bitmap binaryImage;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void LoadImageFromFolder(string imagePath)
        {
            try
            {
                originalImage = new Bitmap(imagePath);
                pictureBox1.Image = originalImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке изображения: {ex.Message}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.gif";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string imagePath = openFileDialog.FileName;
                    originalImage = new Bitmap(imagePath);
                    pictureBox1.Image = originalImage;
                }

            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            // Конвертация в оттенки серого
            grayImage = ConvertToGrayScale(originalImage);
            pictureBox1.Image = grayImage;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                // Бинаризация изображения по порогу
                if (Convert.ToInt32(textBox2.Text) > 255)
                    MessageBox.Show("Введите чило =<255");
                else
                {
                    Bitmap binaryImage = Binarize((Bitmap)pictureBox1.Image, Convert.ToInt32(textBox2.Text));
                    pictureBox1.Image = binaryImage;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (pictureBox1.Image != null)
            {
                // Изменение яркости изображения на
                if (Convert.ToInt32(textBox1.Text) > 100)
                    MessageBox.Show("Введите чило =<100");
                else
                {
                    Bitmap brightImage = AdjustBrightness((Bitmap)pictureBox1.Image, Convert.ToInt32(textBox1.Text));
                    pictureBox1.Image = brightImage;
                }
            }
        }

       

        ///////////////////////////////////////////////////////////////

        private Bitmap _originalImage;

        // Функция для преобразования изображения в оттенки серого
        private Bitmap ConvertToGrayScale(Bitmap inputImage)
        {
            Bitmap grayBitmap = new Bitmap(inputImage.Width, inputImage.Height);

            for (int y = 0; y < inputImage.Height; y++)
            {
                for (int x = 0; x < inputImage.Width; x++)
                {
                    Color originalColor = inputImage.GetPixel(x, y);
                    byte grayValue = (byte)(0.3 * originalColor.R + 0.59 * originalColor.G + 0.11 * originalColor.B);
                    grayBitmap.SetPixel(x, y, Color.FromArgb(grayValue, grayValue, grayValue));
                }
            }

            return grayBitmap;
        }

        public static Bitmap Binarize(Bitmap inputImage, int threshold)
        {

            Bitmap binaryImage = new Bitmap(inputImage.Width, inputImage.Height);

            for (int x = 0; x < inputImage.Width; ++x)
            {
                for (int y = 0; y < inputImage.Height; ++y)
                {
                    Color pixelColor = inputImage.GetPixel(x, y);

                    // Среднее значение RGB
                    int gray = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;

                    // Бинаризация порогом
                    int binaryValue = gray > threshold ? 255 : 0;

                    // Замена цвета пикселя
                    binaryImage.SetPixel(x, y, Color.FromArgb(binaryValue, binaryValue, binaryValue));
                }
            }

            return binaryImage;

        }

        public static Bitmap AdjustBrightness(Bitmap inputImage, int brightnessLevel)
        {
            Bitmap adjustedImage = new Bitmap(inputImage.Width, inputImage.Height);

            for (int x = 0; x < inputImage.Width; ++x)
            {
                for (int y = 0; y < inputImage.Height; ++y)
                {
                    Color pixelColor = inputImage.GetPixel(x, y);

                    // Изменение каждого компонента RGB
                    int red = (int)(pixelColor.R + (brightnessLevel * 2.55));
                    int green = (int)(pixelColor.G + (brightnessLevel * 2.55));
                    int blue = (int)(pixelColor.B + (brightnessLevel * 2.55));

                    // Ограничение значений RGB в диапазоне 0-255
                    red = Math.Min(255, Math.Max(0, red));
                    green = Math.Min(255, Math.Max(0, green));
                    blue = Math.Min(255, Math.Max(0, blue));

                    // Замена цвета пикселя
                    adjustedImage.SetPixel(x, y, Color.FromArgb(red, green, blue));
                }
            }

            return adjustedImage;
        }

       

        

        
        


    }
}
