using SoundscapeGpx;
using SoundscapeGpx.Models;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Microsoft.PerformanceAI.API.Services
{
    public class SoundscapeGpxService : ISoundscapeGpxService
    {
        public string BuildSoundscapeGpx(ExperienceMetadata metadata)
        {
            SoundscapeGpxBuilder builder = new SoundscapeGpxBuilder(metadata);
            var xmlDoc = builder.ExportToGpx();

            using (var stringWriter = new StringWriter())
            using (var xmlTextWriter = XmlWriter.Create(stringWriter))
            {
                xmlDoc.WriteTo(xmlTextWriter);
                xmlTextWriter.Flush();
                return stringWriter.GetStringBuilder().ToString();
            }
        }
    }
}

