using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using GunPracticeApplication.Models;

namespace GunPracticeApplication.Services
{
    public class DataService : IDataService
    {
        private readonly string _connectionString;

        public DataService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString;
        }

        public async Task<List<Scenario>> GetScenariosAsync()
        {
            var scenarios = new List<Scenario>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new MySqlCommand("SELECT scenario_id, scenario_name FROM scenario_db.scenario", connection);
                var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    scenarios.Add(new Scenario
                    {
                        ScenarioId = reader.GetInt32(0),
                        ScenarioName = reader.GetString(1)
                    });
                }
            }
            return scenarios;
        }

        public async Task<List<ScenarioDetail>> GetScenarioDetailsAsync(int scenarioId)
        {
            var details = new List<ScenarioDetail>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new MySqlCommand("SELECT detail_id, scenario_id, detail_no, detail_title, detail_content FROM scenario_db.scenario_detail WHERE scenario_id = @scenarioId", connection);
                command.Parameters.AddWithValue("@scenarioId", scenarioId);
                var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    details.Add(new ScenarioDetail
                    {
                        DetailId = reader.GetInt32(0),
                        ScenarioId = reader.GetInt32(1),
                        DetailNo = reader.GetInt32(2),
                        DetailTitle = reader.GetString(3),
                        DetailContent = reader.GetString(4)
                    });
                }
            }
            return details;
        }

        public async Task<List<Question>> GetQuestionsAsync(int scenarioId)
        {
            var questions = new List<Question>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new MySqlCommand("SELECT question_id, scenario_id, question_score, question_title, question_answer, question_content1, question_content2, question_content3, question_content4 FROM scenario_db.question WHERE scenario_id = @scenarioId", connection);
                command.Parameters.AddWithValue("@scenarioId", scenarioId);
                var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    questions.Add(new Question
                    {
                        QuestionId = reader.GetInt32(0),
                        ScenarioId = reader.GetInt32(1),
                        QuestionScore = reader.GetInt32(2),
                        QuestionTitle = reader.GetString(3),
                        QuestionAnswer = reader.GetInt32(4),
                        QuestionContent1 = reader.GetString(5),
                        QuestionContent2 = reader.GetString(6),
                        QuestionContent3 = reader.GetString(7),
                        QuestionContent4 = reader.GetString(8)
                    });
                }
            }
            return questions;
        }
        
        public async Task<string> GetScenarioNameAsync(int scenarioId)
        {
            string scenarioName = null;
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new MySqlCommand("SELECT scenario_name FROM scenario_db.scenario WHERE scenario_id = @scenarioId", connection);
                command.Parameters.AddWithValue("@scenarioId", scenarioId);
                var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    scenarioName = reader.GetString(0);
                }
            }
            return scenarioName;
        }

        public async Task<Standard> GetStandardAsync(int scenarioId)
        {
            Standard standard = null;
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new MySqlCommand("SELECT scenario_id, standard_pass FROM scenario_db.standard WHERE scenario_id = @scenarioId", connection);
                command.Parameters.AddWithValue("@scenarioId", scenarioId);
                var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    standard = new Standard
                    {
                        ScenarioId = reader.GetInt32(0),
                        StandardPass = reader.GetInt32(1)
                    };
                }
            }
            return standard;
        }
    }
}
