using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace CollageRestAPI.Hypermedia
{
    [DataContract]
    public class Link
    {
        [DataMember]
        public string Rel { get; set; }
        [DataMember]
        public string Href { get; set; }
        [DataMember]
        public string Method { get; set; }
        [DataMember]
        //[XmlElement(IsNullable = false)]
        public string Comment { get; set; }
        
        public Link(string rel, string href, string method, string comment)
        {
            Rel = rel;
            Href = href;
            Method = method;
            Comment = comment;
        }

        public Link(string rel, string href, string method)
        {
            Rel = rel;
            Href = href;
            Method = method;
        }
    }
}