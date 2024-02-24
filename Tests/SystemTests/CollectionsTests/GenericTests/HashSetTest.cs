﻿#if LESSTHAN_NET35
extern alias nunitlinq;
#endif

//
// HashSetTest.cs
//
// Authors:
//  Jb Evain  <jbevain@novell.com>
//
// Copyright (C) 2007 Novell, Inc (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

// ReSharper disable CollectionNeverUpdated.Local

using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tests.Helpers;

namespace Tests.SystemTests.CollectionsTests.GenericTests
{
    [TestFixture]
    public class HashSetTest
    {
        [Test]
        public void EnumeratorCurrent()
        {
            using (var e1 = new HashSet<int>.Enumerator())
            {
                AssertEx.AreEqual(default, e1.Current);
            }

            var d = new HashSet<int>();
            var e2 = d.GetEnumerator();
            AssertEx.AreEqual(default, e2.Current);
            Assert.IsFalse(e2.MoveNext());
            AssertEx.AreEqual(default, e2.Current);
            e2.Dispose();
            AssertEx.AreEqual(default, e2.Current);

            var e3 = ((IEnumerable<int>)d).GetEnumerator();
            AssertEx.AreEqual(default, e3.Current);
            Assert.IsFalse(e3.MoveNext());
            AssertEx.AreEqual(default, e3.Current);
            e3.Dispose();
            AssertEx.AreEqual(default, e3.Current);

            var e4 = ((IEnumerable)d).GetEnumerator();
            // ReSharper disable once AccessToDisposedClosure
            AssertEx.Throws<InvalidOperationException>(() => e4.Current);
            e4.MoveNext();
            // ReSharper disable once AccessToDisposedClosure
            AssertEx.Throws<InvalidOperationException>(() => e4.Current);
            ((IDisposable)e4).Dispose();
            AssertEx.Throws<InvalidOperationException>(() => e4.Current);
        }

        [Test]
        public void TestAdd()
        {
            var set = new HashSet<int>();

            Assert.IsTrue(set.Add(1));
            Assert.IsTrue(set.Add(2));
            Assert.IsTrue(set.Add(3));
            Assert.IsTrue(set.Add(4));
            Assert.IsFalse(set.Add(4));
            Assert.IsFalse(set.Add(3));
            Assert.IsFalse(set.Add(2));
            Assert.IsFalse(set.Add(1));
            Assert.IsTrue(set.Add(0));
            Assert.IsFalse(set.Add(0));
        }

        [Test]
        public void TestClear()
        {
            var data = new[] { 1, 2, 3, 4, 5, 6 };
            var set = new HashSet<int>(data);

            Assert.AreEqual(data.Length, set.Count);
            set.Clear();
            AssertIsEmpty(set);
        }

        [Test]
        public void TestContains()
        {
            var data = new[] { 1, 2, 3, 4, 5, 6 };
            var set = new HashSet<int>(data);

            foreach (var item in data)
            {
                Assert.IsTrue(set.Contains(item));
            }
        }

        [Test]
        public void TestCopyTo()
        {
            var data = new[] { 1, 2, 3, 4, 5 };
            var set = new HashSet<int>(data);

            var array = new int[set.Count];
            set.CopyTo(array, 0);

            AssertContainsOnly(data, array);
        }

        [Test]
        public void TestCopyToEmpty()
        {
            var res = ArrayEx.Empty<int>();
            new HashSet<int>().CopyTo(res, 0);
        }

        [Test]
        public void TestCopyToFull()
        {
            var data = new[] { 1, 2, 3, 4 };

            var set = new HashSet<int>(data);

            var res = new int[set.Count];
            set.CopyTo(res, 0);

            AssertContainsOnly(res, data);
        }

        [Test]
        public void TestCopyToPrecise()
        {
            var set = new HashSet<int>
            {
                42
            };
            var dest = new int[12];

            set.CopyTo(dest, 6, 1);

            Assert.AreEqual(42, dest[6]);
        }

        [Test]
        public void TestEmptyHashSubsetOf()
        {
            var set = new HashSet<int>();

            Assert.IsTrue(set.IsSubsetOf(ArrayEx.Empty<int>()));
            Assert.IsTrue(set.IsSubsetOf(new[] { 1, 2 }));
        }

        [Test]
        public void TestExceptWith()
        {
            var data = new[] { 1, 2, 3, 4, 5, 6 };
            var other = new[] { 2, 4, 6 };
            var result = new[] { 1, 3, 5 };
            var set = new HashSet<int>(data);

            set.ExceptWith(other);

            AssertContainsOnly(result, set);
        }

        [Test]
        public void TestHashSetEqualityComparer()
        {
            var data = new[] { "foo", "bar", "foobar" };
            var set1 = new HashSet<string>(data, StringComparer.Ordinal);
            var set2 = new HashSet<string>(data, StringComparer.OrdinalIgnoreCase);

            var comparer = HashSetEx<string>.CreateSetComparer();
            Assert.IsTrue(comparer.Equals(set1, set1));
            Assert.IsTrue(comparer.Equals(set1, set2));
            Assert.AreEqual(comparer.GetHashCode(set1), comparer.GetHashCode(set2));

            var set3 = new HashSet<string>(new[] { "foobar", "foo", "bar" });
            Assert.IsTrue(comparer.Equals(set1, set3));
            Assert.AreEqual(comparer.GetHashCode(set1), comparer.GetHashCode(set3));

            var set4 = new HashSet<string>(new[] { "oh", "hai", "folks" });
            Assert.IsFalse(comparer.Equals(set2, set4));
            Assert.AreNotEqual(comparer.GetHashCode(set2), comparer.GetHashCode(set4));

            Assert.IsTrue(comparer.Equals(null, null));
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.AreEqual(0, comparer.GetHashCode(null));
            Assert.IsFalse(comparer.Equals(set1, null));
        }

        [Test]
        public void TestICollection()
        {
            var set = new HashSet<int>() as ICollection<int>;
            set.Add(42);
            set.Add(42);

            Assert.AreEqual(1, set.Count);
        }

        [Test]
        public void TestIntersectWith()
        {
            var data = new[] { 1, 2, 3, 4 };
            var other = new[] { 2, 4, 5, 6 };
            var result = new[] { 2, 4 };

            var set = new HashSet<int>(data);

            set.IntersectWith(other);

            AssertContainsOnly(result, set);
        }

        [Test]
        public void TestIntersectWithComparer()
        {
            var data = new[] { "a", "b", "C", "d", "E" };
            var other = new[] { "a", "B", "e" };
            var result = new[] { "a", "b", "E" };

            var set = new HashSet<string>(data, StringComparer.OrdinalIgnoreCase);

            set.IntersectWith(other);

            AssertContainsOnly(result, set);
        }

        [Test]
        public void TestMassiveAdd()
        {
            var set = new HashSet<int>();

            var massive = Enumerable.Range(0, 10000).ToArray();
            foreach (var item in massive)
            {
                Assert.IsTrue(set.Add(item));
            }

            AssertContainsOnly(massive, set);
        }

        [Test]
        public void TestMassiveRemove()
        {
            var massive = Enumerable.Range(0, 10000).ToArray();
            var set = new HashSet<int>(massive);

            foreach (var item in massive)
            {
                Assert.IsTrue(set.Remove(item));
            }

            AssertIsEmpty(set);
        }

        [Test]
        public void TestModifySetWhileForeach()
        {
            Assert.Throws<InvalidOperationException>
            (
                () =>
                {
                    var set = new HashSet<int>(new[] { 1, 2, 3, 4 });
                    foreach (var item in set)
                    {
                        set.Add(item + 2);
                    }
                }
            );
        }

        [Test]
        public void TestNullsWithComparerThrowingException()
        {
            // NOTE: We should get the same errors when using StringComparer.Ordinal on Mono 2.6.1, but the look-alike gives us more control over this test case
            var set = new HashSet<string>(new StringComparerOrdinalLookAlike());
            Assert.IsTrue(set.Add(string.Empty), "#1a");
            Assert.IsFalse(set.Contains(null), "#2a");
            Assert.IsTrue(set.Add(null), "#2b");
            Assert.IsTrue(set.Contains(null), "#2c");
            Assert.AreEqual(2, set.Count, "#3");
            Assert.IsTrue(set.Add("a"), "#4");
            AssertContainsOnly(new[] { string.Empty, null, "a" }, set);
            Assert.IsFalse(set.Add(null), "#5");
            Assert.IsTrue(set.Add("b"), "#6");
            Assert.IsFalse(set.Add("b"), "#7");
            Assert.IsFalse(set.Add(string.Empty), "#8");
            Assert.IsFalse(set.Add("a"), "#9");
            Assert.IsFalse(set.Add(null), "#10");
            Assert.IsTrue(set.Add("c"), "#11");
            Assert.IsFalse(set.Add("c"), "#12");
            Assert.AreEqual(5, set.Count, "#13");
            Assert.IsTrue(set.Remove(null), "#14");
            Assert.IsTrue(set.Remove("b"), "#15");
            Assert.IsFalse(set.Remove(null), "#16");
            Assert.AreEqual(3, set.Count, "#17");
            AssertContainsOnly(new[] { string.Empty, "a", "c" }, set);
        }

        [Test]
        public void TestOverlaps()
        {
            var set = new HashSet<int>(new[] { 1, 2, 3, 4, 5 });

            Assert.IsTrue(set.Overlaps(new[] { 0, 2 }));
        }

        [Test]
        public void TestProperSubsetOf()
        {
            var data = new[] { 1, 2, 3 };
            var other = new[] { 1, 2, 3, 4, 5 };
            var other2 = new[] { 1, 2, 3 };
            var other3 = new[] { 0, 1, 2 };

            var set = new HashSet<int>(data);

            Assert.IsTrue(set.IsProperSubsetOf(other));
            Assert.IsFalse(set.IsProperSubsetOf(other2));
            Assert.IsFalse(set.IsProperSubsetOf(other3));
        }

        [Test]
        public void TestProperSubsetOfComparer()
        {
            var data = new[] { "abc", "DF", "gHIl" };

            var other1 = new[] { "pqR", "ABC", "ghil", "dF", "lmn" };

            var set = new HashSet<string>(data, StringComparer.OrdinalIgnoreCase);

            Assert.IsTrue(set.IsProperSubsetOf(other1));
        }

        [Test]
        public void TestProperSupersetOf()
        {
            var data = new[] { 1, 2, 3, 4, 5 };
            var other = new[] { 2, 3, 4 };
            var other2 = new[] { 1, 2, 3, 4, 5 };
            var other3 = new[] { 4, 5, 6 };

            var set = new HashSet<int>(data);

            Assert.IsTrue(set.IsProperSupersetOf(other));
            Assert.IsFalse(set.IsProperSupersetOf(other2));
            Assert.IsFalse(set.IsProperSupersetOf(other3));
        }

        [Test]
        public void TestProperSupersetOfComparer()
        {
            var data = new[] { "a", "B", "c", "D" };

            var other1 = new[] { "A", "a", "d", "D" };
            var other2 = new[] { "A", "a", "B", "D", "C", "c" };

            var set = new HashSet<string>(data, StringComparer.OrdinalIgnoreCase);

            Assert.IsTrue(set.IsProperSupersetOf(other1));
            Assert.IsFalse(set.IsProperSupersetOf(other2));
        }

        [Test]
        public void TestRemove()
        {
            var set = new HashSet<int>();

            Assert.IsTrue(set.Add(1));
            Assert.IsTrue(set.Add(2));
            Assert.IsTrue(set.Add(3));
            Assert.IsTrue(set.Add(4));

            Assert.IsTrue(set.Remove(2));
            Assert.IsTrue(set.Remove(3));

            AssertContainsOnly(new[] { 1, 4 }, set);
        }

        [Test]
        public void TestRemoveWhere()
        {
            var data = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var result = new[] { 2, 4, 6, 8 };

            var set = new HashSet<int>(data);
            var removed = set.RemoveWhere(i => (i % 2) != 0);

            Assert.AreEqual(data.Length - result.Length, removed);
            AssertContainsOnly(result, set);
        }

        [Test]
        public void TestSetEquals()
        {
            var data = new[] { 1, 2, 3, 4 };

            var other = new[] { 1, 2, 3, 4 };
            var other2 = new[] { 1, 2, 2, 4 };
            var other3 = new[] { 1, 2 };
            var other4 = new[] { 1, 2, 3, 4, 5 };
            var other5 = new[] { 1, 1, 1, 1 };

            var set = new HashSet<int>(data);

            Assert.IsTrue(set.SetEquals(other));
            Assert.IsFalse(set.SetEquals(other2));
            Assert.IsFalse(set.SetEquals(other3));
            Assert.IsFalse(set.SetEquals(other4));
            Assert.IsFalse(set.SetEquals(other5));
        }

        [Test]
        public void TestSetEqualsComparer()
        {
            var data = new[] { "abc", "DF", "gHIl" };

            var other1 = new[] { "ABC", "DF", "GHIL" };
            var other2 = new[] { "ABC", "aBc", "DF", "GHIL", "ghil" };

            var set = new HashSet<string>(data, StringComparer.OrdinalIgnoreCase);

            Assert.IsTrue(set.SetEquals(other1));
            Assert.IsTrue(set.SetEquals(other2));
        }

        [Test]
        public void TestSubsetOf()
        {
            var data = new[] { 1, 2, 3 };
            var other = new[] { 1, 2, 3, 4, 5 };
            var other2 = new[] { 1, 2, 3 };
            var other3 = new[] { 0, 1, 2 };

            var set = new HashSet<int>(data);

            Assert.IsTrue(set.IsSubsetOf(other));
            Assert.IsTrue(set.IsSubsetOf(other2));
            Assert.IsFalse(set.IsSubsetOf(other3));
        }

        [Test]
        public void TestSubsetOfComparer()
        {
            var data = new[] { "abc", "DF", "gHIl" };

            var other1 = new[] { "pqR", "ABC", "ghil", "dF", "lmn" };

            var set = new HashSet<string>(data, StringComparer.OrdinalIgnoreCase);

            Assert.IsTrue(set.IsSubsetOf(other1));
        }

        [Test]
        public void TestSupersetOf()
        {
            var data = new[] { 1, 2, 3, 4, 5 };
            var other = new[] { 2, 3, 4 };
            var other2 = new[] { 1, 2, 3, 4, 5 };
            var other3 = new[] { 4, 5, 6 };

            var set = new HashSet<int>(data);

            Assert.IsTrue(set.IsSupersetOf(other));
            Assert.IsTrue(set.IsSupersetOf(other2));
            Assert.IsFalse(set.IsSupersetOf(other3));
        }

        [Test]
        public void TestSupersetOfComparer()
        {
            var data = new[] { "a", "B", "c", "D" };

            var other1 = new[] { "A", "a", "C", "c" };
            var other2 = new[] { "A", "a", "B", "D", "C", "c" };

            var set = new HashSet<string>(data, StringComparer.OrdinalIgnoreCase);

            Assert.IsTrue(set.IsSupersetOf(other1));
            Assert.IsTrue(set.IsSupersetOf(other2));
        }

        [Test]
        public void TestSymmetricExceptWith()
        {
            var data = new[] { 1, 2, 3, 4, 5 };
            var other = new[] { 4, 5, 6, 7, 8, 9, 9 };
            var result = new[] { 1, 2, 3, 6, 7, 8, 9 };

            var set = new HashSet<int>(data);
            set.SymmetricExceptWith(other);

            AssertContainsOnly(result, set);
        }

        [Test]
        public void TestUnionWith()
        {
            var data = new[] { 1, 2, 3, 4, 5, 6 };
            var other = new[] { 4, 5, 6, 7, 8, 9 };
            var result = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var set = new HashSet<int>(data);
            set.UnionWith(other);

            AssertContainsOnly(result, set);
        }

        [Test]
        [Category("LongRunning")]
        public void TrimWithoutChange()
        {
            var lookup = new HashSet<string>();

            for (var i = 0; i < 10000; i++)
            {
                lookup.Add(i.ToString());
            }

            lookup.Remove(3.ToString());

            for (var i = 0; i < 1000; i++)
            {
                lookup.TrimExcess();
            }
        }

        private static void AssertContainsOnly<T>(IEnumerable<T> result, IEnumerable<T> data)
        {
            var resultAsArray = result.ToArray();
            var dataAsArray = data.ToArray();
            Assert.AreEqual(resultAsArray.Length, dataAsArray.Length);

            var store = new List<T>(resultAsArray);
            foreach (var element in dataAsArray)
            {
                Assert.IsTrue(store.Contains(element));
                store.Remove(element);
            }

            AssertIsEmpty(store);
        }

        private static void AssertIsEmpty<T>(IEnumerable<T> source)
        {
            Assert.AreEqual(0, source.Count());
        }

        private sealed class StringComparerOrdinalLookAlike : IEqualityComparer<string>
        {
            public bool Equals(string x, string y)
            {
                return string.CompareOrdinal(x, y) == 0;
            }

            public int GetHashCode(string obj)
            {
                if (obj == null)
                {
                    throw new ArgumentNullException(); // Important aspect for test (same as what StringComparer.Ordinal does, and different from GenericEqualityComparer<string>)
                }

                return obj.GetHashCode();
            }
        }
    }
}