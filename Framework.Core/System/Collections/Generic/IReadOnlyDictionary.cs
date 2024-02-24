﻿#if LESSTHAN_NET45

using System.Diagnostics.CodeAnalysis;

namespace System.Collections.Generic
{
    public partial interface IReadOnlyDictionary<TKey, TValue> : IReadOnlyCollection<KeyValuePair<TKey, TValue>>
    {
        IEnumerable<TKey> Keys { get; }

        IEnumerable<TValue> Values { get; }
    }

    public partial interface IReadOnlyDictionary<TKey, TValue>
    {
        TValue this[TKey key] { get; }
    }

    public partial interface IReadOnlyDictionary<TKey, TValue>
    {
        bool ContainsKey(TKey key);

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value);
    }
}

#endif