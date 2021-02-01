/// <summary>
/// ElectrodZ deathmatch namespace
/// </summary>
namespace ElectrodZDeathmatch
{
    /// <summary>
    /// Used to signal when asset loading was successful
    /// </summary>
    /// <typeparam name="TData">Data type</typeparam>
    /// <typeparam name="TResult">Result type</typeparam>
    /// <param name="data">Data</param>
    /// <returns>Result</returns>
    public delegate TResult AssetSuccessfullyLoadedDelegate<TData, TResult>(TData data);
}
