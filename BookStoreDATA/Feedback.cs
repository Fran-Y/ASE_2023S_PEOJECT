using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestLoginPage
{
    public class Feedback
    {
        public int UserID { get; set; }
        public string ISBN { get; set; }
        public string Content { get; set; }

        public Feedback(int userID, string isbn, string content)
        {
            UserID = userID;
            ISBN = isbn;
            Content = content;
        }
    }

}
