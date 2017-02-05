﻿using FsCheck;
using FsCheck.Xunit;
using MoreCollection.Set;
using System;
using System.Collections.Generic;
using System.Linq;

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
              (set, arr) => arr.Select(el => set.Contains(el)).ToList(),
              (s1, s2) => s1.SequenceEqual(s2),
              (_, __, set) => set.All(el => el==false), "No Overlaps");
        }

        [Property(MaxTest = 300)]
        public Property Constructor_ReturnsCorrectValue()
        {
            return Prop.ForAll<int[]>((arr1) =>
            {
                var set = new HashSet<int>(arr1);
                var hybridSet = new HybridSet<int>(arr1, 4);
                return set.SetEquals(hybridSet).Classify(set.Count<=1, "Single")
                                                .Classify(set.Count>1 && set.Count <= 4, "List")
                                                .Classify(set.Count >4 , "Hash");
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
            return Prop.ForAll<int[], int[]>((arr1, arr2) =>
            {
                var set = new HashSet<int>(arr1);
                var hybridSet = new HybridSet<int>(arr1, 4);

                var computedSet = perform(set, arr2);
                var computedHybrid = perform(hybridSet, arr2);

                var prop = compare(computedSet, computedHybrid);
                return (categoryExtractor == null) ? prop.ToProperty() : prop.Classify(categoryExtractor(arr1, arr2, computedSet), category);
            });
        }
    }
}
