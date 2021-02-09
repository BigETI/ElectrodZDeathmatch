using ElectrodZMultiplayer;
using ElectrodZMultiplayer.Data;
using Newtonsoft.Json;

/// <summary>
/// ElectrodZ deathmatch data namespace
/// </summary>
namespace ElectrodZDeathmatch.Data
{
    /// <summary>
    /// A class that describes spawn point data
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class SpawnPointData : IValidable
    {
        /// <summary>
        /// Spawn position
        /// </summary>
        [JsonProperty("position")]
        public Vector3FloatData Position { get; set; }

        /// <summary>
        /// Spawn rotation
        /// </summary>
        [JsonProperty("rotation")]
        public QuaternionFloatData Rotation { get; set; }

        /// <summary>
        /// Is object in a valid state
        /// </summary>
        public bool IsValid =>
            (Position != null) &&
            (Rotation != null);
    }
}
