using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReferenceWebApplication.Data
{
    /// <summary>
    /// A class used for pretty printing of a list
    /// </summary>
    internal class MessageQueue : IEnumerable<string>, ICollection<string>
    {
        #region Decorated object

        private readonly List<string> collection = new List<string>();

        #endregion

        public int? MaximumSize { get; }

        public MessageQueue(int maximumSize)
        {
            MaximumSize = maximumSize;
        }

        public void RemoveLast()
        {
            collection.RemoveAt(0);
        }

        #region ICollection properties

        public int Count => collection.Count;

        public bool IsReadOnly => false;

        #endregion

        #region ICollection methods

        public void Add(string item)
        {
            collection.Add(item);
            if (MaximumSize.HasValue && collection.Count > MaximumSize.Value)
                RemoveLast();
        }

        public void Clear()
        {
            collection.Clear();
        }

        public bool Contains(string item)
        {
            return collection.Contains(item);
        }

        public void CopyTo(string[] array, int arrayIndex)
        {
            collection.CopyTo(array, arrayIndex);
        }

        public bool Remove(string item)
        {
            return collection.Remove(item);
        }

        #endregion

        #region IEnumerable methods

        public IEnumerator<string> GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        #endregion

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach(string item in collection)
            {
                builder.AppendLine($"> {item}");
            }
            return builder.ToString();
        }
    }
}
