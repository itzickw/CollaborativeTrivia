using BL.Entities;

namespace BL.Logic
{
    internal partial class BlObject : BlInterface
    {
        public void UpdateUserQuestionState(string name, int questionId)
            => dl.AddUserQuestionState(name, questionId);

        private State GetCategoryState(string name, string category)
        {
            List<DL.Entities.UserQuestionState> userAnswerdQuestions = dl
                .GetUserQuestionStates()
                .FindAll(x => x.UserName == name);
            State state = new()
            {
                Category = category,
                Level = GetUserLevel(userAnswerdQuestions, category),
                AnswerdQuestion = GetAnswerdQuestions(userAnswerdQuestions, category)
            };

            return state;
        }

        private List<Question> GetAnswerdQuestions(List<DL.Entities.UserQuestionState> userAnswerdQuestions, string category)        
            => GetQuestionsList().
                FindAll(x => x.Category == category && userAnswerdQuestions.Any(y => y.QuestionId == x.Id));
        
        private int GetUserLevel(List<DL.Entities.UserQuestionState> userAnswerdQuestions, string category)
        {
            List<DL.Entities.Question> categoryQuestions = dl.GetQuestionsList().FindAll(x => x.Category == category);
            int highLevel = dl.GetLevelColorsList().Count - 1;
            for (int i = 0; i <= highLevel; i++)
            {
                List<int> levelQuestions = categoryQuestions.FindAll(x => x.Level == i).Select(x => x.Id).ToList();
                foreach (int qId in levelQuestions)
                    if (!userAnswerdQuestions.Any(x => x.QuestionId == qId))
                        return i;
            }

            return highLevel;
        }
    }
}