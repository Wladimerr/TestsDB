using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AppForTests.Shared.Domain
{
    public abstract class ViewModelBase : NotifiedProperties
    {
        public object GetProperty(string name)
        {
            FieldInfo fi = this.GetType().GetField(name);
            return fi.GetValue(this);
        }
    }
}
