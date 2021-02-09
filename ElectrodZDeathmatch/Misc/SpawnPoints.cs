using System;
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
        /// Random number generator
        /// </summary>
        private Random random = new Random();

        /// <summary>
        /// Random spawn point
        /// </summary>
        public ISpawnPoint RandomSpawnPoint => (Count > 0) ? this[random.Next(0, Count)] : SpawnPoint.Empty;

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
