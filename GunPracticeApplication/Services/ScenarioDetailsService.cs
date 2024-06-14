using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GunPracticeApplication.Models;

namespace GunPracticeApplication.Services
{
    public class ScenarioDetailsService
    {
        private readonly IDataService _dataService;

        public ScenarioDetailsService()
        {
            _dataService = new DataService(); // 데이터 서비스 인스턴스 생성
        }

        public async Task<List<Scenario>> GetScenariosAsync()
        {
            try
            {
                return await _dataService.GetScenariosAsync();
            }
            catch (Exception ex)
            {
                // 예외 처리 필요: 데이터베이스 접근 오류 등
                Console.WriteLine($"Error fetching scenarios: {ex.Message}");
                return new List<Scenario>(); // 또는 예외를 throw할 수 있음
            }
        }

        public async Task<List<ScenarioDetail>> GetScenarioDetailsAsync(int scenarioId)
        {
            try
            {
                return await _dataService.GetScenarioDetailsAsync(scenarioId);
            }
            catch (Exception ex)
            {
                // 예외 처리 필요: 데이터베이스 접근 오류 등
                Console.WriteLine($"Error fetching scenario details: {ex.Message}");
                return new List<ScenarioDetail>(); // 또는 예외를 throw할 수 있음
            }
        }
    }
}