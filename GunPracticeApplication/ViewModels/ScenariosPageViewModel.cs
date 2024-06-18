using System.Collections.ObjectModel;
using System.ComponentModel;
using GunPracticeApplication.Models;
using GunPracticeApplication.Services;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace GunPracticeApplication.ViewModels
{
    public class ScenariosPageViewModel : INotifyPropertyChanged
    {
        private readonly DataService _dataService;
        private int _scenarioId;
        private ObservableCollection<ScenarioDetail> _scenarioDetails;
        private ScenarioDetail _selectedDetail;
        private string _scenarioName;
        private BitmapImage _imageSource;

        public ObservableCollection<ScenarioDetail> ScenarioDetails
        {
            get => _scenarioDetails;
            set
            {
                _scenarioDetails = value;
                OnPropertyChanged(nameof(ScenarioDetails));
            }
        }

        public ScenarioDetail SelectedDetail
        {
            get => _selectedDetail;
            set
            {
                _selectedDetail = value;
                OnPropertyChanged(nameof(SelectedDetail));
                OnPropertyChanged(nameof(SelectedDetailTitle));
                OnPropertyChanged(nameof(SelectedDetailContent));
                LoadScenarioImage(SelectedDetail.ScenarioId, SelectedDetail.DetailNo);
            }
        }

        public string SelectedDetailTitle => SelectedDetail?.DetailTitle;
        public string SelectedDetailContent => SelectedDetail?.DetailContent;

        public string ScenarioName
        {
            get => _scenarioName;
            set
            {
                _scenarioName = value;
                OnPropertyChanged(nameof(ScenarioName));
            }
        }

        public BitmapImage ImageSource
        {
            get => _imageSource;
            set
            {
                _imageSource = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }

        public ScenariosPageViewModel(int scenarioId)
        {
            _dataService = new DataService();
            _scenarioId = scenarioId;
            LoadScenarioDetails();
            LoadScenarioName();
        }

        private async void LoadScenarioDetails()
        {
            try
            {
                var details = await _dataService.GetScenarioDetailsAsync(_scenarioId);
                ScenarioDetails = new ObservableCollection<ScenarioDetail>(details);
                SelectedDetail = ScenarioDetails.Count > 0 ? ScenarioDetails[0] : null; // 초기 선택
            }
            catch (Exception ex)
            {
                // 예외 처리 필요
                Console.WriteLine($"Error loading scenario details: {ex.Message}");
            }
        }

        private async void LoadScenarioImage(int scenarioId, int DetailNo)
        {
            if (SelectedDetail != null)
            {
                try
                {
                    Uri resourceUri = new Uri($"pack://application:,,,/GunPracticeApplication;component/Images/img_{scenarioId}{DetailNo}.png");
                    ImageSource = new BitmapImage(resourceUri);
                }
                catch (Exception ex)
                {
                    // 예외 처리 필요
                    Console.WriteLine($"Error loading scenario image: {ex.Message}");
                    ImageSource = null;
                }
            }
        }

        private async void LoadScenarioName()
        {
            try
            {
                ScenarioName = await _dataService.GetScenarioNameAsync(_scenarioId);
            }
            catch (Exception ex)
            {
                // 예외 처리 필요
                Console.WriteLine($"Error loading scenario name: {ex.Message}");
            }
        }

        // INotifyPropertyChanged 인터페이스 구현
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
