using BL.Entities;

namespace BL.Logic
{
    internal partial class BlObject : BlInterface
    {
        public void AddUser(User user)
        {
            DL.Entities.User user1 = new DL.Entities.User()
            {
                Name = user.Name,
                Password = user.Password,
            };
            dl.AddUser(user1);
        }

        public User GetUser(string name)
        {
            DL.Entities.User u = dl.GetUsers().Find(x => x.Name == name)!;
            User user = new()
            {
                Name = u.Name,
                Password = u.Password,                
                States = GetUserStates(u.Name),
                UserCategories = dl
                    .GetUserCategoryList()
                    .FindAll(x => x.UserName == name)
                    .Select(y => y.CategoryName)
                    .ToList()
            };
            return user;
        }     

        private List<State> GetUserStates(string name)
        {
            return GetCategoriesList().Select(x=> GetCategoryState(name, x)).ToList();
        }

        public bool UserExist(string name, int password)
            => dl.GetUsers().Any(user => user.Name == name && user.Password == password);

        public bool UserNameExist(string name)
            => dl.GetUsers().Any(u => u.Name == name);
    }
}
