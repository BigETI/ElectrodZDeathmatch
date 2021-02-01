using ElectrodZMultiplayer;
using ElectrodZMultiplayer.Server;

/// <summary>
/// ElectrodZ deathmatch namespace
/// </summary>
namespace ElectrodZDeathmatch
{
    /// <summary>
    /// An interface that represents damage contribution result
    /// </summary>
    public interface IDamageContributionResult : IValidable
    {
        /// <summary>
        /// Contributed damage
        /// </summary>
        float Damage { get; }

        /// <summary>
        /// Issuer
        /// </summary>
        IGameEntity Issuer { get; }
    }
}
