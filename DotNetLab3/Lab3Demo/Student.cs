using System;

namespace Lab3Demo
{
    public class Student : IEquatable<Student>
    {
        public Student(string lastName, string group)
        {
            LastName = lastName;
            Group = group;
        }

        public string LastName { get; }

        public string Group { get; }

        public bool Equals(Student other)
        {
            if (other is null)
                return false;
            return LastName == other.LastName && Group == other.Group;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Student);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(LastName, Group);
        }

        public override string ToString()
        {
            return $"{LastName} ({Group})";
        }
    }
}
