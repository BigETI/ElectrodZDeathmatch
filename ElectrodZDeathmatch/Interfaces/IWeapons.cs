/// <summary>
/// ElectrodZ deathmatch namespace
/// </summary>
namespace ElectrodZDeathmatch
{
    /// <summary>
    /// An interface that represents a set of weapons
    /// </summary>
    public interface IWeapons
    {
        /// <summary>
        /// Random weapon
        /// </summary>
        IWeapon RandomWeapon { get; }

        /// <summary>
        /// Gets weapon by name
        /// </summary>
        /// <param name="weaponName">Weapon name</param>
        /// <returns>Weapon if successful, otherwise "null"</returns>
        IWeapon GetWeaponByName(string weaponName);

        /// <summary>
        /// Tries to get weapon by name
        /// </summary>
        /// <param name="weaponName">Weapon name</param>
        /// <param name="weapon">Weapon</param>
        /// <returns>"true" if weapon exists, otherwise "false"</returns>
        bool TryGetWeaponByName(string weaponName, out IWeapon weapon);
    }
}
