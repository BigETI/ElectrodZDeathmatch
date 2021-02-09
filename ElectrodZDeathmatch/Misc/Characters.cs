using System.Collections.Generic;

/// <summary>
/// ElectrodZ deathmatch namespace
/// </summary>
namespace ElectrodZDeathmatch
{
    /// <summary>
    /// A class that describes characters
    /// </summary>
    public class Characters : List<string>, ICharacters
    {
        /// <summary>
        /// Constructs characters
        /// </summary>
        public Characters() : base()
        {
            // ...
        }

        /// <summary>
        /// Constructs characters
        /// </summary>
        /// <param name="characters">Characters</param>
        public Characters(IEnumerable<string> characters) : base(characters)
        {
            // ...
        }
    }
}
