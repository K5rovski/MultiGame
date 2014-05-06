using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace NameMatchTwo
{


    public partial class MatchTwo : Form
    {
        bool loaded = false;
        double transZ = 39.2;         // !!!pola odsoba tekovna oddalechenost na pogledot
        double rotateA = -15.7;
        static double k_visina = 157.4;
        static double nasoka_x = 0;
        static double nasoka_y = 4;
        static double nasoka_z = 0;
        public int tajmer_ticks = 0;
     
        public Tabla kocki;
        Random gen;
        public Image back;
        public Timer timer1;
        bool Pause = false;
        public MatchTwo( int v)
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(43,0,0);
            timer1 = new Timer();
            timer1.Interval = 20;
            timer1.Tick+=new EventHandler(timer1_Tick);
            gen = new Random();
            kocki = new Tabla(v);
            label1.ForeColor = Color.FromArgb(170,68,0);

            back = Properties.Resources.back;

            DoubleBuffered = true;
            
        }
       
      
 
        public Topka topce;
        private void glControl1_Load(object sender, EventArgs e)
        {
            loaded = true;
            SetupViewport();
            GL.ShadeModel(ShadingModel.Smooth);
            GL.ClearColor(Color.SkyBlue);
            GL.DepthMask(true);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Lequal);
            Tabla.LoadTextures();
            Topka.LoadTextures();
            soba(20,kocki.VoKolona,3);
            topce = new Topka(8,8, 0,5);
            topce.visina = 10;
            timer1.Start();
            
        }

     
   
        PolygonMode poly = PolygonMode.Fill;
        public void kvadrat(double poc, double z_, double poc_y, double size,double redovi, double koloni)
        {
          
            for (int i = 0; i < redovi; i++)
            {
                
                for (int k=0; k < koloni; k++)
                {


                    if ((i + k) % 2 == 0)
                        kocki.Add(new Kvadrat(poc + ((double)(k * 1.5) * size), z_ - size / 2, poc_y + ((double)(i * 1.5) * size), size, Color.Brown));
                    else
                    {
                        kocki.Add(new Kvadrat(poc + ((double)(k * 1.5) * size), z_ - size / 2, poc_y + ((double)(i * 1.5) * size), size, Color.Green));
                     
                    }
                    kocki.get(kocki.tabla.Count - 1).R_speed = 12;

                   }
                
            }

            kocki.SetTextures();


           
           
        }
    void soba(double size,double golemina,double dimZ){

        double temp = ((golemina - 1) * 1.5 * size) / 2;
    
		 
		 kvadrat(-temp,5,-temp,size,golemina,golemina);


    }
        private void display()
        {
            if (!loaded) // Play nice
                return;
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            /* kamerata se naogja vo 0,0,transZ i gleda kon 0,0,0
             * nagore za kamerata e vo pravec na vektorot 0,1,0
             */
            GL.PolygonMode(MaterialFace.FrontAndBack, poly); // 114 transz rotateA=-15.94 k vis =84.3
            OpenTK.Matrix4d modelview = OpenTK.Matrix4d.LookAt(nasoka_x + transZ * Math.Cos(rotateA), k_visina, nasoka_z + transZ * Math.Sin(rotateA), 
          nasoka_x,nasoka_y,nasoka_z,
   0, 1, 0);
            GL.LoadMatrix(ref modelview);
            
            GL.MatrixMode(MatrixMode.Modelview);
            kocki.Draw();
              GL.Color3(Color.YellowGreen);
              topce.Draw(kocki.get(kocki.Topka_Place).X_, kocki.get(kocki.Topka_Place ).Y_ + topce.visina + topce.Radius, kocki.get(kocki.Topka_Place ).Z_);
            
            glControl1.SwapBuffers();
           

        }

        private void glControl1_Resize(object sender, EventArgs e)
        {
            if (!loaded)
                return;
            SetupViewport();
            glControl1.Invalidate();
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            display();
        }

        private void SetupViewport()
        {
            int w = glControl1.Width;
            int h = glControl1.Height;
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Frustum(-1, 1, -1, 1, 1, 1000);
            
        }

      

        private void glControl1_Click(object sender, EventArgs e)
        {
          

        }
        public void ShowScore(Graphics g, int lokacija) {
            string[] temp = label1.Text.Split(':');
            int min, sek,vkupno;
            int.TryParse(temp[0], out min);
            int.TryParse(temp[1], out sek);
            vkupno = min * 60 + sek;

            string s = string.Format("Your score is: {0:00} points", (10000-(vkupno * 10))*kocki.VoKolona);
            Point p= new Point( 0,0);

            g.DrawString(s, label1.Font, new SolidBrush(label1.ForeColor), p);
          //  label1.Text = s;
         
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
           // rotateA += 0.005;
          topce.Move();
            kocki.Update();
            topce.B_size = kocki.tabla.Count;
          if  (!kocki.Move())
          {
              timer1.Stop();
          Invalidate();
          };
            tajmer_ticks++;
            tajmer_ticks %= 50;
            if (tajmer_ticks == 0)
                Brojac();
            display();
        }
        private  void Brojac() {
            string[] dva = label1.Text.ToString().Split(':');
            int min = 0, sek = 0, vkupno;
            int.TryParse(dva[0], out min);
            int.TryParse(dva[1], out sek);
            vkupno = sek + min * 60;
            vkupno++;
            min = vkupno / 60;
            sek = vkupno % 60;
            label1.Text=string.Format("{0:00}:{1:00}",min.ToString(),sek.ToString());

        }
   
        private void glControl1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (kocki.Stopped || Pause) return;
            kocki.Input(e.KeyChar);
            display();
            //switch (e.KeyChar) {

            //    case (char)27:                 // ESC e pritisnat
            //        Application.Exit();
            //        break;
            //    case 'W':
            //        transZ -= 0.5f;
            //        display();  // obnovi go prikazot
            //        break;
            //    case 'S':
            //        transZ += 0.5f;
            //        if (transZ < 0) transZ = 0;
            //        display();  // obnovi go prikazot
            //        break;
            //    case 'X':
            //        k_visina -= 2.3f; // mod na prikaz za poligoni - popolneti
                 
            //        display(); // obnovi go prikazot
            //        break;
            //    case 'Z':
            //        k_visina += 2.3f; // mod na prikaz za poligoni - popolneti
                    
            //        display();  // obnovi go prikazot
            //        break;
            //    case 'k':
            //        // 114 transz rotateA=-15.94 k vis =84.3
            //        transZ = 114;
            //        rotateA = -15.94;
            //        k_visina = 84.3;
            //        display();
            //        break;
                
            //    case 'A':
            //        rotateA += 0.1;
            //        display();  // obnovi go prikazot
            //        break;
            //    case 'D':
            //        rotateA -= 0.01f;
            //        display();  // obnovi go prikazot
            //        break;
            //    case ' ':
            //        kocki.Keyboard_In(kocki.Topka_Place);
            //        display();
            //        break;
                
                    
            //    case '1':
            //        poly=PolygonMode.Line;
            //        display();  // obnovi go prikazot
            //        break;
            //    case '2':
            //        poly = PolygonMode.Fill;
            //        display();  // obnovi go prikazot
            //        break;
            //    case 'w':
            //        kocki.Plus();
            //        display();  // obnovi go prikazot
            //        break;
            //    case 's':
            //        kocki.Minus();
            //        display();  // obnovi go prikazot
            //        break;
            //    case 'd':
            //        kocki.RPlus();
            //        display();  // obnovi go prikazot
            //        break;
            //    case 'a':
            //        kocki.RMinus();
            //        display();  // obnovi go prikazot
            //        break;
            
            //}
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
   
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = Graphics.FromHwnd(panel1.Handle);
        
               
             Rectangle rec2 = new Rectangle(0, 0,panel1.Width,panel1.Height );
            g.DrawImage(back, rec2);
            if (kocki.Stopped)
            {
                ShowScore(Graphics.FromHwnd(panel2.Handle), rec2.Bottom);
                button2.Enabled = false;
            }
            else {
               
            
            }
        }

    

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text.Equals("Пауза"))
            {
                timer1.Stop();
                Pause = true;
                button2.Text = "Почни";
                glControl1.Focus();

            } else if (button2.Text.Equals("Почни"))
            {
                timer1.Start();
                Pause =false;
                button2.Text = "Пауза";
                glControl1.Focus();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            timer1.Stop();
            this.Close();
        }

  

    }
}
 