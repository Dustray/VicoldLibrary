using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VicoldTerminal4Net.Winform
{
    public partial class TerminalForm : AdjustableNoneForm
    {
        private string _headStr = "Dustray> ";
        private string _lastInputStr = "Dustray> ";
        private int _lastSelectionStart;
        public TerminalForm()
        {
            InitializeComponent();
            CanResize = true;
            _lastSelectionStart = _headStr.Length;
            inputText.Text = _headStr;
            inputText.SelectionStart = _lastSelectionStart;
        }

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

        #region 输入事件

        private void inputText_KeyDown(object sender, KeyEventArgs e)
        {
            //e.Handled = true;
            switch (e.KeyCode )
            {
                case Keys.Left:
                    if (inputText.SelectionStart <= _headStr.Length)
                    {
                        e.Handled = true;
                        return;
                    }
                    break;
                case Keys.Right:
                    if (inputText.SelectionStart+1 <= _headStr.Length)
                    {
                        e.Handled = true;
                        return;
                    }
                    break;
                case Keys.Enter:
                    Send();
                    break;
            }

            _lastSelectionStart = inputText.SelectionStart;
        }


        private void inputText_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = true;

        }

        private void inputText_TextChanged(object sender, EventArgs e)
        {
            if(inputText.Text.Length< _headStr.Length)
            {
                inputText.Text = _lastInputStr;
                inputText.SelectionStart = _lastSelectionStart;
                return;
            }
            var inputHeadStr = inputText.Text.Substring(0,_headStr.Length);
            if (inputHeadStr != _headStr)
            {
                inputText.Text = _lastInputStr;
                inputText.SelectionStart=_lastSelectionStart;
                return;
            }
            _lastSelectionStart = inputText.SelectionStart;
            _lastInputStr = inputText.Text;
        }

        private void inputText_MouseDown(object sender, MouseEventArgs e)
        {
            if (inputText.SelectionStart +1 <= _headStr.Length)
            {
                inputText.SelectionStart=_lastSelectionStart;
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

        #endregion

        private void Send()
        {
            inputText.Text = _headStr;
            inputText.SelectionStart = _headStr.Length;
        }
    }
}


