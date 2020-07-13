using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VicoldTerminal4Net.Winform
{
    internal class OrderPool
    {
        public Dictionary<string, Action<CmdParams>> _orderCollection;
        public Control _invorkControl;
        public OrderPool(Control invorkControl)
        {
            _orderCollection = new Dictionary<string, Action<CmdParams>>();
            _invorkControl = invorkControl;
            InitPool();
        }

        private void InitPool()
        {
            //_orderCollection["clr"]= (cmdParams) =>
            //{
            //    HostInvoke(() =>
            //    {
            //        logText.Clear();
            //    });
            //};
            //_orderCollection["exit"]=(cmdParams) =>
            //{
            //    HostInvoke(() =>
            //    {
            //        Close();
            //    });
            //};
        }


        private void HostInvoke(Action action)
        {
            _invorkControl.Invoke(action);
        }
    }
}
