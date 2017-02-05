using FsCheck;
using FsCheck.Xunit;
using MoreCollection.Set;
using System;
using System.Collections.Generic;

namespace MoreCollectionTest.Set.Specification
{
    public class HybridSetSpecificationTest 
    {
        [Property(MaxTest = 1000)]
        public Property HybridSet_ShouldBehaveAsSet()
        {
            return new OperationSpecification().ToProperty();
        }

        [Property(MaxTest = 300)]
        public Property HybridSet_IntersectWith_ReturnsCorrectValue()
        {
            return BuildProperty((set, arr) => set.IntersectWith(arr));
        }

        [Property(MaxTest = 300)]
        public Property HybridSet_ExceptWith_ReturnsCorrectValue()
        {
            return BuildProperty((set, arr) => set.ExceptWith(arr));
        }

        [Property(MaxTest = 300)]
        public Property HybridSet_SymmetricExceptWith_ReturnsCorrectValue()
        {
            return BuildProperty( (set, arr) => set.SymmetricExceptWith(arr));
        }

        private Property BuildProperty(Action<ISet<int>, IEnumerable<int>> perform)
        {
            return Prop.ForAll<int[], int[]>((arr1, arr2) =>
            {
                var set = new HashSet<int>(arr1);
                perform(set, arr2);

                var hybridSet = new HybridSet<int>(arr1);
                perform(hybridSet, arr2);

                return set.SetEquals(hybridSet).Classify(set.Count == 0, "Empty result");
            });
        }
    }
}
