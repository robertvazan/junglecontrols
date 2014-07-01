﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UpdateControls.Collections;
using UpdateControls.Fields;

namespace JungleControls
{
    static class ControlFacade
    {
        public static void Lift<TViewItem, TModelItem>(ObservableCollection<TViewItem> collection, IList<TModelItem> list, Func<TViewItem, TModelItem> converter)
        {
            NotifyCollectionChangedEventHandler handler = (sender, args) =>
            {
                switch (args.Action)
                {
                    case NotifyCollectionChangedAction.Reset:
                        list.Clear();
                        foreach (var vitem in collection)
                            list.Add(converter(vitem));
                        break;
                    case NotifyCollectionChangedAction.Add:
                    case NotifyCollectionChangedAction.Remove:
                    case NotifyCollectionChangedAction.Replace:
                        if (args.Action != NotifyCollectionChangedAction.Add)
                            for (int i = 0; i < args.OldItems.Count; ++i)
                                list.RemoveAt(args.OldStartingIndex);
                        if (args.Action != NotifyCollectionChangedAction.Remove)
                            for (int i = 0; i < args.NewItems.Count; ++i)
                                list.Insert(args.NewStartingIndex + i, converter(collection[args.NewStartingIndex + i]));
                        break;
                    case NotifyCollectionChangedAction.Move:
                        var cut = new List<TModelItem>();
                        for (int i = 0; i < args.OldItems.Count; ++i)
                        {
                            cut.Add(list[args.OldStartingIndex]);
                            list.RemoveAt(args.OldStartingIndex);
                        }
                        cut.Reverse();
                        foreach (var mitem in cut)
                            list.Insert(args.NewStartingIndex, mitem);
                        break;
                }
            };
            collection.CollectionChanged += handler;
            handler(null, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public static void Lift<T>(DependencyObject view, DependencyProperty property, Independent<T> independent)
        {
            EventHandler handler = (sender, args) => independent.Value = (T)view.GetValue(property);
            DependencyPropertyDescriptor.FromProperty(property, view.GetType()).AddValueChanged(view, handler);
            handler(null, EventArgs.Empty);
        }
    }
}