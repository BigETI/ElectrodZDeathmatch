using System;

/// <summary>
/// ElectrodZ deathmatch namespace
/// </summary>
namespace ElectrodZDeathmatch
{
    /// <summary>
    /// A structure that describes a weapon
    /// </summary>
    public readonly struct Weapon : IWeapon
    {
        /// <summary>
        /// Empty weapon
        /// </summary>
        public static Weapon Empty { get; } = new Weapon("Nothing", 0.0f, 0U);

        /// <summary>
        /// Weapon name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Weapon damage
        /// </summary>
        public float Damage { get; }

        /// <summary>
        /// Weapon usage count
        /// </summary>
        public uint UsageCount { get; }

        /// <summary>
        /// Is object in a valid state
        /// </summary>
        public bool IsValid =>
            !string.IsNullOrWhiteSpace(Name) &&
            (Damage >= 0.0f);

        /// <summary>
        /// COnstructs a weapon
        /// </summary>
        /// <param name="name">Weapon name</param>
        /// <param name="damage">Weapon damage</param>
        /// <param name="usageCount">Weapon usage count</param>
        public Weapon(string name, float damage, uint usageCount)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (damage < 0.0f)
            {
                throw new ArgumentException("Damage can't be negative.", nameof(damage));
            }
            Name = name;
            Damage = damage;
            UsageCount = usageCount;
        }
    }
}
