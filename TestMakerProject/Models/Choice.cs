using System.ComponentModel.DataAnnotations.Schema;

namespace TestMakerProject.Models
{
    public class Choice
    {
        public int ChoiceId { get; set; }
        public string ChoiceText { get; set; }
        public bool IsAnswer { get; set; }
        [NotMapped]
        public bool IsUsersAnswerCheck { get; set; }
        [NotMapped]
        public int IsUsersAnswerRadio { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}