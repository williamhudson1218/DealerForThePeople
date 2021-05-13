using Microsoft.VisualStudio.TestTools.UnitTesting;
using DealerForThePeople.Model;
using HtmlAgilityPack;
using DealerForThePeople.Controller;
using System.Collections.Generic;

namespace DealerForThePeopleTest
{
    [TestClass]
    public class ReviewTest
    {
        [TestMethod]
        public void URLIsNotNull()
        {
            Assert.IsNotNull(SettingsLogic.GetURL());
        }

        [TestMethod]
        public void DocListIsNull()
        {
            List<HtmlDocument> htmlDocList = ReviewLogic.GetReviews("");
            Assert.IsNull(htmlDocList);
        }

        [TestMethod]
        public void BadURLTest()
        {
            List<HtmlDocument> htmlDocList = ReviewLogic.GetReviews("https://google.com/;alksdfj;alsdkj");
            Assert.IsTrue(htmlDocList.Count == 0);
        }

        [TestMethod]
        public void ReviewCountIsFive()
        {
            List<HtmlDocument> htmlDocList = ReviewLogic.GetReviews(SettingsLogic.GetURL());
            Assert.IsTrue(htmlDocList.Count == 5);
        }

        [TestMethod]
        public void ReviewHtmlIsNotNull()
        {
            List<HtmlDocument> htmlDocList = ReviewLogic.GetReviews(SettingsLogic.GetURL());
            Assert.IsFalse(string.IsNullOrWhiteSpace(htmlDocList[0].Text));
        }

        [TestMethod]
        public void ReviewsCountIsFifty()
        {
            List<HtmlDocument> htmlDocList = ReviewLogic.GetReviews(SettingsLogic.GetURL());
            List<Review> reviewList = ReviewLogic.ParseReviews(htmlDocList);
            Assert.IsTrue(reviewList.Count == 50);
        }

        [TestMethod]
        public void ParseReviewNotNull()
        {
            HtmlDocument doc = new();
            doc.Load("../../../ParseReviewNotNull.html");

            Review review = ReviewLogic.ParseReview(doc);
            Assert.IsNotNull(review);
        }

        [TestMethod]
        public void EmptyReviewIsNull()
        {
            HtmlDocument doc = new();
            Review review = ReviewLogic.ParseReview(doc);
            Assert.IsNull(review);
        }

        [TestMethod]
        public void RatingParsesCorrectly()
        {
            string ratingHtml = "rating-45 pull-right";
            Assert.IsTrue(ReviewLogic.ParseRating(ratingHtml) == 45);
        }

        [TestMethod]
        public void BadRatingParsesCorrectly()
        {
            string ratingHtml = "asdfasdf pull-right";
            Assert.IsTrue(ReviewLogic.ParseRating(ratingHtml) == 0);
        }
    }
}
