namespace BL.Entities
{
    public class Answer
    {
        public int Id { get; set; }
       
        public int QuestionId { get; set; }
       
        public required string AnswerContent { get; set; }
    }
}
