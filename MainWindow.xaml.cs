using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace game_quiz_demo_cs
{
    public partial class MainWindow : Window
    {
        private List<QuizQuestion> quizQuestions;
        private int currentQuestionIndex = 0;
        private int score = 0;
        private Button selectedAnswerButton = null;

        public MainWindow()
        {
            InitializeComponent();
            LoadQuizData();
            DisplayCurrentQuestion();
        }

        private void LoadQuizData()
        {
            quizQuestions = new List<QuizQuestion>
            {
                new QuizQuestion
                {
                    Question = "Was ISEC nice on 12th June?",
                    CorrectChoice = "It was not nice",
                    Choices = new List<string> { "Was fair", "Was nice", "Was not bad" }
                },
                new QuizQuestion
                {
                    Question = "When is the POE due?",
                    CorrectChoice = "27th June",
                    Choices = new List<string> { "27th May", "27th Dec", "27th Jan" }
                }
            };
        }

        private void DisplayCurrentQuestion()
        {
            if (currentQuestionIndex >= quizQuestions.Count)
            {
                MessageBox.Show("Game over! Final score: " + score, "Quiz Complete", MessageBoxButton.OK, MessageBoxImage.Information);
                ResetGame();
                return;
            }

            selectedAnswerButton = null;

            var question = quizQuestions[currentQuestionIndex];
            questionTextBlock.Text = question.Question;

            var shuffledChoices = question.Choices.OrderBy(_ => Guid.NewGuid()).ToList();
            shuffledChoices.Insert(new Random().Next(0, 4), question.CorrectChoice);

            firstChoiceButton.Content = shuffledChoices[0];
            secondChoiceButton.Content = shuffledChoices[1];
            thirdChoiceButton.Content = shuffledChoices[2];
            fourthChoiceButton.Content = shuffledChoices[3];

            ResetAnswerButtonStyles();
        }

        private void ResetAnswerButtonStyles()
        {
            foreach (var button in new[] { firstChoiceButton, secondChoiceButton, thirdChoiceButton, fourthChoiceButton })
            {
                button.Background = Brushes.LightGray;
            }
        }

        private void HandleAnswerSelection(object sender, RoutedEventArgs e)
        {
            selectedAnswerButton = sender as Button;

            if (selectedAnswerButton == null)
                return;

            string selected = selectedAnswerButton.Content.ToString();
            string correct = quizQuestions[currentQuestionIndex].CorrectChoice;

            if (selected == correct)
            {
                selectedAnswerButton.Background = Brushes.Green;
            }
            else
            {
                selectedAnswerButton.Background = Brushes.DarkRed;
            }
        }

        private void HandleNextQuestion(object sender, RoutedEventArgs e)
        {
            if (selectedAnswerButton == null)
            {
                MessageBox.Show("Please select an answer.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string selected = selectedAnswerButton.Content.ToString();
            string correct = quizQuestions[currentQuestionIndex].CorrectChoice;

            if (selected == correct)
            {
                score++;
                scoreTextBlock.Text = "Score: " + score;
            }

            currentQuestionIndex++;
            DisplayCurrentQuestion();
        }

        private void ResetGame()
        {
            score = 0;
            currentQuestionIndex = 0;
            scoreTextBlock.Text = "Play the game to begin";
            DisplayCurrentQuestion();
        }
    }
}
