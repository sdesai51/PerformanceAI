using SoundscapeGpx.Models;
using System;
using System.Xml;

namespace SoundscapeGpx
{
    public class SoundscapeGpxBuilder
    {
        private string waypointNodeName = "wpt";

        ExperienceMetadata metadata;

        public SoundscapeGpxBuilder(ExperienceMetadata metadata)
        {
            this.metadata = metadata;
        }

        

        public XmlDocument ExportToGpx()
        {
            XmlDocument xmlDoc = new XmlDocument();

            using (XmlWriter writer = XmlWriter.Create(Console.Out))
            {
                // Write doc start
                WriteStartXml(xmlDoc);

                // Write Metadata
                WriteMetadataXml(xmlDoc, xmlDoc.ChildNodes[1]);

                foreach (var wayPoint in this.metadata.waypoints)
                {
                    AddWayPointXmlNode(xmlDoc, xmlDoc.ChildNodes[1], wayPoint);
                }

                xmlDoc.WriteContentTo(writer);
                writer.Flush();
            }

            return xmlDoc;
        }

        private void WriteMetadataXml(XmlDocument doc, XmlNode parent)
        {
            XmlNode metadataNode = doc.CreateElement("metadata");

            XmlNode nameNode = doc.CreateElement("name");
            nameNode.InnerText = this.metadata.name;
            metadataNode.AppendChild(nameNode);

            XmlNode descNode = doc.CreateElement("desc");
            descNode.InnerText = this.metadata.description;
            metadataNode.AppendChild(descNode);


            XmlNode authorNode = doc.CreateElement("author");
            XmlNode authorNameNode = doc.CreateElement("name");
            authorNameNode.InnerText = this.metadata.author;
            authorNode.AppendChild(authorNameNode);
            metadataNode.AppendChild(authorNode);

            XmlNode timeNode = doc.CreateElement("time");
            timeNode.InnerText = this.metadata.creationTime.ToString(); // will need to check formatting
            metadataNode.AppendChild(timeNode);

            XmlNode extensionsNode = doc.CreateElement("extensions");

            XmlNode extensionMetaNode = doc.CreateElement("meta");
            XmlAttribute metaEndAttribute = doc.CreateAttribute("end");
            XmlAttribute metaStartAttribute = doc.CreateAttribute("start");
            metaEndAttribute.Value = this.metadata.endTime.ToString(); // will need to check formatting
            metaStartAttribute.Value = this.metadata.startTime.ToString();// will need to check formatting
            extensionMetaNode.Attributes.Append(metaEndAttribute);
            extensionMetaNode.Attributes.Append(metaStartAttribute);
            
            XmlNode idNode = doc.CreateElement("id");
            idNode.InnerText = this.metadata.identifier;
            extensionMetaNode.AppendChild(idNode);

            XmlNode regionNode = doc.CreateElement("region");
            XmlAttribute latAttribute = doc.CreateAttribute("lat");
            XmlAttribute longAttribute = doc.CreateAttribute("lon");
            XmlAttribute radiusAttribute = doc.CreateAttribute("radius");
            latAttribute.Value = this.metadata.regionLatitude.ToString();
            longAttribute.Value = this.metadata.regionLongitude.ToString();
            radiusAttribute.Value = this.metadata.regionRadius.ToString();
            regionNode.Attributes.Append(latAttribute);
            regionNode.Attributes.Append(longAttribute);
            regionNode.Attributes.Append(radiusAttribute);
            extensionMetaNode.AppendChild(regionNode);

            XmlNode localeNode = doc.CreateElement("locale");
            localeNode.InnerText = this.metadata.locale;
            extensionMetaNode.AppendChild(localeNode);

            XmlNode behaviorNode = doc.CreateElement("behavior");
            behaviorNode.InnerText = this.metadata.behaviour;
            extensionMetaNode.AppendChild(behaviorNode);

            extensionsNode.AppendChild(extensionMetaNode);
            metadataNode.AppendChild(extensionsNode);

            parent.AppendChild(metadataNode);
        }

        private void WriteStartXml(XmlDocument doc)
        {
            string startingXml = "<?xml version=\"1.0\" encoding=\"utf8\"?><gpx xmlns=\"http://www.topografix.com/GPX/1/1\" xmlns:gpxsc=\"http://microsoft.com/Soundscape\" xmlns:gpxtpx=\"http://www.garmin.com/xmlschemas/TrackPointExtension/v1\" xmlns:gpxx=\"http://www.garmin.com/xmlschemas/GpxExtensions/v3\"></gpx>";
            doc.LoadXml(startingXml);
        }

        private void AddWayPointXmlNode(XmlDocument doc, XmlNode parent, SoundscapeWayPoint waypoint)
        {

            XmlNode waypointNode = doc.CreateElement(waypointNodeName);
            XmlAttribute latattribute = doc.CreateAttribute("lat");
            latattribute.Value = waypoint.latitude.ToString();
            XmlAttribute lonattribute = doc.CreateAttribute("lon");
            lonattribute.Value = waypoint.longitude.ToString();
            waypointNode.Attributes.Append(latattribute);
            waypointNode.Attributes.Append(lonattribute);

            XmlNode nameNode = doc.CreateElement("name");
            nameNode.InnerText = waypoint.name;
            waypointNode.AppendChild(nameNode);

            XmlNode descNode = doc.CreateElement("desc");
            descNode.InnerText = waypoint.description;
            waypointNode.AppendChild(descNode);

            XmlNode typeNode = doc.CreateElement("type");
            typeNode.InnerText = waypoint.type;
            waypointNode.AppendChild(typeNode);

            XmlNode elevationNode = doc.CreateElement("ele");
            elevationNode.InnerText = waypoint.elevation.ToString();
            waypointNode.AppendChild(elevationNode);

            XmlNode extensionsNode = doc.CreateElement("extensions");
            XmlNode annotationsNode = doc.CreateElement("annotations");
            XmlNode annotationNode = doc.CreateElement("annotations");
            annotationNode.InnerText = waypoint.description;
            XmlAttribute annotationTitle = doc.CreateAttribute("Title");
            annotationTitle.Value = waypoint.name;
            annotationNode.Attributes.Append(annotationTitle);

            XmlNode poiNode = doc.CreateElement("poi");
            XmlNode streetNode = doc.CreateElement("street");
            streetNode.InnerText = waypoint.street;

            waypointNode.AppendChild(extensionsNode);
            annotationsNode.AppendChild(annotationNode);
            extensionsNode.AppendChild(annotationsNode);
            extensionsNode.AppendChild(poiNode);
            poiNode.AppendChild(streetNode);

            parent.AppendChild(waypointNode);
        }
    }
}
