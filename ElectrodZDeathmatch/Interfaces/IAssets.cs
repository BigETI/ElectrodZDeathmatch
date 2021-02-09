/// <summary>
/// ElectrodZ deathmatch namespace
/// </summary>
namespace ElectrodZDeathmatch
{
    /// <summary>
    /// An interface that represents a set of assets
    /// </summary>
    public interface IAssets
    {
        /// <summary>
        /// Adds a new asset loader
        /// </summary>
        /// <typeparam name="TData">Data type</typeparam>
        /// <typeparam name="TResult">Result type</typeparam>
        /// <param name="onAssetSuccessfullyLoaded">Gets invoked when asset loading was successful</param>
        /// <param name="onAssetLoadingFailed">Gets invoked when asset loading has failed</param>
        /// <returns>Asset loader</returns>
        IAssetLoader<TData, TResult> AddAssetLoader<TData, TResult>(AssetSuccessfullyLoadedDelegate<TData, TResult> onAssetSuccessfullyLoaded, AssetLoadingFailedDelegate<TResult> onAssetLoadingFailed);

        /// <summary>
        /// Reloads asset from file
        /// </summary>
        /// <typeparam name="TData">Data type</typeparam>
        /// <typeparam name="TResult">Result type</typeparam>
        /// <param name="path">Asset file path</param>
        /// <returns>Asset</returns>
        TResult ReloadFromFile<TData, TResult>(string path);

        /// <summary>
        /// Reloads asset from file
        /// </summary>
        /// <typeparam name="TData">Data type</typeparam>
        /// <typeparam name="TResult">Result type</typeparam>
        /// <param name="path">Asset file path</param>
        /// <param name="defaultValue">Default value</param>
        /// <returns>Asset</returns>
        TResult ReloadFromFile<TData, TResult>(string path, TResult defaultValue);

        /// <summary>
        /// Gets asset
        /// </summary>
        /// <typeparam name="TData">Data type</typeparam>
        /// <typeparam name="TResult">Result type</typeparam>
        /// <param name="path">Asset file path</param>
        /// <returns>Asset</returns>
        TResult Get<TData, TResult>(string path);


        /// <summary>
        /// Gets asset
        /// </summary>
        /// <typeparam name="TData">Data type</typeparam>
        /// <typeparam name="TResult">Result type</typeparam>
        /// <param name="path">Asset file path</param>
        /// <param name="defaultValue">Default value</param>
        /// <returns>Asset</returns>
        TResult Get<TData, TResult>(string path, TResult defaultValue);
    }
}
