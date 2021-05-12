using System;
using System.Collections.Generic;

namespace DealerForThePeople.Model
{
    public class Review
    {
        public string Username { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int Rating { get; set; }
        public DateTime Date { get; set; }
        public List<string> EmployeesWorkedWith { get; set; }
        public int Score { get; set; }
    }
}
