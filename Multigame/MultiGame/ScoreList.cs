using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace NameStartScreen
{
    [Serializable]
   public class ScoreList
    {
        private List<Score> scores;
        public static int Limit = 30;
        public ScoreList() {
            scores = new List<Score>();
        }
        public void Add(Score s) {
            scores.Add(s);
            scores = scores.OrderByDescending(x => x.Score_Value,new DescendCompare()).ToList();
            if (scores.Count == Limit + 1) {
                scores.RemoveAt(Limit);
            }

        }
         class DescendCompare : IComparer<string>
        {

            // Calls CaseInsensitiveComparer.Compare with the parameters reversed. 
           
             int IComparer<string>.Compare(string x, string y)
            {
                if (x.Equals("Unfinished") && y.Equals("Unfinished"))
                    return 0;
                else if (x.Equals("Unfinished"))
                    return -1;
                else if (y.Equals("Unfinished"))
                    return 1;
                else
                return (y.CompareTo(x));
            }

        }
         public List<Score> Select(KojaIgra k) {
             return scores.Where(x => x.igra == k).ToList();
        }
    }
}
