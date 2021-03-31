using System.Collections.Generic;

/// <summary>
/// ElectrodZ deathmatch namespace
/// </summary>
namespace ElectrodZDeathmatch
{
    /// <summary>
    /// An interface that represents spawn points
    /// </summary>
    public interface ISpawnPoints : IList<ISpawnPoint>
    {
        /// <summary>
        /// Next spawn point
        /// </summary>
        ISpawnPoint NextSpawnPoint { get; }
    }
}
