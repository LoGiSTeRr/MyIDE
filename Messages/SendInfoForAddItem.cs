using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVisualStudio.Messages
{
    internal class SendInfoForAddItem<A, B>
    {
        public A Value1 { get; }
        public B Value2 { get; }
        public SendInfoForAddItem(A value1, B value2)
        {
            Value1 = value1;
            Value2 = value2;
        }
    }
}
