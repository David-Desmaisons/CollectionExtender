using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace CollectionExtenderTest.TestInfra
{
    public static class DictionaryTestExtender
    {
        public static void ShouldBeCoherent<TK, TV>(this IDictionary<TK, TV> @this)
        {
            int Count = @this.Count;
            var l1 = @this.ToList();
            var l2 = @this.Keys.ToList();
            var l3 = @this.Values.ToList();

            Count.Should().Be(l1.Count);
            Count.Should().Be(l2.Count);
            Count.Should().Be(l3.Count);
            l1.Select(kvp => kvp.Key).Should().BeEquivalentTo(l2);
            l1.Select(kvp => kvp.Value).Should().BeEquivalentTo(l3);

            List<TV> t = new List<TV>();
            foreach (TK k in @this.Keys)
            {
                TV v = @this[k];
                v.Should().NotBeNull();
                TV v2 = default(TV);
                @this.TryGetValue(k, out v2).Should().BeTrue();
                v.Should().Be(v2);
                l3.Should().Contain(v);
                t.Add(v);
                l1.Should().Contain(new KeyValuePair<TK, TV>(k, v));
            }

            foreach (KeyValuePair<TK, TV> kvp in @this)
            {
                TV v = @this[kvp.Key];
                v.Should().Be(kvp.Value);

                TV v2 = default(TV);
                @this.TryGetValue(kvp.Key, out v2).Should().BeTrue();
                v.Should().Be(v2);
                l3.Should().Contain(v);
                l2.Should().Contain(kvp.Key);
                @this.Contains(kvp).Should().BeTrue();
            }
        }

        public static void ShouldBeExtaclyTheSame<TK, TV>(this IDictionary<TK, TV> @this, IDictionary<TK, TV> target)
        {
            @this.Should().Equal(target);
            @this.AsEnumerable().Should().BeEquivalentTo(target);
            @this.AsEnumerable().Count().Should().Be(target.Count);
        }

        public static void ShouldBehaveTheSame<TK, TV>(this IDictionary<TK, TV> @this, IDictionary<TK, TV> target, 
                            Action<IDictionary<TK, TV>> Ac, Type type=null)
        {
            @this.ShouldBeCoherent();
            @this.ShouldBeExtaclyTheSame(target);

            Exception onThis = null;
            Exception onTarget = null;

            try
            {
                Ac(@this);
            }
            catch (Exception e)
            {
                onThis = e;
            }

            try
            {
                Ac(target);
            }
            catch (Exception e)
            {
                onTarget = e;
            }

            @this.ShouldBeExtaclyTheSame(target);
            @this.ShouldBeCoherent();

            if (type!=null)
            {
                onThis.Should().NotBeNull();
                onTarget.Should().NotBeNull();
                onThis.Should().BeOfType(type);
                onThis.Should().BeOfType(onTarget.GetType());
            }
            else
            {
                onThis.Should().BeNull();
                onTarget.Should().BeNull();
            }       
        }

        public static TR ShouldBehaveTheSame<TK, TV, TR>(this IDictionary<TK, TV> @this, IDictionary<TK, TV> target,
                          Func<IDictionary<TK, TV>, TR> Ac)
        {
            @this.ShouldBeCoherent();
            @this.ShouldBeExtaclyTheSame(target);

            TR res = Ac(@this);
            TR res2 = Ac(target);

            res.Should().Be(res2);
            @this.ShouldBeExtaclyTheSame(target);
            @this.ShouldBeCoherent();

            return res;
        }
    }
}
