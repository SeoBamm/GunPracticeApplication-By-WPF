using GunPracticeApplication.Commands;
using System;
using System.Windows.Input;
using GunPracticeApplication.Services;

namespace GunPracticeApplication.ViewModels
{
    public class ExamPageViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;

        public ExamPageViewModel(IDataService dataService)
        {
            // _dataService = dataService;
            //
            // NextQuestionCommand = new RelayCommand(NextQuestion, CanNextQuestion);
            // SubmitExamCommand = new RelayCommand(SubmitExam);
        }

        public ICommand NextQuestionCommand { get; private set; }
        public ICommand SubmitExamCommand { get; private set; }

        private void NextQuestion()
        {
            // Implement logic to load next question
        }

        private bool CanNextQuestion()
        {
            // Implement logic to determine if next question can be loaded
            return true;
        }

        private void SubmitExam()
        {
            // Implement logic to submit exam
        }
    }
}