using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Text.Unicode;
using System.Text.Encodings.Web;

namespace ToDoApp_backend.Utility
{
    public class JsonUtility
    {
        public static void JsonSerialize<T>(T data, string jsonfile)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };

            using (var stream = File.Create(jsonfile))
            {
                //Newtonと同じ名なのでぶつかる
                JsonSerializer.SerializeAsync(stream, data, options);
            }
        }

        public static T JsonDeserialize<T>(string jsonfile)
        {
            if (!File.Exists(jsonfile)) return default;
            var jsonString = File.ReadAllText(jsonfile);
            return !string.IsNullOrEmpty(jsonString) ? JsonSerializer.Deserialize<T>(jsonString) : default;
        }

        public static void DeletedJson(string jsonFile)
        {
            if (File.Exists(jsonFile)) File.Delete(jsonFile);
        }
    }
}
