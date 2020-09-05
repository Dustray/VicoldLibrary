using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VicoldTerminal4Net.Winform.Mini.Entities;

namespace VicoldTerminal4Net.Winform.Mini
{
    public class CmdTerminalForm
    {
        #region 单例模式

        /// <summary>
        /// 模块引擎
        /// </summary>
        private CmdTerminalForm()
        {
        }

        /// <summary>
        /// 模块引擎拥有者
        /// </summary>
        private static class ModEngineHolder
        {
            internal static readonly CmdTerminalForm INSTANCE = new CmdTerminalForm();
        }

        /// <summary>
        /// 模块引擎
        /// <para>当前的</para>
        /// </summary>
        public static CmdTerminalForm Current => ModEngineHolder.INSTANCE;

        #endregion

        private TerminalFormMini _form;

        public OutputViewVisible OutputViewVisibleMode { get; set; } = OutputViewVisible.Auto;

        public void Show()
        {
            if (_form == null || _form.IsFormClosed)
            {
                _form = new TerminalFormMini();
                _form.OutputVisibleMode = OutputViewVisibleMode;
                _form.Show();
            }
        }

        public void Init()
        {
            CmdTerminal.Current.SetAdminMode(true);
            CmdTerminal.Current.AddOrder("clr", "清空日志区。", (cmdParams) =>
            {
                _form?.HostInvoke(() =>
                {
                    _form?.RecordLog("");
                });
            });
            CmdTerminal.Current.AddOrder("exit", "退出命令行工具。", (cmdParams) =>
            {
                _form?.HostInvoke(() =>
                {
                    _form?.Close();
                });
            });
#if DEBUG
            CmdTerminal.Current.AddOrder("test", "清空日志区。", (cmdParams) =>
            {
                _form?.HostInvoke(() =>
                {
                    _form?.RecordLog("You execute [TEST].");
                });
            }).SetExecutableType(ExecutableType.Admin & ExecutableType.Debug)
             .AddParam("a", "AAA")
             .AddParam("b", "BBB")
             .AddParam("c", "CCC");
#endif
        }

        internal void OnClose()
        {
            _form = null;
        }
    }
}
