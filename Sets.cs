using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningByExample1
{
    class Sets
    {
        class Set<T>
        {
            //A set stores only unique items, so it sees whether an item already exists before adding it
            private List<T>[] buckets = new List<T>[100];
            //You split the data in a set of buckets. 
            //Each bucket contains a subgroup of all the items in the set
            //Now your items are distributed over a hundred buckets instead of one single bucket
            //When you see whether an item exists, you first calculate the hash code, go to the corresponding bucket, and look for the item.

            public void Insert(T item)
            {
                //Hashing is the process of taking a large set of data and mapping it 
                //to a smaller data set of fixed length.
                int bucket = GetBucket(item.GetHashCode());
                //GetHashCode is defined on the base class Object
                //you can override this method and provide a specific implementation 
                if (Contains(item, bucket))
                    return;
                if (buckets[bucket] == null)
                    buckets[bucket] = new List<T>();
                buckets[bucket].Add(item);
            }
            public bool Contains(T item)
            {
                return Contains(item, GetBucket(item.GetHashCode()));
            }

            private int GetBucket(int hashcode)
            {
                // A Hash code can be negative. To make sure that you end up with a positive 
                // value cast the value to an unsigned int. The unchecked block makes sure that 
                // you can cast a value larger then int to an int safely.
                unchecked
                {
                    return (int)((uint)hashcode % (uint)buckets.Length);
                }
            }

            private bool Contains(T item, int bucket)
            {
                if (buckets[bucket] != null)
                    foreach (T member in buckets[bucket])
                        if (member.Equals(item))
                            return true;
                return false;
            }
        }

    }
}
