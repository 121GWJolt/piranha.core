/*
 * Copyright (c) .NET Foundation and Contributors
 *
 * This software may be modified and distributed under the terms
 * of the MIT license. See the LICENSE file for details.
 *
 * https://github.com/piranhacms/piranha.core
 *
 */

using Microsoft.Extensions.Caching.Memory;

namespace Piranha.Cache;

/// <summary>
/// Simple in memory cache.
/// </summary>
public sealed class MemoryCache : ICache
{
    /// <summary>
    /// The private memory cache.
    /// </summary>
    private readonly IMemoryCache _cache;

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="cache">The currently configured cache</param>
    public MemoryCache(IMemoryCache cache)
    {
        _cache = cache;
    }

    /// <summary>
    /// Gets the model with the specified key from cache.
    /// </summary>
    /// <typeparam name="T">The model type</typeparam>
    /// <param name="key">The unique key</param>
    /// <returns>The cached model, null it wasn't found</returns>
    public T Get<T>(string key)
    {
        if (_cache.TryGetValue<T>(key, out var obj))
        {
            return obj;
        }
        return default(T);
    }

    /// <summary>
    /// Sets the given model in the cache.
    /// </summary>
    /// <typeparam name="T">The model type</typeparam>
    /// <param name="key">The unique key</param>
    /// <param name="value">The model</param>
    public void Set<T>(string key, T value)
    {
        _cache.Set(key, value);
    }

    /// <summary>
    /// Removes the model with the specified key from cache.
    /// </summary>
    /// <param name="key">The unique key</param>
    public void Remove(string key)
    {
        _cache.Remove(key);
    }
}
