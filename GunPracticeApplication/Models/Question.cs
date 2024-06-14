namespace GunPracticeApplication.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public int ScenarioId { get; set; }
        public int QuestionScore { get; set; }
        public string QuestionTitle { get; set; }
        public int QuestionAnswer { get; set; }
        public string QuestionContent1 { get; set; }
        public string QuestionContent2 { get; set; }
        public string QuestionContent3 { get; set; }
        public string QuestionContent4 { get; set; }
    }
}