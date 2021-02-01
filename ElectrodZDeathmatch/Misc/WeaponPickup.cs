using ElectrodZMultiplayer;
using ElectrodZMultiplayer.Server;
using System;

/// <summary>
/// ElectrodZ deathmatch namespace
/// </summary>
namespace ElectrodZDeathmatch
{
    /// <summary>
    /// A structure that describes a weapon pickup
    /// </summary>
    public class WeaponPickup : IValidable, IDisposable
    {
        /// <summary>
        /// Server lobby
        /// </summary>
        private readonly IServerLobby serverLobby;

        /// <summary>
        /// Weapon
        /// </summary>
        public IWeapon Weapon { get; }

        /// <summary>
        /// Weapon pickup spawn point
        /// </summary>
        public ISpawnPoint SpawnPoint { get; }

        /// <summary>
        /// Weapon pickup entity
        /// </summary>
        public IGameEntity Entity { get; private set; }

        /// <summary>
        /// Respawn time
        /// </summary>
        public double RespawnTime { get; }

        /// <summary>
        /// Remaining spawn time
        /// </summary>
        public double RemainingRespawnTime { get; private set; }

        /// <summary>
        /// Is object in a valid state
        /// </summary>
        public bool IsValid =>
            (Weapon != null) &&
            Weapon.IsValid &&
            (SpawnPoint != null) &&
            ((Entity == null) || Entity.IsValid) &&
            (RespawnTime >= 0.0f) &&
            (RemainingRespawnTime >= 0.0f) &&
            (serverLobby != null) &&
            serverLobby.IsValid;

        /// <summary>
        /// Constructs a weapon pickup
        /// </summary>
        /// <param name="weapon">Weapon</param>
        /// <param name="spawnPoint">Spawn point</param>
        /// <param name="entity">Entity</param>
        /// <param name="remainingRespawnTime">Remaining respawn time</param>
        public WeaponPickup(IWeapon weapon, ISpawnPoint spawnPoint, double respawnTime, IServerLobby serverLobby)
        {
            if (weapon == null)
            {
                throw new ArgumentNullException(nameof(weapon));
            }
            if (!weapon.IsValid)
            {
                throw new ArgumentException("Weapon is not valid.", nameof(weapon));
            }
            if (respawnTime < 0.0f)
            {
                throw new ArgumentException("Respawn time can't be negative", nameof(respawnTime));
            }
            if (serverLobby == null)
            {
                throw new ArgumentNullException(nameof(serverLobby));
            }
            if (!serverLobby.IsValid)
            {
                throw new ArgumentException("Server lobby is not valid.", nameof(serverLobby));
            }
            Weapon = weapon;
            SpawnPoint = spawnPoint ?? throw new ArgumentNullException(nameof(spawnPoint));
            RespawnTime = respawnTime;
            this.serverLobby = serverLobby;
            Respawn();
        }

        /// <summary>
        /// Respawns pickup
        /// </summary>
        public void Respawn()
        {
            RemainingRespawnTime = RespawnTime;
            Destroy();
            Entity = serverLobby.CreateNewGameEntity($"{ Weapon.Name }Weapon");
        }

        /// <summary>
        /// Destroys weapon pickup
        /// </summary>
        public void Destroy()
        {
            if ((Entity != null) && Entity.IsValid)
            {
                serverLobby.RemoveEntity(Entity);
            }
            Entity = null;
        }

        /// <summary>
        /// Performs a tick on weapon pickup
        /// </summary>
        /// <param name="deltaTime">Delta time</param>
        public void Tick(TimeSpan deltaTime)
        {
            if (Entity == null)
            {
                RemainingRespawnTime -= deltaTime.TotalSeconds;
                if (RemainingRespawnTime <= 0.0f)
                {
                    Respawn();
                }
            }
        }

        /// <summary>
        /// Disposes object
        /// </summary>
        public void Dispose() => Destroy();
    }
}
