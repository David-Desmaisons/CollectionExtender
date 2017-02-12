using FsCheck;
using FsCheck.Xunit;
using MoreCollection.Set;
using MoreCollection.Set.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MoreCollectionTest.Set.Specification
{
    [Collection("Changing Default static set stategy")]
    public class HybridSetSpecificationTest 
    {
        [Property(MaxTest = 1000)]
        public Property HybridSet_BuildFromEmptyBehaveAsSet()
        {
            LetterSimpleSetFactoryBuilder.Factory = new LetterSimpleSetFactory(3);
            return SetOperationSpecification.FromEmpty();
        }

        [Property(MaxTest = 1000)]
        public Property HybridSet_BuildFromSingleBehaveAsSet()
        {
            LetterSimpleSetFactoryBuilder.Factory = new LetterSimpleSetFactory(3);
            return SetOperationSpecification.FromSingle();
        }

        [Property(MaxTest = 1000)]
        public Property HybridSet_BuildFromListBehaveAsSet()
        {
            LetterSimpleSetFactoryBuilder.Factory = new LetterSimpleSetFactory(3);
            return SetOperationSpecification.FromList();
        }

        [Property(MaxTest = 1000)]
        public Property HybridSet_BuildFromHashBehaveAsSet()
        {
            LetterSimpleSetFactoryBuilder.Factory = new LetterSimpleSetFactory(3);
            return SetOperationSpecification.FromHash();
        }

        [Property(MaxTest = 300)]
        public Property IntersectWith_ReturnsCorrectValue()
        {
            return BuildProperty((set, arr) => set.IntersectWith(arr));
        }

        [Property(MaxTest = 300)]
        public Property ExceptWith_ReturnsCorrectValue()
        {
            return BuildProperty((set, arr) => set.ExceptWith(arr));
        }

        [Property(MaxTest = 300)]
        public Property SymmetricExceptWith_ReturnsCorrectValue()
        {
            return BuildProperty( (set, arr) => set.SymmetricExceptWith(arr));
        }

        [Property(MaxTest = 300)]
        public Property UnionWith_ReturnsCorrectValue()
        {
            return BuildProperty((set, arr) => set.UnionWith(arr));
        }     

        [Property(MaxTest = 300)]
        public Property Overlaps_ReturnsCorrectValue()
        {
            return BuilPropertyFromArrays(
              (set, arr) => set.Overlaps(arr) ,
              (s1, s2) => s1 == s2,
              (_, __, set) => set == false, "No Overlaps");
        }

        [Property(MaxTest = 300)]
        public Property Contains_ReturnsCorrectValue()
        {
            return BuilPropertyFromArrays(
              (set, arr) => arr.Select(set.Contains).ToList(),
              (s1, s2) => s1.SequenceEqual(s2),
              (_, __, set) => set.All(el => el==false), "No Overlaps");
        }

        [Property(MaxTest = 300)]
        public Property Constructor_ReturnsCorrectValue()
        {          
            const int transition = 10;
            LetterSimpleSetFactoryBuilder.Factory = new LetterSimpleSetFactory(10);
            return Prop.ForAll<int[]>((arr) =>
            {
                var set = new HashSet<int>(arr);          
                var hybridSet = new HybridSet<int>(arr);
                return set.SetEquals(hybridSet).Classify(set.Count <= 1, "Single")
                                                .Classify(set.Count > 1 && set.Count <= transition, "List")
                                                .Classify(set.Count > transition, "Hash")
                                                .Classify(set.Count != arr.Length, "None trivial")
                                                .Classify(set.Count == arr.Length, "trivial");
            });
        }

        private static Property BuildProperty(Action<ISet<int>, IEnumerable<int>> perform)
        {
            return BuilPropertyFromArrays(
                (set, arr) => { perform(set, arr); return set; }, 
                (s1, s2) => s1.SetEquals(s2),
                (_, __, set) => set.Count == 0, "Empty result");
        }

        private static Property BuilPropertyFromArrays<T>(Func<ISet<int>, int[], T> perform, Func<T,T, bool> compare, Func<int[], int[], T, bool> categoryExtractor=null, string category=null)
        {
            LetterSimpleSetFactoryBuilder.Factory = new LetterSimpleSetFactory(10);
            return Prop.ForAll<int[], int[]>((arr1, arr2) =>
            {
                var set = new HashSet<int>(arr1);
                var hybridSet = new HybridSet<int>(arr1);

                var computedSet = perform(set, arr2);
                var computedHybrid = perform(hybridSet, arr2);

                var prop = compare(computedSet, computedHybrid);
                return (categoryExtractor == null) ? prop.ToProperty() : prop.Classify(categoryExtractor(arr1, arr2, computedSet), category);
            });
        }
    }
}
