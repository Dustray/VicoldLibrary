using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VicoldTerminal4Net
{
    public class CommandException : Exception
    {
        public CommandException() : base()
        {

        }

        public CommandException(string message) : base(message)
        {

        }
    }
}
