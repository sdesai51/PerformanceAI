using SoundscapeGpx.Models;

namespace Microsoft.PerformanceAI.API.Services
{
    public interface ISoundscapeGpxService
    {
        /// <summary>
        /// Builds a SoundScapeStyleGpx xml string.
        /// </summary>
        /// <param name="waypoints">A set of waypoints to include in the xml.</param>
        /// <param name="metadata">Data about the experience.</param>
        /// <returns></returns>
        string BuildSoundscapeGpx(ExperienceMetadata metadata);
    }
}
