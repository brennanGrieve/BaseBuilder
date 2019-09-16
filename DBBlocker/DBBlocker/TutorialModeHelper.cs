using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DBBlocker
{
    class TutorialModeHelper
    {

  
        
        private int currentTutorialFlag = -1;

        private string[] tutorialAnswers;

        private string[] tutorialHints;

        private int tutorialSequence;



        public int CurrentTutorialFlag { get => currentTutorialFlag; set => currentTutorialFlag = value; }

        public void CheckResult(string queryToCheck)
        {
            if (queryToCheck == tutorialAnswers[tutorialSequence])
            {
                //show an affirmative popup and move onto the next part of the tutorial
                tutorialSequence++;
            }
            else
            {
                //show a popup that offers hints + optionally, the answer
            }
        }

        public bool Active()
        {
            return (CurrentTutorialFlag >= 0);
        }

        public void PrepareTutorial(Window mainWindow)
        {
            tutorialSequence = 0;
            Button hintBtn = (Button)mainWindow.FindName("HintBtn");
            hintBtn.Visibility = Visibility.Visible;
            //set expected query + hints arrays
            tutorialAnswers = (string[])Application.Current.Resources["Tutorial" + currentTutorialFlag + "Answers"];
            tutorialHints = (string[])Application.Current.Resources["Tutorial" + currentTutorialFlag + "Hints"];
        }

        public void PrepareNextTutorialStep()
        {

        }

        public string GetCurrentHint()
        {
            return tutorialHints[tutorialSequence];
        }

    }
}
