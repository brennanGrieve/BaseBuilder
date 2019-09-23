using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace DBBlocker
{
    class TutorialModeHelper
    {
        
        private int currentTutorialFlag = -1;

        private string[] tutorialAnswers;

        private string[] tutorialHints;

        private int tutorialSequence;




        public int CurrentTutorialFlag { get => currentTutorialFlag; set => currentTutorialFlag = value; }

        public void ProcessInput(string queryToCheck)
        {
            TutorialAnswerDialog answerDialog = new TutorialAnswerDialog();
            if (queryToCheck == tutorialAnswers[tutorialSequence])
            {
                PrepareNextTutorialStep();
                bool tutFinished = answerDialog.PrepareCorrectAnswerDialog(tutorialSequence, tutorialAnswers.Length);
                answerDialog.ShowDialog();
                if (!tutFinished) {
                    TutorialHintDialog nextHint = new TutorialHintDialog(GetCurrentHint());
                    nextHint.ShowDialog();
                }
            }
            else
            {
                answerDialog.PrepareIncorrectAnswerDialog(tutorialAnswers[tutorialSequence]);
                answerDialog.ShowDialog();
            }
        }

        public bool Active()
        {
            return (CurrentTutorialFlag >= 0);
        }

        public void PrepareTutorial()
        {
            tutorialSequence = 0;
            //set expected query + hints arrays
            tutorialAnswers = (string[])Application.Current.Resources["Tutorial" + currentTutorialFlag + "Answers"];
            tutorialHints = (string[])Application.Current.Resources["Tutorial" + currentTutorialFlag + "Hints"];
        }

        public void PrepareNextTutorialStep()
        {
            tutorialSequence++;
            if (tutorialSequence >= tutorialAnswers.Length)
            {
                currentTutorialFlag = -1;

            }
       
        }

        public string GetCurrentHint()
        {
            if (currentTutorialFlag >= 0)
            {
                return tutorialHints[tutorialSequence];
            }
            else
            {
                return "out of bounds";
            }
        }

    }
}
