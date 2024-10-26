using DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public interface DlInterface
    {
        void AddAnswer(Answer a);
        void AddCategory(Category category);
        int AddQuestion(Question q);
        void AddUser(User user);       
        void AddUserQuestionState(string userName, int questionId);         
        void DefineLevelColor(int level, string color);
        void UpdateQuestion(Question q);
        void UpdateAnswer(Answer answer);
        void DeleteQuestion(int id);
        void DeleteAnswer(int id);
        List<Answer> GetAnswersList();
        List<string> GetCategoriesList();
        List<string> GetLevelColorsList();
        List<Question> GetQuestionsList();
        List<User> GetUsers();
        List<UserQuestionState> GetUserQuestionStates();
        List<Category> GetUserCategoryList();
    }
}
