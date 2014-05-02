using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace opentkForms
{
    public partial class StartScreen : Form
    {
        public StartScreen()
        {
            InitializeComponent();
            BackColor = Color.FromArgb(255, 230, 128);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MatchTwo m = new MatchTwo((listBox1.SelectedIndex + 1) * 2);
            m.ShowDialog();
            m.timer1.Stop();
        }

      

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                button1.Enabled = true;
            }
            else button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void StartScreen_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = Graphics.FromHwnd(panel1.Handle);


            Rectangle rec2 = new Rectangle(0, 0, panel1.Width, panel1.Height);
            g.DrawImage(Properties.Resources.back, rec2);
        }
    }
}
