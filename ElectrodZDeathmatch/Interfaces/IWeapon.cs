using ElectrodZMultiplayer;

/// <summary>
/// ElectrodZ deathmatch namespace
/// </summary>
namespace ElectrodZDeathmatch
{
    /// <summary>
    /// An interface that represents a weapon
    /// </summary>
    public interface IWeapon : IValidable
    {
        /// <summary>
        /// Weapon name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Weapon damage
        /// </summary>
        float Damage { get; }

        /// <summary>
        /// Weapon usage count
        /// </summary>
        uint UsageCount { get; }
    }
}
