using DealerForThePeople.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerForThePeople.Controller
{
    public static class ScoreBO
    {
        public static int CalculateScore(this Review review)
        {
            int score = 0;
            //2 points for every star
            score += review.Rating / 5;
            if (review.Body != null)
            {
                //1 point for every !, maximum of 3 points
                score += review.Body.Count(x => x == '!') < 3 ? review.Body.Count(x => x == '!') : 3;
                //2 point for having the word 'love'
                score += review.Body.ToLower().Contains("love") ? 2 : 0;
                //2 point for having the word 'excellent'
                score += review.Body.ToLower().Contains("excellent") ? 2 : 0;
                //2 point for having the word 'fantastic'
                score += review.Body.ToLower().Contains("fantastic") ? 2 : 0;
                //1 point for every 100 characters
                score += review.Body.Length >= 1000 ? 10 : review.Body.Length / 100;
            }
            return score;
        }
    }
}
