namespace ChatAppClient.ViewController
{
    partial class ChatForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxSendMessage = new System.Windows.Forms.TextBox();
            this.buttonSendMessage = new System.Windows.Forms.Button();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.menuStripConnect = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemServerConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripConnect.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxSendMessage
            // 
            this.textBoxSendMessage.Location = new System.Drawing.Point(29, 413);
            this.textBoxSendMessage.Name = "textBoxSendMessage";
            this.textBoxSendMessage.Size = new System.Drawing.Size(606, 25);
            this.textBoxSendMessage.TabIndex = 0;
            // 
            // buttonSendMessage
            // 
            this.buttonSendMessage.Enabled = false;
            this.buttonSendMessage.Location = new System.Drawing.Point(656, 353);
            this.buttonSendMessage.Name = "buttonSendMessage";
            this.buttonSendMessage.Size = new System.Drawing.Size(109, 83);
            this.buttonSendMessage.TabIndex = 1;
            this.buttonSendMessage.Text = "送信";
            this.buttonSendMessage.UseVisualStyleBackColor = true;
            this.buttonSendMessage.Click += new System.EventHandler(this.buttonSendMessage_Click);
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.Location = new System.Drawing.Point(41, 43);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.Size = new System.Drawing.Size(724, 294);
            this.richTextBoxLog.TabIndex = 2;
            this.richTextBoxLog.Text = "";
            // 
            // menuStripConnect
            // 
            this.menuStripConnect.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStripConnect.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStripConnect.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemConnect});
            this.menuStripConnect.Location = new System.Drawing.Point(0, 0);
            this.menuStripConnect.Name = "menuStripConnect";
            this.menuStripConnect.Size = new System.Drawing.Size(801, 33);
            this.menuStripConnect.TabIndex = 3;
            this.menuStripConnect.Text = "menuStrip1";
            // 
            // toolStripMenuItemConnect
            // 
            this.toolStripMenuItemConnect.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemServerConnect});
            this.toolStripMenuItemConnect.Name = "toolStripMenuItemConnect";
            this.toolStripMenuItemConnect.Size = new System.Drawing.Size(64, 29);
            this.toolStripMenuItemConnect.Text = "接続";

            // 
            // toolStripMenuItemServerConnect
            // 
            this.toolStripMenuItemServerConnect.Name = "toolStripMenuItemServerConnect";
            this.toolStripMenuItemServerConnect.Size = new System.Drawing.Size(218, 34);
            this.toolStripMenuItemServerConnect.Text = "サーバーに接続";
            this.toolStripMenuItemServerConnect.Click += new System.EventHandler(this.toolStripMenuItemServerConnect_Click);
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 477);
            this.Controls.Add(this.richTextBoxLog);
            this.Controls.Add(this.buttonSendMessage);
            this.Controls.Add(this.textBoxSendMessage);
            this.Controls.Add(this.menuStripConnect);
            this.MainMenuStrip = this.menuStripConnect;
            this.Name = "ChatForm";
            this.Text = "ChatForm";
            this.Load += new System.EventHandler(this.ChatForm_Load);
            this.menuStripConnect.ResumeLayout(false);
            this.menuStripConnect.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxSendMessage;
        private System.Windows.Forms.Button buttonSendMessage;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private System.Windows.Forms.MenuStrip menuStripConnect;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemConnect;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemServerConnect;
    }
}