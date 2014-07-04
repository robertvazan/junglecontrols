﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using UpdateControls;
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

        class LiftAllHelper<T>
        {
            public static void ForwardLift(DependencyObject view, DependencyProperty property, Independent independent)
            {
                Lift(view, property, (Independent<T>)independent);
            }
        }

        public static void LiftAll(DependencyObject view, object model, Func<string, string, bool> matcher)
        {
            var fields = (from field in model.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                          where field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(Independent<>) && field.GetValue(model) != null
                          select field).ToList();
            var properties = (from field in view.GetType().GetFields(BindingFlags.Public | BindingFlags.Static)
                              where field.FieldType == typeof(DependencyProperty) && field.Name.EndsWith("Property")
                              select (DependencyProperty)field.GetValue(null)).ToList();
            foreach (var property in properties)
            {
                var field = fields.SingleOrDefault(f => matcher(property.Name, f.Name));
                if (field != null)
                {
                    typeof(LiftAllHelper<>).MakeGenericType(field.FieldType.GetGenericArguments()[0]).GetMethod("ForwardLift")
                        .Invoke(null, new object[] { view, property, field.GetValue(model) });
                }
            }
        }

        public static void LiftAll(DependencyObject view, object model)
        {
            LiftAll(view, model, (property, field) => field == property + "Independent");
        }

        public static void InitializeFacade<T>(DependencyProperty defaultStyleKey = null)
        {
            var resources = (ResourceDictionary)Application.LoadComponent(new Uri("/JungleControls;component/UniversalTemplate.xaml", UriKind.Relative));
            Control.TemplateProperty.OverrideMetadata(typeof(T), new FrameworkPropertyMetadata((ControlTemplate)resources["JungleControlsUniversalTemplate"]));
            if (defaultStyleKey != null)
                defaultStyleKey.OverrideMetadata(typeof(T), new FrameworkPropertyMetadata((object)null));
        }

        public static void TemplateFacade<T>(DependencyObject placeholder, object data)
            where T : FrameworkElement
        {
            var presenter = placeholder as ContentPresenter;
            if (presenter != null)
            {
                var view = (FrameworkElement)Activator.CreateInstance(typeof(T));
                view.DataContext = data;
                presenter.Content = view;
            }
        }
    }
}
