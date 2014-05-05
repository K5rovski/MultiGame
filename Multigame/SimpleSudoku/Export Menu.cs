using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Text;

namespace SimpleSudoku
{
    public partial class Export_Menu : Form
    {
        public Export_Menu()
        {
            InitializeComponent();
            this.BackColor= Color.FromArgb(17, 17, 17);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog so the user can save the Image
            // assigned to Button2.
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Png Image|*.png|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();
            Random gen = new Random();
            SudokuTabla sud;
            int width = 320;
            if (listBox1.SelectedIndex > 0) width = 1020;
            Image img= new Bitmap(1020+20,520*(listBox1.SelectedIndex+1)/2);
            Graphics g = Graphics.FromImage(img);
                g.Clear(Color.White);
                g.TextRenderingHint = TextRenderingHint.AntiAlias;
                g.TextContrast = 8;
            List<Image> lista = new List<Image>();
            for (int i = 0; i < listBox1.SelectedIndex + 1; i++) {
                 sud = new SudokuTabla(gen);
                sud.polni();
                sud.prazni();
                Image im= new Bitmap(500, 500);
                SimpleSudoku.draw(sud, im, Color.Black, true);
                g.DrawImageUnscaled(im, 500 * (i % 2)+20, 500 * (i / 2)+20);

            }
         

                // If the file name is not an empty string open it for saving.
                if (saveFileDialog1.FileName != "")
                {
                    // Saves the Image via a FileStream created by the OpenFile method.
                    System.IO.FileStream fs =
                       (System.IO.FileStream)saveFileDialog1.OpenFile();
                    // Saves the Image in the appropriate ImageFormat based upon the
                    // File type selected in the dialog box.
                    // NOTE that the FilterIndex property is one-based.
                    switch (saveFileDialog1.FilterIndex)
                    {
                        case 1:
                            img.Save(fs,
                             System.Drawing.Imaging.ImageFormat.Png);
                            break;

                        case 2:
                            img.Save(fs,
                               System.Drawing.Imaging.ImageFormat.Bmp);
                            break;

                        case 3:
                            img.Save(fs,
                               System.Drawing.Imaging.ImageFormat.Gif);
                            break;
                    }

                    fs.Close();
                }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1) {
                button1.Enabled = true;
            }
        }
    }
}
