using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDatabaseWalkthrough.Specs.Utils
{
    // Вспомогательный класс для получения табличных данных из фичи
    public class TableExtensions
    {
        public static Dictionary<string, string> ToDictionary(Table table)
        {
            // Заполнение коллекции в цикле строками таблицы из фичи
            var dictionary = new Dictionary<string, string>();
            foreach (var row in table.Rows)
            {
                dictionary.Add(row[0], row[1]);
            }
            return dictionary;
        }

    }
}
