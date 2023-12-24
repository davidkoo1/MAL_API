using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class Json
    {

        private readonly static string _nameJsonFile = "Anime.json";

        //Запись
        public static void WriteDown(ListClasses listClasses)
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            string objectSer = JsonSerializer.Serialize(listClasses, options);
            File.WriteAllText(_nameJsonFile, objectSer);
        }


        //Считывание
        public static void Provide(out ListClasses obj)
        {
            try
            {
                string objJsonFile = File.ReadAllText(_nameJsonFile);
                obj = JsonSerializer.Deserialize<ListClasses>(objJsonFile);
            }
            catch (JsonException)
            {

                // Обработка исключения, если возникла ошибка при десериализации JSON
                obj = new ListClasses(); // Проинициализировать пустым списком или другим значением по умолчанию

            }
        }
    }
}
