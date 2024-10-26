namespace BL.Entities
{
    public class User
    {
        public required string Name { get; set; }
        
        public int Password { get; set; }
        
        public bool IsAdmin { get; set; }
        
        public List<State>? States { get; set; }
        
        public List<string>? UserCategories { get; set; }
    }
}
