using BL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Logic
{
    internal partial class BlObject : BlInterface
    {
        public Answer GetAnswer(int id)
        {
            DL.Entities.Answer answer = dl.GetAnswersList().Find(x => x.Id == id);
            Answer answer2 = new Answer()
            {
                Id = answer.Id,
                QuestionId = answer.QuestionId,
                AnswerContent = answer.Content,
            };
            return answer2;
        }
    }
}
