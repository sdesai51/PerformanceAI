using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.PerformanceAI.API.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Partition a given list into smaller chunks of N size
        /// </summary>
        /// <param name="source">Source list.</param>
        /// <param name="partitionSize">A partition size.</param>
        /// <returns>A list of partitions.</returns>
        public static IEnumerable<List<T>> Partition<T>(this IEnumerable<T> source, int partitionSize)
        {
            for (int i = 0; i < Math.Ceiling(source.Count() / (Double)partitionSize); i++)
            {
                yield return new List<T>(source.Skip(partitionSize * i).Take(partitionSize));
            }
        }
    }
}
