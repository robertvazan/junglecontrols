﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UpdateControls.Fields;
using UpdateControls.XAML;

namespace JungleControls
{
    public partial class StatCounter : UserControl
    {
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register("Header", typeof(string), typeof(StatCounter));
        public string Header { get { return (string)GetValue(HeaderProperty); } set { SetValue(HeaderProperty, value); } }

        public static readonly DependencyProperty HeaderPositionProperty = DependencyProperty.Register("HeaderPosition", typeof(StatCounterHeaderPosition), typeof(StatCounter));
        public StatCounterHeaderPosition HeaderPosition { get { return (StatCounterHeaderPosition)GetValue(HeaderPositionProperty); } set { SetValue(HeaderPositionProperty, value); } }

        public static readonly DependencyProperty DataProperty = DependencyProperty.Register("Data", typeof(object), typeof(StatCounter));
        public object Data { get { return GetValue(DataProperty); } set { SetValue(DataProperty, value); } }

        public StatCounter()
        {
            InitializeComponent();
            ((FrameworkElement)Content).DataContext = ForView.Wrap(new ViewModel(this));
        }

        class ViewModel
        {
            readonly Independent<string> HeaderIndependent = new Independent<string>();
            readonly Independent<StatCounterHeaderPosition> HeaderPositionIndependent = new Independent<StatCounterHeaderPosition>();
            readonly Independent<object> DataIndependent = new Independent<object>();

            public string Header { get { return HeaderIndependent.Value; } }
            public int HeaderRow { get { return HeaderPositionIndependent.Value == StatCounterHeaderPosition.Top ? 0 : 1; } }
            public string Data { get { return DataIndependent.Value != null ? DataIndependent.Value.ToString() : ""; } }
            public int DataRow { get { return 1 - HeaderRow; } }

            public ViewModel(StatCounter element)
            {
                ControlFacade.LiftAll(element, this);
            }
        }
    }
}
