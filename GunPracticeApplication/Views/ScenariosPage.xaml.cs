using System.Windows;
using GunPracticeApplication.ViewModels;

namespace GunPracticeApplication.Views
{
    public partial class ScenariosPage : Window
    {
        private ScenariosPageViewModel _viewModel;

        public ScenariosPage(int scenarioId)
        {
            InitializeComponent();
            _viewModel = new ScenariosPageViewModel(scenarioId);
            DataContext = _viewModel;
        }
    }
}