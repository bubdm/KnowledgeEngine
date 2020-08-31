using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeEngineRules.Core
{
    class Set : IEnumerable<SetItem>
    {
        #region Member Variables

        private List<SetItem> _items;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Set(int capacity)
        {
            _items = new List<SetItem>(capacity);
        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Set()
            : this(12)
        {
        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Set(Set otherSet)
            : this(otherSet.Count * 2)
        {
            AddRange(otherSet);
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        public SetItem this[int index]
        {
            get { return _items[index]; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Contains(SetItem item)
        {
            return _items.Contains(item);
        }

        /// <summary>
        ///  Adds an item to the set. Items are stored as keys, with no associated values.
        /// </summary>
        public void Add(SetItem item)
        {
            _items.Add(item);
        }

        /// <summary>
        ///  Adds an item to the set. Items are stored as keys, with no associated values.
        /// </summary>
        public void AddRange(IEnumerable<SetItem> items)
        {
            _items.AddRange(items);
        }

        /// <summary>
        /// Performs a wildcard match to 's' (s == Owns(porchse ?who))
        /// </summary>
        public bool IsMatchTo(Set s)
        {
            if (Count != s.Count)
            {
                return false;
            }

            for (int i = 0; i < s.Count; i++)
            {
                SetItem item = s[i];
                if (!item.IsWildcard && item != this[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Helper function that does most of the work in the class.
        /// </summary>
        private static Set Generate(Set a, Set b, Set startingSet, bool containment)
        {
            // Find larger of the two sets, iterate over it
            // to compare to other set.
            Set iterSet = a.Count < b.Count ? b : a;
            Set containsSet = a.Count < b.Count ? a : b;

            // Returned set either starts out empty or as copy of the starting set.
            Set returnSet = (startingSet == null) ? new Set(iterSet.Count + containsSet.Count) : startingSet;

            foreach (SetItem item in iterSet)
            {
                if (!(containment ^ containsSet.Contains(item)))
                {
                    returnSet.Add(item);
                }
            }

            return returnSet;
        }

        /// <summary>
        /// Union of this set and otherSet.
        /// </summary>
        public Set Union(Set otherSet)
        {
            return this | otherSet;
        }

        /// <summary>
        /// Intersection of this set and otherSet.
        /// </summary>
        public Set Intersection(Set otherSet)
        {
            return this & otherSet;
        }

        /// <summary>
        /// Exclusive-OR of this set and otherSet.
        /// </summary>
        public Set ExclusiveOr(Set otherSet)
        {
            return this ^ otherSet;
        }

        /// <summary>
        /// This set minus otherSet. This is not associative.
        /// </summary>
        public Set Difference(Set otherSet)
        {
            return this - otherSet;
        }

        #endregion

        #region Opertors

        /// <summary>
        /// Union of set1 and set2.
        /// </summary>
        public static Set operator |(Set set1, Set set2)
        {
            // Copy set1, then add items from set2 not already in set 1.
            Set unionSet = new Set(set1);
            return Generate(set2, unionSet, unionSet, false);
        }

        /// <summary>
        /// Intersection of set1 and set2.
        /// </summary>
        public static Set operator &(Set set1, Set set2)
        {
            return Generate(set1, set2, null, true);
        }

        /// <summary>
        /// Exclusive-OR of set1 and set2.
        /// </summary>
        public static Set operator ^(Set set1, Set set2)
        {
            // Find items in set1 that aren't in set2. Then find
            // items in set2 that aren't in set1. Return combination
            // of those two subresults.
            return Generate(set2, set1, Generate(set1, set2, null, false), false);
        }

        /// <summary>
        /// The set1 minus set2. This is not associative.
        /// </summary>
        public static Set operator -(Set set1, Set set2)
        {
            return Generate(set1, set2, null, false);
        }

        /// <summary>
        /// Overloaded == operator to determine if 2 sets are equal.
        /// </summary>
        /// <param name="s1">Any set.</param>
        /// <param name="s2">Any set.</param>
        /// <returns>True if the two comparison sets have the same number of elements, and
        /// all of the elements of set s1 are contained in s2.</returns>
        public static bool operator ==(Set s1, Set s2)
        {
            if (s1.Count != s2.Count)
            {
                return false;
            }

            for (int i = 0; i < s1.Count; i++)
            {
                if (s1[i] != s2[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Overloaded != operator to determine if 2 sets are unequal.
        /// </summary>
        /// <param name="s1">A benchmark set.</param>
        /// <param name="s2">The set to compare against the benchmark.</param>
        /// <returns>True if the two comparison sets fail the equality (==) test,
        /// false if the pass the equality test.</returns>
        public static bool operator !=(Set s1, Set s2)
        {
            return !(s1 == s2);
        }

        #endregion

        #region Overridden Methods
        
        /// <summary>
        /// Determines whether two <see cref="Set">Set</see> instances are equal.
        /// </summary>
        /// <param name="obj">The <see cref="Set">Set</see> to compare to the current Set.</param>
        /// <returns>true if the specified <see cref="Set">Set</see> is equal to the current
        /// Set; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            Set s = obj as Set;

            if ((object)obj == null)
            {
                return false;
            }
            return this == s;
        }

        /// <summary>
        /// Serves as a hash function for a particular type, suitable for use in hashing
        /// algorithms and data structures like a hash table.
        /// </summary>
        /// <returns>A hash code for the current <see cref="Set">Set</see>.</returns>
        public override int GetHashCode()
        {
            return _items.GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(256);

            foreach (SetItem o in this)
            {
                if (sb.Length > 0)
                {
                    sb.Append(", ");
                }
                sb.Append(o.ToString());
            }

            return sb.ToString();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            get { return _items.Count; }
        }

        #endregion

        #region IEnumerable<SetItem> Members

        public IEnumerator<SetItem> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        #endregion
    }
}
