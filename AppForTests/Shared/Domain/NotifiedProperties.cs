using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AppForTests.Shared.Domain
{
    public abstract class NotifiedProperties : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public virtual void PropertyHasChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
