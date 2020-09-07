using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Vicold.Terminal4Net.Winform.Mini.Entities;
using Vicold.Terminal4Net.Winform.Mini.Hook;

namespace Vicold.Terminal4Net.Winform.Mini
{
    public partial class TerminalFormMini : AdjustableNoneForm
    {
        private MouseHook _mouse = new MouseHook();
        public TerminalFormMini()
        {
            InitializeComponent();
            TopMost = true;
            CmdTerminal.Current.BindingInternalOutput(TerminalBackCommand);
            textInput.ImeMode = ImeMode.Off;
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(System.Globalization.CultureInfo.GetCultureInfo("en-US")) ?? InputLanguage.DefaultInputLanguage;
        }

        /// <summary>
        /// 输出隐藏模式
        /// </summary>
        public OutputViewVisible OutputVisibleMode { get; set; } = OutputViewVisible.Auto;

        /// <summary>
        /// 窗体是否关闭
        /// </summary>
        public bool IsFormClosed { get; private set; } = false;
        #region 事件

        private void TerminalForm_MouseDown(object sender, MouseEventArgs e)
        {
            Penetrate();
        }

        private void OnMouseActivity(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.X < Left || e.X > Right || e.Y < Top || e.Y > Bottom)
                {
                    Close();
                    return;
                }
            }
        }

        private void TerminalForm_Load(object sender, EventArgs e)
        {
            SetWindowLong(this.Handle, (-20), 0x80);
            _mouse.OnMouseActivity += OnMouseActivity;
            _mouse.Start();
            HideOutputView(null);
        }

        private void TextInput_MouseDown(object sender, MouseEventArgs e)
        {
            Penetrate();
        }

        private void textInput_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    var cmd = CmdTerminal.Current.FlipHistoryUp();
                    textInput.Text = cmd;
                    break;
                case Keys.Down:
                    var cmd1 = CmdTerminal.Current.FlipHistoryDown();
                    textInput.Text = cmd1;
                    break;
                case Keys.Enter:
                    Send();
                    break;
            }
        }

        #endregion

        #region 成员方法

        /// <summary>
        /// 发送命令
        /// </summary>
        private async void Send()
        {
            var orderStr = textInput.Text;
            textInput.Clear();
            orderStr = new Regex("[\\s]+").Replace(orderStr, " ");
            if (orderStr.Length == 0)
            {
                return;
            }

            if (!string.IsNullOrWhiteSpace(orderStr))
            {
                RecordLog(orderStr);
                _ = await CmdTerminal.Current.TryExecuteOrder(orderStr);
            }
        }

        internal void RecordLog(string log)
        {
            if (this.IsDisposed) return;
            textOutput.Clear();
            textOutput.AppendText(log);
            HideOutputView(log);
        }

        private void HideOutputView(string content)
        {
            switch (OutputVisibleMode)
            {
                case OutputViewVisible.Auto:
                    ShowOutput(!string.IsNullOrWhiteSpace(content));
                    break;
                case OutputViewVisible.Visible:
                    ShowOutput(true);
                    break;
                case OutputViewVisible.Hidden:
                    ShowOutput(false);
                    break;
            }

            void ShowOutput(bool isShow)
            {
                Height = isShow ? 200 : textInput.Height + 6;
            }
        }

        private void TerminalBackCommand(string back, CmdOutPutType type)
        {
            HostInvoke(() =>
            {
                RecordLog(back);
            });
        }
        internal void HostInvoke(Action action)
        {
            this.Invoke(action);
        }

        #endregion

        private void TerminalFormMini_FormClosing(object sender, FormClosingEventArgs e)
        {
            _mouse.Stop();
            IsFormClosed = true;
            CmdTerminalForm.Current.OnClose();
        }
    }
}
