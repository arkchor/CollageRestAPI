using System.Xml.Serialization;

namespace CollageRestAPI.Hypermedia
{
    public class Link
    {
        public string Rel { get; set; }
        public string Href { get; set; }
        public string Method { get; set; }
        [XmlElement(IsNullable = false)]
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