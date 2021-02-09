using ElectrodZMultiplayer.Server;

/// <summary>
/// ElectrodZ deathmatch factories namespace
/// </summary>
namespace ElectrodZDeathmatch.Factories
{
    /// <summary>
    /// A class that describes a deathmatch game user factory
    /// </summary>
    internal class DeathmatchGameUserFactory : IGameUserFactory
    {
        /// <summary>
        /// Creates a new game user
        /// </summary>
        /// <param name="serverUser">Server user</param>
        /// <returns>Game user</returns>
        public IGameUser CreateNewGameUser(IServerUser serverUser) => new DeathmatchGameUser(serverUser);
    }
}
