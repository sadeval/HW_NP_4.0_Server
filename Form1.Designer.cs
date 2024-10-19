namespace ComputerPartsServerGUI
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.startServerButton = new System.Windows.Forms.Button();
            this.stopServerButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxLog
            // 
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.ItemHeight = 16;
            this.listBoxLog.Location = new System.Drawing.Point(192, 24);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(577, 388);
            this.listBoxLog.TabIndex = 0;
            this.listBoxLog.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // startServerButton
            // 
            this.startServerButton.Location = new System.Drawing.Point(12, 84);
            this.startServerButton.Name = "startServerButton";
            this.startServerButton.Size = new System.Drawing.Size(120, 82);
            this.startServerButton.TabIndex = 1;
            this.startServerButton.Text = "Запустить сервер";
            this.startServerButton.UseVisualStyleBackColor = true;
            this.startServerButton.Click += new System.EventHandler(this.StartServerButton_Click);
            // 
            // stopServerButton
            // 
            this.stopServerButton.Location = new System.Drawing.Point(12, 194);
            this.stopServerButton.Name = "stopServerButton";
            this.stopServerButton.Size = new System.Drawing.Size(120, 89);
            this.stopServerButton.TabIndex = 2;
            this.stopServerButton.Text = "Остановить сервер";
            this.stopServerButton.UseVisualStyleBackColor = true;
            this.stopServerButton.Click += new System.EventHandler(this.StopServerButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.startServerButton);
            this.Controls.Add(this.stopServerButton);
            this.Controls.Add(this.listBoxLog);
            this.Name = "MainForm";
            this.Text = "Server";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }



        #endregion

        private System.Windows.Forms.ListBox listBoxLog;
        private System.Windows.Forms.Button startServerButton;
        private System.Windows.Forms.Button stopServerButton;
    }
}

