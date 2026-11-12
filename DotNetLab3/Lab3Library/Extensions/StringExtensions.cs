using System;
using System.Globalization;
using System.Text;

namespace Lab3Library.Extensions
{
    public static class StringExtensions
    {
        public static string Reverse(this string text)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));

            var elements = new string[new StringInfo(text).LengthInTextElements];
            TextElementEnumerator enumerator = StringInfo.GetTextElementEnumerator(text);
            int index = elements.Length;
            while (enumerator.MoveNext())
                elements[--index] = enumerator.GetTextElement();

            var builder = new StringBuilder(text.Length);
            foreach (string element in elements)
                builder.Append(element);
            return builder.ToString();
        }

        public static int CountOf(this string text, char symbol)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));

            int count = 0;
            foreach (char c in text)
            {
                if (c == symbol)
                    count++;
            }
            return count;
        }

        public static int CountOf(this string text, char symbol, bool ignoreCase)
        {
            if (!ignoreCase)
                return text.CountOf(symbol);
            if (text == null)
                throw new ArgumentNullException(nameof(text));

            char lower = char.ToLowerInvariant(symbol);
            int count = 0;
            foreach (char c in text)
            {
                if (char.ToLowerInvariant(c) == lower)
                    count++;
            }
            return count;
        }
    }
}
