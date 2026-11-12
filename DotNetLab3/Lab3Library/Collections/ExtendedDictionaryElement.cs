namespace Lab3Library.Collections
{
    public class ExtendedDictionaryElement<T, U, V>
    {
        public ExtendedDictionaryElement(T key, U value1, V value2)
        {
            Key = key;
            Value1 = value1;
            Value2 = value2;
        }

        public T Key { get; }

        public U Value1 { get; set; }

        public V Value2 { get; set; }

        public override string ToString()
        {
            return $"[{Key}] => ({Value1}; {Value2})";
        }
    }
}
