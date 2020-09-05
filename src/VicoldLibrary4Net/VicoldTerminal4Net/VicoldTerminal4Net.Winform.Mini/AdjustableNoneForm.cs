using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace VicoldTerminal4Net.Winform.Mini
{
    /// <summary>
    /// 类文件功能说明。
    /// </summary>
    public partial class AdjustableNoneForm : Form
    {
        const int HTLEFT = 10;
        const int HTRIGHT = 11;
        const int HTTOP = 12;
        const int HTTOPLEFT = 13;
        const int HTTOPRIGHT = 14;
        const int HTBOTTOM = 15;
        const int HTBOTTOMLEFT = 0x10;
        const int HTBOTTOMRIGHT = 17;

        public bool CanResize { get; set; } = true;

        public bool ManualResize
        {
            get
            {
                return this.FormBorderStyle == System.Windows.Forms.FormBorderStyle.None
                    && this.WindowState == FormWindowState.Normal;
            }
        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0084:
                    if (ManualResize)
                    {
                        Point vPoint = new Point((int)m.LParam & 0xFFFF,
                            (int)m.LParam >> 16 & 0xFFFF);
                        vPoint = PointToClient(vPoint);

                        if (CanResize && vPoint.X <= 5)
                        {
                            if (vPoint.Y <= 5)
                                m.Result = (IntPtr)HTTOPLEFT;
                            else if (vPoint.Y >= ClientSize.Height - 5)
                                m.Result = (IntPtr)HTBOTTOMLEFT;
                            else m.Result = (IntPtr)HTLEFT;
                        }
                        else if (CanResize && vPoint.X >= ClientSize.Width - 5)
                        {
                            if (vPoint.Y <= 5)
                                m.Result = (IntPtr)HTTOPRIGHT;
                            else if (vPoint.Y >= ClientSize.Height - 5)
                                m.Result = (IntPtr)HTBOTTOMRIGHT;
                            else m.Result = (IntPtr)HTRIGHT;
                        }
                        else if (CanResize && vPoint.Y <= 5)
                        {
                            m.Result = (IntPtr)HTTOP;
                        }
                        else if (CanResize && vPoint.Y >= ClientSize.Height - 5)
                        {
                            m.Result = (IntPtr)HTBOTTOM;

                        }
                        else if (vPoint.Y <= 40)//可拖动窗口区域事件必须放最后，否则会覆盖窗口调整事件
                        {
                            this.DefWndProc(ref m);
                            if (m.Result.ToInt32() == 0x01)
                            {
                                m.Result = new IntPtr(0x02);
                            }
                        }

                    }
                    break;
                case 0x0201:    //鼠标左键按下的消息
                    if (ManualResize)
                    {
                        m.Msg = 0x00A1;    //更改消息为非客户区按下鼠标
                        m.LParam = IntPtr.Zero;    //默认值
                        m.WParam = new IntPtr(2);    //鼠标放在标题栏内
                    }
                    base.WndProc(ref m);
                    break;
                default:
                    try
                    {
                        base.WndProc(ref m);
                    }
                    catch (Exception) { }
                    break;
            }
        }
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        /// <summary>
        /// 穿透空间可拖动窗体
        /// </summary>
        protected void Penetrate()
        {
            //为当前应用程序释放鼠标捕获
            ReleaseCapture();
            //发送消息 让系统误以为在标题栏上按下鼠标
            int VM_NCLBUTTONDOWN = 0XA1;//定义鼠标左键按下
            SendMessage((IntPtr)this.Handle, VM_NCLBUTTONDOWN, HTCAPTION, 0);
        }

        #region Window styles

        /// <summary>
        /// Alt+Tab隐藏
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nIndex"></param>
        /// <param name="dwNewLong"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "SetWindowLong", SetLastError = true)]
        protected static extern Int32 SetWindowLong(IntPtr hWnd, int nIndex, Int32 dwNewLong);

        #endregion

        #region 鼠标钩子

        #endregion
    }
}
