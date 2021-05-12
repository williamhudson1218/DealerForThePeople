using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Username: {Username}");
            sb.Append(Environment.NewLine); 
            sb.Append($"Date: {Date.ToString("MM/dd/yyyy")}");
            sb.Append(Environment.NewLine);
            sb.Append($"Rating: {Rating/10}/5");
            sb.Append(Environment.NewLine);
            sb.Append($"Title: {Title}");
            sb.Append(Environment.NewLine);
            sb.Append($"Body: {Body}");
            sb.Append(Environment.NewLine);
            sb.Append($"Score: {Score}");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            sb.Append(Environment.NewLine);
            return sb.ToString();
        }
    }
}
