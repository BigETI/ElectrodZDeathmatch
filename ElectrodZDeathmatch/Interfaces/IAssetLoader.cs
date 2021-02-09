/// <summary>
/// ElectrodZ deathmatch namespace
/// </summary>
namespace ElectrodZDeathmatch
{
    /// <summary>
    /// An interface that represents an asset loader
    /// </summary>
    /// <typeparam name="TData">Data type</typeparam>
    /// <typeparam name="TResult">Result type</typeparam>
    public interface IAssetLoader<TData, TResult> : IBaseAssetLoader
    {
        /// <summary>
        /// Gets invoked when asset loading was successful
        /// </summary>
        event AssetSuccessfullyLoadedDelegate<TData, TResult> OnAssetSuccessfullyLoaded;

        /// <summary>
        /// Gets invoked when asset loading has failed
        /// </summary>
        event AssetLoadingFailedDelegate<TResult> OnAssetLoadingFailed;

        /// <summary>
        /// Loads an asset from file
        /// </summary>
        /// <param name="path">Asset file path</param>
        /// <returns>Asset</returns>
        TResult LoadAssetFromFile(string path);
    }
}
