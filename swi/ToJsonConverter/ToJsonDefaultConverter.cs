using System;
using System.Collections.Generic;
using System.Text;

namespace swi
{
    public class ToJsonDefaultConverter
    {
        public static string ConvertToJson(XmlObjectList xmlObjectList)
        {
            StringBuilder stringBuilder = new StringBuilder();

            appendObjects(stringBuilder, xmlObjectList.XmlObjects);

            stringBuilder.Remove(stringBuilder.Length - 2, 2);
            stringBuilder.Insert(0, "{");
            stringBuilder.Append("}");

            return stringBuilder.ToString();
        }

        private static void appendObjects(StringBuilder stringBuilder, List<XmlObject> xmlObjects)
        {
            foreach (var item in xmlObjects)
            {
                stringBuilder.Append($"\"{item.Obj_Name}\":{{"); // "nazwa_obiektu": {

                appendFields(stringBuilder, item.Fields);

                stringBuilder.Remove(stringBuilder.Length - 2, 2);
                stringBuilder.Append("}, "); // },
            }
        }

        private static void appendFields(StringBuilder stringBuilder, List<XmlField> fields)
        {
            foreach (var field in fields)
            {
                switch (field.Type.ToLower())
                {
                    case "int":
                        stringBuilder.Append($"\"{field.Name}\": {field.Value}, ");
                        break;
                    case "string":
                        stringBuilder.Append($"\"{field.Name}\": \"{field.Value}\", ");
                        break;
                }

            }
        }

    }
}
