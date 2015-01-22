using System;
using System.Collections.Generic;

namespace StringReaderCatalog.Test
{
    public class DataBagEqualityComparer : IEqualityComparer<DataBag>
    {
        public bool Equals(DataBag bag1, DataBag bag2)
        {
            return (bag1.Id == bag2.Id) && bag1.Name.Equals(bag2.Name, StringComparison.Ordinal);
        }

        public int GetHashCode(DataBag bag)
        {
            return bag.Id ^ bag.Name.GetHashCode();
        }
    }
}