using System.Collections.Generic;
using System.Linq;
using TaskLibrary.Models;

namespace TaskLibrary.SortCollection
{
    public class ContactViewSortCollection
    {
        public static ICollection<ContactView> Sort(IEnumerable<ContactView> collection)
        {
            return collection.OrderBy(p => p.LastName).OrderBy(p => p.FirstName).ToArray();
        }
    }
}
