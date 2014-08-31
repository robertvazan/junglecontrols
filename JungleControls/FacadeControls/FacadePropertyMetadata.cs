using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JungleControls
{
    public class FacadePropertyMetadata : FrameworkPropertyMetadata
    {
        internal FacadeType FacadeType;
        internal int Index = -1;

        public FacadePropertyMetadata()
        {
            PropertyChangedCallback = NotifyModel;
        }

        public FacadePropertyMetadata(object defaultValue)
            : base(defaultValue)
        {
            PropertyChangedCallback = NotifyModel;
        }

        void NotifyModel(object sender, DependencyPropertyChangedEventArgs args)
        {
            if (FacadeType != null && FacadeType.ComponentType == sender.GetType())
                FacadeType.NotifyModel(sender, args);
        }
    }
}
