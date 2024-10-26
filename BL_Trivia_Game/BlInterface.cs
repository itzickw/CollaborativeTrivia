using BL.Entities;

namespace BL
{
    public interface BlInterface
    {                   
        void AddCategory(string categoryName, string userName);
        void DefineLevelColor(int level, string color);
        void AddUser(User user);
        void AddQuestion(Question qustion);
        void UpdateQuestion(Question question);        
        void UpdateUserQuestionState(string name, int questionId);        
        User GetUser(string name);
        Questionary GetQuestionary(string category, int level);              
        List<string> GetCategoriesList();
        List<string> GetLevelColorsList();        
        void DeleteQuestion(int id);
        bool UserExist(string text, int res);
        bool UserNameExist(string name);
    }
}
