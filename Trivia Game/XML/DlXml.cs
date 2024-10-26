using DL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.DlXml
{
    internal partial class DlXml : DlInterface
    {
        string CategoriesFile = "Categories.xml";
        string LevelsColorFile = "LevelsColor.xml";
        string IDsFile = "IDsFile.xml";

        protected static DlXml _instance = null;
        protected DlXml() { }
        public static DlXml GetInstance()
        {
            if (_instance == null)
                _instance = new DlXml();
            return _instance;
        }

        protected int GetEntityId(string EntityType)
        {
            int id = 0;
            List<string> types = ["question", "answer"];
            List<int> ids = XmlTools.LoadListFromXMLSerializer<int>(IDsFile);

            if (ids.Count == 0)
            {
                ids = [0, 0];
                ids[types.IndexOf(EntityType)] = 1;
                XmlTools.SaveListToXMLSerializer(ids, IDsFile);
                return id;
            }

            id = ids[types.IndexOf(EntityType)];
            ids[types.IndexOf(EntityType)]++;
            XmlTools.SaveListToXMLSerializer(ids, IDsFile);
            return id;
        }

        public void AddCategory(Category category)
        {
            List<Category> categories = XmlTools.LoadListFromXMLSerializer<Category>(CategoriesFile);

            if (categories.Any(c => c.CategoryName == category.CategoryName))
            {
                Console.WriteLine("הקטגוריה הזאת כבר קיימת");
                return;
            }
            categories.Add(category);
            XmlTools.SaveListToXMLSerializer(categories, CategoriesFile);
        }

        public void DefineLevelColor(int level, string color)
        {
            List<string> levelColor = XmlTools.LoadListFromXMLSerializer<string>(LevelsColorFile);

            if (level < levelColor.Count)
            {
                levelColor[level] = color;
                XmlTools.SaveListToXMLSerializer(levelColor, LevelsColorFile);
                return;
            }

            if (level == levelColor.Count)
            {
                levelColor.Add(color);
                XmlTools.SaveListToXMLSerializer(levelColor, LevelsColorFile);
                return;
            }

            if (level > levelColor.Count)
                Console.WriteLine("pre levels dismiss");
        }

        public List<string> GetCategoriesList()
            => XmlTools.LoadListFromXMLSerializer<Category>(CategoriesFile).Select(x => x.CategoryName).ToList();

        public List<Category> GetUserCategoryList()
            => XmlTools.LoadListFromXMLSerializer<Category>(CategoriesFile);
        public List<string> GetLevelColorsList()
            => XmlTools.LoadListFromXMLSerializer<string>(LevelsColorFile).ToList();
    }

}
