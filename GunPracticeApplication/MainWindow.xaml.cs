using System.Windows;

namespace GunPracticeApplication
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // 시작 시 MainPage를 로드합니다.
            MainFrame.Navigate(new Views.MainPage());
        }
    }
}