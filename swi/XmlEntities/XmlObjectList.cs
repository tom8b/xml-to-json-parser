using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace swi
{
    [XmlRoot("root")]
    public class XmlObjectList
    {
        [XmlElement("object")]
        public List<XmlObject> XmlObjects { get; set; }
    }
}
