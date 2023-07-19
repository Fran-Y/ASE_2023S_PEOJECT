using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreDATA
{
    public class FeedbackDialogViewModel
    {
        public string FeedbackText { get; set; }

        public bool SubmitFeedback()
        {
            if (string.IsNullOrWhiteSpace(FeedbackText) || FeedbackText == "Please enter your feedback in here ^_^")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
