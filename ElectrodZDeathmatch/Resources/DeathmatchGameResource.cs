using ElectrodZDeathmatch.Data;
using ElectrodZDeathmatch.Factories;
using ElectrodZMultiplayer;
using ElectrodZMultiplayer.Server;
using System.IO;
using System.Threading.Tasks;

/// <summary>
/// ElectrodZ deathmatch resources namespace
/// </summary>
namespace ElectrodZDeathmatch.Resources
{
    /// <summary>
    /// A class that describes a deathmatch game resource
    /// </summary>
    public class DeathmatchGameResource : AGameResource, IDeathmatchGameResource
    {
        /// <summary>
        /// Assets
        /// </summary>
        public IAssets Assets { get; } = new Assets();

        /// <summary>
        /// Constructs a deathmatch game resource
        /// </summary>
        public DeathmatchGameResource()
        {
            Assets.AddAssetLoader<string[], ICharacters>((data) => new Characters(data), () => new Characters());
            Assets.AddAssetLoader<RulesData, IRules>((data) => new Rules(new Vector3(data.OutOfMapPosition.X, data.OutOfMapPosition.Y, data.OutOfMapPosition.Z), data.PlayerCharacterHealth, data.PlayerCharacterRespawnTime, data.RoundTime, data.WeaponPickupRadius, data.WeaponPickupRespawnTime), () => new Rules());
            Assets.AddAssetLoader<SpawnPointData[], ISpawnPoints>
            (
                (data) =>
                {
                    ISpawnPoint[] spawn_points = new ISpawnPoint[data.Length];
                    Parallel.For(0, spawn_points.Length, (index) =>
                    {
                        SpawnPointData spawn_point_data = data[index];
                        if ((spawn_point_data == null) || !spawn_point_data.IsValid)
                        {
                            throw new InvalidDataException("Spawn points contain invalid entries.");
                        }
                        spawn_points[index] = new SpawnPoint(new Vector3(spawn_point_data.Position.X, spawn_point_data.Position.Y, spawn_point_data.Position.Z), new Quaternion(spawn_point_data.Rotation.X, spawn_point_data.Rotation.Y, spawn_point_data.Rotation.Z, spawn_point_data.Rotation.W));
                    });
                    return new SpawnPoints(spawn_points);
                },
                () => new SpawnPoints()
            );
            Assets.AddAssetLoader<WeaponData[], IWeapons>
            (
                (data) =>
                {
                    IWeapon[] weapons = new IWeapon[data.Length];
                    Parallel.For(0, weapons.Length, (index) =>
                    {
                        WeaponData weapon = data[index];
                        if ((weapon == null) || !weapon.IsValid)
                        {
                            throw new InvalidDataException("Weapons contain invalid weapon entries.");
                        }
                        weapons[index] = new Weapon(weapon.Name, weapon.Damage, weapon.UsageCount);
                    });
                    return new Weapons(weapons);
                },
                () => new Weapons()
            );
        }

        /// <summary>
        /// Creates a new game user factory
        /// </summary>
        /// <returns>Game user factory</returns>
        public override IGameUserFactory CreateNewGameUserFactory() => new DeathmatchGameUserFactory();

        /// <summary>
        /// Creates a new game entity factory
        /// </summary>
        /// <returns>Game entity factory</returns>
        public override IGameEntityFactory CreateNewGameEntityFactory() => new DeathmatchGameEntityFactory();
    }
}
