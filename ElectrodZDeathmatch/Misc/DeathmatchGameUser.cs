using ElectrodZMultiplayer;
using ElectrodZMultiplayer.Server;
using System;
using System.Collections.Generic;

/// <summary>
/// ElectrodZ deathmatch namespace
/// </summary>
namespace ElectrodZDeathmatch
{
    /// <summary>
    /// A class that describes a deathmatch game user
    /// </summary>
    internal class DeathmatchGameUser : AGameUser, IDeathmatchGameUser
    {
        /// <summary>
        /// Damage contributions
        /// </summary>
        private readonly List<IDamageContribution> damageContributions = new List<IDamageContribution>();

        /// <summary>
        /// Maximal health
        /// </summary>
        private float maximalHealth = 1.0f;

        /// <summary>
        /// Respawn time
        /// </summary>
        private double respawnTime;

        /// <summary>
        /// Damage contributions
        /// </summary>
        public IEnumerable<IDamageContribution> DamageContributions => damageContributions;

        /// <summary>
        /// Health
        /// </summary>
        public float Health { get; private set; } = 1.0f;

        /// <summary>
        /// Maximal health
        /// </summary>
        public float MaximalHealth
        {
            get => maximalHealth;
            set
            {
                if (value < 0.0f)
                {
                    throw new ArgumentException("Maximal health can't be negative.", nameof(value));
                }
                maximalHealth = value;
                Health = Math.Min(Health, maximalHealth);
            }
        }

        /// <summary>
        /// Is user alive
        /// </summary>
        public bool IsAlive => Health > float.Epsilon;

        /// <summary>
        /// Respawn time
        /// </summary>
        public double RespawnTime
        {
            get => respawnTime;
            set
            {
                if (value < 0.0f)
                {
                    throw new ArgumentException("Respawn time can't be negative.", nameof(value));
                }
                respawnTime = value;
            }
        }

        /// <summary>
        /// This event will be invoked when this user has respawned
        /// </summary>
        public event RespawnedDelegate OnRespawned;

        /// <summary>
        /// This event will be invoked when this user took damage
        /// </summary>
        public event DamageTakenDelegate OnDamageTaken;

        /// <summary>
        /// This event will be invoked when this user has died
        /// </summary>
        public event DiedDelegate OnDied;

        /// <summary>
        /// Constructs a deathmatch game user
        /// </summary>
        /// <param name="serverUser">Server user</param>
        public DeathmatchGameUser(IServerUser serverUser) : base(serverUser)
        {
            // ...
        }

        /// <summary>
        /// Applys damage to user
        /// </summary>
        /// <param name="damage">Damage</param>
        public void ApplyDamage(float damage) => ApplyDamage(damage, null);

        /// <summary>
        /// Applys damage to user
        /// </summary>
        /// <param name="damage">Damage</param>
        /// <param name="issuer">Issuer</param>
        public void ApplyDamage(float damage, IGameEntity issuer)
        {
            if (damage < 0.0)
            {
                throw new ArgumentException("Damage contribution must be positive.", nameof(damage));
            }
            if ((issuer != null) && !issuer.IsValid)
            {
                throw new ArgumentException("Issuer is not valid.", nameof(issuer));
            }
            if (IsAlive)
            {
                if (issuer != null)
                {
                    damageContributions.Add(new DamageContribution(damage, issuer, DateTime.Now));
                }
                Health = Math.Max(Health - damage, 0.0f);
                OnDamageTaken?.Invoke(damage, issuer);
                if (!IsAlive)
                {
                    Dictionary<string, IDamageContributionResult> issuer_lookup = new Dictionary<string, IDamageContributionResult>();
                    DateTime now = DateTime.Now;
                    foreach (IDamageContribution damage_contribution in damageContributions)
                    {
                        if ((now - damage_contribution.DateAndTime).TotalSeconds < 10.0f)
                        {
                            string key = damage_contribution.Issuer.GUID.ToString();
                            if (issuer_lookup.ContainsKey(key))
                            {
                                issuer_lookup[key] = new DamageContributionResult(issuer_lookup[key].Damage + damage_contribution.Damage, damage_contribution.Issuer);
                            }
                            else
                            {
                                issuer_lookup.Add(key, new DamageContributionResult(damage_contribution.Damage, damage_contribution.Issuer));
                            }
                        }
                    }
                    List<IDamageContributionResult> issuers = new List<IDamageContributionResult>(issuer_lookup.Values);
                    issuer_lookup.Clear();
                    issuers.Sort((left, right) => left.Damage.CompareTo(right.Damage));
                    OnDied?.Invoke(issuers);
                }
            }
        }

        /// <summary>
        /// Heals user
        /// </summary>
        public void Heal() => Health = maximalHealth;

        /// <summary>
        /// Processes tick for user
        /// </summary>
        /// <param name="deltaTime">Delta time</param>
        public void ProcessTick(TimeSpan deltaTime)
        {
            if (RespawnTime > float.Epsilon)
            {
                respawnTime -= deltaTime.TotalSeconds;
                if (respawnTime <= float.Epsilon)
                {
                    respawnTime = 0.0;
                    OnRespawned?.Invoke();
                }
            }
        }
    }
}
