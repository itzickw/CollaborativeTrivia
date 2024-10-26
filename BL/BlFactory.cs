using BL.Logic;

namespace BL
{
    public class BlFactory
    {
        public static BlInterface GetBlInterface()        
            => BlObject.GetInstance();        
    }
}
