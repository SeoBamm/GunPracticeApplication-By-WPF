using GunPracticeApplication.Models;
using GunPracticeApplication.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GunPracticeApplication.Commands;
using GunPracticeApplication.Views;

namespace GunPracticeApplication.ViewModels
{
    public class ExamPageViewModel : INotifyPropertyChanged
    {
        private readonly IDataService _dataService;
        private int currentQuestionIndex = 0;
        private List<Question> questions;
        private int scenarioId;

        private int[] selectedAnswers;

        private int questionNo;
        private string questionTitle;
        private string answer1;
        private string answer2;
        private string answer3;
        private string answer4;

        public int QuestionNo
        {
            get => questionNo;
            set { questionNo = value; OnPropertyChanged(); }
        }

        public string QuestionTitle
        {
            get => questionTitle;
            set { questionTitle = value; OnPropertyChanged(); }
        }

        public string Answer1
        {
            get => answer1;
            set { answer1 = value; OnPropertyChanged(); }
        }

        public string Answer2
        {
            get => answer2;
            set { answer2 = value; OnPropertyChanged(); }
        }

        public string Answer3
        {
            get => answer3;
            set { answer3 = value; OnPropertyChanged(); }
        }

        public string Answer4
        {
            get => answer4;
            set { answer4 = value; OnPropertyChanged(); }
        }

        public ICommand NextQuestionCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ExamPageViewModel(IDataService dataService, int scenarioId)
        {
            _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
            this.scenarioId = scenarioId;
            NextQuestionCommand = new RelayCommand(async _ => await LoadNextQuestion());
            LoadQuestions();
        }

        private async void LoadQuestions()
        {
            try
            {
                questions = await _dataService.GetQuestionsAsync(scenarioId);
                selectedAnswers = new int[questions.Count];
                for (int i = 0; i < selectedAnswers.Length; i++)
                {
                    selectedAnswers[i] = -1; // 초기값을 -1로 설정하여 답안이 선택되지 않았음을 나타냅니다.
                }
                if (questions.Count > 0)
                {
                    currentQuestionIndex = 0;
                    LoadQuestion(currentQuestionIndex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading questions: " + ex.Message);
            }
        }

        private void LoadQuestion(int index)
        {
            if (questions == null || index < 0 || index >= questions.Count) return;

            var question = questions[index];
            QuestionNo = (question.QuestionId) - ((scenarioId - 1)*5);
            QuestionTitle = question.QuestionTitle;
            Answer1 = question.QuestionContent1;
            Answer2 = question.QuestionContent2;
            Answer3 = question.QuestionContent3;
            Answer4 = question.QuestionContent4;

            // 선택된 답안이 있으면 해당 RadioButton을 선택 상태로 변경
            int selectedAnswer = selectedAnswers[index];
            switch (selectedAnswer)
            {
                case 1:
                    IsAnswer1Selected = true;
                    break;
                case 2:
                    IsAnswer2Selected = true;
                    break;
                case 3:
                    IsAnswer3Selected = true;
                    break;
                case 4:
                    IsAnswer4Selected = true;
                    break;
                default:
                    // 선택된 답안이 없으면 모든 RadioButton을 선택 해제
                    IsAnswer1Selected = false;
                    IsAnswer2Selected = false;
                    IsAnswer3Selected = false;
                    IsAnswer4Selected = false;
                    break;
            }
        }

        private async Task LoadNextQuestion()
        {
            // 현재 문제의 답안을 저장
            SaveSelectedAnswer();

            // 다음 문제로 이동
            currentQuestionIndex++;
            if (currentQuestionIndex < questions.Count)
            {
                LoadQuestion(currentQuestionIndex);
            }
            else
            {
                // 모든 문제를 다 푼 경우, 결과 페이지로 이동
                ShowExamResultPage();
            }
        }

        private void SaveSelectedAnswer()
        {
            // 선택된 답안을 배열에 저장
            if (IsAnswer1Selected) selectedAnswers[currentQuestionIndex] = 1;
            else if (IsAnswer2Selected) selectedAnswers[currentQuestionIndex] = 2;
            else if (IsAnswer3Selected) selectedAnswers[currentQuestionIndex] = 3;
            else if (IsAnswer4Selected) selectedAnswers[currentQuestionIndex] = 4;
            else selectedAnswers[currentQuestionIndex] = -1; // 선택된 답안이 없는 경우
        }

        private void ShowSelectedAnswers()
        {
            string message = $"Scenario ID: {scenarioId}\n";
            for (int i = 0; i < selectedAnswers.Length; i++)
            {
                message += $"Question {i + 1}: Answer {selectedAnswers[i]}\n";
            }
            MessageBox.Show(message, "Selected Answers");
        }

        private bool isAnswer1Selected;
        public bool IsAnswer1Selected
        {
            get { return isAnswer1Selected; }
            set
            {
                if (isAnswer1Selected != value)
                {
                    isAnswer1Selected = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool isAnswer2Selected;
        public bool IsAnswer2Selected
        {
            get { return isAnswer2Selected; }
            set
            {
                if (isAnswer2Selected != value)
                {
                    isAnswer2Selected = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool isAnswer3Selected;
        public bool IsAnswer3Selected
        {
            get { return isAnswer3Selected; }
            set
            {
                if (isAnswer3Selected != value)
                {
                    isAnswer3Selected = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool isAnswer4Selected;
        public bool IsAnswer4Selected
        {
            get { return isAnswer4Selected; }
            set
            {
                if (isAnswer4Selected != value)
                {
                    isAnswer4Selected = value;
                    OnPropertyChanged();
                }
            }
        }
        private void ShowExamResultPage()
        {
            var selectedAnswersArray = selectedAnswers.ToArray();
            var resultViewModel = new ExamResultPageViewModel(_dataService, scenarioId, selectedAnswersArray);
            var resultPage = new ExamResultPage(resultViewModel);

            // 현재 창을 닫습니다.
            Application.Current.Windows.OfType<ExamPage>().FirstOrDefault()?.Close();

            // 결과 페이지를 보여줍니다.
            resultPage.Show();
        }

    }
}
