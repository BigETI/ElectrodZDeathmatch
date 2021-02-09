using ElectrodZMultiplayer.Server;

/// <summary>
/// ElectrodZ deathmatch namespace
/// </summary>
namespace ElectrodZDeathmatch
{
    /// <summary>
    /// Used to signal when damage was taken
    /// </summary>
    /// <param name="damage">Damage</param>
    /// <param name="issuer">Issuer</param>
    public delegate void DamageTakenDelegate(float damage, IGameEntity issuer);
}
