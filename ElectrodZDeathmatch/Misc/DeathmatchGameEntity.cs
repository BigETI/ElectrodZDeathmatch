using ElectrodZMultiplayer.Server;

/// <summary>
/// ElectrodZ deathmatch namespace
/// </summary>
namespace ElectrodZDeathmatch
{
    /// <summary>
    /// A class that describes a deathmatch game entity
    /// </summary>
    internal class DeathmatchGameEntity : AGameEntity
    {
        /// <summary>
        /// Constructs a deathmatch game entity
        /// </summary>
        /// <param name="serverEntity">Server entity</param>
        public DeathmatchGameEntity(IServerEntity serverEntity) : base(serverEntity)
        {
            // ...
        }
    }
}
