using ElectrodZMultiplayer.Server;
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
        /// Is user alive
        /// </summary>
        bool IsAlive { get; }

        /// <summary>
        /// This event will be invoked when a user took damage
        /// </summary>
        event DamageTakenDelegate OnDamageTaken;

        /// <summary>
        /// This event will be invoked when a user has died
        /// </summary>
        event DiedDelegate OnDied;

        /// <summary>
        /// Applys damage to user
        /// </summary>
        /// <param name="damage">Damage</param>
        /// <param name="issuer">Issuer</param>
        void ApplyDamage(float damage, IGameEntity issuer);
    }
}
