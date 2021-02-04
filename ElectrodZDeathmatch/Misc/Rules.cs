using ElectrodZMultiplayer;
using System;

/// <summary>
/// ElectrodZ deathmatch namespace
/// </summary>
namespace ElectrodZDeathmatch
{
    /// <summary>
    /// A structure that describes game mode rules
    /// </summary>
    public class Rules : IRules
    {
        /// <summary>
        /// Out of map position
        /// </summary>
        public Vector3 OutOfMapPosition { get; }

        /// <summary>
        /// Player character health
        /// </summary>
        public float PlayerCharacterHealth { get; } = 1.0f;

        /// <summary>
        /// Player character respawn time
        /// </summary>
        public double PlayerCharacterRespawnTime { get; } = 2.0;

        /// <summary>
        /// Round time
        /// </summary>
        public double RoundTime { get; } = 180.0;

        /// <summary>
        /// Weapon pickup radius
        /// </summary>
        public float WeaponPickupRadius { get; } = 2.0f;

        /// <summary>
        /// Weapon pickup respawn time
        /// </summary>
        public double WeaponPickupRespawnTime { get; } = 10.0;

        /// <summary>
        /// Is object in a valid state
        /// </summary>
        public bool IsValid =>
            (PlayerCharacterHealth >= 0.0f) &&
            (PlayerCharacterRespawnTime >= 0.0) &&
            (RoundTime > float.Epsilon) &&
            (WeaponPickupRadius >= 0.0f) &&
            (WeaponPickupRespawnTime >= 0.0);

        /// <summary>
        /// Constructs defaults
        /// </summary>
        public Rules()
        {
            // ...
        }

        /// <summary>
        /// Constructs defaults
        /// </summary>
        /// <param name="outOfMapPosition">Out of map position</param>
        /// <param name="playerCharacterHealth">Player character health</param>
        /// <param name="playerCharacterRespawnTime">Player character respawn time</param>
        /// <param name="weaponPickupRadius">Weapon pickup radius</param>
        /// <param name="weaponPickupRespawnTime">Weapon pickup respawn time</param>
        public Rules(Vector3 outOfMapPosition, float playerCharacterHealth, double playerCharacterRespawnTime, double roundTime, float weaponPickupRadius, double weaponPickupRespawnTime)
        {
            if (playerCharacterHealth < 0.0)
            {
                throw new ArgumentException("Player health can't be negative.", nameof(playerCharacterHealth));
            }
            if (playerCharacterRespawnTime < 0.0)
            {
                throw new ArgumentException("Player character respawn time can't be negative.", nameof(playerCharacterRespawnTime));
            }
            if (roundTime <= float.Epsilon)
            {
                throw new ArgumentException("Round time must be greater than zero.", nameof(roundTime));
            }
            if (weaponPickupRadius < 0.0)
            {
                throw new ArgumentException("Weapon pickup radius can't be negative.", nameof(weaponPickupRadius));
            }
            if (weaponPickupRespawnTime < 0.0)
            {
                throw new ArgumentException("Weapon pickup respawn time can't be negative.", nameof(weaponPickupRespawnTime));
            }
            OutOfMapPosition = outOfMapPosition;
            PlayerCharacterHealth = playerCharacterHealth;
            PlayerCharacterRespawnTime = playerCharacterRespawnTime;
            RoundTime = roundTime;
            WeaponPickupRadius = weaponPickupRadius;
            WeaponPickupRespawnTime = weaponPickupRespawnTime;
        }
    }
}
