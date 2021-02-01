using ElectrodZDeathmatch.Data;
using ElectrodZMultiplayer;
using ElectrodZMultiplayer.Server;
using System;
using System.Collections.Generic;

/// <summary>
/// ElectrodZ deathmatch game modes namespace
/// </summary>
namespace ElectrodZDeathmatch.GameModes
{
    /// <summary>
    /// A class that describes a deathmatch game mode
    /// </summary>
    public class DeathmatchGameMode : IGameMode
    {
        /// <summary>
        /// Game users
        /// </summary>
        private readonly Dictionary<string, IDeathmatchGameUser> deathmatchGameUsers = new Dictionary<string, IDeathmatchGameUser>();

        /// <summary>
        /// Weapon pickup
        /// </summary>
        private WeaponPickup[] weaponPickups = Array.Empty<WeaponPickup>();

        /// <summary>
        /// Weapon pickup radius squared
        /// </summary>
        private float weaponPickupRadiusSquared;

        /// <summary>
        /// Characters
        /// </summary>
        public ICharacters Characters { get; private set; }

        /// <summary>
        /// Defaults
        /// </summary>
        public IDefaults Defaults { get; private set; }

        /// <summary>
        /// Player character spawn points
        /// </summary>
        public ISpawnPoints PlayerCharacterSpawnPoints { get; private set; }

        /// <summary>
        /// Weapon spawn points
        /// </summary>
        public ISpawnPoints WeaponSpawnPoints { get; private set; }

        /// <summary>
        /// Weapons
        /// </summary>
        public IWeapons Weapons { get; private set; }

        /// <summary>
        /// Users with results
        /// </summary>
        public IReadOnlyDictionary<string, UserWithResults> UserResults { get; } = new Dictionary<string, UserWithResults>();

        /// <summary>
        /// Results
        /// </summary>
        public IReadOnlyDictionary<string, object> Results { get; } = new Dictionary<string, object>();

        /// <summary>
        /// SPawns the specified user
        /// </summary>
        /// <param name="gameUser">Game user</param>
        private void SpawnUser(IGameUser gameUser)
        {
            ISpawnPoint spawn_point = PlayerCharacterSpawnPoints.RandomSpawnPoint;
            gameUser.SetPosition(spawn_point.Position);
            gameUser.SetRotation(spawn_point.Rotation);
            gameUser.SetVelocity(Vector3.Zero);
            gameUser.SetAngularVelocity(Vector3.Zero);
        }

        /// <summary>
        /// Game mode has been initialized
        /// </summary>
        /// <param name="gameResource">Game resource</param>
        /// <param name="serverLobby">Server lobby</param>
        public void OnInitialized(IGameResource gameResource, IServerLobby serverLobby)
        {
            if (gameResource is IDeathmatchGameResource deathmatch_game_resource)
            {
                Characters = deathmatch_game_resource.Assets.Get<string[], ICharacters>("./Assets/characters.json");
                Defaults = deathmatch_game_resource.Assets.Get<DefaultsData, IDefaults>("./Assets/defaults.json");
                PlayerCharacterSpawnPoints = deathmatch_game_resource.Assets.Get<SpawnPointData[], ISpawnPoints>("./Assets/player-character-spawn-points.json");
                WeaponSpawnPoints = deathmatch_game_resource.Assets.Get<SpawnPointData[], ISpawnPoints>("./Assets/weapon-spawn-points.json");
                Weapons = deathmatch_game_resource.Assets.Get<WeaponData[], IWeapons>("./Assets/weapons.json");
                if (WeaponSpawnPoints.Count > 0)
                {
                    weaponPickups = new WeaponPickup[WeaponSpawnPoints.Count];
                    for (int index = 0; index < WeaponSpawnPoints.Count; index++)
                    {
                        weaponPickups[index] = new WeaponPickup(Weapons.RandomWeapon, WeaponSpawnPoints[index], Defaults.WeaponPickupRespawnTime, serverLobby);
                    }
                }
                else
                {
                    weaponPickups = Array.Empty<WeaponPickup>();
                }
            }
            else
            {
                Characters = new Characters();
                Defaults = new Defaults();
                PlayerCharacterSpawnPoints = new SpawnPoints();
                WeaponSpawnPoints = new SpawnPoints();
                Weapons = new Weapons();
            }
            weaponPickupRadiusSquared = Defaults.WeaponPickupRadius * Defaults.WeaponPickupRadius;
            Console.WriteLine("=========================================================");
            Console.WriteLine("=                                                       =");
            Console.WriteLine("=                 ElectrodZ Deathmatch                  =");
            Console.WriteLine("= GitHub: https://github.com/BigETI/ElectrodZDeathmatch =");
            Console.WriteLine("=                                                       =");
            Console.WriteLine("=========================================================");
            Console.WriteLine("=                                                       =");
            Console.WriteLine("=                     by Ethem Kurt                     =");
            Console.WriteLine("=                                                       =");
            Console.WriteLine("=                        loaded!                        =");
            Console.WriteLine("=                                                       =");
            Console.WriteLine("=========================================================");
        }

        /// <summary>
        /// Game mode has been closed
        /// </summary>
        public void OnClosed()
        {
            foreach (WeaponPickup weapon_pickup in weaponPickups)
            {
                weapon_pickup.Dispose();
            }
            weaponPickups = Array.Empty<WeaponPickup>();
            Console.WriteLine("=========================================================");
            Console.WriteLine("=                                                       =");
            Console.WriteLine("=                 ElectrodZ Deathmatch                  =");
            Console.WriteLine("= GitHub: https://github.com/BigETI/ElectrodZDeathmatch =");
            Console.WriteLine("=                                                       =");
            Console.WriteLine("=========================================================");
            Console.WriteLine("=                                                       =");
            Console.WriteLine("=                     by Ethem Kurt                     =");
            Console.WriteLine("=                                                       =");
            Console.WriteLine("=                       unloaded!                       =");
            Console.WriteLine("=                                                       =");
            Console.WriteLine("=========================================================");
        }

        /// <summary>
        /// User has joined the game
        /// </summary>
        /// <param name="gameUser">Game user</param>
        public void OnUserJoined(IGameUser gameUser)
        {
            if (gameUser is IDeathmatchGameUser deathmatch_game_user)
            {
                deathmatch_game_user.OnDied += (issuers) =>
                {
                    // TODO: Do something after a deathmatch game user dies
                };
                SpawnUser(gameUser);
                Console.WriteLine($"User \"{ deathmatch_game_user.Name }\" with GUID \"{ deathmatch_game_user.GUID }\" has joined the game.");
            }
            else
            {
                Console.Error.WriteLine($"User \"{ gameUser.Name }\" with GUID \"{ gameUser.GUID }\" has joined the game but is not a deathmatch game user.");
            }
        }

        /// <summary>
        /// User has left the game
        /// </summary>
        /// <param name="gameUser">Game user</param>
        public void OnUserLeft(IGameUser gameUser) => Console.WriteLine($"User \"{ gameUser.Name }\" with GUID \"{ gameUser.GUID }\" has left the game.");

        /// <summary>
        /// Game entity has been created
        /// </summary>
        /// <param name="gameEntity">Game entity</param>
        public void OnGameEntityCreated(IGameEntity gameEntity) => Console.WriteLine($"Game entity with GUID \"{ gameEntity.GUID }\" has been created.");

        /// <summary>
        /// Game entity has been destroyed
        /// </summary>
        /// <param name="gameEntity">Game entity</param>
        public void OnGameEntityDestroyed(IGameEntity gameEntity) => Console.WriteLine($"Game entity with GUID \"{ gameEntity.GUID }\" has been destroyed.");

        /// <summary>
        /// Game entity has been hit
        /// </summary>
        /// <param name="issuer">Issuer</param>
        /// <param name="victim">Victim</param>
        /// <param name="weaponName">Weapon name</param>
        /// <param name="hitPosition">Hit position</param>
        /// <param name="hitForce">Hit force</param>
        /// <param name="damage">Damage</param>
        /// <returns>"true" if game entity hit is valid, otherwise "false"</returns>
        public bool OnGameEntityHit(IGameEntity issuer, IGameEntity victim, string weaponName, Vector3 hitPosition, Vector3 hitForce, float damage)
        {
            if (Weapons.TryGetWeaponByName(weaponName, out IWeapon weapon))
            {
                bool is_damage_too_high = weapon.Damage < damage;
                if (issuer == null)
                {
                    Console.WriteLine($"Victim with GUID \"{ victim.GUID }\" has been hit with weapon \"{ weapon.Name }\". ");
                }
                else
                {
                    if (is_damage_too_high)
                    {
                        Console.Error.WriteLine($"Weapon \"{ weapon.Name }\" damage \"{ damage }\" by issuer with GUID \"{ issuer.GUID }\" is higher than defined \"{ weapon.Damage }\".");
                    }
                    else
                    {
                        if (victim is IDeathmatchGameUser victim_deathmatch_game_user)
                        {
                            victim_deathmatch_game_user.ApplyDamage(damage, issuer);
                        }
                        Console.WriteLine($"Victim with GUID \"{ victim.GUID }\" has been hit by entity with GUID \"{ issuer.GUID }\" with weapon \"{ weaponName }\". Damage: \"{ damage }\"; Hit point: \"{ hitPosition }\"; Hit force: \"{ hitForce }\".");
                    }
                }
            }
            else
            {
                Console.Error.WriteLine($"Weapon \"{ weaponName }\" is not defined.");
            }
            return true;
        }

        /// <summary>
        /// Game has been ticked
        /// </summary>
        /// <param name="deltaTime">Delta time</param>
        public void OnGameTicked(TimeSpan deltaTime)
        {
            foreach (WeaponPickup weapon_pickup in weaponPickups)
            {
                weapon_pickup.Tick(deltaTime);
                if (weapon_pickup.Entity != null)
                {
                    foreach (IDeathmatchGameUser deathmatch_game_user in deathmatchGameUsers.Values)
                    {
                        Vector3 delta = deathmatch_game_user.Position - weapon_pickup.Entity.Position;
                        if (delta.MagnitudeSquared <= weaponPickupRadiusSquared)
                        {
                            // TODO: Give weapon
                            weapon_pickup.Destroy();
                        }
                    }
                }
            }
        }
    }
}
