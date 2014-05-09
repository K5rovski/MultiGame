using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NameStartScreen
{
    public partial class ScoreScreen : Form
    {
        public KojaIgra igra;
        public ScoreScreen(KojaIgra k)
        {
            InitializeComponent();
           igra= k;
           listBox1.DataSource=StartScreen.scores.Select(igra);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
