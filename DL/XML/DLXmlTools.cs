using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace DL.DlXml
{
    internal class XmlTools
    {
        // שימוש בנתיב יחסי המבוסס על התיקייה שבה מופעלת האפליקציה
        static string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "xml");

        static XmlTools()
        {
            // בדיקה אם התיקייה קיימת, ואם לא - יצירתה
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        public static void SaveListToXMLSerializer<T>(List<T> list, string filePath)
        {
            try
            {
                // בניית הנתיב המלא לקובץ XML ושמירתו
                string fullPath = Path.Combine(dir, filePath);
                using FileStream file = new(fullPath, FileMode.Create);
                XmlSerializer x = new(list.GetType());
                x.Serialize(file, list);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving XML: " + ex.Message);
            }
        }

        public static List<T> LoadListFromXMLSerializer<T>(string filePath)
        {
            try
            {
                // בניית הנתיב המלא לקובץ XML
                string fullPath = Path.Combine(dir, filePath);

                if (File.Exists(fullPath))
                {
                    List<T> list;
                    XmlSerializer x = new(typeof(List<T>));
                    using FileStream file = new(fullPath, FileMode.Open);
                    list = (List<T>)x.Deserialize(file);
                    return list;
                }
                else
                    return new List<T>(); // אם הקובץ לא קיים, החזרת רשימה ריקה
            }
            catch (Exception ex)
            {
                throw new KeyNotFoundException("The path does not exist: " + ex.Message);
            }
        }
    }
}
