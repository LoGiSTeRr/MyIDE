using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVisualStudio.Messages
{
    internal class SendPathMessage
    {
        public string Value { get; }
        public SendPathMessage(string val)
        {
            Value = val;
        }
    }
}
