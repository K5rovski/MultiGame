using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NameStartScreen
{
   public enum KojaIgra {Sudoku,MatchTwo};
    [Serializable]
    public class Score
    {
       public KojaIgra igra;
        public string Ime;
        public string Score_Value;
        
        public Score(KojaIgra i,string ime, string score) {
            igra = i;
            Ime = ime;
            Score_Value = score;
        }
        //public void Add(string ime, string score){
           

        //        if (scores[scores.Count - 1].CompareTo(score) >= 0) {
        //            scores.Add(score);
        //            iminja.Add(ime);
        //        }
                   

        //        if (scores.Count == Limit+1) {
        //            scores.RemoveAt(Limit);
        //            iminja.RemoveAt(Limit);
        //        }
        //}
        public override string ToString()
        {
            string s = "";
            string Kraj="";
            if (igra == KojaIgra.MatchTwo) {
                Kraj = " Points";
            }
            else if (igra == KojaIgra.Sudoku)
            {
                Kraj = " Time";
            }
            if (Score_Value.Equals("Unfinished")) Kraj = "";
            s = Ime + " " + Score_Value + Kraj;
            return s;
        }
    }
}
