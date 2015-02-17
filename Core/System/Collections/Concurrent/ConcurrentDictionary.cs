﻿using System.Collections.Generic;
using Theraot.Collections;
using Theraot.Collections.Specialized;
using Theraot.Collections.ThreadSafe;
using Theraot.Threading.Needles;

namespace System.Collections.Concurrent
{
    [SerializableAttribute]
    public class ConcurrentDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IDictionary
    {
        private const int INT_DefaultCapacity = 31;
        private const int INT_DefaultConcurrency = 4;
        private readonly LockableContext _context;
        private readonly Pool<LockableNeedle<TValue>> _pool;
        private readonly ConvertedValueCollection<TKey, LockableNeedle<TValue>, TValue> _valueCollection;
        private readonly SafeDictionary<TKey, LockableNeedle<TValue>> _wrapped;

        public ConcurrentDictionary()
            : this(INT_DefaultConcurrency, INT_DefaultCapacity, EqualityComparer<TKey>.Default)
        {
            //Empty
        }

        public ConcurrentDictionary(int concurrencyLevel, int capacity)
            : this(concurrencyLevel, capacity, EqualityComparer<TKey>.Default)
        {
            //Empty
        }

        public ConcurrentDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection)
            : this(INT_DefaultConcurrency, INT_DefaultCapacity, EqualityComparer<TKey>.Default)
        {
            if (ReferenceEquals(collection, null))
            {
                throw new ArgumentNullException("collection");
            }
            AddRange(collection);
        }

        public ConcurrentDictionary(IEqualityComparer<TKey> comparer)
            : this(INT_DefaultConcurrency, INT_DefaultCapacity, comparer)
        {
            //Empty
        }

        public ConcurrentDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer)
            : this(INT_DefaultConcurrency, INT_DefaultCapacity, comparer)
        {
            if (ReferenceEquals(collection, null))
            {
                throw new ArgumentNullException("collection");
            }
            AddRange(collection);
        }

        public ConcurrentDictionary(int concurrencyLevel, IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer)
            : this(concurrencyLevel, INT_DefaultCapacity, comparer)
        {
            if (ReferenceEquals(collection, null))
            {
                throw new ArgumentNullException("collection");
            }
            AddRange(collection);
        }

        public ConcurrentDictionary(int concurrencyLevel, int capacity, IEqualityComparer<TKey> comparer)
        {
            if (ReferenceEquals(comparer, null))
            {
                throw new ArgumentNullException("comparer");
            }
            if (concurrencyLevel < 1)
            {
                throw new ArgumentOutOfRangeException("concurrencyLevel", "concurrencyLevel < 1");
            }
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException("capacity", "capacity < 0");
            }
            _context = new LockableContext(concurrencyLevel);
            _wrapped = new SafeDictionary<TKey, LockableNeedle<TValue>>();
            _valueCollection = new ConvertedValueCollection<TKey, LockableNeedle<TValue>, TValue>(_wrapped, input => input.Value);
            _pool = new Pool<LockableNeedle<TValue>>(64, Recycle);
        }

        public int Count
        {
            get
            {
                using (_context.Enter())
                {
                    AcquireAllLocks();
                    return _wrapped.Count;
                }
            }
        }

        public bool IsEmpty
        {
            get
            {
                return Count == 0;
            }
        }

        bool IDictionary.IsFixedSize
        {
            get
            {
                return false;
            }
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        bool IDictionary.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        bool ICollection.IsSynchronized
        {
            get
            {
                return false;
            }
        }

        ICollection<TKey> IDictionary<TKey, TValue>.Keys
        {
            get
            {
                return _wrapped.Keys;
            }
        }

        ICollection IDictionary.Keys
        {
            get
            {
                return (ICollection)_wrapped.Keys;
            }
        }

        object ICollection.SyncRoot
        {
            get
            {
                return this;
            }
        }

        ICollection<TValue> IDictionary<TKey, TValue>.Values
        {
            get
            {
                return _valueCollection;
            }
        }

        ICollection IDictionary.Values
        {
            get
            {
                return _valueCollection;
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                // key can be null
                if (ReferenceEquals(key, null))
                {
                    // ConcurrentDictionary hates null
                    throw new ArgumentNullException("key");
                }
                var needle = _wrapped[key];
                return needle.Value;
            }
            set
            {
                if (ReferenceEquals(key, null))
                {
                    // ConcurrentDictionary hates null
                    throw new ArgumentNullException("key");
                }
                using (_context.Enter())
                {
                    LockableNeedle<TValue> created = GetNeedle(value);
                    LockableNeedle<TValue> stored;
                    if (!_wrapped.TryGetOrAdd(key, created, out stored))
                    {
                        // created but not added
                        _pool.Donate(created);
                    }
                    stored.Value = value;
                }
            }
        }

        object IDictionary.this[object key]
        {
            get
            {
                if (key == null)
                {
                    throw new ArgumentNullException("key");
                }
                // keep the is operator
                if (key is TKey)
                {
                    LockableNeedle<TValue> result;
                    if (_wrapped.TryGetValue((TKey) key, out result))
                    {
                        return result.Value;
                    }
                }
                return null;
            }
            set
            {
                if (ReferenceEquals(key, null))
                {
                    // ConcurrentDictionary hates null
                    throw new ArgumentNullException("key");
                }
                // keep the is operator
                if (key is TKey && value is TValue)
                {
                    this[(TKey)key] = (TValue)value;
                }
                throw new ArgumentException();
            }
        }

        public TValue AddOrUpdate(TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory)
        {
            if (ReferenceEquals(key, null))
            {
                // ConcurrentDictionary hates null
                throw new ArgumentNullException("key");
            }
            if (ReferenceEquals(addValueFactory, null))
            {
                throw new ArgumentNullException("addValueFactory");
            }
            if (ReferenceEquals(updateValueFactory, null))
            {
                throw new ArgumentNullException("updateValueFactory");
            }
            using (_context.Enter())
            {
                bool added;
                LockableNeedle<TValue> created = null;
                var result = _wrapped.AddOrUpdate
                    (
                        key,
                        input => created = GetNeedle(addValueFactory(input)),
                        (inputKey, inputValue) =>
                        {
                            inputValue.Value = updateValueFactory(inputKey, inputValue.Value);
                            return inputValue;
                        },
                        out added
                    ).Value;
                if (!added)
                {
                    _pool.Donate(created);
                }
                return result;
            }
        }

        public TValue AddOrUpdate(TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory)
        {
            if (ReferenceEquals(key, null))
            {
                // ConcurrentDictionary hates null
                throw new ArgumentNullException("key");
            }
            if (ReferenceEquals(updateValueFactory, null))
            {
                throw new ArgumentNullException("updateValueFactory");
            }
            using (_context.Enter())
            {
                bool added;
                LockableNeedle<TValue> created = null;
                var result = _wrapped.AddOrUpdate
                    (
                        key,
                        input => created = GetNeedle(addValue),
                        (inputKey, inputValue) =>
                        {
                            inputValue.Value = updateValueFactory(inputKey, inputValue.Value);
                            return inputValue;
                        },
                        out added
                    ).Value;
                if (!added)
                {
                    _pool.Donate(created);
                }
                return result;
            }
        }

        public void Clear()
        {
            using (_context.Enter())
            {
                AcquireAllLocks();
                _wrapped.Clear();
            }
        }

        public bool ContainsKey(TKey key)
        {
            if (ReferenceEquals(key, null))
            {
                // ConcurrentDictionary hates null
                throw new ArgumentNullException("key");
            }
            // No existing value is set, so no locking, right?
            return _wrapped.ContainsKey(key);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var pair in _wrapped)
            {
                yield return new KeyValuePair<TKey, TValue>(pair.Key, pair.Value.Value);
            }
        }

        public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
        {
            // No existing value is set, so no locking, right?
            if (ReferenceEquals(key, null))
            {
                // ConcurrentDictionary hates null
                throw new ArgumentNullException("key");
            }
            if (ReferenceEquals(valueFactory, null))
            {
                throw new ArgumentNullException("valueFactory");
            }
            using (_context.Enter())
            {
                LockableNeedle<TValue> created = null;
                LockableNeedle<TValue> result;
                if (!_wrapped.TryGetOrAdd(key, input => created = GetNeedle(valueFactory(input)), out result))
                {
                    _pool.Donate(created);
                }
                return result.Value;
            }
        }

        public TValue GetOrAdd(TKey key, TValue value)
        {
            // No existing value is set, so no locking, right?
            if (ReferenceEquals(key, null))
            {
                // ConcurrentDictionary hates null
                throw new ArgumentNullException("key");
            }
            using (_context.Enter())
            {
                LockableNeedle<TValue> created = null;
                LockableNeedle<TValue> result;
                if (!_wrapped.TryGetOrAdd(key, input => created = GetNeedle(value), out result))
                {
                    _pool.Donate(created);
                }
                return result.Value;
            }
        }

        public KeyValuePair<TKey, TValue>[] ToArray()
        {
            using (_context.Enter())
            {
                AcquireAllLocks();
                var result = new List<KeyValuePair<TKey, TValue>>(_wrapped.Count);
                foreach (var pair in _wrapped)
                {
                    result.Add(new KeyValuePair<TKey, TValue>(pair.Key, pair.Value.Value));
                }
                return result.ToArray();
            }
        }

        public bool TryAdd(TKey key, TValue value)
        {
            if (ReferenceEquals(key, null))
            {
                // ConcurrentDictionary hates null
                throw new ArgumentNullException("key");
            }
            using (_context.Enter())
            {
                var created = GetNeedle(value);
                if (!_wrapped.TryAdd(key, created))
                {
                    _pool.Donate(created);
                    return false;
                }
                return true;
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (ReferenceEquals(key, null))
            {
                // ConcurrentDictionary hates null
                throw new ArgumentNullException("key");
            }
            LockableNeedle<TValue> found;
            var result = _wrapped.TryGetValue(key, out found);
            if (result)
            {
                value = found.Value;
                return true;
            }
            value = default(TValue);
            return false;
        }

        public bool TryRemove(TKey key, out TValue value)
        {
            if (ReferenceEquals(key, null))
            {
                // ConcurrentDictionary hates null
                throw new ArgumentNullException("key");
            }
            throw new NotImplementedException();
        }

        public bool TryUpdate(TKey key, TValue newValue, TValue comparisonValue)
        {
            if (ReferenceEquals(key, null))
            {
                // ConcurrentDictionary hates null
                throw new ArgumentNullException("key");
            }
            using (_context.Enter())
            {
                return _wrapped.TryUpdate
                    (
                        key,
                        GetNeedle(newValue),
                        input => EqualityComparer<TValue>.Default.Equals(input.Value, comparisonValue)
                    );
            }
        }

        private void AcquireAllLocks()
        {
            foreach (var resource in _wrapped)
            {
                resource.Value.CaptureAndWait();
            }
        }

        void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
        {
            if (ReferenceEquals(key, null))
            {
                // ConcurrentDictionary hates null
                throw new ArgumentNullException("key");
            }
            using (_context.Enter())
            {
                LockableNeedle<TValue> created = GetNeedle(value);
                if (!_wrapped.TryAdd(key, created))
                {
                    _pool.Donate(created);
                    throw new ArgumentException("An item with the same key has already been added");
                }
            }
        }

        void IDictionary.Add(object key, object value)
        {
            throw new NotImplementedException();
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            if (ReferenceEquals(item.Key, null))
            {
                // ConcurrentDictionary hates null
                // While technically item is not null and item.Key is not an argument...
                // This is what happens when you do the call on Microsoft's implementation
                throw new ArgumentNullException("key");
            }
            using (_context.Enter())
            {
                var created = GetNeedle(item.Value);
                if (!_wrapped.TryAdd(item.Key, created))
                {
                    _pool.Donate(created);
                    throw new ArgumentException("An item with the same key has already been added");
                }
            }
        }

        private void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> collection)
        {
            using (_context.Enter())
            {
                foreach (var pair in collection)
                {
                    var created = GetNeedle(pair.Value);
                    if (!_wrapped.TryAdd(pair.Key, created))
                    {
                        _pool.Donate(created);
                        throw new ArgumentException("The source contains duplicate keys.");
                    }
                }
            }
        }

        bool IDictionary.Contains(object key)
        {
            if (ReferenceEquals(key, null))
            {
                // ConcurrentDictionary hates null
                throw new ArgumentNullException("key");
            }
            // keep the is operator
            if (key is TKey)
            {
                return ContainsKey((TKey)key);
            }
            return false;
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            if (ReferenceEquals(item.Key, null))
            {
                // ConcurrentDictionary hates null
                // While technically item is not null and item.Key is not an argument...
                // This is what happens when you do the call on Microsoft's implementation
                throw new ArgumentNullException("key");
            }
            LockableNeedle<TValue> found;
            if (_wrapped.TryGetValue(item.Key, out found))
            {
                if (EqualityComparer<TValue>.Default.Equals(found.Value, item.Value))
                {
                    return true;
                }
            }
            return false;
        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            // TODO: How locking should work here?
            Extensions.CanCopyTo(_wrapped.Count, array, arrayIndex);
            this.CopyTo(array, arrayIndex);
        }

        void ICollection.CopyTo(Array array, int index)
        {
            // TODO: How locking should work here?
            // WORST API EVER - I shouldn't be supporting this
            // I'm checking size before checking type - I have no plans to fix that
            Extensions.CanCopyTo(_wrapped.Count, array, index);
            try
            {
                var pairs = array as KeyValuePair<TKey, TValue>[]; // most decent alternative
                if (pairs != null)
                {
                    var _array = pairs;
                    foreach (var pair in _wrapped)
                    {
                        _array[index] = new KeyValuePair<TKey, TValue>(pair.Key, pair.Value.Value);
                        index++;
                    }
                    return;
                }
                var objects = array as object[];
                if (objects != null)
                {
                    var _array = objects;
                    foreach (var pair in _wrapped)
                    {
                        _array[index] = new KeyValuePair<TKey, TValue>(pair.Key, pair.Value.Value);
                        index++;
                    }
                    return;
                }
                var entries = array as DictionaryEntry[]; // that thing exists, I was totally unaware, I may as well use it.
                if (entries != null)
                {
                    var _array = entries;
                    foreach (var pair in _wrapped)
                    {
                        _array[index] = new DictionaryEntry { Key = pair.Key, Value = pair.Value.Value };
                        index++;
                    }
                    return;
                }
                throw new ArgumentException("Not supported array type"); // A.K.A ScrewYouException
            }
            catch (IndexOutOfRangeException exception)
            {
                throw new ArgumentException("array", exception.Message);
            }
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        private LockableNeedle<TValue> GetNeedle(TValue value)
        {
            LockableNeedle<TValue> result;
            if (_pool.TryGet(out result))
            {
                result.Value = value;
            }
            else
            {
                result = new LockableNeedle<TValue>(value, _context);
            }
            return result;
        }

        private void Recycle(LockableNeedle<TValue> obj)
        {
            obj.Free();
        }

        void IDictionary.Remove(object key)
        {
            throw new NotImplementedException();
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            if (ReferenceEquals(item.Key, null))
            {
                // ConcurrentDictionary hates null
                // While technically item is not null and item.Key is not an argument...
                // This is what happens when you do the call on Microsoft's implementation
                throw new ArgumentNullException("key");
            }
            throw new NotImplementedException();
        }

        bool IDictionary<TKey, TValue>.Remove(TKey key)
        {
            if (ReferenceEquals(key, null))
            {
                // ConcurrentDictionary hates null
                throw new ArgumentNullException("key");
            }
            using (_context.Enter())
            {
                // TODO: How locking should work here?
                return _wrapped.Remove(key);
            }
        }
    }
}