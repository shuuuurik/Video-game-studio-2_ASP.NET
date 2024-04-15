using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VideogameStudio
{
    public class TodoList : ICollection<Todo>
    {
        public ObservableCollection<Todo> Items { get; set; }

        public TodoList()
        {
            Items = new ObservableCollection<Todo>();
        }

        public TodoList(ObservableCollection<Todo> items)
        {
            Items = items;
        }

        public Todo TakeMostPriorityItem()
        {
            if (Items.Count == 0)
            {
                throw new NotSupportedException("Add Todos before calling TakeMostPriorityItem");
            }
            Todo item = Items[0];
            Items.RemoveAt(0);
            return item;
        }

        #region ICollection<Todo> Members
        public void Add(Todo item)
        {
            if (Items.Count == 0)
            {
                Items.Add(item);
                return;
            }
            foreach (Todo todo in Items)
            {
                if (todo.Title == item.Title)
                    throw new NotSupportedException("Разработка с таким названием уже существует.");
            }
            for (int i = 0; i < Items.Count; ++i)
            {
                if (item > Items[i])
                {
                    Items.Insert(i, item);
                    return;
                }
            }
            Items.Insert(Items.Count, item);
        }

        public void Clear()
        {
            Items.Clear();
        }

        public bool Contains(Todo item)
        {
            return Items.Contains(item);
        }

        public void CopyTo(Todo[] array, int arrayIndex)
        {
            Items.CopyTo(array, arrayIndex);
        }

        public bool Remove(Todo item)
        {
            return Items.Remove(item);
        }

        public int Count
        {
            get { return Items.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }
        #endregion

        public Todo this[string title]
        {
            get
            {
                foreach (Todo todo in Items)
                {
                    if (todo.Title == title)
                        return todo;
                }
                throw new ArgumentOutOfRangeException
                    ("название", title, "Разработки с таким названием не существует");
            }
            set
            {
                this[title] = value;
            }
        }

        public Todo this[int id]
        {
            get
            {
                foreach (Todo todo in Items)
                {
                    if (todo.ID == id)
                        return todo;
                }
                throw new ArgumentOutOfRangeException
                    ("название", id, "Разработки с таким ID не существует");
            }
            set
            {
                this[id] = value;
            }
        }

        #region IList<Todo> Members
        /*public Todo this[int index]
        {
            get
            {
                if (index < 0 | index >= Count)
                {
                    throw new ArgumentOutOfRangeException
                        ("index", index, "Indexer out of range");
                }

                return Items[index];
            }
            set
            {
                Items[index] = value;
            }
        }

        public int IndexOf(Todo item)
        {
            return Items.IndexOf(item);
        }

        public void Insert(int index, Todo item)
        {
            Items.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            Items.RemoveAt(index);
        }*/
        #endregion

        #region IEnumerable<T> Members
        public IEnumerator<Todo> GetEnumerator()
        {
            foreach (var item in Items)
            {
                yield return item;
            }
        }
        #endregion

        #region IEnumerable Members
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
