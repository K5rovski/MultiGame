using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NameSimpleSudoku
{
    public partial class SimpleSudoku : Form
    {
        System.Drawing.Graphics GraphicsObject;
        
        public int kliknatoI;
        public int kliknatoJ;
        public int SkliknatoI;
        public int SkliknatoJ;
        public int SaveY;
        public int SaveX;
        public int KadeX;
        public int KadeY;
        public Image pick;
        private Bitmap GraphicsImage;
        public SudokuTabla Stabla;
        Random gen;
       public int desetinaX;
        public int pocX;
        public int desetinaY;
        public int pocY;
        public Rectangle PicRect;
        public Image logo;
        public Image back;
        public Timer timer;
        public bool Stopped = false;
        public SimpleSudoku()
        {
           
            InitializeComponent();



            logo = Properties.Resources.title;
            pick = Properties.Resources.picker2;
            timer = new Timer();
            timer.Interval = 1000;
            timer.Start();
            timer.Tick += new EventHandler(timer_Tick);
            this.BackColor = panel1.BackColor = Color.FromArgb(17,17,17);
            back = Properties.Resources.bac2;
           // panel1.BackColor = Color.Transparent;
            //this.BackColor = panel1.BackColor = Color.Wheat;
    
            GraphicsImage = new Bitmap(panel1.Width, panel1.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            //Fills the image we just created with white
            Stabla = new SudokuTabla();
            Stabla.polni();
            Stabla.prazni();
         //   DoubleBuffered = true;
            Graphics.FromImage(GraphicsImage).Clear(this.BackColor);
            panel1.BringToFront();
           
            GraphicsObject = Graphics.FromImage(GraphicsImage);
            double faktor = 0.15 * (3.0 / 4);
            PicRect = new Rectangle(0, 0, (int)(panel1.Width * 0.15), (int)(panel1.Height * faktor));

            kliknatoI = kliknatoJ = 0;
            KadeX = 0;
            KadeY = 0;
            RemainingLabel.ForeColor =TimeLabel.ForeColor = Color.Beige;
            
           gen = new Random();
            desetinaY = panel1.Width / 13;
      pocY = (int)(2.5 * desetinaY);

      desetinaX = panel1.Width / 13;
      pocX = 2 * desetinaX;
      RemainingLabel.Location = new Point(desetinaX, (int)(1.8 * desetinaY));
            //    GraphicsObject.PageUnit = GraphicsUnit.Millimeter;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            Brojac();
        }
        private void Brojac()
        {
            string[] dva = TimeLabel.Text.ToString().Split(':');
            int min = 0, sek = 0, vkupno;
            int.TryParse(dva[1], out min);
            int.TryParse(dva[2], out sek);
            vkupno = sek + min * 60;
            vkupno++;
            min = vkupno / 60;
            sek = vkupno % 60;
            TimeLabel.Text = string.Format("Time: {0:00}:{1:00}", min.ToString(), sek.ToString());

        }
        private void Form2_Load(object sender, EventArgs e)
        {


        }
        public void Redraw(bool Zaprintanje=false)
        {
            RedrawSquares(0, 9, 0, 9);
        }
        public static Image draw(SudokuTabla sud, Image im, Color Fboja,bool ZaPrintanje=false) {
            Color Bboja;
            Graphics g = Graphics.FromImage(im);
            int devetina = (im.Width / 9) ;
            Font f = new Font(FontFamily.GenericMonospace,12);
            Font fBold = new Font(FontFamily.GenericSerif, 14, FontStyle.Underline);
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if ((j) / 3 == 0)
                        Bboja = Color.FromArgb(10, 20 + 25 * ((j) / 3), 20 + 20 * ((i) / 3));
                    else if ((j) / 3 == 1)
                        Bboja = Color.FromArgb(25 * ((j) / 3), 10, 20 * ((i) / 3));
                    else
                        Bboja = Color.FromArgb(25 * ((j) / 3), 20 * ((i) / 3), 10);

                    if (!ZaPrintanje)
                    {

                        if (sud.Workingtabla[i][j] == sud.tablata[i][j] || sud.Workingtabla[i][j] == 0)
                            g.FillRectangle(new SolidBrush(Bboja), (devetina * j), (devetina * i), devetina - 2, devetina - 2);
                        else
                            g.FillRectangle(new SolidBrush(Color.Red), (devetina * j), (devetina * i), devetina - 2, devetina - 2);
                        g.DrawRectangle(new Pen(Fboja), (devetina * j), (devetina * i), devetina - 2, devetina - 2);
                    }
                    if (ZaPrintanje) 
                    g.DrawRectangle(new Pen(Bboja), (devetina * j), (devetina * i), devetina - 2, devetina - 2);

                    if (sud.Workingtabla[i][j] != 0 && sud.IspraznetaTabla[i][j] != 0)
                        g.DrawString((sud.Workingtabla[i][j]).ToString(), fBold, new SolidBrush(Fboja), devetina * j + 10, devetina * i + 10);
                    else if (sud.Workingtabla[i][j] != 0)
                        g.DrawString((sud.Workingtabla[i][j]).ToString(), f, new SolidBrush(Fboja), devetina * j + 10, devetina * i + 10);
                    
                }
            }


            return im;
        }
        private void Form2_Shown(object sender, EventArgs e)
        {
       //     Redraw();
         

        }

   


 




  
      
        public void RedrawSquare(int i, int j)
        {
            RedrawSquares(i, i + 1, j, j + 1);
        }
      
        public void RedrawSquares(int i1, int i2, int j1, int j2)
        {
            Color red;
            Font f = new Font(Font.FontFamily, 10);
            Font fBold = new Font(Font.FontFamily, 12, FontStyle.Underline);
            if (i2 > 9) i2 = 9;
            if (j2 > 9) j2 = 9;
           
            for (int i = i1; i < i2; i++)
            {
                for (int j = j1; j < j2; j++)
                {
                    if ((j) / 3 == 0)
                        red = Color.FromArgb(10, 20 + 25 * ((j) / 3), 20 + 20 * ((i) / 3));
                    else if ((j) / 3 == 1)
                        red = Color.FromArgb(25 * ((j) / 3), 10, 20 * ((i) / 3));
                    else
                        red = Color.FromArgb(25 * ((j) / 3), 20 * ((i) / 3), 10);
                    if (Stabla.Workingtabla[i][j]==Stabla.tablata[i][j] || Stabla.Workingtabla[i][j]==0)
                    GraphicsObject.FillRectangle(new SolidBrush(red), (desetinaX * j +pocX ), (desetinaY * i +pocY),desetinaX-2, desetinaY-2);
                    else
                        GraphicsObject.FillRectangle(new SolidBrush(Color.Red), (desetinaX * j + pocX), (desetinaY * i + pocY), desetinaX - 2, desetinaY - 2);

                    GraphicsObject.DrawRectangle(new Pen(Color.Wheat), (desetinaX * j + pocX), (desetinaY * i + pocY), desetinaX - 2, desetinaY - 2);

                    if (Stabla.Workingtabla[i][j] != 0 && Stabla.IspraznetaTabla[i][j]!=0)  
                        GraphicsObject.DrawString((Stabla.Workingtabla[i][j]).ToString(), fBold, Brushes.Wheat, desetinaX * j + 10 +pocX, desetinaY * i + 10 + pocY);
                    else if (Stabla.Workingtabla[i][j]!=0)
                        GraphicsObject.DrawString((Stabla.Workingtabla[i][j]).ToString(), f, Brushes.Beige, desetinaX * j + 10 + pocX, desetinaY * i + 10 + pocY);

                }
            }
            
           
            Rectangle rect = new Rectangle(0, 0, panel1.Width, panel1.Height);
            if (Stabla.Remaining == 0)
                kraj();
            
         //   PicRect = new Rectangle(0, 0, (int)(panel1.Width*0.15),(int) (panel1.Height*faktor));
            panel1.CreateGraphics().DrawImage(GraphicsImage, rect);
          if (Kliknato)  panel1.CreateGraphics().DrawImage(pick, PicRect);
          RemainingLabel.Text = "Remaining: " + Stabla.Remaining + " number/s";

        }


        public void kraj() {
            Stopped = true;
            timer.Stop();
            button4.Enabled = false;
            string[] dva=TimeLabel.Text.ToString().Split(':');
            TimeLabel.Text = "Your time was: " + dva[1] + ':' + dva[2];
        }



    
        public string ManyTests(int n)
        {
            bool ff = true;
            DateTime date = DateTime.Now;
            float sum = 0;

            long pocetno = (date.Minute * 60000) + (date.Second * 1000) + date.Millisecond;
            Random generatorot = new Random();
            for (int i = 0; i < n; i++)
            {
                SudokuTabla t = new SudokuTabla();
                t.generator = generatorot;
                t.polni();
                t.prazni();
                int vnes = 0;
                for (int j = 0; j < 81; j++)
                    if (t.Workingtabla[j / 9][j % 9] == 0) vnes++;
                sum += vnes;
                //         int[] niza = { 1, 2, 4, 6, 7, 89, 1 };
                //           List<int> lniza = niza.ToList();
                //          List<int> lniza2 = new List<int>();
                //            lniza2.AddRange(lniza);
                //              lniza2.Clear();
                ff &= check(t.tablata, t.Trtablata);
                // Console.WriteLine(lniza.Distinct().Count()+"   "+lniza.Count() );
                // Console.ReadKey();

            }
            date = DateTime.Now;


            //Console.WriteLine(ff);
            long krajno = (date.Minute * 60000) + (date.Second * 1000) + date.Millisecond;


            return string.Format("{0}  {1}  sekundi ,Prosek {2} prazni", ff, ((krajno - pocetno) / n), sum / n);

        }

        static bool check(List<List<int>> tabla, List<List<int>> vtabla)
        {

            foreach (var temp in tabla)
            {
                if (temp.Distinct().Count() != temp.Count()) return false;

            }
            foreach (var temp in vtabla)
            {
                if (temp.Distinct().Count() != temp.Count()) return false;

            }
            List<int> lniza = new List<int>();
            for (int i = 0; i < 9; i++)
            {
                lniza.Clear();
                for (int j = 0; j < 9; j++)
                {

                    lniza.Add(tabla[3 * (i / 3) + j / 3][3 * (i % 3) + j % 3]);
                }

                //                for (int k = 0; k < lniza.Count; k++)
                //                       Console.WriteLine(lniza[k]);
                //                    Console.WriteLine();
                if (lniza.Distinct().Count() != lniza.Count()) return false;
            }

            return true;
        }

   

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        //    Redraw();
       //     e.Graphics.DrawImageUnscaled(GraphicsImage, new Point(0, 0));
            
        }


        public bool Kliknato=false;
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (Stopped) return;
            if (Kliknato) {
                Kliknato = !Kliknato;
                Rectangle check = new Rectangle(SaveX, SaveY, PicRect.Width, PicRect.Height);
                if (check.Contains(e.X, e.Y))
                {

               //     Kliknato = !Kliknato;
                    int ipsilon = (int)(100 * ((double)(-SaveY + e.Y)) / check.Height) / 25;
                    int iks = (int)(100 * ((double)(-SaveX + e.X)) / check.Width) / 33;
                    //label2.Text = iks + "  " + ipsilon;
                    //label3.Text = check.Width + "  " + check.Height;
                    if (ipsilon == 3 && Stabla.Workingtabla[SkliknatoI][SkliknatoJ]!=0)
                    { Stabla.Workingtabla[SkliknatoI][SkliknatoJ] = 0; Stabla.Remaining++; }
                    else if (ipsilon!=3)
                    {
                        if (Stabla.Workingtabla[SkliknatoI][SkliknatoJ] == 0)
                            Stabla.Remaining--;
                        Stabla.Workingtabla[SkliknatoI][SkliknatoJ] = (ipsilon) * 3 + (iks) + 1;
                       
                    }
                        Invalidate();
                    return;
                }
               
               // panel1.Invalidate();
             
                    
            }

          if (!Kliknato)
            {
               
                kliknatoI = ((e.Y)  - pocY) / desetinaY;
                kliknatoJ = ((e.X)  - pocX) / desetinaX;
                
                //label2.Text = kliknatoI.ToString() + "  " + kliknatoJ.ToString();
                if (kliknatoI < 0 || kliknatoJ < 0 || kliknatoJ > 8 || kliknatoI > 8 || Stabla.IspraznetaTabla[kliknatoI][kliknatoJ] != 0) return;
                Kliknato = !Kliknato;
                SaveY = e.Y;
                SaveX = e.X;
              SkliknatoI = kliknatoI;
                SkliknatoJ = kliknatoJ;
                
                //    listBox1.SelectedIndex = tabla[kliknatoI][kliknatoJ] - 1;
               
                Rectangle rect2 = new Rectangle((int)e.X, (int)e.Y, PicRect.Width, PicRect.Height);
               // panel1.CreateGraphics().DrawImageUnscaled(GraphicsImage, new Point(0, 0));
                PicRect = rect2;
                //panel1.CreateGraphics().DrawImage(pick, rect2);
               // panel1.Invalidate();
                //    listBox1.Show();
                //     listBox1.BringToFront();
            }
            Invalidate();
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
      //      e.Graphics.DrawImage(back, 0, 0, Width, Height);
          //  panel1.CreateGraphics().DrawImage(back, 0, 0, panel1.Width, panel1.Height);
            
            GraphicsObject.DrawImage(back,0,0,GraphicsImage.Width,GraphicsImage.Height);
            GraphicsObject.DrawImage(logo, 4 * desetinaX, (int)(0.2 * desetinaY), 5 * desetinaX, (int)(1.5 * desetinaY));
            Redraw();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer.Stop();
            this.Close();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 9;i++ )
            {
                for(int j=0;j<9;j++)
                {
                    Stabla.Workingtabla[i][j] = Stabla.IspraznetaTabla[i][j];

                }

            }
            Invalidate();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Stabla.Workingtabla[i][j] = Stabla.tablata[i][j];

                }

            }
            Stopped = true;
            timer.Stop();
            button4.Enabled = false;
            Invalidate();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Stopped = false;
            Stabla = new SudokuTabla(gen);
            Stabla.polni();
            Stabla.prazni();
            TimeLabel.Text = "Time: 0:0";
            button4.Enabled = true;
           
            Invalidate();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Export_Menu ex = new Export_Menu();
            timer.Stop();
            ex.ShowDialog();
            timer.Start();
        }

  

    }
}
