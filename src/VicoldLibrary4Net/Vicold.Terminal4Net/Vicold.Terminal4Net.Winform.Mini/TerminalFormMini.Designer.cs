namespace Vicold.Terminal4Net.Winform.Mini
{
    partial class TerminalFormMini
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TerminalFormMini));
            this.textInput = new System.Windows.Forms.TextBox();
            this.textOutput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textInput
            // 
            resources.ApplyResources(this.textInput, "textInput");
            this.textInput.BackColor = System.Drawing.Color.White;
            this.textInput.Name = "textInput";
            this.textInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textInput_KeyDown);
            this.textInput.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TextInput_MouseDown);
            // 
            // textOutput
            // 
            resources.ApplyResources(this.textOutput, "textOutput");
            this.textOutput.BackColor = System.Drawing.Color.White;
            this.textOutput.Name = "textOutput";
            this.textOutput.ReadOnly = true;
            // 
            // TerminalFormMini
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.textOutput);
            this.Controls.Add(this.textInput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TerminalFormMini";
            this.TransparencyKey = System.Drawing.Color.Tomato;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TerminalFormMini_FormClosing);
            this.Load += new System.EventHandler(this.TerminalForm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TerminalForm_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textInput;
        private System.Windows.Forms.TextBox textOutput;
    }
}

