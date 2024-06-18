using System.Windows;
using GunPracticeApplication.ViewModels;

namespace GunPracticeApplication.Views
{
    public partial class ExamResultPage : Window
    {
        public ExamResultPage(ExamResultPageViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }

}