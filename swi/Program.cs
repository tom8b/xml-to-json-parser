using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Xml.Serialization;

namespace swi
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string input;
                XmlObjectList xmlObjectList;

                using (StreamReader streamReader = new StreamReader("input.xml"))
                {
                    input = "<root>" + streamReader.ReadToEnd() + "</root>"; 
                }

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(XmlObjectList));

                using (TextReader reader = new StringReader(input))
                {
                    xmlObjectList = (XmlObjectList)xmlSerializer.Deserialize(reader);
                }

                Validator validator = new Validator(xmlObjectList);

                validator.ObjectValidate();

                var result = JsonConvert.DeserializeObject<JObject>(ToJsonDefaultConverter
                    .ConvertToJson(xmlObjectList))
                    .ToString();

                File.WriteAllText("output.json", result);
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

        }

    }
}
