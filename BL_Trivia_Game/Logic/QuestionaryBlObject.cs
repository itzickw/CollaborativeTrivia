using BL.Entities;

namespace BL.Logic
{
    internal partial class BlObject : BlInterface
    {
        public Questionary GetQuestionary(string category, int level)
        {
            Questionary questionary = new()
            { 
                Category = category, 
                Level = level,
                Questions = GetQuestionsList().FindAll(x => x.Category == category && x.Level == level)
            };
            return questionary;
        }
    }
}
