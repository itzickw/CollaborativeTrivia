using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class DlFactory
    {
        public static DlInterface GetDl(string type)                                            
            => DL.DlXml.DlXml.GetInstance();
        
    }
}
