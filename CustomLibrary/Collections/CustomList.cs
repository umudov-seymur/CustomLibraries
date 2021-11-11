using System;
using System.Collections;
using System.Collections.Generic;

namespace CustomLibrary.Collections
{
    public class CustomList<T> : IEnumerable<T>
    {
        /// <summary>
        /// The array that will hold the values of the list
        /// </summary>
        private T[] Storage { get; set; }

        public T this[int index]
        {
            get
            {
                ThrowIfIndexOutOfRange(index);
                return Storage[index];
            }

            set
            {
                ThrowIfIndexOutOfRange(index);
                Storage[index] = (T)value;
            }
        }

        /// <summary>
        /// The number of initialised slots in the array
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// The total number of available slots in the array
        /// </summary>
        private int _capacity;
        public int Capacity
        {
            get
            {
                return _capacity;
            }
            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException("Index was out of range. Must be non-negative and less than the size of the collection.");
                _capacity = value;
            }
        }

        public CustomList() : this(4)
        {
        }

        public CustomList(int initialCapacity)
        {
            Capacity = initialCapacity;
            Storage = new T[initialCapacity];
        }

        /// <summary>
        /// Adds an object to the end of the CustomLibrary.Collections.CustomList`1
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            if (Count == Capacity)
            {
                Resize();
            }
            Storage[Count] = item;
            Count++;
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the CustomLibrary.Collections.CustomList`1
        /// </summary>
        /// <param name="collection"></param>
        public void AddRange(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                Add(item);
            }
        }

        /// <summary>
        /// Inserts an element into the CustomLibrary.Collections.CustomList`1 at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        public void Insert(int index, T item)
        {
            ThrowIfIndexOutOfRange(index);

            if (Count == Capacity)
            {
                Resize();
            }

            for (int i = Count; i > index; i--)
            {
                Storage[i] = Storage[i - 1];
            }

            Storage[index] = item;
            Count++;
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the CustomLibrary.Collections.CustomList`1
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(T item)
        {
            int findedItemIndex;

            for (findedItemIndex = 0; findedItemIndex < Count; findedItemIndex++)
            {
                if (Storage[findedItemIndex].Equals(item))
                {
                    break;
                }
            }

            if (findedItemIndex < Count)
            {
                Count--;
                for (int i = findedItemIndex; i < Count; i++)
                {
                    Storage[i] = Storage[i + 1];
                }
            }

            return findedItemIndex < Count;
        }

        /// <summary>
        /// Removes the element at the specified index of the CustomLibrary.Collections.CustomList`1
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            ThrowIfIndexOutOfRange(index);

            for (int i = index; i < Count - 1; i++)
            {
                Storage[i] = Storage[i + 1];
            }

            Storage[Count - 1] = default(T);
            Count--;
        }

        /// <summary>
        /// Reverses the order of the elements in the entire CustomLibrary.Collections.CustomList`1
        /// </summary>
        public void Reverse()
        {
            for (int i = 0; i < Count / 2; i++)
            {
                T temp = Storage[i];
                Storage[i] = Storage[Count - i - 1];
                Storage[Count - i - 1] = temp;
            }
        }

        /// <summary>
        /// Reverses the order of the elements in the specified range.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        public void Reverse(int index, int count)
        {
            for (int i = index; i < count; i++)
            {
                T temp = Storage[i];
                Storage[i] = Storage[count - i + 1];
                Storage[count - i + 1] = temp;
            }
        }

        /// <summary>
        /// Searches for the specified object and returns the zero-based index of the first occurrence 
        /// within the CustomLibrary.Collections.CustomList`1
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (Storage[i].Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// Searches for the specified object and returns the zero-based index of the last occurrence 
        /// within the CustomLibrary.Collections.CustomList`1
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int LastIndexOf(T item)
        {
            for (int i = Count - 1; i >= 0; i--)
            {
                if (Storage[i].Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// Searches for an element that matches the conditions defined by the specified
        //  predicate, and returns the first occurrence within the entire CustomLibrary.Collections.CustomList`1.
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public T Find(Predicate<T> match)
        {
            for (int i = 0; i < Count; i++)
            {
                if (match(Storage[i]))
                {
                    return Storage[i];
                }
            }

            return default(T);
        }

        /// <summary>
        /// Retrieves all the elements that match the conditions defined by the specified predicate.
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public CustomList<T> FindAll(Predicate<T> match)
        {
            CustomList<T> items = new CustomList<T>();
            for (int i = 0; i < Count; i++)
            {
                if (match(Storage[i]))
                {
                    items.Add(Storage[i]);
                }
            }
            return items;
        }

        /// <summary>
        /// Performs the specified action on each element of the CustomLibrary.Collections.CustomList`1.
        /// </summary>
        /// <param name="action"></param>
        public void ForEach(Action<T> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException();
            }
            for (int i = 0; i < Count; i++)
            {
                action(Storage[i]);
            }
        }

        /// <summary>
        /// Determines whether an element is in the CustomLibrary.Collections.CustomList`1
        /// </summary>
        /// <param name="item"></param>
        /// <returns>true or false</returns>
        public bool Contains(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (Storage[i].Equals(item))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Removes all elements from the CustomLibrary.Collections.CustomList`1
        /// </summary>
        public void Clear()
        {
            Storage = new T[Capacity];
            Count = 0;
        }

        /// <summary>
        /// Doubles the size of our CustomLibrary.Collections.CustomList`1
        /// </summary>
        private void Resize()
        {
            T[] resizedList = new T[Capacity * 2];
            for (int i = 0; i < Capacity; i++)
            {
                resizedList[i] = Storage[i];
            }
            Storage = resizedList;
            Capacity *= 2;
        }

        /// <summary>
        /// Throw if index out range
        /// </summary>
        /// <param name="index"></param>
        private void ThrowIfIndexOutOfRange(int index)
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException("Index out of range");
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return Storage[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
