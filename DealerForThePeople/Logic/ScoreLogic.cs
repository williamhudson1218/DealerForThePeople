using DealerForThePeople.Model;
using System.Collections.Generic;
using System.Linq;

namespace DealerForThePeople.Controller
{
    public static class ScoreLogic
    {
        /// <summary>
        /// Iterates over each Review to calculate score
        /// </summary>
        /// <param name="reviews">List<Review> objects.</param>
        public static void CalculateScores(List<Review> reviews)
        {
            foreach(Review r in reviews)
            {
                r.Score = r.CalculateScore();
            }
        }

        /// <summary>
        /// Takes in a Review object
        /// Outputs a score reflecting the "positivity" of the review
        /// </summary>
        /// <param name="review">A single Review object.</param>
        public static int CalculateScore(this Review review)
        {
            int score = 0;
            //2 points for every star
            score += review.Rating / 5;
            if (review.Body != null)
            {
                //2 points for each positive word
                var postiveWordList = SettingsLogic.GetPositiveWords();
                foreach(string s in postiveWordList)
                {
                    score += review.Body.ToLower().Contains(s) ? 2 : 0;
                }

                //-2 points for each negative word
                var negativeWordList = SettingsLogic.GetNegativeWords();
                foreach (string s in negativeWordList)
                {
                    score -= review.Body.ToLower().Contains(s) ? 2 : 0;
                }

                //1 point for every !, maximum of 3 points
                score += review.Body.Count(x => x == '!') < 3 ? review.Body.Count(x => x == '!') : 3;
                //1 point for every 100 characters
                score += review.Body.Length >= 1000 ? 10 : review.Body.Length / 100;
            }
            return score;
        }
    }
}
