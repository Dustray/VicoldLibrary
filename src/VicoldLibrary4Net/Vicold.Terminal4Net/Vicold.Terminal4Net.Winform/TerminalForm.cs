using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vicold.Terminal4Net.Winform
{
    public partial class TerminalForm : AdjustableNoneForm
    {
        private string _headStr = "Dustray> ";
        private string _lastInputStr = "Dustray> ";
        private int _lastSelectionStart;
        public TerminalForm()
        {
            InitializeComponent();
            TopMost = true;
            CanResize = true;
            _lastSelectionStart = _headStr.Length;
            inputText.Text = _headStr;
            inputText.SelectionStart = _lastSelectionStart;

            CmdTerminal.Current.BindingInternalOutput(TerminalBackCommand);
            PresetCommand();
        }


        #region 窗体事件

        private void closeBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void titleBar_MouseDown(object sender, MouseEventArgs e)
        {
            Penetrate();
        }
        private void TerminalForm_Load(object sender, EventArgs e)
        {
            inputText.Focus();
        }

        #endregion

        #region 输入事件

        private void inputText_KeyDown(object sender, KeyEventArgs e)
        {
            //e.Handled = true;
            switch (e.KeyCode)
            {
                case Keys.Left:
                    if (inputText.SelectionStart <= _headStr.Length)
                    {
                        e.Handled = true;
                        return;
                    }
                    break;
                case Keys.Right:
                    if (inputText.SelectionStart + 1 <= _headStr.Length)
                    {
                        e.Handled = true;
                        return;
                    }
                    break;
                case Keys.Up:
                    var cmd = CmdTerminal.Current.FlipHistoryUp();
                    inputText.Text = $"{_headStr}{cmd}";
                    break;
                case Keys.Down:
                    var cmd1 = CmdTerminal.Current.FlipHistoryDown();
                    inputText.Text = $"{_headStr}{cmd1}";
                    break;
                case Keys.Enter:
                    Send();
                    break;
            }

            if (this.IsDisposed) return;
            _lastSelectionStart = inputText.SelectionStart;
        }


        private void inputText_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = true;

        }

        private void inputText_TextChanged(object sender, EventArgs e)
        {
            if (inputText.Text.Length < _headStr.Length)
            {
                inputText.Text = _lastInputStr;
                inputText.SelectionStart = _lastSelectionStart;
                return;
            }
            var inputHeadStr = inputText.Text.Substring(0, _headStr.Length);
            if (inputHeadStr != _headStr)
            {
                inputText.Text = _lastInputStr;
                inputText.SelectionStart = _lastSelectionStart;
                return;
            }
            _lastSelectionStart = inputText.SelectionStart;
            _lastInputStr = inputText.Text;
        }

        private void inputText_MouseDown(object sender, MouseEventArgs e)
        {
            if (inputText.SelectionStart + 1 <= _headStr.Length)
            {
                inputText.SelectionStart = _lastSelectionStart;
                return;
            }
            _lastSelectionStart = inputText.SelectionStart;
        }

        private void inputText_MouseMove(object sender, MouseEventArgs e)
        {
            if (inputText.SelectionStart + 1 <= _headStr.Length)
            {
                inputText.SelectionStart = _lastSelectionStart;
                return;
            }
        }

        private void logText_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        #endregion

        #region 成员方法

        /// <summary>
        /// 发送命令
        /// </summary>
        private async void Send()
        {
            var orderStr = inputText.Text;
            inputText.Text = _headStr;
            inputText.SelectionStart = _headStr.Length;
            orderStr = new Regex("[\\s]+").Replace(orderStr, " ");
            if (orderStr.Length <= _headStr.Length)
            {
                return;
            }
            var orderArray = orderStr.Substring(_headStr.Length, orderStr.Length - _headStr.Length);
            if (!string.IsNullOrWhiteSpace(orderArray))
            {
                RecordLog(orderStr);
                var result = await CmdTerminal.Current.TryExecuteOrder(orderArray);
                if (result)
                {
                    //RecordLog($"[SYS]Command executed: [{orderArray}]");
                }
                else
                {
                    RecordLog($"[SYS]Error: Command [{orderArray}] not found");
                }
                RecordLog($"\r\n");
            }
        }

        private void RecordLog(string log)
        {
            if (this.IsDisposed) return;
            logText.AppendText($"{log}\r\n");
            logText.ScrollToCaret();
        }

        #endregion

        private void PresetCommand()
        {
            CmdTerminal.Current.SetAdminMode(true);
            CmdTerminal.Current.AddOrder("clr", "清空日志区。", (cmdParams) =>
             {
                 HostInvoke(() =>
                 {
                     logText.Clear();
                 });
             });
            CmdTerminal.Current.AddOrder("exit", "退出命令行工具。", (cmdParams) =>
             {
                 HostInvoke(() =>
                 {
                     Close();
                 });
             });
#if DEBUG
            CmdTerminal.Current.AddOrder("test", "清空日志区。", (cmdParams) =>
            {
                HostInvoke(() =>
                {
                    RecordLog("You execute TEST.");
                });
            }).SetExecutableType(ExecutableType.Admin & ExecutableType.Debug)
             .AddParam("a", "AAA")
             .AddParam("b", "BBB")
             .AddParam("c", "CCC");
#endif
        }
        private void TerminalBackCommand(string back, CmdOutPutType type)
        {
            HostInvoke(() =>
            {
                RecordLog(back);
            });
        }
        private void HostInvoke(Action action)
        {
            this.Invoke(action);
        }

    }
}


