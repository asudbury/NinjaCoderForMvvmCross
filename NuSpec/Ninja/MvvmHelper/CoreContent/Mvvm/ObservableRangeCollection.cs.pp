// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ObservableRangeCollection type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$.Mvvm
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;

    /// <summary> 
    /// Represents a dynamic data collection that provides notifications when items get added, removed, or when the whole list is refreshed. 
    /// </summary> 
    /// <typeparam name="T"></typeparam> 
    public class ObservableRangeCollection<T> : ObservableCollection<T>
    {
        /// <summary> 
        /// Initializes a new instance of the System.Collections.ObjectModel.ObservableCollection(Of T) class. 
        /// </summary> 
        public ObservableRangeCollection()
        {
        }

        /// <summary> 
        /// Initializes a new instance of the System.Collections.ObjectModel.ObservableCollection(Of T) class that contains elements copied from the specified collection. 
        /// </summary> 
        /// <param name="collection">collection: The collection from which the elements are copied.</param> 
        /// <exception cref="System.ArgumentNullException">The collection parameter cannot be null.</exception> 
        public ObservableRangeCollection(IEnumerable<T> collection)
            : base(collection)
        {
        }

        /// <summary> 
        /// Adds the elements of the specified collection to the end of the ObservableCollection(Of T). 
        /// </summary> 
        public void AddRange(IEnumerable<T> collection, NotifyCollectionChangedAction notificationMode = NotifyCollectionChangedAction.Add)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            this.CheckReentrancy();

            if (notificationMode == NotifyCollectionChangedAction.Reset)
            {
                foreach (var i in collection)
                {
                    this.Items.Add(i);
                }

                this.OnPropertyChanged(new PropertyChangedEventArgs("Count"));
                this.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
                this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

                return;
            }

            int startIndex = this.Count;

            List<T> list = collection as List<T>;
            List<T> changedItems = list ?? new List<T>(collection);

            foreach (var i in changedItems)
            {
                this.Items.Add(i);
            }

            this.OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            this.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, changedItems, startIndex));
        }

        /// <summary> 
        /// Removes the first occurence of each item in the specified collection from ObservableCollection(Of T). 
        /// </summary> 
        public void RemoveRange(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            foreach (var i in collection)
            {
                this.Items.Remove(i);
            }

            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        /// <summary> 
        /// Clears the current collection and replaces it with the specified item. 
        /// </summary> 
        public void Replace(T item)
        {
            this.ReplaceRange(new T[] { item });
        }

        /// <summary> 
        /// Clears the current collection and replaces it with the specified collection. 
        /// </summary> 
        public void ReplaceRange(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            this.Items.Clear();

            this.AddRange(collection, NotifyCollectionChangedAction.Reset);
        }
    }
}
