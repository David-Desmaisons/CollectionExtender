using FsCheck;
using System.Collections.Generic;

namespace MoreCollectionTest.Set.Specification
{
    internal abstract class SetComand : Command<ISet<int>, ISet<int>>
    {
        protected abstract void Perform(ISet<int> set);

        public override ISet<int> RunActual(ISet<int> c)
        {
            return Run(c);
        }

        public override ISet<int> RunModel(ISet<int> m)
        {
            return Run(m);
        }

        private ISet<int> Run(ISet<int> set)
        {
            Perform(set);
            return set;
        }

        public override Property Post(ISet<int> c, ISet<int> m)
        {
            return (m.SetEquals(c)).Label("Same collection").And(c.Count == m.Count).Label("Count");
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}";
        }
    }
}
