using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vicold.Terminal4Net.Winform.Mini;

namespace Vicold.Terminal4Net.Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CmdTerminalForm.Current.Init();
            //CmdTerminalForm.Current.OutputViewVisibleMode = Winform.Mini.Entities.OutputViewVisible.Visible;
            CmdTerminal.Current.AddOrder("open", "打开文件", (param) =>
             {
                 var pair = param.PairParams[0];
                 MessageBox.Show("dsdsdsds" + pair.Value);
             }).AddParam("-p", "文件路径");
            CmdTerminal.Current.SetAdminMode(true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CmdTerminalForm.Current.Show();
        }
    }
}
