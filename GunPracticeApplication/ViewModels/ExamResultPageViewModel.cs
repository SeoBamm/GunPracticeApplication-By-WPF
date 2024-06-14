using System.Windows.Input;
using GunPracticeApplication.Commands;

namespace GunPracticeApplication.ViewModels
{
    public class ExamResultPageViewModel : ViewModelBase
    {
        private string examScoreMessage;
        public string ExamScoreMessage
        {
            get { return examScoreMessage; }
            set
            {
                examScoreMessage = value;
                OnPropertyChanged(nameof(ExamScoreMessage));
            }
        }

        public ICommand BackToMainPageCommand { get; private set; }

        public ExamResultPageViewModel(int examScore)
        {
            // Example: ExamScoreMessage = $"Your exam score: {examScore}";

            // BackToMainPageCommand = new RelayCommand(BackToMainPage);
        }

        private void BackToMainPage()
        {
            // Navigate back to MainPage
            // Example: NavigationService.Navigate(new MainPage());
        }
    }
}