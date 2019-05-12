using Newtonsoft.Json;
using System.Xml;
using System.Xml.Serialization;

namespace swi
{
    public class XmlField
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("type")]
        public string Type { get; set; }

        [XmlElement("value")]
        public string Value { get; set; }

        [JsonIgnore]
        [XmlAnyElement]
        public XmlElement[] Unsupported { get; set; }
    }
}
