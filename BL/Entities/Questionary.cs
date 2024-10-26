namespace BL.Entities
{
    public class Questionary
    {
        public required string Category { get; set; }
        
        public int Level { get; set; }
        
        public List<Question>? Questions { get; set; }
    }
}
