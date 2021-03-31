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
        /// Server lobby
        /// </summary>
        private IServerLobby serverLobby;

        /// <summary>
        /// Weapon pickup
        /// </summary>
        private WeaponPickup[] weaponPickups = Array.Empty<WeaponPickup>();

        /// <summary>
        /// Weapon pickup radius squared
        /// </summary>
        private float weaponPickupRadiusSquared;

        /// <summary>
        /// Remaining round time
        /// </summary>
        public double RemainingRoundTime { get; private set; } = 180.0;

        /// <summary>
        /// Characters
        /// </summary>
        public ICharacters Characters { get; private set; }

        /// <summary>
        /// Rules
        /// </summary>
        public IRules Rules { get; private set; }

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
        /// Spawns the specified user
        /// </summary>
        /// <param name="gameUser">Game user</param>
        private void SpawnUser(IDeathmatchGameUser gameUser)
        {
            ISpawnPoint spawn_point = PlayerCharacterSpawnPoints.NextSpawnPoint;
            if (spawn_point != null)
            {
                gameUser.SetSpectatingState(false);
                gameUser.Heal();
                gameUser.SetPosition(spawn_point.Position);
                gameUser.SetRotation(spawn_point.Rotation);
                gameUser.SetVelocity(Vector3.Zero);
                gameUser.SetAngularVelocity(Vector3.Zero);
            }
        }

        /// <summary>
        /// Game mode has been initialized
        /// </summary>
        /// <param name="gameResource">Game resource</param>
        /// <param name="serverLobby">Server lobby</param>
        public void OnInitialized(IGameResource gameResource, IServerLobby serverLobby)
        {
            this.serverLobby = serverLobby;
            if (gameResource is IDeathmatchGameResource deathmatch_game_resource)
            {
                Characters = deathmatch_game_resource.Assets.Get<string[], ICharacters>("./Assets/Deathmatch/characters.json");
                Rules = deathmatch_game_resource.Assets.Get<RulesData, IRules>("./Assets/Deathmatch/rules.json");
                PlayerCharacterSpawnPoints = deathmatch_game_resource.Assets.Get<SpawnPointData[], ISpawnPoints>("./Assets/Deathmatch/player-character-spawn-points.json");
                WeaponSpawnPoints = deathmatch_game_resource.Assets.Get<SpawnPointData[], ISpawnPoints>("./Assets/Deathmatch/weapon-spawn-points.json");
                Weapons = deathmatch_game_resource.Assets.Get<WeaponData[], IWeapons>("./Assets/Deathmatch/weapons.json");
                if (WeaponSpawnPoints.Count > 0)
                {
                    weaponPickups = new WeaponPickup[WeaponSpawnPoints.Count];
                    for (int index = 0; index < WeaponSpawnPoints.Count; index++)
                    {
                        weaponPickups[index] = new WeaponPickup(Weapons.RandomWeapon, WeaponSpawnPoints[index], Rules.WeaponPickupRespawnTime, serverLobby);
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
                Rules = new Rules();
                PlayerCharacterSpawnPoints = new SpawnPoints();
                WeaponSpawnPoints = new SpawnPoints();
                Weapons = new Weapons();
            }
            RemainingRoundTime = Rules.RoundTime;
            weaponPickupRadiusSquared = Rules.WeaponPickupRadius * Rules.WeaponPickupRadius;
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
            RemainingRoundTime = 0.0;
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
                deathmatchGameUsers.Add(deathmatch_game_user.GUID.ToString(), deathmatch_game_user);
                deathmatch_game_user.OnRespawned += () => SpawnUser(deathmatch_game_user);
                deathmatch_game_user.OnDied += (issuers) =>
                {
                    deathmatch_game_user.SetSpectatingState(true);
                    deathmatch_game_user.SetPosition(Rules.OutOfMapPosition);
                    deathmatch_game_user.SetRotation(Rules.OutOfMapRotation);
                    deathmatch_game_user.RespawnTime = Rules.PlayerCharacterRespawnTime;
                };
                deathmatch_game_user.MaximalHealth = Rules.PlayerCharacterHealth;
                SpawnUser(deathmatch_game_user);
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
        public void OnUserLeft(IGameUser gameUser)
        {
            deathmatchGameUsers.Remove(gameUser.GUID.ToString());
            Console.WriteLine($"User \"{ gameUser.Name }\" with GUID \"{ gameUser.GUID }\" has left the game.");
        }

        /// <summary>
        /// Game entity has been created
        /// </summary>
        /// <param name="gameEntity">Game entity</param>
        public void OnGameEntityCreated(IGameEntity gameEntity)
        {
            // ...
        }

        /// <summary>
        /// Game entity has been destroyed
        /// </summary>
        /// <param name="gameEntity">Game entity</param>
        public void OnGameEntityDestroyed(IGameEntity gameEntity)
        {
            // ...
        }

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
            bool ret = false;
            if ((victim is IDeathmatchGameUser victim_deathmatch_game_user) && victim_deathmatch_game_user.IsAlive && ((issuer == null) || ((issuer is IDeathmatchGameUser issuer_deathmatch_game_user) && issuer_deathmatch_game_user.IsAlive)))
            {
                if (Weapons.TryGetWeaponByName(weaponName, out IWeapon weapon))
                {
                    if (weapon.Damage < damage)
                    {
                        if (issuer == null)
                        {
                            Console.Error.WriteLine($"Weapon \"{ weapon.Name }\" damage \"{ damage }\" on victim with GUID \"{ victim_deathmatch_game_user.GUID }\" is higher than defined \"{ weapon.Damage }\".");
                        }
                        else
                        {
                            Console.Error.WriteLine($"Weapon \"{ weapon.Name }\" damage \"{ damage }\" on victim with GUID \"{ victim_deathmatch_game_user.GUID }\" by issuer with GUID \"{ issuer.GUID }\" is higher than defined \"{ weapon.Damage }\".");
                        }
                    }
                    else
                    {
                        victim_deathmatch_game_user.ApplyDamage(damage, issuer);
                        ret = true;
                    }
                }
                else
                {
                    Console.Error.WriteLine($"Weapon \"{ weaponName }\" is not defined.");
                }
            }
            return ret;
        }

        /// <summary>
        /// Game has been ticked
        /// </summary>
        /// <param name="deltaTime">Delta time</param>
        public void OnGameTicked(TimeSpan deltaTime)
        {
            if (RemainingRoundTime > float.Epsilon)
            {
                RemainingRoundTime = Math.Max(RemainingRoundTime - deltaTime.TotalSeconds, 0.0);
                foreach (WeaponPickup weapon_pickup in weaponPickups)
                {
                    weapon_pickup.Tick(deltaTime);
                    if (weapon_pickup.Entity != null)
                    {
                        foreach (IDeathmatchGameUser deathmatch_game_user in deathmatchGameUsers.Values)
                        {
                            if (deathmatch_game_user.IsAlive)
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
                foreach (IDeathmatchGameUser deathmatch_game_user in deathmatchGameUsers.Values)
                {
                    deathmatch_game_user.ProcessTick(deltaTime);
                }
                if ((RemainingRoundTime <= 0.0) && (serverLobby != null))
                {
                    serverLobby.StopGameModeInstance();
                }
            }
        }
    }
}
