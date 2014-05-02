using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK;
namespace opentkForms
{
   public class Kvadrat
    {
        public double X_;
        public double Y_;
        public double Z_;
        public double Size;
      
        public int N_Tekstura;
        public double agol;
        public static int temp = 0;
        public Color boja;
        public double R_speed = 0;
        public double KrajnaPozicija = 0;
        public int nasoka = -1;
        public bool moving = false;
        public double opseg = 60 * Math.PI;
        public bool ToDelete = false;
        public int Partner = -1;
        public int Serenity;
        public bool Deleted = false;
        public Kvadrat(double x, double y, double z, double size,Color b, int teks=1 )
        {
            X_ = x;
            Y_ = y;
            Z_ = z;
            Size = size;
            Serenity = 0;
            N_Tekstura = teks;
            boja = b;
            agol = 0;
        }
        public void Move() {
            if (Math.Abs(Math.Abs(agol) - Math.Abs(KrajnaPozicija)) > R_speed)
            {
                moving = true;
                Serenity = 0;
                agol += nasoka * R_speed;
            }
            else { moving = false; Serenity++;
            Serenity %= 50;
            }
        }
        public void ChangeDirection() {
            KrajnaPozicija = opseg - KrajnaPozicija;
            moving = true;
            if (nasoka == -1)
            {
                nasoka = 1;

            }
            else {
                nasoka = -1;
            }
        
        }
        
        public void Draw() {

            
            GL.PushMatrix();
            GL.Translate(X_,Y_,Z_);
            GL.Rotate(agol,0,0,1);


            GL.Color3(boja);
            GL.Begin(PrimitiveType.Quads);


            // gore
            GL.Vertex3(-(Size / 2), Size / 2, -Size / 2);
            GL.Vertex3((Size / 2), Size / 2, -Size / 2);
            GL.Vertex3((Size / 2), Size / 2, Size / 2);
            GL.Vertex3(-(Size / 2), Size / 2, Size / 2);

            // napred
            GL.Vertex3(-(Size / 2), Size / 2, -Size / 2);
            GL.Vertex3((Size / 2), Size / 2, -Size / 2);
            GL.Vertex3((Size / 2), -Size / 2, -Size / 2);
            GL.Vertex3(-(Size / 2), -Size / 2, -Size / 2);

            // nazad
            GL.Vertex3(-(Size / 2), Size / 2, Size / 2);
            GL.Vertex3((Size / 2), Size / 2, Size / 2);
            GL.Vertex3((Size / 2), -Size / 2, Size / 2);
            GL.Vertex3(-(Size / 2), -Size / 2, Size / 2);

            //levo
            GL.Vertex3(-(Size / 2), Size / 2, -Size / 2);
            GL.Vertex3(-(Size / 2), Size / 2, Size / 2);
            GL.Vertex3(-(Size / 2), -Size / 2, Size / 2);
            GL.Vertex3(-(Size / 2), -Size / 2, -Size / 2);


            // desno
            GL.Vertex3((Size / 2), Size / 2, -(Size / 2));
            GL.Vertex3((Size / 2), Size / 2, Size / 2);
            GL.Vertex3((Size / 2), -Size / 2, Size / 2);
            GL.Vertex3((Size / 2), -Size / 2, -Size / 2);
            GL.End();
            GL.Color3(Color.White);
            GL.BindTexture(TextureTarget.Texture2D, N_Tekstura);
            GL.Begin(PrimitiveType.Quads);
            // dole
            GL.TexCoord2(0, 0);
            GL.Vertex3(-(Size / 2), -(1 * Size / 2), -Size / 2);
            GL.TexCoord2(0, 1);
            GL.Vertex3((Size / 2), -(1 * Size / 2), -Size / 2);
            GL.TexCoord2(1, 1);
            GL.Vertex3((Size / 2), -(1 * Size / 2), Size / 2);
            GL.TexCoord2(1, 0);
            GL.Vertex3(-(Size / 2), -(1 * Size / 2), Size / 2);

            GL.End();
            GL.BindTexture(TextureTarget.Texture2D, 0);

            GL.PopMatrix();
        
        }

    }
}
