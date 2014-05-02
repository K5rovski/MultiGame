using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK;
namespace MultiGame
{
 public   class Tabla
    {

     public List<Kvadrat> tabla;
     public int SelektiranOne, SelektiranTwo;
     public int Topka_Place;
     public int VoKolona;
     public static int[] Site_Teksturi = { 1, 2, 3, 4, 5 };
     public bool Stopped;
     public Tabla(int v) {
         tabla = new List<Kvadrat>();
         SelektiranOne = SelektiranTwo = -1;
         Topka_Place = 0;
         VoKolona = v;
         Stopped = false;
     }
    
     public void Add(Kvadrat k) {
         tabla.Add(k);
     }
     public void Input(char c)
     {
         if (Stopped) return;
         switch (c)
         {
             case 'w':
                 Plus();
                 // obnovi go prikazot
                 break;
             case 's':
                 Minus();
                 // obnovi go prikazot
                 break;
             case 'd':
                 RPlus();
                 // obnovi go prikazot
                 break;
             case 'a':
                 RMinus();
                 // obnovi go prikazot
                 break;
             case ' ':
                 Keyboard_In(Topka_Place);

                 break;


         }

     }
     public void InputK(ConsoleKey l)
     {
         
         if (Stopped) return;
         switch (l)
         {
             case ConsoleKey.W:
                 Plus();
                 // obnovi go prikazot
                 break;
             case ConsoleKey.S:
                 Minus();
                 // obnovi go prikazot
                 break;
             case ConsoleKey.D:
                 RPlus();
                 // obnovi go prikazot
                 break;
             case ConsoleKey.A:
                 RMinus();
                 // obnovi go prikazot
                 break;
             case ConsoleKey.Spacebar:
                 Keyboard_In(Topka_Place);

                 break;
             case ConsoleKey.UpArrow:
                 Plus();
                 // obnovi go prikazot
                 break;
             case ConsoleKey.DownArrow:
                 Minus();
                 // obnovi go prikazot
                 break;
             case ConsoleKey.RightArrow:
                 RPlus();
                 // obnovi go prikazot
                 break;
             case ConsoleKey.LeftArrow:
                 RMinus();
                 // obnovi go prikazot
                 break;
             case ConsoleKey.Enter:
                 Keyboard_In(Topka_Place);

                 break;


         }

     }
     public void Keyboard_In (int kliknat){
         if (SelektiranOne == -1) {
             SelektiranOne = kliknat;
             tabla[SelektiranOne].ChangeDirection(); // 
         }
              else if (SelektiranOne!=-1 && SelektiranTwo!=-1) {
             tabla[SelektiranOne].ChangeDirection();  // 
             tabla[SelektiranTwo].ChangeDirection(); //
             SelektiranTwo=SelektiranOne=-1;
             SelektiranOne = kliknat;
             tabla[SelektiranOne].ChangeDirection();
         
         
         }
         else if (SelektiranOne == kliknat) { 
         tabla[SelektiranOne].ChangeDirection(); // odselektiraj krajna pozicija i nasoka
         SelektiranOne=-1;
         }
         else  if (SelektiranTwo==-1){
             SelektiranTwo=kliknat;
         tabla[SelektiranTwo].ChangeDirection();  // selektiraj 
             CheckDelete(SelektiranOne,SelektiranTwo); // ako se ednakvi teksturite napravi selecttwo i izbrisi two 
         }
        
    
     }
     public Kvadrat get(int i) {
         return tabla[i];
     }
     public void CheckDelete(int a, int b) {
         if (tabla[a].N_Tekstura == tabla[b].N_Tekstura) { 
         tabla[b].ToDelete=true; // delete flag true when finishes 
         tabla[b].Partner = a; // spari go so a
         SelektiranOne = SelektiranTwo = -1;
         }
     }
     public void Update()
     {
         int i = 0;
         for (; i < tabla.Count; i++)
         {
             if (!tabla[i].Deleted && !tabla[i].moving && tabla[i].ToDelete)
             {
                 if (tabla[i].Serenity == 20)
                 {
                     tabla[i].Deleted = true;
                     tabla[tabla[i].Partner].Deleted = true;
                     RPlus();
                 }
                 
                 break;
             }
         }
        
     }
     public static void LoadTextures()
     {
         string _Name = "../../Pictures/";
         for (int i = 0; i < 5; i++)
         {
             _Name = "../../Pictures/" + ((char)(97 + i)) + ".png";
             if (System.IO.File.Exists(_Name))
             {
                 //make a bitmap out of the file on the disk
                 GL.Enable(EnableCap.Texture2D);
                 Bitmap TextureBitmap = new Bitmap(_Name);
                 //get the data out of the bitmap

                 GL.GenTextures(1, out Site_Teksturi[i]);
                 GL.BindTexture(TextureTarget.Texture2D, Site_Teksturi[i]);

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

     public void SetTextures() {
         Random gen = new Random();
         List<int> temp = new List<int>();
         for (int i = 0; i < tabla.Count; i++)
             temp.Add(i);
         int teks,koja;
         for (int i = 0; i < tabla.Count / 2; i++)
         {
             teks = gen.Next(Site_Teksturi.Length);
             
             koja = gen.Next(temp.Count);
             tabla[temp[koja]].N_Tekstura = Site_Teksturi[teks];
             temp.RemoveAt(koja);
             
             koja = gen.Next(temp.Count);
             tabla[temp[koja]].N_Tekstura = Site_Teksturi[teks];
             temp.RemoveAt(koja);
         }
     }
     public void Plus()
     {
         for (int i = 0; i < tabla.Count; i++)
         {
             Topka_Place++;
             Topka_Place %= tabla.Count;
             if (!tabla[Topka_Place].Deleted) break;
         }
         }
     public void Minus()
     {
         for (int i = 0; i < tabla.Count; i++)
         {
             Topka_Place+=tabla.Count-1;
             Topka_Place %= tabla.Count;
             if (!tabla[Topka_Place].Deleted) break;
         }
     }
     public void RPlus()
     {
         for (int i = 0; i < tabla.Count; i++)
         {
             if (Topka_Place ==(VoKolona * (VoKolona-1)) ) Topka_Place += VoKolona;
             Topka_Place += VoKolona;
             Topka_Place +=-(Topka_Place/tabla.Count)+(VoKolona*VoKolona);
             
             Topka_Place %= tabla.Count;
           if (!tabla[Topka_Place].Deleted) break;
         }
     }
     public void RMinus()
     {
         for (int i = 0; i <tabla.Count; i++)
         {
             if (Topka_Place == VoKolona - 1) Topka_Place += (VoKolona-1)*(VoKolona-1);
          else{ 
            Topka_Place += ((VoKolona - 1) * VoKolona)  ;
             Topka_Place %= tabla.Count;
             Topka_Place = Topka_Place + (Topka_Place/((VoKolona)*(VoKolona-1)));
         }
             Topka_Place %= tabla.Count;
            if (!tabla[Topka_Place].Deleted) break;
         }
     }
     public void Draw() { 
     foreach (var temp in tabla)
      if (!temp.Deleted)   temp.Draw();
     
     }
     public bool Move()
     {
         bool moved = false;
         foreach (var temp in tabla)
             if (!temp.Deleted) { moved = true; temp.Move(); }
         if (moved == false) Stopped = true;
         return moved;
     }
    }
}
