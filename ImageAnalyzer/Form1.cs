using System;
using System.Windows.Forms;
using ImageAnalyzer.SpecialClasses;

namespace ImageAnalyzer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void MainImgButton_Click(object sender, EventArgs e)
        {
            ImageHandler.OpenImages(ImageHandler.ImageType.MAIN, mainImg);
        }

        private void LeftImgButton_Click(object sender, EventArgs e)
        {
            ImageHandler.OpenImages(ImageHandler.ImageType.LEFT, leftImg);
        }

        private void TopImgButton_Click(object sender, EventArgs e)
        {
            ImageHandler.OpenImages(ImageHandler.ImageType.TOP, topImg);
        }

        private void FindButton_Click(object sender, EventArgs e)
        {
            ImageHandler.BuildAnObject(fileName.Text);
        }
    }
}
