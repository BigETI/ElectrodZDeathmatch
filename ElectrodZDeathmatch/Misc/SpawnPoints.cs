using System.Collections.Generic;

/// <summary>
/// ElectrodZ deathmatch namespace
/// </summary>
namespace ElectrodZDeathmatch
{
    /// <summary>
    /// Spawn positions
    /// </summary>
    public class SpawnPoints : List<ISpawnPoint>, ISpawnPoints
    {
        /// <summary>
        /// Current spawn index
        /// </summary>
        private uint currentSpawnIndex;

        /// <summary>
        /// Next spawn point
        /// </summary>
        public ISpawnPoint NextSpawnPoint
        {
            get
            {
                ISpawnPoint ret = null;
                if (Count > 0)
                {
                    currentSpawnIndex %= (uint)Count;
                    ret = this[(int)currentSpawnIndex++];
                }
                return ret;
            }
        }

        /// <summary>
        /// Constructs spawn points
        /// </summary>
        public SpawnPoints() : base()
        {
            // ...
        }

        /// <summary>
        /// Constructs spawn points
        /// </summary>
        /// <param name="points">Spawn points</param>
        public SpawnPoints(IEnumerable<ISpawnPoint> points) : base(points)
        {
            // ...
        }
    }
}
