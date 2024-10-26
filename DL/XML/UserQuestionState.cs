using DL.DlXml;
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
        string UserQuestionStateFile = "UserQuestionStateFile.xml";

        public void AddUserQuestionState(string userName, int questionId)
        {
            List<UserQuestionState> list = XmlTools.LoadListFromXMLSerializer<UserQuestionState>(UserQuestionStateFile);
            list.Add(item: new UserQuestionState()
            {
                QuestionId = questionId,
                UserName = userName
            });
            XmlTools.SaveListToXMLSerializer(list, UserQuestionStateFile);
        }

        public List<Entities.UserQuestionState> GetUserQuestionStates()
        {
            return [.. XmlTools.LoadListFromXMLSerializer<UserQuestionState>(UserQuestionStateFile)];
        }
    }
}
