using System;
using System.Collections.Generic;

namespace Application.Test.Core.TestUtility
{
    public class Compare
    {
        public static bool AreEqual(string x, string y)
        {
            return string.Compare(x??"", y??"", true) == 0;
        }

        public static bool AreEquals(string x, string y)
        {
            return string.Compare(x ?? "", y ?? "") == 0;
        }

        public static bool IsListItemsAreInOrder<TItem1, TItem2>(List<Tuple<TItem1, TItem2>> values, string listName, bool isAsceding, bool isItem1) where TItem1 : IComparable where TItem2 : IComparable
        {
            bool areInOrder = true;
            int cmpVal = isAsceding ? -1 : 1;
            for (int i = 1; i < values.Count; i++)
            {
                if (isItem1)
                {
                    areInOrder = values[i - 1].Item1.CompareTo(values[i].Item1) == cmpVal || values[i - 1].Item1.CompareTo(values[i].Item1) == 0;
                }
                else
                {
                    areInOrder = values[i - 1].Item2.CompareTo(values[i].Item2) == cmpVal || values[i - 1].Item2.CompareTo(values[i].Item2) == 0;
                }

                if (!areInOrder)
                    break;
            }

            return areInOrder;
        }

        public static bool IsListItemsAreInOrder<TItem>(List<TItem> values, bool isAsceding, string listName) where TItem : IComparable
        {
            bool areInOrder = true;
            int cmpVal = isAsceding ? -1 : 1;

            for (int i = 1; i < values.Count; i++)
            {
                areInOrder = values[i - 1].CompareTo(values[i]) == cmpVal || values[i - 1].CompareTo(values[i]) == 0;
                if (!areInOrder)
                    break;
            }

            return areInOrder;
        }
    }
}
