using System;

/// <summary>
/// ElectrodZ deathmatch namespace
/// </summary>
namespace ElectrodZDeathmatch
{
    /// <summary>
    /// A structure that describes game mode defaults
    /// </summary>
    public class Defaults : IDefaults
    {
        /// <summary>
        /// Player character respawn time
        /// </summary>
        public double PlayerCharacterRespawnTime { get; } = 2.0;

        /// <summary>
        /// Weapon pickup respawn time
        /// </summary>
        public double WeaponPickupRespawnTime { get; } = 10.0;

        /// <summary>
        /// Weapon pickup respawn time
        /// </summary>
        public float WeaponPickupRadius { get; } = 2.0f;

        /// <summary>
        /// Is object in a valid state
        /// </summary>
        public bool IsValid =>
            (PlayerCharacterRespawnTime >= 0.0) &&
            (WeaponPickupRespawnTime >= 0.0) &&
            (WeaponPickupRadius >= 0.0f);

        /// <summary>
        /// Constructs defaults
        /// </summary>
        public Defaults()
        {
            // ...
        }

        /// <summary>
        /// Constructs defaults
        /// </summary>
        /// <param name="playerCharacterRespawnTime">Player character respawn time</param>
        /// <param name="weaponPickupRespawnTime">Weapon pickup respawn time</param>
        /// <param name="weaponPickupRadius">Weapon pickup radius</param>
        public Defaults(double playerCharacterRespawnTime, double weaponPickupRespawnTime, float weaponPickupRadius)
        {
            if (playerCharacterRespawnTime < 0.0)
            {
                throw new ArgumentException("Player character respawn time can't be negative.", nameof(playerCharacterRespawnTime));
            }
            if (weaponPickupRespawnTime < 0.0)
            {
                throw new ArgumentException("Weapon pickup respawn time can't be negative.", nameof(weaponPickupRespawnTime));
            }
            if (weaponPickupRadius < 0.0)
            {
                throw new ArgumentException("Weapon pickup radius can't be negative.", nameof(weaponPickupRadius));
            }
            PlayerCharacterRespawnTime = playerCharacterRespawnTime;
            WeaponPickupRespawnTime = weaponPickupRespawnTime;
            WeaponPickupRadius = weaponPickupRadius;
        }
    }
}
