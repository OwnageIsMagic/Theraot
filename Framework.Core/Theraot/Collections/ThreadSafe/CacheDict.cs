﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Theraot.Core;

namespace Theraot.Collections.ThreadSafe
{
    /// <summary>
    ///     Provides a dictionary-like object used for caches which holds onto a maximum
    ///     number of elements specified at construction time.
    /// </summary>
    public sealed class CacheDict<TKey, TValue>
        where TKey : class
    {
        private readonly Entry[] _entries;
        private readonly Func<TKey, TValue>? _valueFactory;

        public CacheDict(int capacity)
        {
            _valueFactory = null;
            _entries = new Entry[NumericHelper.PopulationCount(capacity) == 1 ? capacity : NumericHelper.NextPowerOf2(capacity)];
        }

        /// <summary>
        ///     Creates a dictionary-like object used for caches.
        /// </summary>
        /// <param name="valueFactory">Value factory.</param>
        /// <param name="capacity">The maximum number of elements to store will be this number aligned to next ^2.</param>
        public CacheDict(Func<TKey, TValue> valueFactory, int capacity)
        {
            _valueFactory = valueFactory ?? throw new ArgumentNullException(nameof(valueFactory));
            _entries = new Entry[NumericHelper.PopulationCount(capacity) == 1 ? capacity : NumericHelper.NextPowerOf2(capacity)];
        }

        /// <summary>
        ///     Gets or sets the value associated with the given key.
        /// </summary>
        public TValue this[TKey key]
        {
            get
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                var hash = key.GetHashCode();
                var index = hash & (_entries.Length - 1);
                var entry = Volatile.Read(ref _entries[index]);
                if (entry?.Hash == hash && entry.Key.Equals(key))
                {
                    return entry.Value;
                }

                if (_valueFactory == null)
                {
                    throw new KeyNotFoundException();
                }

                var value = _valueFactory(key);
                Volatile.Write(ref _entries[index], new Entry(hash, key, value));
                return value;
            }
            set => Add(key, value);
        }

        public void Add(TKey key, TValue value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var hash = key.GetHashCode();
            var index = hash & (_entries.Length - 1);
            var entry = Volatile.Read(ref _entries[index]);
            if (entry?.Hash != hash || !entry.Key.Equals(key))
            {
                Volatile.Write(ref _entries[index], new Entry(hash, key, value));
            }
        }

        public bool TryGetValue(TKey key, [NotNullWhen(true)] out TValue value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var hash = key.GetHashCode();
            var index = hash & (_entries.Length - 1);
            var entry = Volatile.Read(ref _entries[index]);
            if (entry?.Hash == hash && entry.Key.Equals(key))
            {
                value = entry.Value;
                return value != null!;
            }

            value = default!;
            return false;
        }

        // class, to ensure atomic updates.
        private sealed class Entry
        {
            internal readonly int Hash;
            internal readonly TKey Key;
            internal readonly TValue Value;

            internal Entry(int hash, TKey key, TValue value)
            {
                Hash = hash;
                Key = key;
                Value = value;
            }
        }
    }
}