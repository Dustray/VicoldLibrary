using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VicoldTerminal4Net
{
    public class CmdException : Exception
    {
        public CmdException() : base()
        {

        }

        public CmdException(string message) : base(message)
        {

        }
    }
}
