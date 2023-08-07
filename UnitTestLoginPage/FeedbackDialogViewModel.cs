using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreGUI
{
    public class FeedbackDialogViewModel
    {
        // Add an ISBN property
        public string ISBN { get; set; }
        private string feedbackText;
        public string FeedbackText
        {
            get { return feedbackText; }
            set
            {
                if (feedbackText != value)
                {
                    feedbackText = value;
                   // OnPropertyChanged(nameof(FeedbackText));
                }
            }
        }
        // Removed the SubmitFeedback method because it's better suited in the controller
    }
}
