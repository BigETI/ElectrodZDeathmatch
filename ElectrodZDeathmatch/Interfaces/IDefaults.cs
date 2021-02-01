using ElectrodZMultiplayer;

/// <summary>
/// ElectrodZ deathmatch namespace
/// </summary>
namespace ElectrodZDeathmatch
{
    /// <summary>
    /// An interface that represents game mode defaults
    /// </summary>
    public interface IDefaults : IValidable
    {
        /// <summary>
        /// Player character respawn time
        /// </summary>
        double PlayerCharacterRespawnTime { get; }

        /// <summary>
        /// Weapon pickup respawn time
        /// </summary>
        double WeaponPickupRespawnTime { get; }

        /// <summary>
        /// Weapon pickup respawn time
        /// </summary>
        float WeaponPickupRadius { get; }
    }
}
