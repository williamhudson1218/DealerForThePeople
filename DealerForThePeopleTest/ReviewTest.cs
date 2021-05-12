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
            Assert.IsNotNull(SettingsBO.GetURL());
        }

        [TestMethod]
        public void DocListIsNull()
        {
            List<HtmlDocument> htmlDocList = ReviewBO.GetReviews("");
            Assert.IsNull(htmlDocList);
        }

        [TestMethod]
        public void BadURLTest()
        {
            List<HtmlDocument> htmlDocList = ReviewBO.GetReviews("https://google.com/;alksdfj;alsdkj");
            Assert.IsTrue(htmlDocList.Count == 0);
        }

        [TestMethod]
        public void TestDealerWithOnePageOfResults()
        {
            List<HtmlDocument> htmlDocList = ReviewBO.GetReviews("https://www.dealerrater.com/dealer/Ferrari-of-Salt-Lake-City-review-13155/page/?filter=#link");
            Assert.IsTrue(htmlDocList.Count == 1);
        }

        [TestMethod]
        public void ReviewCountIsFive()
        {
            List<HtmlDocument> htmlDocList = ReviewBO.GetReviews(SettingsBO.GetURL());
            Assert.IsTrue(htmlDocList.Count == 5);
        }

        [TestMethod]
        public void ReviewHtmlIsNotNull()
        {
            List<HtmlDocument> htmlDocList = ReviewBO.GetReviews(SettingsBO.GetURL());
            Assert.IsFalse(string.IsNullOrWhiteSpace(htmlDocList[0].Text));
        }

        [TestMethod]
        public void ParseReviewNotNull()
        {
            HtmlDocument doc = new();
            doc.Load("../../../ParseReviewNotNull.html");

            Review review = ReviewBO.ParseReview(doc);
            Assert.IsNotNull(review);
        }

        [TestMethod]
        public void EmptyReviewIsNull()
        {
            HtmlDocument doc = new();
            Review review = ReviewBO.ParseReview(doc);
            Assert.IsNull(review);
        }

        [TestMethod]
        public void RatingParsesCorrectly()
        {
            string ratingHtml = "rating-45 pull-right";
            Assert.IsTrue(ReviewBO.ParseRating(ratingHtml) == 45);
        }

        [TestMethod]
        public void BadRatingParsesCorrectly()
        {
            string ratingHtml = "asdfasdf pull-right";
            Assert.IsTrue(ReviewBO.ParseRating(ratingHtml) == 0);
        }
    }
}
