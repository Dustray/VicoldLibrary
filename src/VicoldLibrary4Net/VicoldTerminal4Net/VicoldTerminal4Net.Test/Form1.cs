using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VicoldTerminal4Net.Winform.Mini;

namespace VicoldTerminal4Net.Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CmdTerminalForm.Current.Init();
            //CmdTerminalForm.Current.OutputViewVisibleMode = Winform.Mini.Entities.OutputViewVisible.Visible;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CmdTerminalForm.Current.Show();
        }
    }
}
