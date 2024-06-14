using System;
using System.Windows;
using System.Windows.Input;
using GunPracticeApplication.Commands;
using GunPracticeApplication.ViewModels;
using GunPracticeApplication.Views;

namespace GunPracticeApplication.Views
{
    public partial class MainPage : Window
    {
        public MainPage()
        {
            InitializeComponent();
            DataContext = new MainPageViewModel(); // MainPageViewModel을 데이터 컨텍스트로 설정
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
            ScenariosPage scenariosPage = new ScenariosPage(parameter); // ScenariosPage를 생성하고 매개변수 전달
            scenariosPage.Show();
        }

        private void NavigateToExamPage(int parameter)
        {
            ExamPage examPage = new ExamPage(parameter); // ExamPage를 생성하고 매개변수 전달
            examPage.Show();
        }
    }
}