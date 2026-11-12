using System;
using System.Collections.Generic;
using System.Text;
using Lab3Library.Collections;
using Lab3Library.Extensions;

namespace Lab3Demo
{
    internal static class Program
    {
        private static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            DemoStringExtensions();
            DemoArrayExtensions();
            DemoExtendedDictionary();

            if (!Console.IsInputRedirected)
            {
                Console.WriteLine();
                Console.WriteLine("Натисніть будь-яку клавішу для завершення...");
                Console.ReadKey(true);
            }
        }

        private static void DemoStringExtensions()
        {
            Header("1. Методи розширення для класу String");

            string[] samples = { "Житомирська політехніка", "Hello, World!", "А роза упала на лапу Азора", "" };
            foreach (string s in samples)
            {
                Console.WriteLine($"Рядок:       \"{s}\"");
                Console.WriteLine($"Інвертований: \"{s.Reverse()}\"");
                Console.WriteLine();
            }

            string text = "Програмування мовою C# — це просто";
            Console.WriteLine($"Рядок: \"{text}\"");
            foreach (char symbol in new[] { 'о', 'р', 'П', ' ', 'z' })
                Console.WriteLine($"  Символ '{symbol}' зустрічається {text.CountOf(symbol)} раз(и)");
            Console.WriteLine($"  Символ 'п' без урахування регістру: {text.CountOf('п', true)} раз(и)");
        }

        private static void DemoArrayExtensions()
        {
            Header("2. Узагальнені методи розширення для одновимірних масивів");

            int[] numbers = { 5, 3, 5, 1, 9, 3, 5, 7, 1 };
            Console.WriteLine($"int[]:     {Format(numbers)}");
            Console.WriteLine($"  CountOf(5) = {numbers.CountOf(5)}");
            Console.WriteLine($"  CountOf(4) = {numbers.CountOf(4)}");
            Console.WriteLine($"  Unique()   = {Format(numbers.Unique())}");
            Console.WriteLine();

            double[] values = { 2.5, 1.0, 2.5, 3.75, 1.0 };
            Console.WriteLine($"double[]:  {Format(values)}");
            Console.WriteLine($"  CountOf(2.5) = {values.CountOf(2.5)}");
            Console.WriteLine($"  Unique()     = {Format(values.Unique())}");
            Console.WriteLine();

            string[] words = { "яблуко", "груша", "яблуко", null, "слива", "груша", null };
            Console.WriteLine($"string[]:  {Format(words)}");
            Console.WriteLine($"  CountOf(\"яблуко\") = {words.CountOf("яблуко")}");
            Console.WriteLine($"  CountOf(null)     = {words.CountOf(null)}");
            Console.WriteLine($"  Unique()          = {Format(words.Unique())}");
            Console.WriteLine();

            char[] letters = "банан".ToCharArray();
            Console.WriteLine($"char[]:    {Format(letters)}");
            Console.WriteLine($"  CountOf('а') = {letters.CountOf('а')}");
            Console.WriteLine($"  Unique()     = {Format(letters.Unique())}");
            Console.WriteLine();

            DateTime[] dates =
            {
                new DateTime(2026, 9, 1), new DateTime(2026, 12, 25), new DateTime(2026, 9, 1)
            };
            Console.WriteLine($"DateTime[]: {Format(dates)}");
            Console.WriteLine($"  CountOf(01.09.2026) = {dates.CountOf(new DateTime(2026, 9, 1))}");
            Console.WriteLine($"  Unique()            = {Format(dates.Unique())}");
            Console.WriteLine();

            Student[] students =
            {
                new Student("Ярошук", "ІПЗ-24-5"),
                new Student("Петренко", "ІПЗ-24-5"),
                new Student("Ярошук", "ІПЗ-24-5"),
                new Student("Коваль", "ІПЗ-24-4")
            };
            Console.WriteLine($"Student[]: {Format(students)}");
            Console.WriteLine($"  CountOf(Ярошук (ІПЗ-24-5)) = {students.CountOf(new Student("Ярошук", "ІПЗ-24-5"))}");
            Console.WriteLine($"  Unique() = {Format(students.Unique())}");
        }

        private static void DemoExtendedDictionary()
        {
            Header("3. Узагальнений клас ExtendedDictionary<T, U, V>");

            var grades = new ExtendedDictionary<int, string, double>();
            grades.Add(1, "Ярошук Даниїл", 91.5);
            grades.Add(2, "Петренко Олена", 87.0);
            grades.Add(3, "Коваль Андрій", 74.25);
            grades.Add(new ExtendedDictionaryElement<int, string, double>(4, "Мельник Ірина", 95.0));

            Console.WriteLine("Словник <int, string, double> (номер, студент, середній бал):");
            PrintDictionary(grades);

            Console.WriteLine();
            Console.WriteLine($"Індексування grades[3]: {grades[3]}");
            Console.WriteLine($"Ім'я студента з ключем 1: {grades[1].Value1}, бал: {grades[1].Value2}");
            Console.WriteLine($"ContainsKey(2) = {grades.ContainsKey(2)}");
            Console.WriteLine($"ContainsKey(10) = {grades.ContainsKey(10)}");
            Console.WriteLine($"ContainsValue(\"Коваль Андрій\", 74.25) = {grades.ContainsValue("Коваль Андрій", 74.25)}");
            Console.WriteLine($"ContainsValue(\"Коваль Андрій\", 80) = {grades.ContainsValue("Коваль Андрій", 80)}");

            Console.WriteLine();
            Console.WriteLine($"Remove(2) = {grades.Remove(2)}");
            Console.WriteLine($"Remove(2) повторно = {grades.Remove(2)}");
            Console.WriteLine("Після видалення:");
            PrintDictionary(grades);

            Console.WriteLine();
            Console.WriteLine("Перевірка помилок:");
            Try(() => grades.Add(1, "Дублікат", 0), "Add з наявним ключем 1");
            Try(() => Console.WriteLine(grades[42]), "Індексування неіснуючого ключа 42");
            Try(() =>
            {
                foreach (var element in grades)
                    grades.Remove(element.Key);
            }, "Видалення під час foreach");

            Console.WriteLine();
            var capitals = new ExtendedDictionary<string, string, int>();
            capitals.Add("UA", "Київ", 2952);
            capitals.Add("PL", "Варшава", 1861);
            capitals.Add("DE", "Берлін", 3878);
            Console.WriteLine("Словник <string, string, int> (код країни, столиця, населення в тис.):");
            PrintDictionary(capitals);
            Console.WriteLine($"capitals[\"PL\"].Value1 = {capitals["PL"].Value1}");

            Console.WriteLine();
            var schedule = new ExtendedDictionary<DateTime, string, List<string>>();
            schedule.Add(new DateTime(2026, 11, 16), "Лекція .NET", new List<string> { "делегати", "події" });
            schedule.Add(new DateTime(2026, 11, 18), "Лабораторна .NET", new List<string> { "узагальнення" });
            Console.WriteLine("Словник <DateTime, string, List<string>>:");
            foreach (var item in schedule)
                Console.WriteLine($"  {item.Key:dd.MM.yyyy} — {item.Value1}: {string.Join(", ", item.Value2)}");
        }

        private static void PrintDictionary<T, U, V>(ExtendedDictionary<T, U, V> dictionary)
        {
            foreach (var element in dictionary)
                Console.WriteLine($"  {element}");
            Console.WriteLine($"  Кількість елементів (Count): {dictionary.Count}");
        }

        private static void Try(Action action, string description)
        {
            try
            {
                action();
                Console.WriteLine($"  {description}: виконано без помилок");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  {description}: {ex.GetType().Name} — {ex.Message}");
            }
        }

        private static string Format<T>(T[] array)
        {
            var parts = new string[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                object item = array[i];
                if (item == null)
                    parts[i] = "null";
                else if (item is DateTime date)
                    parts[i] = date.ToString("dd.MM.yyyy");
                else
                    parts[i] = item.ToString();
            }
            return "[" + string.Join(", ", parts) + "]";
        }

        private static void Header(string title)
        {
            Console.WriteLine();
            Console.WriteLine(new string('=', 70));
            Console.WriteLine(title);
            Console.WriteLine(new string('=', 70));
        }
    }
}
