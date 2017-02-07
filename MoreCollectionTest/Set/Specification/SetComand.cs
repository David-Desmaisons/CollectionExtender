using FsCheck;
using MoreCollectionTest.FsCheckHelper;
using System.Collections.Generic;

namespace MoreCollectionTest.Set.Specification
{
    internal abstract class SetComand : CommandInterface<ISet<int>>
    {
        public override Property Post(ISet<int> c, ISet<int> m)
        {
            return (m.SetEquals(c)).Label($"Same collection compared from model. Expected:[{(string.Join(",",m))}], actual:[{(string.Join(",", c))}]")
                        .And(c.SetEquals(m)).Label($"Same collection compared from hybrid. Expected:[{(string.Join(", ",m))}], actual:[{(string.Join(", ", c))}]")
                        .And(c.Count == m.Count).Label($"Count expected:{m.Count} actual {c.Count}");
        }
    }
}
