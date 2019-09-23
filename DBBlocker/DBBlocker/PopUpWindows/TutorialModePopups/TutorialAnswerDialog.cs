using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DBBlocker
{
    /// <summary>
    /// Interaction logic for TutorialCorrectAnswerDialog.xaml
    /// </summary>
    public partial class TutorialAnswerDialog : Window
    {
        public TutorialAnswerDialog()
        {
            InitializeComponent();
        }

        public bool PrepareCorrectAnswerDialog(int currentSequence, int totalSteps)
        {
            FeedbackImage.Source = new BitmapImage(new Uri(@"/Shapes/correct.png", UriKind.Relative));
            if (currentSequence >= totalSteps)
            {
                Feedback.Text = (string)Application.Current.Resources["TutorialFinishedText"];
                return true;
            }
            else
            {
                Feedback.Text = (string)Application.Current.Resources["CorrectAnswerText"];
                return false;
            }
        }

        public void PrepareIncorrectAnswerDialog(string expectedAnswer)
        {
            Feedback.Text = (string)Application.Current.Resources["IncorrectAnswerText"];
            FeedbackImage.Source = new BitmapImage(new Uri(@"/Shapes/incorrect.png", UriKind.Relative));
            Answer.Text = "The expected query is: " + expectedAnswer;
            AnswerBtn.Visibility = Visibility.Visible;
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void AnswerBtn_Click(object sender, RoutedEventArgs e)
        {
            Feedback.Visibility = Visibility.Hidden;
            Answer.Visibility = Visibility.Visible;
        }
    }
}
