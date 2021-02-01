using ElectrodZMultiplayer;

/// <summary>
/// ElectrodZ deathmatch namespace
/// </summary>
namespace ElectrodZDeathmatch
{
    /// <summary>
    /// A structure that describes a spawn position
    /// </summary>
    public readonly struct SpawnPoint : ISpawnPoint
    {
        /// <summary>
        /// Empty spawn point
        /// </summary>
        public static SpawnPoint Empty { get; } = new SpawnPoint(Vector3.Zero, Quaternion.Identity);

        /// <summary>
        /// Spawn position
        /// </summary>
        public Vector3 Position { get; }

        /// <summary>
        /// Spawn rotation
        /// </summary>
        public Quaternion Rotation { get; }

        /// <summary>
        /// Constructs a spawn point
        /// </summary>
        /// <param name="position">Position</param>
        /// <param name="rotation">Rotation</param>
        public SpawnPoint(Vector3 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }
    }
}
