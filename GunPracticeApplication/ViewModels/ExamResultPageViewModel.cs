using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using GunPracticeApplication.Models;
using GunPracticeApplication.Services;

namespace GunPracticeApplication.ViewModels
{
    public class ExamResultPageViewModel : INotifyPropertyChanged
    {
        private readonly IDataService _dataService;

        private int scenarioId;
        private int[] selectedAnswers;

        private string answer1;
        private string answer2;
        private string answer3;
        private string answer4;
        private string answer5;

        private string answer1IsCorrect;
        private string answer2IsCorrect;
        private string answer3IsCorrect;
        private string answer4IsCorrect;
        private string answer5IsCorrect;

        private int score;
        private string isPassed;
        private int standardPass;

        public int ScenarioId
        {
            get => scenarioId;
            set { scenarioId = value; OnPropertyChanged(); }
        }

        public int[] SelectedAnswers
        {
            get => selectedAnswers;
            set { selectedAnswers = value; OnPropertyChanged(); }
        }

        public string Answer1
        {
            get => (answer1 == "-1") ? "x" : answer1;
            set { answer1 = value; OnPropertyChanged(); }
        }

        public string Answer2
        {
            get => (answer2 == "-1") ? "x" : answer2;
            set { answer2 = value; OnPropertyChanged(); }
        }

        public string Answer3
        {
            get => (answer3 == "-1") ? "x" : answer3;
            set { answer3 = value; OnPropertyChanged(); }
        }

        public string Answer4
        {
            get => (answer4 == "-1") ? "x" : answer4;
            set { answer4 = value; OnPropertyChanged(); }
        }

        public string Answer5
        {
            get => (answer5 == "-1") ? "x" : answer5;
            set { answer5 = value; OnPropertyChanged(); }
        }

        public string Answer1IsCorrect
        {
            get => answer1IsCorrect;
            set { answer1IsCorrect = value; OnPropertyChanged(); }
        }

        public string Answer2IsCorrect
        {
            get => answer2IsCorrect;
            set { answer2IsCorrect = value; OnPropertyChanged(); }
        }

        public string Answer3IsCorrect
        {
            get => answer3IsCorrect;
            set { answer3IsCorrect = value; OnPropertyChanged(); }
        }

        public string Answer4IsCorrect
        {
            get => answer4IsCorrect;
            set { answer4IsCorrect = value; OnPropertyChanged(); }
        }

        public string Answer5IsCorrect
        {
            get => answer5IsCorrect;
            set { answer5IsCorrect = value; OnPropertyChanged(); }
        }

        public int Score
        {
            get => score;
            set { score = value; OnPropertyChanged(); }
        }

        public string IsPassed
        {
            get => isPassed;
            set { isPassed = value; OnPropertyChanged(); }
        }

        public int StandardPass
        {
            get => standardPass;
            set { standardPass = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ExamResultPageViewModel(IDataService dataService, int scenarioId, int[] selectedAnswers)
        {
            _dataService = dataService;
            ScenarioId = scenarioId;
            SelectedAnswers = selectedAnswers;

            if (selectedAnswers.Length >= 1) Answer1 = selectedAnswers[0].ToString();
            if (selectedAnswers.Length >= 2) Answer2 = selectedAnswers[1].ToString();
            if (selectedAnswers.Length >= 3) Answer3 = selectedAnswers[2].ToString();
            if (selectedAnswers.Length >= 4) Answer4 = selectedAnswers[3].ToString();
            if (selectedAnswers.Length >= 5) Answer5 = selectedAnswers[4].ToString();

            Task.Run(async () =>
            {
                StandardPass = await _dataService.GetStandardPassAsync(ScenarioId);
                await LoadQuestionsAndCheckAnswers();
            });
        }

        private async Task LoadQuestionsAndCheckAnswers()
        {
            var questions = await _dataService.GetQuestionsAsync(ScenarioId);

            int tempScore = 0;

            if (questions.Count >= 1)
            {
                Answer1IsCorrect = Answer1 == questions[0].QuestionAnswer.ToString() ? "정답" : "오답";
                if (Answer1IsCorrect == "정답") tempScore += 20;
            }

            if (questions.Count >= 2)
            {
                Answer2IsCorrect = Answer2 == questions[1].QuestionAnswer.ToString() ? "정답" : "오답";
                if (Answer2IsCorrect == "정답") tempScore += 20;
            }

            if (questions.Count >= 3)
            {
                Answer3IsCorrect = Answer3 == questions[2].QuestionAnswer.ToString() ? "정답" : "오답";
                if (Answer3IsCorrect == "정답") tempScore += 20;
            }

            if (questions.Count >= 4)
            {
                Answer4IsCorrect = Answer4 == questions[3].QuestionAnswer.ToString() ? "정답" : "오답";
                if (Answer4IsCorrect == "정답") tempScore += 20;
            }

            if (questions.Count >= 5)
            {
                Answer5IsCorrect = Answer5 == questions[4].QuestionAnswer.ToString() ? "정답" : "오답";
                if (Answer5IsCorrect == "정답") tempScore += 20;
            }

            Score = tempScore;
            IsPassed = (tempScore >= StandardPass) ? "합격" : "불합격";
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
