using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookStoreDATA;

namespace UnitTestProject
{
    [TestClass]
    public class FeedbackDialogViewModelTests
    {
        [TestMethod]
        public void SubmitFeedback_ReturnsFalse_WhenFeedbackTextIsNullOrWhiteSpace()
        {
            var viewModel = new FeedbackDialogViewModel();

            viewModel.FeedbackText = null;
            Assert.IsFalse(viewModel.SubmitFeedback());

            viewModel.FeedbackText = string.Empty;
            Assert.IsFalse(viewModel.SubmitFeedback());

            viewModel.FeedbackText = " ";
            Assert.IsFalse(viewModel.SubmitFeedback());
        }

        [TestMethod]
        public void SubmitFeedback_ReturnsTrue_WhenFeedbackTextIsValid()
        {
            var viewModel = new FeedbackDialogViewModel();
            viewModel.FeedbackText = "This is my feedback.";
            Assert.IsTrue(viewModel.SubmitFeedback());
        }
    }
}
