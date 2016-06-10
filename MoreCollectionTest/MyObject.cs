using System;

namespace MoreCollectionTest
{
    internal class MyObject : IComparable<MyObject>
    {
        public MyObject(string v1, int v2)
        {
            Name = v1;
            Value = v2;
        }

        public int Value { get; set; }

        public string Name { get; set; }
        public int CompareTo(MyObject other)
        {
            return Name.CompareTo(other.Name);
        }
    }
}
