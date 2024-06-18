using System.Collections.Generic;
using System.Threading.Tasks;
using GunPracticeApplication.Models;

namespace GunPracticeApplication.Services
{
    public interface IDataService
    {
        Task<List<Scenario>> GetScenariosAsync();
        Task<List<ScenarioDetail>> GetScenarioDetailsAsync(int scenarioId);
        Task<List<Question>> GetQuestionsAsync(int scenarioId);
        Task<Standard> GetStandardAsync(int scenarioId);
        Task<int> GetStandardPassAsync(int scenarioId);
        // Task<string> GetScenarioImageAsync(int detailId);
    }
}