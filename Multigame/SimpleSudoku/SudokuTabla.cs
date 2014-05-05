using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NameSimpleSudoku
{
   public class SudokuTabla
    {
        public List<List<int>> tablata;
        public List<List<int>> Trtablata;
        public List<List<int>> Workingtabla;
        public List<List<int>> IspraznetaTabla;
        public int Remaining;
        public Random generator;
        public SudokuTabla()
        {
            generator = new Random();
        }
        public SudokuTabla(Random gen)
        {
            generator =gen;
        }
        public void polni()
        {
            tablata = new List<List<int>>();
            Trtablata = new List<List<int>>();
            //    generator = new Random();
            for (int i = 0; i < 9; i++)
            {
                tablata.Add(new List<int>());
                Trtablata.Add(new List<int>());
                for (int j = 0; j < 9; j++)
                {
                    tablata[i].Add(0);

                }
            }
            FirstThree();
            for (int i = generator.Next(216), j = 0; j < 216; j++, i = (i + 1) % 216)
            {
                //    Console.WriteLine("\n\n");
                //ovie              Console.WriteLine("ova e kolku napravil "+j);
                if (SecondThree(Perm(i), 3, 3, Perm((i + 43) % 216), true)) break;

            }
            //     Console.ReadKey();

            for (int k = generator.Next(216), l = 0; l < 216; l++, k = (k + 1) % 216)
            {

                //   Console.WriteLine("\n\n");
                //   Console.WriteLine("ova e kolku napravil "+j);
                if (ThirdThree(Perm(k)))
                {
                    //ovie                   Console.WriteLine("ova e kolku napravil " +" i l "+l);
                    break;
                }
                //ovie                Console.ReadKey();


            }
            //ovie        printaj();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Trtablata[i].Add(tablata[i][j]);

                }
            }
            Trtablata = transpose(Trtablata);
            //   printaj(Trtablata);

        }
        public List<List<int>> ThirdTriplet(List<List<int>> triplet, int Odkade)
        {

            for (int i = 0; i < 3; i++)
            {
                List<int> lniza = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                for (int j = 0; j < 6; j++)
                {
                    lniza.Remove(tablata[j][Odkade + i]);
                }
                triplet[i].AddRange(lniza);
                // ptriplet[i].AddRange(new int[] { 0, 0, 0 });
            }
            //  printaj();
            //  printajTriplet(triplet);
            //    Console.ReadKey();
            return triplet;
        }

        public List<int> Perm(int n)
        {
            List<int> niza = new List<int>();
            for (int i = 0; i < 3; i++)
            {
                niza.Add(n % 6);
                n /= 6;
            }
            return niza;
        }
        public bool check(int doTuka, int kojTri, List<List<int>> triplet)
        {
            List<int> lniza = new List<int>();
            for (int j = 0; j < 3; j++)
            {
                lniza.Clear();
                for (int i = 0; i < doTuka; i++)
                {
                    lniza.Add(tablata[i][kojTri + j]);


                }
                for (int k = 0; k < 3; k++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        lniza.Add(triplet[k][i]);
                    }
                    if (lniza.Count() - lniza.Distinct().Count() == 3) return false;
                    lniza.RemoveRange(doTuka, 3);
                }
            }
            return true;

        }
        public bool checkPoedinecno(int doTuka, int kojTri, List<List<int>> triplet)
        {
            List<int> lniza = new List<int>();
            for (int j = 0; j < 3; j++)
            {
                lniza.Clear();
                for (int i = 0; i < doTuka; i++)
                {
                    lniza.Add(tablata[i][kojTri + j]);


                }
                for (int i = 0; i < 3; i++)
                {
                    lniza.Add(triplet[i][j]);
                }
                if (lniza.Count() != lniza.Distinct().Count()) return false;
            }
            return true;
        }
        public List<List<int>> SecondTriplet(List<List<int>> triplet, List<List<int>> ptriplet)
        {
            for (int i = 0; i < 9; i++)
            {
                triplet[i / 3].Add(tablata[i % 3][i / 3]);


            }
            //  printaj();
            //  printajTriplet(triplet);
            triplet = shuffleTriplet(triplet, new List<int> { generator.Next(6), generator.Next(6), generator.Next(6) });
            for (int i = 0, j = 1; i < 3; i++, j = (j + 1) % 3)
            {

                if (j == i) continue;

                permutacii tri = new permutacii(new int[3] { triplet[i][0], triplet[i][1], triplet[j][2] });
                tri.OrderByNum(generator.Next(permutacii.Faktoriel(3)));
                int mesto = razlicnoOd(i, j);
                ptriplet[mesto].AddRange(tri.realni);

            }
            //  printajTriplet(vtriplet);

            return ptriplet;

        }
        public bool ThirdThree(List<int> nizaZaPrv)
        {
            List<List<int>> triplet = new List<List<int>>();
            List<List<int>> vtriplet = new List<List<int>>();
            List<List<int>> ttriplet = new List<List<int>>();
            List<List<int>> ptriplet = new List<List<int>>();
            for (int i = 0; i < 3; i++)
            {
                triplet.Add(new List<int>());
                vtriplet.Add(new List<int>());
                ttriplet.Add(new List<int>());
                ptriplet.Add(new List<int>());
            }
            ptriplet = ThirdTriplet(ptriplet, 0);
            ptriplet = shuffleTriplet(ptriplet, nizaZaPrv);
            vtriplet = ThirdTriplet(vtriplet, 3);
            //     vtriplet = shuffleTriplet(vtriplet, nizaZaVtor);
            ttriplet = ThirdTriplet(ttriplet, 6);
            //   ttriplet = shuffleTriplet(ttriplet, nizaZaTret);
            List<permutacii> perm = new List<permutacii>();
            for (int i = 0; i < 3; i++)
            {
                //      perm.Add(new permutacii(ptriplet[i]));
                perm.Add(new permutacii(vtriplet[i]));
                perm.Add(new permutacii(ttriplet[i]));
            }
            bool exit = false;
            for (int k = 0, l = generator.Next(216); k < 216; k++, l = (l + 7) % 216)
            {
                int tempJ = k;
                perm[1].OrderByNum(tempJ % 6);
                tempJ /= 6;
                perm[3].OrderByNum(tempJ % 6);
                tempJ /= 6;
                perm[5].OrderByNum(tempJ % 6);
                for (int i = 0, j = generator.Next(216); i < 216; i++, j = (j + 7) % 216)
                {
                    tempJ = i;
                    perm[0].OrderByNum(j % 6);
                    tempJ /= 6;
                    perm[2].OrderByNum(tempJ % 6);
                    tempJ /= 6;
                    perm[4].OrderByNum(tempJ % 6);

                    //            printajTripleti(ptriplet, vtriplet, ttriplet);

                    //   printaj();
                    //          printajPerm(perm);
                    if (checkThree(perm, ptriplet)) { exit = true; break; }
                    else continue;
                }
                if (exit) break;
            }
            for (int i = 0; i < 9; i++)
            {
                tablata[6 + i / 3][i % 3] = ptriplet[i % 3][i / 3];
                tablata[6 + i / 3][3 + (i % 3)] = perm[2 * (i % 3)].realni[i / 3];
                tablata[6 + i / 3][6 + (i % 3)] = perm[2 * (i % 3) + 1].realni[i / 3];
            }
            //ovie         Console.WriteLine(exit);
            //ovie         printaj();
            //ovie           printajTriplet(ptriplet);
            //ovie           printajPerm(perm);
            // Console.ReadKey();
            return exit;

        }
        public bool checkThree(List<permutacii> perm, List<List<int>> triplet)
        {
            for (int i = 0; i < 3; i++)
            {
                List<int> lniza = new List<int>();
                for (int j = 0; j < 3; j++)
                {
                    lniza.Add(triplet[j][i]);
                    lniza.Add(perm[2 * j].realni[i]);
                    lniza.Add(perm[2 * j + 1].realni[i]);

                    //                   for (int k = 0; k < lniza.Count; k++)
                    //                     Console.WriteLine(lniza[k]);
                    //  Console.ReadKey();
                    //  Console.Clear();
                    if (lniza.Distinct().Count() != lniza.Count()) return false;
                }



            }

            return true;


        }

        public bool SecondThree(List<int> nizaZaParovi, int doKade, int Kade,
            List<int> nizaZaPrviot, bool second)
        {
            List<List<int>> triplet = new List<List<int>>();
            List<List<int>> vtriplet = new List<List<int>>();
            List<List<int>> ttriplet = new List<List<int>>();
            List<List<int>> ptriplet = new List<List<int>>();
            for (int i = 0; i < 3; i++)
            {
                triplet.Add(new List<int>());
                vtriplet.Add(new List<int>());
                ttriplet.Add(new List<int>());
                ptriplet.Add(new List<int>());
            }
            if (second)
                ptriplet = SecondTriplet(triplet, ptriplet);

            ptriplet = shuffleTriplet(ptriplet, nizaZaPrviot);  // ova se mesti samoto sebe
            ptriplet = transpose(ptriplet);


            for (int i = 0; i < 9; i++)
            {
                triplet[i / 3][i % 3] = ptriplet[i / 3][i % 3];


            }
            ptriplet = shuffleTriplet(ptriplet, nizaZaParovi);   // ova mesti parovi i singles
            for (int i = 0, j = 1; i < 3; i++, j = (j + 1) % 3)
            {

                if (j == i) continue;

                permutacii tri = new permutacii(new int[3] { ptriplet[i][0], ptriplet[i][1], ptriplet[j][2] });
                tri.OrderByNum(generator.Next(permutacii.Faktoriel(3)));
                int mesto = razlicnoOd(i, j);
                vtriplet[mesto].AddRange(tri.realni);
                tri = new permutacii(new int[3] { ptriplet[i][0], ptriplet[i][1], ptriplet[(j + 1) % 3][2] });
                tri.OrderByNum(generator.Next(permutacii.Faktoriel(3)));
                mesto = razlicnoOd(i, (j + 1) % 3);
                ttriplet[mesto].AddRange(tri.realni);



            }
            if (!second)
            {
                printaj();
                //      printajTripleti(ptriplet, vtriplet, ttriplet);
                Console.WriteLine(!check(doKade, 3, vtriplet) || !check(doKade, 6, ttriplet));
                Console.ReadKey();
            }
            if (!check(doKade, 3, vtriplet) || !check(doKade, 6, ttriplet)) return false;
            for (int i = 0; i < 9; i++)
            {
                ptriplet[i / 3][i % 3] = vtriplet[i / 3][i % 3];


            }
            for (int i = generator.Next(216), j = 0; j < 216; j++, i = (i + 7) % 216)
            {

                // Console.WriteLine("ima volku poedineecno " + j+ff);
                if (j == 210) Console.WriteLine("ima volku poedineecno " + j);
                vtriplet = nshuffleTriplet(ptriplet, Perm(i));


                if (checkPoedinecno(doKade, 3, vtriplet))
                {
                    // Console.WriteLine("ima volku poedineecno " + j);
                    break;
                }


            }


            for (int i = 0; i < 9; i++)
            {
                ptriplet[i / 3][i % 3] = ttriplet[i / 3][i % 3];


            }

            for (int i = generator.Next(216), j = 0; j < 216; j++, i = (i + 7) % 216)
            {

                // Console.WriteLine("ima volku poedineecno " + j+ff);

                ttriplet = nshuffleTriplet(ptriplet, Perm(i));
                if (j == 210) Console.WriteLine("ima volku poedineecno " + j);

                if (checkPoedinecno(doKade, 6, ttriplet))
                {
                    //Console.WriteLine("ima volku poedineecno " + j );
                    break;
                }


            }
            for (int i = 0; i < 9; i++)
            {
                tablata[Kade + i / 3][i % 3] = triplet[i / 3][i % 3];
                tablata[Kade + i / 3][3 + (i % 3)] = vtriplet[i / 3][i % 3];
                tablata[Kade + i / 3][6 + (i % 3)] = ttriplet[i / 3][i % 3];
            }

            //               printaj();
            return true;


            //  printajTriplet(triplet);

        }
        public List<List<int>> transpose(List<List<int>> triplet)
        {
            int temp;
            for (int i = 0; i < triplet.Count; i++)
            {
                for (int j = 0; j < triplet[i].Count; j++)
                {
                    if (j <= i) continue;
                    temp = triplet[i][j];
                    triplet[i][j] = triplet[j][i];
                    triplet[j][i] = temp;
                }
            }

            return triplet;
        }

        public List<List<int>> nshuffleTriplet(List<List<int>> triplet, List<int> ran)
        {
            List<List<int>> vrati = new List<List<int>>();

            for (int i = 0; i < 3; i++)
                vrati.Add(new List<int>());
            for (int i = 0; i < 3; i++)
            {
                permutacii tri = new permutacii(triplet[i]);
                tri.OrderByNum(ran[i]);
                vrati[i].AddRange(tri.realni);


            }
            return vrati;
        }
        public List<List<int>> shuffleTriplet(List<List<int>> triplet, List<int> ran)
        {
            for (int i = 0; i < 3; i++)
            {
                permutacii tri = new permutacii(triplet[i]);
                tri.OrderByNum(ran[i]);
                triplet[i] = tri.realni;


            }
            return triplet;
        }
        public void FirstThree()
        {
            List<List<int>> triplet = new List<List<int>>();
            for (int i = 0; i < 3; i++)
            {
                triplet.Add(new List<int>());

            }
            int[] niza = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            permutacii pocetni = new permutacii(niza);

            pocetni.OrderByRan();
            for (int i = 0; i < 9; i++)
            {
                triplet[i / 3].Add(pocetni.realni[i]);

            }
            for (int i = 0; i < 3; i++)
            {
                permutacii tri = new permutacii(triplet[i]);
                tri.OrderByRan();
                triplet[i] = tri.realni;


            }


            List<List<int>> vtriplet = new List<List<int>>();
            List<List<int>> ttriplet = new List<List<int>>();
            for (int i = 0; i < 3; i++)
            {
                ttriplet.Add(new List<int>());
                vtriplet.Add(new List<int>());
            }
            for (int i = 0, j = 1; i < 3; i++, j = (j + 1) % 3)
            {

                if (j == i) continue;

                permutacii tri = new permutacii(new int[3] { triplet[i][0], triplet[i][1], triplet[j][2] });
                tri.OrderByNum(generator.Next(permutacii.Faktoriel(3)));
                int mesto = razlicnoOd(i, j);
                vtriplet[mesto].AddRange(tri.realni);
                tri = new permutacii(new int[3] { triplet[i][0], triplet[i][1], triplet[(j + 1) % 3][2] });
                tri.OrderByNum(generator.Next(permutacii.Faktoriel(3)));
                mesto = razlicnoOd(i, (j + 1) % 3);
                ttriplet[mesto].AddRange(tri.realni);



            }

            for (int i = 0; i < 3; i++)
            {
                permutacii tri = new permutacii(new int[3] { triplet[i][0], triplet[i][1], triplet[i][2] });
                tri.OrderByNum(generator.Next(permutacii.Faktoriel(3)));
                triplet[i] = tri.realni;

                tri = new permutacii(new int[3] { vtriplet[i][0], vtriplet[i][1], vtriplet[i][2] });
                tri.OrderByNum(generator.Next(permutacii.Faktoriel(3)));
                vtriplet[i] = tri.realni;

                tri = new permutacii(new int[3] { ttriplet[i][0], ttriplet[i][1], ttriplet[i][2] });
                tri.OrderByNum(generator.Next(permutacii.Faktoriel(3)));
                ttriplet[i] = tri.realni;

            }

            int ebroj = 3, vbroj = 6;
            if (generator.Next(2) == 1)
            {
                int temp = ebroj; ebroj = vbroj; vbroj = temp;
            }
            for (int i = 0; i < 9; i++)
            {
                tablata[i / 3][i % 3] = triplet[i / 3][i % 3];
                tablata[i / 3][ebroj + (i % 3)] = vtriplet[i / 3][i % 3];
                tablata[i / 3][vbroj + (i % 3)] = ttriplet[i / 3][i % 3];
            }

            //   printaj();

            //     printajTripleti(triplet, vtriplet, ttriplet);

        }
        public int razlicnoOd(int a, int b)
        {
            int vrati = 0;
            if (a == vrati || b == vrati) vrati++;
            if (a == vrati || b == vrati) vrati++;
            return vrati;
        }

        public void prazni()
        {
            List<List<int>> Proveri;

            Workingtabla = new List<List<int>>();
            Proveri = new List<List<int>>();
            for (int i = 0; i < 81; i++)
            {
                if (i / 9 == 0)
                {
                    Workingtabla.Add(new List<int>());
                    Proveri.Add(new List<int>());
                }
                Workingtabla[i / 9].Add(tablata[i / 9][i % 9]);

                Proveri[i / 9].Add(tablata[i / 9][i % 9]);
            }

            List<List<int>> Golemo = new List<List<int>>();
            for (int i = 0; i < 81; i++)
            {
                Golemo.Add(new List<int>());
                for (int j = 0; j < 9; j++)
                {

                    Golemo[i].Add(0);
                }
            }
            List<int> random = new List<int>();

            for (int i = 0; i < 81; i++)
            {
                random.Add(i);
            }

            int mx = 81, r, sbr, zbr = 0, szbr, lok;
            bool nw = false;
            int srb = 0;
            int[] sr = new int[90];
            while (true)
            {
                r = generator.Next(mx);
                sr[srb++] = r;
                sbr = Workingtabla[random[r] / 9][random[r] % 9];
                Workingtabla[random[r] / 9][random[r] % 9] = 0;

                szbr = zbr; zbr = 0;

                one(ref Workingtabla, ref Proveri, ref Golemo);

                for (int i = 0; i < 9; i++)
                    for (int j = 0; j < 9; j++)
                        zbr += Proveri[i][j];

                lok = Proveri[random[r] / 9][random[r] % 9];

                if (lok != 1 && !nw)
                    nw = true;
                else if (nw && lok == 1)
                    nw = false;

                if (nw)
                    Workingtabla[random[r] / 9][random[r] % 9] = sbr;

                for (int i = r; i < mx - 1; i++)
                    random[i] = random[i + 1];

                mx--;
                if (mx <= 0) break;
            }
            int vnes = 0;
            for (int i = 0; i < 81; i++)
                if (Workingtabla[i / 9][i % 9] == 0) vnes++;
            Remaining =  vnes;
            //vtoriK       printaj(Wtabla);
            //vtoriK          Console.WriteLine("ova e toa "+vnes);
            //vtoriKomentirani          Console.ReadKey();
            IspraznetaTabla = new List<List<int>>();
            for (int i = 0; i < 9; i++) {
                IspraznetaTabla.Add(new List<int>());
                for (int j = 0; j < 9; j++) {
                    IspraznetaTabla[i].Add(Workingtabla[i][j]);
                }
            
            }
        }

        public void one(ref List<List<int>> Wtabla,
        ref List<List<int>> Proveri,
        ref List<List<int>> Golemo)
        {

            int br = 0, redica, kolona;
            int[] sav = new int[9];
            bool t = true;
            int trt = 0;
            for (int i = 0; i < 81; i++)
                for (int j = 0; j < 9; j++)
                    Golemo[i][j] = 0;

            for (int i = 0; i < 81; i++)
                Proveri[i / 9][i % 9] = 0;

            for (int i = 0; i < 81; )
            {
                while (Wtabla[i / 9][i % 9] != 0)
                {
                    i++;
                    if (i == 81) break;
                }
                if (i == 81) break;
                for (int j = 1; j < 10; j++)
                {
                    //o=0;
                    for (int z = 0; z < 9; z++)
                    {

                        if (Wtabla[i / 9][((i % 9) + z) % 9] == j) { t = false; }
                    }  // do Tuka sum
                    // o=0;
                    if (t == true)
                        for (int z = 0; z < 9; z++)
                        {

                            if (Wtabla[((i + z * 9) / 9) % 9][i % 9] == j) { t = false; }
                        }
                    redica = (i / 9) / 3;
                    kolona = (i % 9) / 3;
                    if (t == true)
                        for (int z = 0; z < 9; z++)
                        {
                            if (Wtabla[3 * redica + z / 3][3 * kolona + z % 3] == j) { t = false; }
                        }
                    if (t == true) { br++; Golemo[i][trt] = j; trt++; }
                    t = true;

                }
                Proveri[(i / 9)][(i % 9)] = br;


                br = 0; i++;
                trt = 0;
            }
            int[] b = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] b2 = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] b3 = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] sav2 = new int[9], sav3 = new int[9];


            for (int i = 1; i < 10; i++)
            {
                for (int z = 0; z < 9; z++)
                { b[z] = 0; b3[z] = 0; b2[z] = 0; }

                for (int z = 0; z < 81; )
                {                                      //this thong
                    int j = 0;
                    if (Golemo[z][0] != 0)
                    {
                        while (Golemo[z][j] != 0 && j < 9)
                        {
                            if (Golemo[z][j] == i)
                            {
                                b[z % 9]++; sav[z % 9] = z;
                                b2[z / 9]++;
                                b3[((i % 9) / 3) * 3 + ((i / 9) / 3)]++;
                                sav2[z / 9] = z; sav3[((i % 9) / 3) * 3 + ((i / 9) / 3)] = z;
                            }
                            j++;

                        }
                    }

                    z++;
                }
                for (int z = 0; z < 9; z++)
                {
                    if (b[z] == 1) { Proveri[sav[z] / 9][sav[z] % 9] = 1; }
                    if (b2[z] == 1) { Proveri[sav2[z] / 9][sav2[z] % 9] = 1; }
                    if (b3[z] == 1) { Proveri[sav3[z] / 9][sav3[z] % 9] = 1; }
                }

            }

        }



        public void printaj()
        {
            Console.WriteLine();
            foreach (var temp in tablata)
            {

                foreach (var i in temp)
                {

                    Console.Write(i + "  ");
                }

                Console.WriteLine();

            }


        }




    }
}
