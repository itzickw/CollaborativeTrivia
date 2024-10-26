namespace BL.Logic
{
    internal partial class BlObject : BlInterface
    {
        public void AddQuestion(Entities.Question qustion)
        {
            DL.Entities.Question q = new()
            {
                Category = qustion.Category,
                Level = qustion.Level,
                QuestionContent = qustion.QuestionContent,
                EnswerContent = qustion.AnswerContent,
                IsClosedQuestion = qustion.IsClosedQuestion,
            };
            qustion.Id = dl.AddQuestion(q);

            if (qustion.IsClosedQuestion)
            {
                foreach (Entities.Answer answer in qustion.OptionalAnswers!)
                {
                    DL.Entities.Answer a = new()
                    {
                        Content = answer.AnswerContent,
                        QuestionId = qustion.Id
                    };
                    dl.AddAnswer(a);
                }
            }
        }
        
        public void UpdateQuestion(Entities.Question question)
        {
            DL.Entities.Question q = new DL.Entities.Question() 
            {
                Id = question.Id,
                Category = question.Category,
                Level= question.Level,
                QuestionContent = question.QuestionContent,
                EnswerContent = question.AnswerContent,
                IsClosedQuestion= question.IsClosedQuestion,
            };
            dl.UpdateQuestion(q);

            if (question.IsClosedQuestion)
            {
                foreach(Entities.Answer a in question.OptionalAnswers!)
                {
                    DL.Entities.Answer answer = new DL.Entities.Answer()
                    {
                        Id = a.Id,
                        QuestionId = a.QuestionId,
                        Content = a.AnswerContent,
                    };
                    dl.UpdateAnswer(answer);
                }
            }
        }

        public void DeleteQuestion(int id)
        {
            dl.DeleteQuestion(id);           
        }

        private Entities.Question GetQuestion(int id)
        {
            DL.Entities.Question question = dl.GetQuestionsList().Find(x => x.Id == id)!;

            Entities.Question result = new()
            {
                Category = question.Category,
                Level = question.Level,
                Id = question.Id,
                QuestionContent = question.QuestionContent,
                AnswerContent = question.EnswerContent,
                IsClosedQuestion = question.IsClosedQuestion,
                OptionalAnswers = GetQuestionOptionalAnswersList(question.Id),
            };

            return result;
        }

        private List<Entities.Question> GetQuestionsList()
        {
            return dl.GetQuestionsList().Select(x => GetQuestion(x.Id)).ToList();
        }

        private List<Entities.Answer> GetQuestionOptionalAnswersList(int Qid)
        {
            return dl.GetAnswersList().Where(x => x.QuestionId == Qid).Select(x => GetAnswer(x.Id)).ToList();
        }
    }
}
