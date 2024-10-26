namespace DL.Entities
{
    public class Answer
    {
        public string Content { get; set; }
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public override string ToString()
        {
            return Content + ", " + Id + ", " + QuestionId;
        }
    }
}
