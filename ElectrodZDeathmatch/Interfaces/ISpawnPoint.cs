using ElectrodZMultiplayer;

/// <summary>
/// ElectrodZ deathmatch namespace
/// </summary>
namespace ElectrodZDeathmatch
{
    /// <summary>
    /// An interface that represents a spawn point
    /// </summary>
    public interface ISpawnPoint
    {
        /// <summary>
        /// Position
        /// </summary>
        Vector3 Position { get; }

        /// <summary>
        /// Rotation
        /// </summary>
        Quaternion Rotation { get; }
    }
}
