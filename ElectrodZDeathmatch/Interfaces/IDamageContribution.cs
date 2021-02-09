using System;

/// <summary>
/// ElectrodZ deathmatch namespace
/// </summary>
namespace ElectrodZDeathmatch
{
    /// <summary>
    /// An interface that represents damage contribution
    /// </summary>
    public interface IDamageContribution : IDamageContributionResult
    {
        /// <summary>
        /// Date and time
        /// </summary>
        DateTime DateAndTime { get; }
    }
}
