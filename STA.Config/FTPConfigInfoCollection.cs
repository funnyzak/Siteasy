﻿using System;
using System.Collections;

namespace STA.Config
{
    /// <summary>
    /// FTP配置信息集合类
    /// </summary>
    [Serializable]
    public class FTPConfigInfoCollection : CollectionBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="FTPConfigInfoCollection">FTPConfigInfoCollection</see> class.
        /// </summary>
        public FTPConfigInfoCollection()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FTPConfigInfoCollection">FTPConfigInfoCollection</see> class containing the elements of the specified source collection.
        /// </summary>
        /// <param name="value">A <see cref="FTPConfigInfoCollection">FTPConfigInfoCollection</see> with which to initialize the collection.</param>
        public FTPConfigInfoCollection(FTPConfigInfoCollection value)
        {
            this.AddRange(value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FTPConfigInfoCollection">FTPConfigInfoCollection</see> class containing the specified array of <see cref="FTPConfigInfo">FTPConfigInfo</see> Components.
        /// </summary>
        /// <param name="value">An array of <see cref="FTPConfigInfo">FTPConfigInfo</see> Components with which to initialize the collection. </param>
        public FTPConfigInfoCollection(FTPConfigInfo[] value)
        {
            this.AddRange(value);
        }

        /// <summary>
        /// Gets the <see cref="FTPConfigInfoCollection">FTPConfigInfoCollection</see> at the specified index in the collection.
        /// <para>
        /// In C#, this property is the indexer for the <see cref="FTPConfigInfoCollection">FTPConfigInfoCollection</see> class.
        /// </para>
        /// </summary>
        public FTPConfigInfo this[int index]
        {
            get { return ((FTPConfigInfo)(this.List[index])); }
        }

        public int Add(FTPConfigInfo value)
        {
            return this.List.Add(value);
        }

        /// <summary>
        /// Copies the elements of the specified <see cref="FTPConfigInfo">FTPConfigInfo</see> array to the end of the collection.
        /// </summary>
        /// <param name="value">An array of type <see cref="FTPConfigInfo">FTPConfigInfo</see> containing the Components to add to the collection.</param>
        public void AddRange(FTPConfigInfo[] value)
        {
            for (int i = 0; (i < value.Length); i = (i + 1))
            {
                this.Add(value[i]);
            }
        }

        /// <summary>
        /// Adds the contents of another <see cref="FTPConfigInfoCollection">FTPConfigInfoCollection</see> to the end of the collection.
        /// </summary>
        /// <param name="value">A <see cref="FTPConfigInfoCollection">FTPConfigInfoCollection</see> containing the Components to add to the collection. </param>
        public void AddRange(FTPConfigInfoCollection value)
        {
            for (int i = 0; (i < value.Count); i = (i + 1))
            {
                this.Add((FTPConfigInfo)value.List[i]);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the collection contains the specified <see cref="FTPConfigInfoCollection">FTPConfigInfoCollection</see>.
        /// </summary>
        /// <param name="value">The <see cref="FTPConfigInfoCollection">FTPConfigInfoCollection</see> to search for in the collection.</param>
        /// <returns><b>true</b> if the collection contains the specified object; otherwise, <b>false</b>.</returns>
        public bool Contains(FTPConfigInfo value)
        {
            return this.List.Contains(value);
        }

        /// <summary>
        /// Copies the collection Components to a one-dimensional <see cref="T:System.Array">Array</see> instance beginning at the specified index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array">Array</see> that is the destination of the values copied from the collection.</param>
        /// <param name="index">The index of the array at which to begin inserting.</param>
        public void CopyTo(FTPConfigInfo[] array, int index)
        {
            this.List.CopyTo(array, index);
        }

        /// <summary>
        /// Gets the index in the collection of the specified <see cref="FTPConfigInfoCollection">FTPConfigInfoCollection</see>, if it exists in the collection.
        /// </summary>
        /// <param name="value">The <see cref="FTPConfigInfoCollection">FTPConfigInfoCollection</see> to locate in the collection.</param>
        /// <returns>The index in the collection of the specified object, if found; otherwise, -1.</returns>
        public int IndexOf(FTPConfigInfo value)
        {
            return this.List.IndexOf(value);
        }

        public void Insert(int index, FTPConfigInfo value)
        {
            List.Insert(index, value);
        }

        public void Remove(FTPConfigInfo value)
        {
            List.Remove(value);
        }

        /// <summary>
        /// Returns an enumerator that can iterate through the <see cref="FTPConfigInfoCollection">FTPConfigInfoCollection</see> instance.
        /// </summary>
        /// <returns>An <see cref="BaseConfigInfoCollectionEnumerator">BaseConfigInfoCollectionEnumerator</see> for the <see cref="FTPConfigInfoCollection">FTPConfigInfoCollection</see> instance.</returns>
        public new FTPConfigInfoCollectionEnumerator GetEnumerator()
        {
            return new FTPConfigInfoCollectionEnumerator(this);
        }

        /// <summary>
        /// Supports a simple iteration over a <see cref="FTPConfigInfoCollection">FTPConfigInfoCollection</see>.
        /// </summary>
        public class FTPConfigInfoCollectionEnumerator : IEnumerator
        {
            private IEnumerator _enumerator;
            private IEnumerable _temp;

            /// <summary>
            /// Initializes a new instance of the <see cref="BaseConfigInfoCollectionEnumerator">BaseConfigInfoCollectionEnumerator</see> class referencing the specified <see cref="FTPConfigInfoCollection">FTPConfigInfoCollection</see> object.
            /// </summary>
            /// <param name="mappings">The <see cref="FTPConfigInfoCollection">FTPConfigInfoCollection</see> to enumerate.</param>
            public FTPConfigInfoCollectionEnumerator(FTPConfigInfoCollection mappings)
            {
                _temp = ((IEnumerable)(mappings));
                _enumerator = _temp.GetEnumerator();
            }

            /// <summary>
            /// Gets the current element in the collection.
            /// </summary>
            public FTPConfigInfo Current
            {
                get { return ((FTPConfigInfo)(_enumerator.Current)); }
            }

            object IEnumerator.Current
            {
                get { return _enumerator.Current; }
            }

            /// <summary>
            /// Advances the enumerator to the next element of the collection.
            /// </summary>
            /// <returns><b>true</b> if the enumerator was successfully advanced to the next element; <b>false</b> if the enumerator has passed the end of the collection.</returns>
            public bool MoveNext()
            {
                return _enumerator.MoveNext();
            }

            bool IEnumerator.MoveNext()
            {
                return _enumerator.MoveNext();
            }

            /// <summary>
            /// Sets the enumerator to its initial position, which is before the first element in the collection.
            /// </summary>
            public void Reset()
            {
                _enumerator.Reset();
            }

            void IEnumerator.Reset()
            {
                _enumerator.Reset();
            }




        }
    }
}
