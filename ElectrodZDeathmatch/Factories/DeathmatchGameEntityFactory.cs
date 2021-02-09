using ElectrodZMultiplayer.Server;

/// <summary>
/// ElectrodZ deathmatch factories namespace
/// </summary>
namespace ElectrodZDeathmatch.Factories
{
    /// <summary>
    /// A class that describes a deathmatch game entity factory
    /// </summary>
    internal class DeathmatchGameEntityFactory : IGameEntityFactory
    {
        /// <summary>
        /// Creates a new game entity
        /// </summary>
        /// <param name="serverEntity">Game entity type</param>
        /// <returns>Game entity</returns>
        public IGameEntity CreateNewGameEntity(IServerEntity serverEntity) => new DeathmatchGameEntity(serverEntity);
    }
}
