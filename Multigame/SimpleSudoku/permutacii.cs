using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NameSimpleSudoku
{
  public  class permutacii
    {
        public int ran;
        public List<int> staticni;
        public List<int> realni;
        public List<int> transition;

        public permutacii(int[] niza)
        {

            Random r = new Random();

            ran = r.Next(Faktoriel(niza.Length));

            staticni = new List<int>();
            realni = new List<int>();
            transition = new List<int>();
            staticni.AddRange(niza);
            //     Console.WriteLine(staticni.Count);

        }
        public permutacii(List<int> niza)
        {

            Random r = new Random();

            ran = r.Next(Faktoriel(niza.Count));

            staticni = new List<int>();
            realni = new List<int>();
            transition = new List<int>();

            staticni.AddRange(niza);
            //   Console.WriteLine(staticni.Count);

        }
        public static int Faktoriel(int n)
        {

            if (n == 1) return 1;
            return n * Faktoriel(n - 1);
        }
        public override string ToString()
        {
            string s = "";
            foreach (int t in realni)
            {
                s += "  " + t;
            }
            return s;
        }
        public void OrderByRan()
        {
            OrderByNum(ran);
        }
        public void OrderByNRan()
        {
            ran = new Random().Next(Faktoriel(staticni.Count));
            OrderByNum(ran);
        }

        public void OrderByNum(int num)
        {
            realni.Clear();
            transition.Clear();
            transition.AddRange(staticni);
            int t_num = num;
            for (int i = staticni.Count; i > 1; i--)
            {
                realni.Add(transition[t_num % i]);
                transition.Remove(transition[t_num % i]);
                t_num /= i;



            }
            realni.Add(transition[0]);

        }
    }
}
