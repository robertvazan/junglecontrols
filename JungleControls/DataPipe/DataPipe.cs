// Part of JungleControls: https://blog.machinezoo.com/junglecontrols-free-wpf-controls-for
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JungleControls
{
    public class DataPipe : Freezable
    {
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(object), typeof(DataPipe), new FrameworkPropertyMetadata(null, HandleSourceChanged));
        public object Source { get { return (object)GetValue(SourceProperty); } set { SetValue(SourceProperty, value); } }

        public static readonly DependencyProperty TargetProperty = DependencyProperty.Register("Target", typeof(object), typeof(DataPipe), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public object Target { get { return (object)GetValue(TargetProperty); } set { SetValue(TargetProperty, value); } }

        public static readonly DependencyProperty PipesProperty = DependencyProperty.RegisterAttached("ShadowPipes", typeof(DataPipeCollection), typeof(DataPipe), new UIPropertyMetadata(null));
        public static DataPipeCollection GetPipes(DependencyObject obj)
        {
            var pipes = (DataPipeCollection)obj.GetValue(PipesProperty);
            if (pipes == null)
                obj.SetValue(PipesProperty, pipes = new DataPipeCollection());
            return pipes;
        }
        public static void SetPipes(DependencyObject obj, DataPipeCollection value) { obj.SetValue(PipesProperty, value); }

        static void HandleSourceChanged(DependencyObject self, DependencyPropertyChangedEventArgs args) { ((DataPipe)self).Target = args.NewValue; }

        protected override Freezable CreateInstanceCore() { return new DataPipe(); }
    }
}
