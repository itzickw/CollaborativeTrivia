using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Entities
{
    public class Question
    {
        public int Id { get; set; }        
        public string QuestionContent { get; set; }
        public string EnswerContent { get; set; }
        public string Category { get; set; }
        public int Level { get; set; }
        public bool IsClosedQuestion { get; set; }
        public override string ToString()
        {
            return QuestionContent + ", " + EnswerContent  + ", " + Id + ", " +IsClosedQuestion;
        }

    }
}
