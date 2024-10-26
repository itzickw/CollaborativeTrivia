using DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.DlXml
{
    internal partial class DlXml : DlInterface
    {
        string QuestionFile = "Question.xml";
        public int AddQuestion(Question q)
        {
            List<Question> list = XmlTools.LoadListFromXMLSerializer<Question>(QuestionFile);
            if (!GetCategoriesList().Any(c => c == q.Category))
            {
                Console.WriteLine("not match ctegory");
                return -1;
            }
            if (q.Level >= GetLevelColorsList().Count)
            {
                Console.WriteLine("not match level");
                return -1;
            }

            q.Id = GetEntityId("question");
            list.Add(q);
            XmlTools.SaveListToXMLSerializer(list, QuestionFile);

            return q.Id;
        }

        public void UpdateQuestion(Question q)
        {
            List<Question> questions = GetQuestionsList();
            Question question = questions.First(x => x.Id == q.Id);
            questions[questions.IndexOf(question)] = q;
            XmlTools.SaveListToXMLSerializer(questions, QuestionFile);
        }

        public void DeleteQuestion(int id)
        {
            List<Question> questions = GetQuestionsList();
            questions.RemoveAll(q => q.Id == id);
            XmlTools.SaveListToXMLSerializer(questions, QuestionFile);

            List<UserQuestionState> userQuestions = GetUserQuestionStates();
            userQuestions.RemoveAll(u1 => u1.QuestionId == id);
            XmlTools.SaveListToXMLSerializer(userQuestions, UserQuestionStateFile);

            List<Answer> answers = GetAnswersList();
            answers.RemoveAll(a => a.QuestionId == id);
            XmlTools.SaveListToXMLSerializer(answers, AnswerFile);
        }

        public Question GetQuestion(int id)
            => XmlTools.LoadListFromXMLSerializer<Question>(QuestionFile).ToList().First(q => q.Id == id);

        public List<Question> GetQuestionsList()
            => [.. XmlTools.LoadListFromXMLSerializer<Question>(QuestionFile)];
    }
}
