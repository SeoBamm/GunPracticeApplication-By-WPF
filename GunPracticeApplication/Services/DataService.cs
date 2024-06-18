using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using System.Windows;
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
                var commandText = "SELECT scenario_id, scenario_name FROM scenario_db.scenario";
                var command = new MySqlCommand(commandText, connection);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        scenarios.Add(new Scenario
                        {
                            ScenarioId = reader.GetInt32("scenario_id"),
                            ScenarioName = reader.GetString("scenario_name")
                        });
                    }
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
                var commandText = "SELECT detail_id, scenario_id, detail_no, detail_title, detail_content FROM scenario_db.scenario_detail WHERE scenario_id = @scenarioId";
                var command = new MySqlCommand(commandText, connection);
                command.Parameters.AddWithValue("@scenarioId", scenarioId);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        details.Add(new ScenarioDetail
                        {
                            DetailId = reader.GetInt32("detail_id"),
                            ScenarioId = reader.GetInt32("scenario_id"),
                            DetailNo = reader.GetInt32("detail_no"),
                            DetailTitle = reader.GetString("detail_title"),
                            DetailContent = reader.GetString("detail_content")
                        });
                    }
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
                var commandText = "SELECT question_id, scenario_id, question_score, question_title, question_answer, question_content1, question_content2, question_content3, question_content4 FROM scenario_db.question WHERE scenario_id = @scenarioId";
                var command = new MySqlCommand(commandText, connection);
                command.Parameters.AddWithValue("@scenarioId", scenarioId);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        questions.Add(new Question
                        {
                            QuestionId = reader.GetInt32("question_id"),
                            ScenarioId = reader.GetInt32("scenario_id"),
                            QuestionScore = reader.GetInt32("question_score"),
                            QuestionTitle = reader.GetString("question_title"),
                            QuestionAnswer = reader.GetInt32("question_answer"),
                            QuestionContent1 = reader.GetString("question_content1"),
                            QuestionContent2 = reader.GetString("question_content2"),
                            QuestionContent3 = reader.GetString("question_content3"),
                            QuestionContent4 = reader.GetString("question_content4")
                        });
                    }
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
                var commandText = "SELECT scenario_name FROM scenario_db.scenario WHERE scenario_id = @scenarioId";
                var command = new MySqlCommand(commandText, connection);
                command.Parameters.AddWithValue("@scenarioId", scenarioId);
                var result = await command.ExecuteScalarAsync();
                if (result != null)
                {
                    scenarioName = result.ToString();
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
                var commandText = "SELECT scenario_id, standard_pass FROM scenario_db.standard WHERE scenario_id = @scenarioId";
                var command = new MySqlCommand(commandText, connection);
                command.Parameters.AddWithValue("@scenarioId", scenarioId);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        standard = new Standard
                        {
                            ScenarioId = reader.GetInt32("scenario_id"),
                            StandardPass = reader.GetInt32("standard_pass")
                        };
                    }
                }
            }
            return standard;
        }

        public async Task<int> GetStandardPassAsync(int scenarioId)
        {
            int standardPass = 0;
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var commandText = "SELECT standard_pass FROM scenario_db.standard WHERE scenario_id = @scenarioId";
                var command = new MySqlCommand(commandText, connection);
                command.Parameters.AddWithValue("@scenarioId", scenarioId);
                var result = await command.ExecuteScalarAsync();
                if (result != null && Int32.TryParse(result.ToString(), out standardPass))
                {
                    return standardPass;
                }
                else
                {
                    throw new Exception($"Failed to retrieve standard pass for scenarioId {scenarioId}");
                }
            }
        }
        
        // public async Task<string> GetScenarioImageAsync(int detailId)
        // {
        //     string base64String = null;
        //
        //     using (var connection = new MySqlConnection(_connectionString))
        //     {
        //         await connection.OpenAsync();
        //         var commandText = "SELECT detail_img FROM scenario_db.scenario_detail WHERE detail_id = @DetailId";
        //         var command = new MySqlCommand(commandText, connection);
        //         command.Parameters.AddWithValue("@DetailId", detailId);
        //
        //         var result = await command.ExecuteScalarAsync();
        //         if (result != DBNull.Value)
        //         {
        //             Console.WriteLine($"{result}");
        //             byte[] imageBytes = (byte[])result;
        //             base64String = Convert.ToBase64String(imageBytes);
        //             // MessageBox.Show(base64String);
        //         }
        //     }
        //
        //     return base64String;
        // }
    }
}
