﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sones.GraphDB.ErrorHandling.Expression;

namespace sones.GraphDB.Expression.Tree.Literals
{
    /// <summary>
    /// A wrapper that wraps sets
    /// This data structure is needed, because generic sets do not implement IComparable
    /// </summary>
    public sealed class SetCollectionWrapper : ICollectionWrapper
    {
        #region data

        /// <summary>
        /// The actual value of the wrapper
        /// </summary>
        public readonly ISet<IComparable> Value;

        #endregion

        #region constructor

        /// <summary>
        /// Creates a new set collection wrapper
        /// </summary>
        /// <param name="myCollection">The collection that needs to be wrapped</param>
        public SetCollectionWrapper(ISet<IComparable> myCollection)
        {
            Value = myCollection;
        }

        #endregion


        #region IComparable Members

        public int CompareTo(object obj)
        {
            SetCollectionWrapper counterPart = obj as SetCollectionWrapper;

            if (counterPart != null)
            {
                #region count of objects

                //A set of comparables is greater than another one, if their count is also greater

                var countCompare = this.Value.Count.CompareTo(counterPart.Value.Count);

                if (countCompare != 0)
                {
                    return countCompare;
                }

                #endregion

                #region inner comparables

                var thisAsOrderedList = this.Value.OrderBy(member => member).ToList<IComparable>();
                var counterpartAsOrderedList = counterPart.Value.OrderBy(member => member).ToList<IComparable>();

                //every member within the list is compared

                int memberCompare = 0;

                for (int i = 0; i < Value.Count; i++)
                {
                    memberCompare = thisAsOrderedList[i].CompareTo(counterpartAsOrderedList[i]);
                }

                if (memberCompare != 0)
                {
                    return memberCompare;
                }

                #endregion

                return 0;
            }

            throw new InvalidCollectionComparismException(String.Format("It is not allowed to compare a {0} to a Set", obj.GetType().Name));
        }

        #endregion
    }
}