using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NameSimpleSudoku;
using NameMatchTwo;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace NameStartScreen
{
    public partial class StartScreen : Form
    {
        public static ScoreList scores;
        public ErrorProvider errorProvider1;
        public StartScreen()
        {
            
            InitializeComponent();
            BackColor = Color.FromArgb(255, 230, 128);
            OpenScores();
            errorProvider1 = new ErrorProvider();
            this.AutoValidate = AutoValidate.Disable;
        }
        private void OpenScores() {
            FileStream myStream;
            try
            {
                if (File.Exists("../../SavedScores.savedS") && 
                    (myStream = File.Open("../../SavedScores.savedS", FileMode.Open)) != null)
                {
                    using (myStream)
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        scores = bf.Deserialize(myStream) as ScoreList;
                    }
                }
                else if (!File.Exists("../../SavedScores.savedS")) {
                    scores = new ScoreList();
                }
            }
          
            catch (Exception ex) {
                MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
            }
        
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren()) return;
            MatchTwo m = new MatchTwo((listBox1.SelectedIndex + 1) * 2);
            m.ShowDialog();
            m.timer1.Stop();
            if (m.Score == null) m.Score = "Unfinished";
            scores.Add(new Score(KojaIgra.MatchTwo, textBox1.Text.ToString(), m.Score));
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
            g.DrawImage(NameMatchTwo.Properties.Resources.back, rec2);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rec2 = new Rectangle(0, 0, panel2.Width, panel2.Height);
            e.Graphics.DrawImage(NameSimpleSudoku.Properties.Resources.title, rec2);
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren()) return;
            SimpleSudoku s = new SimpleSudoku();
            s.ShowDialog();
            s.timer.Stop();
            if (s.Score == null) s.Score = "Unfinished";
            scores.Add(new Score(KojaIgra.Sudoku,textBox1.Text.ToString(),s.Score));
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (textBox1.Text.Trim().Length == 0)
            {
                e.Cancel = true;

                errorProvider1.SetError(textBox1, "Внесете име!");
            }
            else
            {
                errorProvider1.SetError(textBox1, null);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ScoreScreen s = new ScoreScreen(KojaIgra.MatchTwo);
            s.ShowDialog();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            ScoreScreen s = new ScoreScreen(KojaIgra.Sudoku);
            s.ShowDialog();
        }
        private void SaveScores() {
            FileStream myStream;
            try
            {
                if ((myStream = File.Open("../../SavedScores.savedS",FileMode.Create)) != null)
                {
                    using (myStream)
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        bf.Serialize(myStream, scores);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
            }
        
        }

        private void StartScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveScores();
        }
    }
}
