using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
namespace NameMatchTwo
{
 public   class Topka
    {
        public int VrzKocka,B_size;

        public double  Radius;
        public int lats = 30, longs = 30;
        public   double cela = 2*Math.PI, izob = 1.0, x_izob = 1;
       public bool po_sto = true;
           public double del = 1, norm = 1;
           public int nasoka,visina;
           public int red, kols;
           public static int teks;

        public Topka(int kols,int red,int vrz,double rad){
        B_size=red*kols;
            VrzKocka=vrz;
            nasoka = 1;
            Radius=rad;
            this.red = red;
            this.kols = kols;
        
        }
           public void Move() {
               
               if (visina== 40)
                   nasoka = -1;
               else if (visina == 10)
                   nasoka = 1;
               visina += nasoka * 2;


           }
           public void Plus()
           {
               VrzKocka++;
               VrzKocka %= B_size;
           }
           public void Minus()
           {
               VrzKocka += B_size - 1;
               VrzKocka %= B_size;
           }
           public void RPlus()
           {
               VrzKocka+=kols;
               VrzKocka %= B_size;
           }
           public void RMinus()
           {
               VrzKocka += (kols - 1)*red;
               VrzKocka %= B_size;
           }
        public void Draw(double X_,double Y_,double Z_)
        {
            double i, j;
            if (!po_sto)
            {
                double t = Z_;
                Z_ = Y_; Y_ = t;
            }
            //r=1;
            GL.PushMatrix();
            //glScalef(r,r,r);
            //r=1;
         //   GL.Rotate(2 * Math.PI, 1, 0, 0);
            for (i = 0; i < lats; i++)
            {
                double lat0 = Math.PI * (-0.5 + (double)(i) / lats);
                double z0 = Math.Sin(lat0);
                double zr0 = Math.Cos(lat0);

                double lat1 = Math.PI * (-0.5 + (double)(i + 1) / lats);
                double z1 = Math.Sin(lat1);
                double zr1 = Math.Cos(lat1);
                GL.BindTexture(TextureTarget.Texture2D, teks);

                GL.Begin(PrimitiveType.QuadStrip);
                for (j = 0; j <= longs / del; j++)
                {
                    double lng = cela * (double)(j) / longs;
                    double x = Math.Cos(lng);
                    double y = Math.Sin(lng);
                    if (po_sto)
                    {

                        GL.TexCoord2(j / (longs / del), i / lats);
                        GL.Normal3(X_ + (Radius * x * zr1), Y_ + (Radius * y * zr1) * izob, Z_ + (Radius * z1));
                       GL.Vertex3(X_ + (Radius * x * zr1), Y_ + (Radius * y * zr1) * izob, Z_ + (Radius * z1));

                       GL.TexCoord2((j) / (longs / del), (i + 1) / lats);
                     
                         GL.Normal3(X_ + (Radius * x * zr0), Y_ + (Radius * y * zr0) * izob, Z_ + (Radius * z0));
                        GL.Vertex3(X_ + (Radius * x * zr0), Y_ + (Radius * y * zr0) * izob, Z_ + (Radius * z0));

                    }

                }
                GL.End();
            }
            GL.PopMatrix();
            GL.BindTexture(TextureTarget.Texture2D, 0);
            // glNormal3f(0,1,0);
        }
     public static void LoadTextures(){
     
               
                    //make a bitmap out of the file on the disk
                    
                    Bitmap TextureBitmap =Properties.Resources.topka;
                    //get the data out of the bitmap

                    GL.GenTextures(1, out teks);
                    GL.BindTexture(TextureTarget.Texture2D, teks);

                    System.Drawing.Imaging.BitmapData TextureData =
                    TextureBitmap.LockBits(
                            new System.Drawing.Rectangle(0, 0, TextureBitmap.Width, TextureBitmap.Height),
                            System.Drawing.Imaging.ImageLockMode.ReadOnly,
                            System.Drawing.Imaging.PixelFormat.Format32bppRgb
                        );

                    //Code to get the data to the OpenGL Driver

                    //generate one texture and put its ID number into the "Texture" variable


                    //the following code sets certian parameters for the texture


                    //load the data by telling OpenGL to build mipmaps out of the bitmap data
                    GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, TextureData.Width, TextureData.Height,
                        0, PixelFormat.Bgra, PixelType.UnsignedByte, TextureData.Scan0);



                    //free the bitmap data (we dont need it anymore because it has been passed to the OpenGL driver
                    TextureBitmap.UnlockBits(TextureData);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

                
     
     
     }
    }
}
