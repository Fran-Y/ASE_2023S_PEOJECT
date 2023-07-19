using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreGUI
{
    public class FeedbackDialogViewModel
    {
        public string FeedbackText { get; set; }

        public bool SubmitFeedback()
        {
            if (string.IsNullOrWhiteSpace(FeedbackText))
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
