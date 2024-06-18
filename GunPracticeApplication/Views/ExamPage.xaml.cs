using GunPracticeApplication.Models;
using GunPracticeApplication.Services;
using GunPracticeApplication.ViewModels;
using System.Windows;

namespace GunPracticeApplication.Views
{
    public partial class ExamPage : Window
    {
        public ExamPage(int scenarioId)
        {
            InitializeComponent();
            DataContext = new ExamPageViewModel(new DataService(), scenarioId);
        }
    }
}