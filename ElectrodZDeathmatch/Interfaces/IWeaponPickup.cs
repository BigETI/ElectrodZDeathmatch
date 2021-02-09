using ElectrodZMultiplayer;
using ElectrodZMultiplayer.Server;
using System;

/// <summary>
/// ElectrodZ deathmatch namespace
/// </summary>
namespace ElectrodZDeathmatch
{
    /// <summary>
    /// An interface that represents a weapon pickup
    /// </summary>
    public interface IWeaponPickup : IValidable, IDisposable
    {
        /// <summary>
        /// Weapon
        /// </summary>
        IWeapon Weapon { get; }

        /// <summary>
        /// Weapon pickup spawn point
        /// </summary>
        ISpawnPoint SpawnPoint { get; }

        /// <summary>
        /// Weapon pickup entity
        /// </summary>
        IGameEntity Entity { get; }

        /// <summary>
        /// Respawn time
        /// </summary>
        double RespawnTime { get; set; }

        /// <summary>
        /// Remaining spawn time
        /// </summary>
        double RemainingRespawnTime { get; }

        /// <summary>
        /// Respawns pickup
        /// </summary>
        void Respawn();

        /// <summary>
        /// Destroys weapon pickup
        /// </summary>
        void Destroy();

        /// <summary>
        /// Performs a tick on weapon pickup
        /// </summary>
        /// <param name="deltaTime">Delta time</param>
        void Tick(TimeSpan deltaTime);
    }
}
