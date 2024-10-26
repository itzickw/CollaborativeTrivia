namespace BL.Entities
{
    public class Question
    {
        public int Id { get; set; }

        public required string Category { get; set; }

        public int Level { get; set; }

        public required string QuestionContent { get; set; }

        public required string AnswerContent { get; set; }

        public bool IsClosedQuestion { get; set; }

        public List<Answer>? OptionalAnswers { get; set; }
    }
}
