using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace ListBoxRepro
{
    internal class SpecialList : IList<string>, INotifyCollectionChanged, IList
    {
        private List<string> backing = new List<string>();
        private object locko = new object();
        public SpecialList()
        {
            //backing.AddRange(Enumerable.Repeat())
        }
        public async Task PutStuffInList(CoreDispatcher dispatcher)
        {
            Random r = new Random(0);
            int g = 0;
            while (true)
            {
                List<string> inserted = new List<string>();
                int index;
                //lock (locko)
            
                {
                  //  for (int i = 0; i < 100; i++)
                    {
                        string value = r.Next().ToString();
                        inserted.Add(g++ + " "+value);
                    }
                    index = r.Next(backing.Count);
                    backing.InsertRange(index, inserted);
                }
                Debug.WriteLine(backing.Count + " elements");
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, inserted, index)));
            }
        }

        public string this[int index]
        {
            get
            {
                //lock (locko)
                    return ((IList<string>)backing)[index];
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int Count
        {
            get
            {
                //lock (locko)
                    return ((IList<string>)backing).Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return true;
            }
        }

        public bool IsFixedSize
        {
            get
            {
                return ((IList)backing).IsFixedSize;
            }
        }

        public bool IsSynchronized
        {
            get
            {
                return false;
            }
        }

        public object SyncRoot
        {
            get
            {
                return ((IList)backing).SyncRoot;
            }
        }

        object IList.this[int index]
        {
            get
            {
                //lock (locko)
                    return ((IList)backing)[index];
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public void Add(string item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            //lock (locko)
                ((IList<string>)backing).Clear();
        }

        public bool Contains(string item)
        {
            //lock (locko)
                return ((IList<string>)backing).Contains(item);
        }

        public void CopyTo(string[] array, int arrayIndex)
        {
            //lock (locko)
                ((IList<string>)backing).CopyTo(array, arrayIndex);
        }


        public int IndexOf(string item)
        {
            //lock (locko)
                return ((IList<string>)backing).IndexOf(item);
        }

        public void Insert(int index, string item)
        {
            throw new NotImplementedException();
        }


        public bool Remove(string item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public int Add(object value)
        {
            throw new NotImplementedException();
        }

        public bool Contains(object value)
        {
            //lock (locko)
                return ((IList)backing).Contains(value);
        }

        public int IndexOf(object value)
        {
            //lock (locko)
                return ((IList)backing).IndexOf(value);
        }

        public void Insert(int index, object value)
        {
            throw new NotImplementedException();
        }

        public void Remove(object value)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Array array, int index)
        {
            //lock (locko)
                ((IList)backing).CopyTo(array, index);
        }

        public IEnumerator<string> GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}