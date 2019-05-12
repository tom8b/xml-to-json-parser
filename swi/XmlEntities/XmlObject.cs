using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace swi
{
    public class XmlObject
    {
        [XmlElement("obj_name")]
        public string Obj_Name { get; set; }

        [XmlElement("field")]
        public List<XmlField> Fields { get; set; }

        [JsonIgnore]
        [XmlAnyElement]
        public XmlElement[] Unsupported { get; set; }
    }

}
