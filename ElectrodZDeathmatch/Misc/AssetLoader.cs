using Newtonsoft.Json;
using System;
using System.IO;

/// <summary>
/// ElectrodZ deathmatch namespace
/// </summary>
namespace ElectrodZDeathmatch
{
    /// <summary>
    /// A class that describes an asset loader
    /// </summary>
    /// <typeparam name="TData">Data type</typeparam>
    /// <typeparam name="TResult">Result type</typeparam>
    public class AssetLoader<TData, TResult> : IAssetLoader<TData, TResult>
    {
        /// <summary>
        /// Gets invoked when asset loading was successful
        /// </summary>
        private AssetSuccessfullyLoadedDelegate<TData, TResult> onAssetSuccessfullyLoaded;

        /// <summary>
        /// Gets invoked when asset loading has failed
        /// </summary>
        private AssetLoadingFailedDelegate<TResult> onAssetLoadingFailed;

        /// <summary>
        /// Gets invoked when asset loading was successful
        /// </summary>
        public event AssetSuccessfullyLoadedDelegate<TData, TResult> OnAssetSuccessfullyLoaded;

        /// <summary>
        /// Gets invoked when asset loading has failed
        /// </summary>
        public event AssetLoadingFailedDelegate<TResult> OnAssetLoadingFailed;

        /// <summary>
        /// Constructs an asset loader
        /// </summary>
        /// <param name="onAssetSuccessfullyLoaded">Gets invoked when asset loading was successful</param>
        public AssetLoader(AssetSuccessfullyLoadedDelegate<TData, TResult> onAssetSuccessfullyLoaded)
        {
            this.onAssetSuccessfullyLoaded = onAssetSuccessfullyLoaded ?? throw new ArgumentNullException(nameof(onAssetSuccessfullyLoaded));
            onAssetLoadingFailed = () => default;
            OnAssetSuccessfullyLoaded += onAssetSuccessfullyLoaded;
            OnAssetLoadingFailed += onAssetLoadingFailed;
        }

        /// <summary>
        /// Constructs an asset loader
        /// </summary>
        /// <param name="onAssetSuccessfullyLoaded">Gets invoked when asset loading was successful</param>
        /// <param name="onAssetLoadingFailed">Gets invoked when asset loading has failed</param>
        public AssetLoader(AssetSuccessfullyLoadedDelegate<TData, TResult> onAssetSuccessfullyLoaded, AssetLoadingFailedDelegate<TResult> onAssetLoadingFailed)
        {
            this.onAssetSuccessfullyLoaded = onAssetSuccessfullyLoaded ?? throw new ArgumentNullException(nameof(onAssetSuccessfullyLoaded));
            this.onAssetLoadingFailed = onAssetLoadingFailed ?? throw new ArgumentNullException(nameof(onAssetLoadingFailed));
            OnAssetSuccessfullyLoaded += onAssetSuccessfullyLoaded;
            OnAssetLoadingFailed += onAssetLoadingFailed;
        }

        /// <summary>
        /// Loads an asset from file
        /// </summary>
        /// <param name="path">Asset file path</param>
        /// <returns>Asset</returns>
        public TResult LoadAssetFromFile(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            TResult ret = default;
            bool is_not_loaded = true;
            try
            {
                if (File.Exists(path))
                {
                    using (FileStream file_stream = File.OpenRead(path))
                    {
                        using (StreamReader file_stream_reader = new StreamReader(file_stream))
                        {
                            TData data = JsonConvert.DeserializeObject<TData>(file_stream_reader.ReadToEnd());
                            if (data == null)
                            {
                                Console.Error.WriteLine($"Failed to parse asset from \"{ path }\".");
                            }
                            else
                            {
                                is_not_loaded = false;
                                ret = onAssetSuccessfullyLoaded(data);
                                OnAssetSuccessfullyLoaded?.Invoke(data);
                            }
                        }
                    }
                }
                else
                {
                    Console.Error.WriteLine($"Asset file \"{ path }\" does not exist.");
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
            if (is_not_loaded)
            {
                ret = onAssetLoadingFailed();
                OnAssetLoadingFailed?.Invoke();
            }
            return ret;
        }
    }
}
