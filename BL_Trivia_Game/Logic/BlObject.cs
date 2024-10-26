namespace BL.Logic
{
    internal partial class BlObject : BlInterface
    {
        private static object _locker = new();
        private static BlObject? _instance = null;
        private DL.DlInterface dl = DL.DlFactory.GetDl("xml");

        private BlObject() { }

        public static BlObject GetInstance()
        {
            if (_instance == null)
            {
                lock (_locker)
                {
                    if (_instance == null)
                        _instance = new BlObject();
                }
            }

            return _instance;
        }

        public void AddCategory(string categoryName, string userName)
        {
            if (dl.GetCategoriesList().Any(x => x == categoryName))
                return;

            DL.Entities.Category category = new DL.Entities.Category
            { 
                CategoryName = categoryName, 
                UserName = userName 
            };
            dl.AddCategory(category);
        }

        public void DefineLevelColor(int level, string color)
            => dl.DefineLevelColor(level, color);

        public List<string> GetCategoriesList()
            => dl.GetCategoriesList().ToList();

        public List<string> GetLevelColorsList()
            => dl.GetLevelColorsList().ToList();
    }
}
