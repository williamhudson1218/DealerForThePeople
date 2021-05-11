using Microsoft.VisualStudio.TestTools.UnitTesting;
using DealerForThePeople.Model;
using HtmlAgilityPack;
using DealerForThePeople;
using System;

namespace DealerForThePeopleTest
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void CalculateScoreEmptyReviewTest()
        {
            Review review = new();
            Assert.AreEqual(review.Score, 0);
        }

        [TestMethod]
        public void CalculateScoreTest()
        {
            //1179 characters = 11
            //9 exclamations = 3
            //5 rating = 10
            //word love = 2
            //total 26
            Review review = new();
            review.Body = "This is the 2nd vehicle I have purchased from McKaig Chevrolet Buick, and I wouldn’t purchase a vehicle anywhere else!  Their service department has taken care of me exceptionally several times, especially Patrick Evans!  He is so friendly and so knowledgeable.  I had a small wreck in my 2018 Chevy Traverse that I purchased from McKaig, and Patrick recommended Adrian Cortes to sell me a new vehicle on the spot instead of fixing my Traverse and I took the bait willingly!  LOL !!!  I knew that Adrian would put me in a reliable vehicle that would fit my needs, including 4-wheel drive, having to fit a 6’4” man in the front passenger seat and 2 big car seats in the back, not to mention TONS of room in the back for all my junk and groceries.  Needless to say, Adrian  fixed me up in a 2018 4x4 Jeep Grand Cherokee that is loaded to the max and is WAY sportier and more luxurious than what I had before.  I told Adrian what I wanted in a vehicle and he took me right to it, where I immediately fell in love with the All-Black look, even the 20” wheels.  I feel like a Cool Mom now thanks to Adrian!  I will DEFINITELY purchase my next vehicle from him!  I couldn’t be happier!";
            review.Rating = 50;

            Assert.AreEqual(review.Score, 25);
        }

        [TestMethod]
        public void ParseHtml()
        {
            HtmlDocument doc = new();
            doc.Load("../../../Review.html");

            Review review = Helpers.ParseReview(doc);
            Assert.IsNotNull(review);
        }

        [TestMethod]
        public void ParseEmptyHtml()
        {
            HtmlDocument doc = new();
            Review review = Helpers.ParseReview(doc);
            Assert.IsNull(review);
        }

        [TestMethod]
        public void ParseBadRating()
        {
            string ratingHtml = "asdfasdf pull-right";
            Assert.IsTrue(Helpers.ParseRating(ratingHtml) == 0);
        }
    }
}
