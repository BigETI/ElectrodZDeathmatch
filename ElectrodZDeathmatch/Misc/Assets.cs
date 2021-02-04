using System;
using System.Collections.Generic;

/// <summary>
/// ElectrodZ deathmatch namespace
/// </summary>
namespace ElectrodZDeathmatch
{
    /// <summary>
    /// A class that describes a set of assets
    /// </summary>
    public class Assets : IAssets
    {
        /// <summary>
        /// Asset cache
        /// </summary>
        private readonly Dictionary<string, object> assetCache = new Dictionary<string, object>();

        /// <summary>
        /// Asset loaders
        /// </summary>
        private readonly Dictionary<string, IBaseAssetLoader> assetLoaders = new Dictionary<string, IBaseAssetLoader>();

        /// <summary>
        /// Get asset loader key
        /// </summary>
        /// <typeparam name="TData">Data type</typeparam>
        /// <typeparam name="TResult">Result type</typeparam>
        /// <returns></returns>
        private static string GetAssetLoaderKey<TData, TResult>() => $"{ typeof(TData) }->{ typeof(TResult) }";

        /// <summary>
        /// Adds a new asset loader
        /// </summary>
        /// <typeparam name="TData">Data type</typeparam>
        /// <typeparam name="TResult">Result type</typeparam>
        /// <param name="onAssetSuccessfullyLoaded">Gets invoked when asset loading was successful</param>
        /// <param name="onAssetLoadingFailed">Gets invoked when asset loading has failed</param>
        /// <returns>Asset loader</returns>
        public IAssetLoader<TData, TResult> AddAssetLoader<TData, TResult>(AssetSuccessfullyLoadedDelegate<TData, TResult> onAssetSuccessfullyLoaded)
        {
            IAssetLoader<TData, TResult> ret = new AssetLoader<TData, TResult>(onAssetSuccessfullyLoaded ?? throw new ArgumentNullException(nameof(onAssetSuccessfullyLoaded)));
            string key = GetAssetLoaderKey<TData, TResult>();
            if (assetLoaders.ContainsKey(key))
            {
                assetLoaders[key] = ret;
            }
            else
            {
                assetLoaders.Add(key, ret);
            }
            return ret;
        }

        /// <summary>
        /// Adds a new asset loader
        /// </summary>
        /// <typeparam name="TData">Data type</typeparam>
        /// <typeparam name="TResult">Result type</typeparam>
        /// <param name="onAssetSuccessfullyLoaded">Gets invoked when asset loading was successful</param>
        /// <param name="onAssetLoadingFailed">Gets invoked when asset loading has failed</param>
        /// <returns>Asset loader</returns>
        public IAssetLoader<TData, TResult> AddAssetLoader<TData, TResult>(AssetSuccessfullyLoadedDelegate<TData, TResult> onAssetSuccessfullyLoaded, AssetLoadingFailedDelegate<TResult> onAssetLoadingFailed)
        {
            IAssetLoader<TData, TResult> ret = new AssetLoader<TData, TResult>(onAssetSuccessfullyLoaded ?? throw new ArgumentNullException(nameof(onAssetSuccessfullyLoaded)), onAssetLoadingFailed ?? throw new ArgumentNullException(nameof(onAssetLoadingFailed)));
            string key = GetAssetLoaderKey<TData, TResult>();
            if (assetLoaders.ContainsKey(key))
            {
                assetLoaders[key] = ret;
            }
            else
            {
                assetLoaders.Add(key, ret);
            }
            return ret;
        }

        /// <summary>
        /// Reloads asset from file
        /// </summary>
        /// <typeparam name="TData">Data type</typeparam>
        /// <typeparam name="TResult">Result type</typeparam>
        /// <param name="path">Asset file path</param>
        /// <returns>Asset</returns>
        public TResult ReloadFromFile<TData, TResult>(string path) => ReloadFromFile<TData, TResult>(path, default);

        /// <summary>
        /// Reloads asset from file
        /// </summary>
        /// <typeparam name="TData">Data type</typeparam>
        /// <typeparam name="TResult">Result type</typeparam>
        /// <param name="path">Asset file path</param>
        /// <param name="defaultValue">Default value</param>
        /// <returns>Asset</returns>
        public TResult ReloadFromFile<TData, TResult>(string path, TResult defaultValue)
        {
            TResult ret = defaultValue;
            string key = GetAssetLoaderKey<TData, TResult>();
            if (assetLoaders.ContainsKey(key) && assetLoaders[key] is IAssetLoader<TData, TResult> asset_loader)
            {
                ret = asset_loader.LoadAssetFromFile(path);
                if (assetCache.ContainsKey(path))
                {
                    assetCache[path] = ret;
                }
                else
                {
                    assetCache.Add(path, ret);
                }
            }
            return ret;
        }

        /// <summary>
        /// Gets asset
        /// </summary>
        /// <typeparam name="TData">Data type</typeparam>
        /// <typeparam name="TResult">Result type</typeparam>
        /// <param name="path">Asset file path</param>
        /// <returns>Asset</returns>
        public TResult Get<TData, TResult>(string path) => Get<TData, TResult>(path, default);


        /// <summary>
        /// Gets asset
        /// </summary>
        /// <typeparam name="TData">Data type</typeparam>
        /// <typeparam name="TResult">Result type</typeparam>
        /// <param name="path">Asset file path</param>
        /// <param name="defaultValue">Default value</param>
        /// <returns>Asset</returns>
        public TResult Get<TData, TResult>(string path, TResult defaultValue) => (assetCache.ContainsKey(path) && (assetCache[path] is TResult result)) ? result : ReloadFromFile<TData, TResult>(path, defaultValue);
    }
}
