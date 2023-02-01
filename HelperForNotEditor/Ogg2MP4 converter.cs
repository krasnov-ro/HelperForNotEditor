using NReco.VideoConverter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Web;

namespace HelperForNotEditor
{
    public partial class Ogg2MP4_converter : Form
    {
        private string projectDirectory = string.Empty;
        public Ogg2MP4_converter()
        {
            InitializeComponent();
        }

        public void sendFolder( string folder)
        {
            projectDirectory = folder;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "projectDirectory\\";
                openFileDialog.Filter = "ogg files (*.ogg)|*.ogg";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var ffMpeg = new FFMpegConverter();
                    ffMpeg.ConvertMedia(openFileDialog.FileName, "video.mp4", Format.mp4);
                }
            }
        }
    }
}
