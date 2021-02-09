using ElectrodZMultiplayer;
using ElectrodZMultiplayer.Data;
using Newtonsoft.Json;

/// <summary>
/// ElectrodZ deathmatch data namespace
/// </summary>
namespace ElectrodZDeathmatch.Data
{
    /// <summary>
    /// A class that describes rules data
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class RulesData : IValidable
    {
        /// <summary>
        /// Out of map position
        /// </summary>
        [JsonProperty("outOfMapPosition")]
        public Vector3FloatData OutOfMapPosition { get; set; }

        /// <summary>
        /// Player character health
        /// </summary>
        [JsonProperty("playerCharacterHealth")]
        public float PlayerCharacterHealth { get; set; } = 1.0f;

        /// <summary>
        /// Player character respawn time
        /// </summary>
        [JsonProperty("playerCharacterRespawnTime")]
        public double PlayerCharacterRespawnTime { get; set; } = 2.0;

        /// <summary>
        /// Round time
        /// </summary>
        [JsonProperty("roundTime")]
        public double RoundTime { get; set; } = 180.0;

        /// <summary>
        /// Weapon pickup radius
        /// </summary>
        [JsonProperty("weaponPickupRadius")]
        public float WeaponPickupRadius { get; set; } = 2.0f;

        /// <summary>
        /// Weapon pickup respawn time
        /// </summary>
        [JsonProperty("weaponPickupRespawnTime")]
        public double WeaponPickupRespawnTime { get; set; } = 10.0;

        /// <summary>
        /// Is object in a valid state
        /// </summary>
        public bool IsValid =>
            (OutOfMapPosition != null) &&
            (PlayerCharacterHealth >= 0.0f) &&
            (PlayerCharacterRespawnTime >= 0.0f) &&
            (RoundTime > float.Epsilon) &&
            (WeaponPickupRadius >= 0.0f) &&
            (WeaponPickupRespawnTime >= 0.0f);
    }
}
