using ElectrodZMultiplayer;
using Newtonsoft.Json;

/// <summary>
/// ElectrodZ deathmatch data namespace
/// </summary>
namespace ElectrodZDeathmatch.Data
{
    /// <summary>
    /// A class that describes weapon data
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class WeaponData : IValidable
    {
        /// <summary>
        /// Weapon name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Weapon damage
        /// </summary>
        [JsonProperty("damage")]
        public float Damage { get; set; }

        /// <summary>
        /// Weapon usage count
        /// </summary>
        [JsonProperty("usageCount")]
        public uint UsageCount { get; set; }

        /// <summary>
        /// Is object in a valid state
        /// </summary>
        public bool IsValid =>
            !string.IsNullOrWhiteSpace(Name) &&
            (Damage >= 0.0f);
    }
}
