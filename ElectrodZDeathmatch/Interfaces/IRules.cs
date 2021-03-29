using ElectrodZMultiplayer;

/// <summary>
/// ElectrodZ deathmatch namespace
/// </summary>
namespace ElectrodZDeathmatch
{
    /// <summary>
    /// An interface that represents game mode rules
    /// </summary>
    public interface IRules : IValidable
    {
        /// <summary>
        /// Out of map position
        /// </summary>
        Vector3 OutOfMapPosition { get; }

        /// <summary>
        /// Out of map rotation
        /// </summary>
        Quaternion OutOfMapRotation { get; }

        /// <summary>
        /// Player character health
        /// </summary>
        float PlayerCharacterHealth { get; }

        /// <summary>
        /// Player character respawn time
        /// </summary>
        double PlayerCharacterRespawnTime { get; }

        /// <summary>
        /// Round time
        /// </summary>
        double RoundTime { get; }

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
