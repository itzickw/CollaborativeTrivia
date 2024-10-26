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
        string AnswerFile = "Answer.xml";
        public void AddAnswer(Answer a)
        {
            a.Id = GetEntityId("answer");
            
            List<Answer> list = XmlTools.LoadListFromXMLSerializer<Answer>(AnswerFile);
            list.Add(a);
            XmlTools.SaveListToXMLSerializer(list, AnswerFile);            
        }

        public List<Answer> GetAnswersList()
            => XmlTools.LoadListFromXMLSerializer<Answer>(AnswerFile).ToList();

        public void UpdateAnswer(Answer answer)
        {
            List<Answer> answers = GetAnswersList();
            Answer a = answers.First(x => x.Id == answer.Id);
            answers[answers.IndexOf(a)] = answer;
            XmlTools.SaveListToXMLSerializer(answers, AnswerFile);
        }

        public void DeleteAnswer(int id)
        {
            List<Answer> answers = GetAnswersList();
            answers.Remove(GetAnswer(id));
            XmlTools.SaveListToXMLSerializer(answers, AnswerFile);            
        }

        private Answer GetAnswer(int id)
            => GetAnswersList().Find(x => x.Id == id);
    }
}
