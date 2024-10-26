namespace BL.Entities
{
    public class State
    {
        public required string Category { get; set; }
        
        public int Level { get; set; }
        
        public List<Question>? AnswerdQuestion { get; set; }
    }
}
