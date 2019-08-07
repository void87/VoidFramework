using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

namespace Test20190807 {

    [System.Serializable]
    public class TestSerialize {

        [XmlAttribute("Id")]
        public int Id { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlElement("List")]
        public List<int> List { get; set; }
    }
}