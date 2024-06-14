using System;
using System.Windows.Input;
using GunPracticeApplication.Commands;
using GunPracticeApplication.Views;

namespace GunPracticeApplication.ViewModels
{
    public class MainPageViewModel
    {
        public ICommand ViewScenariosCommand1 { get; private set; }
        public ICommand ViewScenariosCommand2 { get; private set; }
        public ICommand ViewScenariosCommand3 { get; private set; }

        public ICommand ViewTestCommand1 { get; private set; }
        public ICommand ViewTestCommand2 { get; private set; }
        public ICommand ViewTestCommand3 { get; private set; }

        public MainPageViewModel()
        {
            ViewScenariosCommand1 = new RelayCommand(param => ExecuteViewScenariosCommand1(param));
            ViewScenariosCommand2 = new RelayCommand(param => ExecuteViewScenariosCommand2(param));
            ViewScenariosCommand3 = new RelayCommand(param => ExecuteViewScenariosCommand3(param));

            ViewTestCommand1 = new RelayCommand(param => ExecuteViewTestCommand1(param));
            ViewTestCommand2 = new RelayCommand(param => ExecuteViewTestCommand2(param));
            ViewTestCommand3 = new RelayCommand(param => ExecuteViewTestCommand3(param));
        }

        private void ExecuteViewScenariosCommand1(object parameter)
        {
            int param = 1; // 매개변수 설정 (예: 1)
            NavigateToScenariosPage(param);
        }

        private void ExecuteViewScenariosCommand2(object parameter)
        {
            int param = 2; // 매개변수 설정 (예: 2)
            NavigateToScenariosPage(param);
        }

        private void ExecuteViewScenariosCommand3(object parameter)
        {
            int param = 3; // 매개변수 설정 (예: 3)
            NavigateToScenariosPage(param);
        }

        private void ExecuteViewTestCommand1(object parameter)
        {
            int param = 1; // 매개변수 설정 (예: 1)
            NavigateToExamPage(param);
        }

        private void ExecuteViewTestCommand2(object parameter)
        {
            int param = 2; // 매개변수 설정 (예: 2)
            NavigateToExamPage(param);
        }

        private void ExecuteViewTestCommand3(object parameter)
        {
            int param = 3; // 매개변수 설정 (예: 3)
            NavigateToExamPage(param);
        }

        private void NavigateToScenariosPage(int parameter)
        {
            // ScenariosPage를 생성하고 매개변수 전달
            ScenariosPage scenariosPage = new ScenariosPage(parameter);
            scenariosPage.Show();
        }

        private void NavigateToExamPage(int parameter)
        {
            // ExamPage를 생성하고 매개변수 전달
            ExamPage examPage = new ExamPage(parameter);
            examPage.Show();
        }
    }
}
