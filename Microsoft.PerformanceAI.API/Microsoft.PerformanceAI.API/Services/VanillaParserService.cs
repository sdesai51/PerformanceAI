using Microsoft.PerformanceAI.API.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Microsoft.PerformanceAI.API.Services
{
    public class VanillaParserService : IVanillaParserService
    {
        private const string COORDIANTE_NODE_XPATH = "//ns:trkpt";
        private const string LAT_ATT_NAME = "lat";
        private const string LON_ATT_NAME = "lon";
        private const string NAMESPACE = "http://www.topografix.com/GPX/1/1";

        public IEnumerable<Coordinate> ExtractCoordinates(string xmlDocumentString)
        {
            var coordinates = new List<Coordinate>();

            if (!string.IsNullOrWhiteSpace(xmlDocumentString))
            {
                var xmlDocument = this.ParseToXmlDocument(xmlDocumentString);
                XmlNamespaceManager m = new XmlNamespaceManager(xmlDocument.NameTable);
                m.AddNamespace("ns", NAMESPACE);
                var coordinateNodes = xmlDocument.SelectNodes("//ns:trkpt", m);

                foreach (XmlNode node in coordinateNodes)
                {
                    coordinates.Add(new Coordinate
                    {
                        Lat = Convert.ToDouble(node.Attributes[LAT_ATT_NAME].InnerText),
                        Long = Convert.ToDouble(node.Attributes[LON_ATT_NAME].InnerText)
                    });
                }

            }

            return coordinates;
        }

        private XmlDocument ParseToXmlDocument(string xmlDocumentString)
        {
            XmlDocument document = new XmlDocument();

            using (var stream = new MemoryStream(Encoding.Default.GetBytes(xmlDocumentString)))
            {
                document.Load(stream);
            }

            return document;
        }
    }
}

