namespace Автоматизированная_СИ
{
    partial class Авторизация
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Авторизация));
            this.LabelNameHat = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxLoginAdmin = new System.Windows.Forms.TextBox();
            this.textBoxPassAdmin = new System.Windows.Forms.TextBox();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LabelNameHat
            // 
            this.LabelNameHat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.LabelNameHat.AutoSize = true;
            this.LabelNameHat.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.LabelNameHat.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelNameHat.ForeColor = System.Drawing.Color.White;
            this.LabelNameHat.Location = new System.Drawing.Point(23, 28);
            this.LabelNameHat.Name = "LabelNameHat";
            this.LabelNameHat.Size = new System.Drawing.Size(392, 29);
            this.LabelNameHat.TabIndex = 0;
            this.LabelNameHat.Text = "СИСТЕМА ИНВЕНТАРИЗАЦИИ";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(122, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "АДМИНИСТРАТОР";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // textBoxLoginAdmin
            // 
            this.textBoxLoginAdmin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxLoginAdmin.Location = new System.Drawing.Point(127, 156);
            this.textBoxLoginAdmin.Multiline = true;
            this.textBoxLoginAdmin.Name = "textBoxLoginAdmin";
            this.textBoxLoginAdmin.Size = new System.Drawing.Size(196, 22);
            this.textBoxLoginAdmin.TabIndex = 3;
            this.textBoxLoginAdmin.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBoxPassAdmin
            // 
            this.textBoxPassAdmin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxPassAdmin.Location = new System.Drawing.Point(127, 212);
            this.textBoxPassAdmin.Multiline = true;
            this.textBoxPassAdmin.Name = "textBoxPassAdmin";
            this.textBoxPassAdmin.Size = new System.Drawing.Size(196, 22);
            this.textBoxPassAdmin.TabIndex = 4;
            // 
            // buttonOpen
            // 
            this.buttonOpen.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonOpen.BackColor = System.Drawing.Color.White;
            this.buttonOpen.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.buttonOpen.FlatAppearance.BorderSize = 0;
            this.buttonOpen.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Window;
            this.buttonOpen.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Window;
            this.buttonOpen.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonOpen.Location = new System.Drawing.Point(127, 285);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(196, 44);
            this.buttonOpen.TabIndex = 12;
            this.buttonOpen.Text = "ВХОД";
            this.buttonOpen.UseVisualStyleBackColor = false;
            this.buttonOpen.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonClose.BackColor = System.Drawing.Color.White;
            this.buttonClose.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.buttonClose.FlatAppearance.BorderSize = 0;
            this.buttonClose.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Window;
            this.buttonClose.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Window;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonClose.Location = new System.Drawing.Point(127, 350);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(196, 47);
            this.buttonClose.TabIndex = 13;
            this.buttonClose.Text = "ВЫХОД";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.button3_Click);
            // 
            // Авторизация
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(443, 520);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.textBoxPassAdmin);
            this.Controls.Add(this.textBoxLoginAdmin);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LabelNameHat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Авторизация";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Авторизация";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelNameHat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxLoginAdmin;
        private System.Windows.Forms.TextBox textBoxPassAdmin;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.Button buttonClose;
    }
}

