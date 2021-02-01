using ElectrodZMultiplayer.Server;
using System;

/// <summary>
/// ElectrodZ deathmatch namespace
/// </summary>
namespace ElectrodZDeathmatch
{
    /// <summary>
    /// Damage contribution structure
    /// </summary>
    public class DamageContribution : IDamageContribution
    {
        /// <summary>
        /// Contributed damage
        /// </summary>
        public float Damage { get; }

        /// <summary>
        /// Issuer
        /// </summary>
        public IGameEntity Issuer { get; }

        /// <summary>
        /// Date and time
        /// </summary>
        public DateTime DateAndTime { get; }

        /// <summary>
        /// Is object in a valid state
        /// </summary>
        public bool IsValid =>
            (Damage >= 0.0f) &&
            (Issuer != null) &&
            (DateAndTime <= DateTime.Now);

        /// <summary>
        /// Constructs a damage contribution data instance
        /// </summary>
        /// <param name="damage">Contributed damage</param>
        /// <param name="issuer">Issuer</param>
        /// <param name="dateAndTime">Date and time</param>
        public DamageContribution(float damage, IGameEntity issuer, DateTime dateAndTime)
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
            if (dateAndTime > DateTime.Now)
            {
                throw new ArgumentException("Date and time must be in the past or present.", nameof(dateAndTime));
            }
            Damage = damage;
            Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
            DateAndTime = dateAndTime;
        }
    }
}
