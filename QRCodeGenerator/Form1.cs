using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QRCodeGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Please enter some text first.");
                return;
            }

            var qrCode = GenerateQRCode(textBox1.Text,300,300);
            pictureBox1.Image = qrCode;

        }
        static Bitmap GenerateQRCode(string inputText, int width, int height)
        {
            var barcodeWriter = new ZXing.BarcodeWriter
            {
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions
                {
                    Width = width,
                    Height = height
                }
            };


            var qrImage = barcodeWriter.Write(inputText);
            return qrImage;
        }
        private void SaveQRCode()
        {
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PNG Files (*.png)|*.png";
                saveFileDialog.FileName = "QRCode.png";


                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image.Save(saveFileDialog.FileName, ImageFormat.Png);
                    MessageBox.Show("QR Code saved successfully!");
                }
            };
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Please generate a QR code first.");
                return;
            }

            SaveQRCode();
        }
    }
}
