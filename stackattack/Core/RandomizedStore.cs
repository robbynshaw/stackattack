using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace stackattack.Core
{
    /// <summary>
    /// Storage class useful for retrieving random sets or items.
    /// Use one instance repeatedly for proper randomization.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public class RandomizedStore<TItem>
    {
        private Random randomizer = new Random();
        private List<TItem> items = new List<TItem>();

        //TODO The locking in the class would make it uber slow in real life
        private object collectionLock = new object();

        // We implememnt the counter manually for performance
        private int count = 0;

        public void Insert(TItem item)
        {
            if (item != null)
            {
                lock(collectionLock)
                {
                    this.items.Add(item);
                    this.count++;
                }
            }
        }

        public void Insert(IEnumerable<TItem> items)
        {
            if (items != null)
            {
                lock (collectionLock)
                {
                    this.items.AddRange(items);
                    this.count += items.Count();
                }
            }
        }

        public void Clear()
        {
            lock (collectionLock)
            {
                this.items.Clear();
                this.count = 0;
            }
        }

        public TItem Get()
        {
            lock (collectionLock)
            {
                int i = randomizer.Next(this.count);
                return this.items[i];
            }
        }

        public IEnumerable<TItem> Get(int count)
        {
            List<TItem> randomItems = new List<TItem>();

            lock (collectionLock)
            {
                int i = randomizer.Next(this.count);
                randomItems.Add(this.items[i]);
            }

            return randomItems;
        }
    }
}