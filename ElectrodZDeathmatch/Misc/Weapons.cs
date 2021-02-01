using System;
using System.Collections.Generic;

/// <summary>
/// ElectrodZ deathmatch namespace
/// </summary>
namespace ElectrodZDeathmatch
{
    /// <summary>
    /// A class that describes a set of weapons
    /// </summary>
    public class Weapons : IWeapons
    {
        /// <summary>
        /// Weapons
        /// </summary>
        private readonly Dictionary<string, IWeapon> weapons = new Dictionary<string, IWeapon>();

        /// <summary>
        /// Random number generator
        /// </summary>
        private Random random = new Random();

        /// <summary>
        /// Random weapon
        /// </summary>
        public IWeapon RandomWeapon
        {
            get
            {
                IWeapon ret = Weapon.Empty;
                if (weapons.Count > 0)
                {
                    List<IWeapon> weapon_list = new List<IWeapon>(weapons.Values);
                    ret = weapon_list[random.Next(weapon_list.Count)];
                    weapon_list.Clear();
                }
                return ret;
            }
        }

        /// <summary>
        /// Constructs spawn points
        /// </summary>
        public Weapons()
        {
            // ...
        }

        /// <summary>
        /// Constructs spawn points
        /// </summary>
        /// <param name="weapons">Weapons</param>
        public Weapons(IEnumerable<IWeapon> weapons)
        {
            foreach (IWeapon weapon in weapons)
            {
                if ((weapon == null) || !weapon.IsValid)
                {
                    throw new ArgumentException("Weapons contain invalid weapons.", nameof(weapons));
                }
                if (this.weapons.ContainsKey(weapon.Name))
                {
                    Console.Error.WriteLine($"Skipping duplicate weapon entry \"{ weapon.Name }\".");
                }
                else
                {
                    this.weapons.Add(weapon.Name, new Weapon(weapon.Name, weapon.Damage, weapon.UsageCount));
                }
            }
        }

        /// <summary>
        /// Gets weapon by name
        /// </summary>
        /// <param name="weaponName">Weapon name</param>
        /// <returns>Weapon if successful, otherwise "null"</returns>
        public IWeapon GetWeaponByName(string weaponName) => TryGetWeaponByName(weaponName, out IWeapon ret) ? ret : null;

        /// <summary>
        /// Tries to get weapon by name
        /// </summary>
        /// <param name="weaponName">Weapon name</param>
        /// <param name="weapon">Weapon</param>
        /// <returns>"true" if weapon exists, otherwise "false"</returns>
        public bool TryGetWeaponByName(string weaponName, out IWeapon weapon)
        {
            if (weaponName == null)
            {
                throw new ArgumentNullException(nameof(weaponName));
            }
            return weapons.TryGetValue(weaponName, out weapon);
        }
    }
}
