using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab3Library.Collections
{
    public class ExtendedDictionary<T, U, V> : IEnumerable<ExtendedDictionaryElement<T, U, V>>
    {
        private const int DefaultCapacity = 4;

        private ExtendedDictionaryElement<T, U, V>[] items;
        private int count;
        private int version;

        public ExtendedDictionary() : this(DefaultCapacity)
        {
        }

        public ExtendedDictionary(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));
            items = new ExtendedDictionaryElement<T, U, V>[capacity];
        }

        public int Count
        {
            get { return count; }
        }

        public ExtendedDictionaryElement<T, U, V> this[T key]
        {
            get
            {
                int index = IndexOfKey(key);
                if (index < 0)
                    throw new KeyNotFoundException($"Елемент з ключем \"{key}\" відсутній у словнику.");
                return items[index];
            }
        }

        public void Add(T key, U value1, V value2)
        {
            Add(new ExtendedDictionaryElement<T, U, V>(key, value1, value2));
        }

        public void Add(ExtendedDictionaryElement<T, U, V> element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));
            if (ContainsKey(element.Key))
                throw new ArgumentException($"Елемент з ключем \"{element.Key}\" уже існує.", nameof(element));

            if (count == items.Length)
                Array.Resize(ref items, items.Length == 0 ? DefaultCapacity : items.Length * 2);

            items[count++] = element;
            version++;
        }

        public bool Remove(T key)
        {
            int index = IndexOfKey(key);
            if (index < 0)
                return false;

            count--;
            if (index < count)
                Array.Copy(items, index + 1, items, index, count - index);
            items[count] = null;
            version++;
            return true;
        }

        public bool ContainsKey(T key)
        {
            return IndexOfKey(key) >= 0;
        }

        public bool ContainsValue(U value1, V value2)
        {
            var comparer1 = EqualityComparer<U>.Default;
            var comparer2 = EqualityComparer<V>.Default;
            for (int i = 0; i < count; i++)
            {
                if (comparer1.Equals(items[i].Value1, value1) && comparer2.Equals(items[i].Value2, value2))
                    return true;
            }
            return false;
        }

        public bool TryGetElement(T key, out ExtendedDictionaryElement<T, U, V> element)
        {
            int index = IndexOfKey(key);
            element = index >= 0 ? items[index] : null;
            return index >= 0;
        }

        public void Clear()
        {
            Array.Clear(items, 0, count);
            count = 0;
            version++;
        }

        public IEnumerator<ExtendedDictionaryElement<T, U, V>> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private int IndexOfKey(T key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            var comparer = EqualityComparer<T>.Default;
            for (int i = 0; i < count; i++)
            {
                if (comparer.Equals(items[i].Key, key))
                    return i;
            }
            return -1;
        }

        private sealed class Enumerator : IEnumerator<ExtendedDictionaryElement<T, U, V>>
        {
            private readonly ExtendedDictionary<T, U, V> dictionary;
            private readonly int version;
            private int position = -1;

            public Enumerator(ExtendedDictionary<T, U, V> dictionary)
            {
                this.dictionary = dictionary;
                version = dictionary.version;
            }

            public ExtendedDictionaryElement<T, U, V> Current
            {
                get
                {
                    if (position < 0 || position >= dictionary.count)
                        throw new InvalidOperationException();
                    return dictionary.items[position];
                }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public bool MoveNext()
            {
                if (version != dictionary.version)
                    throw new InvalidOperationException("Словник було змінено під час перебору.");
                if (position < dictionary.count)
                    position++;
                return position < dictionary.count;
            }

            public void Reset()
            {
                if (version != dictionary.version)
                    throw new InvalidOperationException("Словник було змінено під час перебору.");
                position = -1;
            }

            public void Dispose()
            {
            }
        }
    }
}
