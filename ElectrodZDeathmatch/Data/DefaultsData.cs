using ElectrodZMultiplayer;
using Newtonsoft.Json;

/// <summary>
/// ElectrodZ deathmatch data namespace
/// </summary>
namespace ElectrodZDeathmatch.Data
{
    /// <summary>
    /// A class that describes defaults data
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class DefaultsData : IValidable
    {
        /// <summary>
        /// Player character respawn time
        /// </summary>
        [JsonProperty("playerCharacterRespawnTime")]
        public double PlayerCharacterRespawnTime { get; set; } = 2.0;

        /// <summary>
        /// Weapon pickup respawn time
        /// </summary>
        [JsonProperty("weaponPickupRespawnTime")]
        public double WeaponPickupRespawnTime { get; set; } = 10.0;

        /// <summary>
        /// Weapon pickup respawn time
        /// </summary>
        [JsonProperty("weaponPickupRadius")]
        public float WeaponPickupRadius { get; set; } = 2.0f;

        /// <summary>
        /// Is object in a valid state
        /// </summary>
        public bool IsValid =>
            (PlayerCharacterRespawnTime >= 0.0f) &&
            (WeaponPickupRespawnTime >= 0.0f) &&
            (WeaponPickupRadius >= 0.0f);
    }
}
