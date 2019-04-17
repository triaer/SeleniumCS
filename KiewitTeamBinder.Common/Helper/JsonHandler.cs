using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.Common.Helper
{
    public class JsonHandler
    {
        private static string jsonFilePath = AppDomain.CurrentDomain.BaseDirectory + "\\TestData\\";
        internal static void WriteToJson(Object obj, string testCaseName)
        {
            //Create Serializer and set its properties
            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;

            //Write the object to a json file 
            //File name is based on the type of object, the type of test it will be used in, and a desscriptor
            //Example file name: CoolUserObject.ValidUserCanLogOnAndOff.Inputs.json or CoolUserObject.ValidUserCanLogOnAndOff.Expected.json
            using (StreamWriter sw = new StreamWriter(jsonFilePath + obj.GetType().Name + "." + testCaseName + @".json"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, obj);
            }
        }

        public static List<Employee> ReadDataFromJson(string path)
        { 
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                Console.WriteLine(json);
                List<Employee> items = JsonConvert.DeserializeObject<List<Employee>>(json);
                return items;
            }
            
        }

    }


    // Test class
    public class Employee
    {
        public string firstName { get; set; }
        public string lastName { get; set; }

        public Employee()
        {
            // init
        }
    }


}
