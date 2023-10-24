using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyVisualStudio.Models;

namespace MyVisualStudio.Messages
{
    internal class SendTextEditorStringMessage
    {
        public MyFile Value { get; }
        public SendTextEditorStringMessage(MyFile val)
        {
            Value = val;
        }
    }
}
