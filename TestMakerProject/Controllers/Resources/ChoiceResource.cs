namespace TestMakerProject.Controllers.Resources
{
    public class ChoiceResource
    {
        public int ChoiceId { get; set; }
        public string ChoiceText { get; set; }
        public bool IsAnswer { get; set; }
        public bool IsUsersAnswerCheck { get; set; }
        public int IsUsersAnswerRadio { get; set; }
    }
}
