using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.DlXml
{
    internal partial class DlXml : DlInterface
    {
        string userFile = "User.xml";

        public void AddUser(Entities.User user)
        {            
            List<DL.Entities.User> users = XmlTools.LoadListFromXMLSerializer<Entities.User>(userFile);
            users.Add(user);
            XmlTools.SaveListToXMLSerializer(users, userFile);
        }

        public List<Entities.User> GetUsers() 
            => [.. XmlTools.LoadListFromXMLSerializer<Entities.User>(userFile)];
    }
}
