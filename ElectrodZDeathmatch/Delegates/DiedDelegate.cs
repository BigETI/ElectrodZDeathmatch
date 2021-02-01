using System.Collections.Generic;

/// <summary>
/// ElectrodZ deathmatch namespace
/// </summary>
namespace ElectrodZDeathmatch
{
    /// <summary>
    /// Used to signal when an user dies
    /// </summary>
    /// <param name="issuers">Issuers</param>
    public delegate void DiedDelegate(IReadOnlyList<IDamageContributionResult> issuers);
}
