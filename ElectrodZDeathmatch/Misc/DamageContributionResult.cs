using ElectrodZMultiplayer.Server;
using System;

/// <summary>
/// ElectrodZ deathmatch namespace
/// </summary>
namespace ElectrodZDeathmatch
{
    /// <summary>
    /// Damage contribution result structure
    /// </summary>
    public readonly struct DamageContributionResult : IDamageContributionResult
    {
        /// <summary>
        /// Contributed Damage
        /// </summary>
        public float Damage { get; }

        /// <summary>
        /// Issuer
        /// </summary>
        public IGameEntity Issuer { get; }

        /// <summary>
        /// Is object in a valid state
        /// </summary>
        public bool IsValid =>
            (Damage >= 0.0f) &&
            (Issuer != null);

        /// <summary>
        /// Constructs a damage contribution result
        /// </summary>
        /// <param name="damage">Contributed damage</param>
        /// <param name="issuer">Issuer</param>
        public DamageContributionResult(float damage, IGameEntity issuer)
        {
            if (damage < 0.0)
            {
                throw new ArgumentException("Damage contribution must be positive.", nameof(damage));
            }
            if (issuer == null)
            {
                throw new ArgumentNullException(nameof(issuer));
            }
            if (!issuer.IsValid)
            {
                throw new ArgumentException("Issuer is not valid.", nameof(issuer));
            }
            Damage = damage;
            Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
        }
    }
}
