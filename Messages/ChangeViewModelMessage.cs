using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVisualStudio.Messages
{
    internal class ChangeViewModelMessage<T>
    {
        public T Value { get; }
        public ChangeViewModelMessage(T value)
        {
            Value = value;
        }
    }
}
