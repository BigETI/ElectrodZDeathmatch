using ElectrodZMultiplayer.Server;

/// <summary>
/// ElectrodZ deathmatch namespace
/// </summary>
namespace ElectrodZDeathmatch
{
    /// <summary>
    /// An interface that represents a deathmatch game resource
    /// </summary>
    public interface IDeathmatchGameResource : IGameResource
    {
        /// <summary>
        /// Assets
        /// </summary>
        IAssets Assets { get; }
    }
}
