using ElectrodZMultiplayer;
using ElectrodZMultiplayer.Server;
using System;
using System.Collections.Generic;

/// <summary>
/// ElectrodZ deathmatch namespace
/// </summary>
namespace ElectrodZDeathmatch
{
    /// <summary>
    /// An interface that represents a deathmatch game user
    /// </summary>
    public interface IDeathmatchGameUser : IGameUser
    {
        /// <summary>
        /// Damage contributions
        /// </summary>
        IEnumerable<IDamageContribution> DamageContributions { get; }

        /// <summary>
        /// Health
        /// </summary>
        float Health { get; }

        /// <summary>
        /// Maximal health
        /// </summary>
        float MaximalHealth { get; set; }

        /// <summary>
        /// Is user alive
        /// </summary>
        bool IsAlive { get; }

        /// <summary>
        /// Respawn time
        /// </summary>
        double RespawnTime { get; set; }

        /// <summary>
        /// This event will be invoked when this user has respawned
        /// </summary>
        event RespawnedDelegate OnRespawned;

        /// <summary>
        /// This event will be invoked when this user took damage
        /// </summary>
        event DamageTakenDelegate OnDamageTaken;

        /// <summary>
        /// This event will be invoked when this user has died
        /// </summary>
        event DiedDelegate OnDied;

        /// <summary>
        /// Applys damage to user
        /// </summary>
        /// <param name="damage">Damage</param>
        void ApplyDamage(float damage);

        /// <summary>
        /// Applys damage to user
        /// </summary>
        /// <param name="damage">Damage</param>
        /// <param name="issuer">Issuer</param>
        void ApplyDamage(float damage, IGameEntity issuer);

        /// <summary>
        /// Heals user
        /// </summary>
        void Heal();

        /// <summary>
        /// Processes tick for user
        /// </summary>
        /// <param name="deltaTime">Delta time</param>
        void ProcessTick(TimeSpan deltaTime);
    }
}
