namespace hjgxl
{
    partial class Fxtsz
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
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.mm = new System.Windows.Forms.TextBox();
            this.yhm = new System.Windows.Forms.TextBox();
            this.sjk = new System.Windows.Forms.TextBox();
            this.fwq = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ck = new System.Windows.Forms.ComboBox();
            this.btl = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(243, 315);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(100, 28);
            this.button3.TabIndex = 26;
            this.button3.Text = "测试";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(44, 315);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 28);
            this.button2.TabIndex = 25;
            this.button2.Text = "保存";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // mm
            // 
            this.mm.Location = new System.Drawing.Point(135, 197);
            this.mm.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.mm.Name = "mm";
            this.mm.PasswordChar = '*';
            this.mm.Size = new System.Drawing.Size(342, 25);
            this.mm.TabIndex = 24;
            // 
            // yhm
            // 
            this.yhm.Location = new System.Drawing.Point(135, 139);
            this.yhm.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.yhm.Name = "yhm";
            this.yhm.Size = new System.Drawing.Size(342, 25);
            this.yhm.TabIndex = 23;
            // 
            // sjk
            // 
            this.sjk.Location = new System.Drawing.Point(135, 79);
            this.sjk.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.sjk.Name = "sjk";
            this.sjk.Size = new System.Drawing.Size(342, 25);
            this.sjk.TabIndex = 22;
            // 
            // fwq
            // 
            this.fwq.Location = new System.Drawing.Point(135, 20);
            this.fwq.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.fwq.Name = "fwq";
            this.fwq.Size = new System.Drawing.Size(342, 25);
            this.fwq.TabIndex = 21;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(40, 207);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 15);
            this.label6.TabIndex = 18;
            this.label6.Text = "密码";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(40, 149);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 15);
            this.label5.TabIndex = 17;
            this.label5.Text = "用户名";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(40, 89);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 15);
            this.label7.TabIndex = 20;
            this.label7.Text = "数据库";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 30);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 19;
            this.label4.Text = "服务器";
            // 
            // ck
            // 
            this.ck.FormattingEnabled = true;
            this.ck.Location = new System.Drawing.Point(601, 26);
            this.ck.Name = "ck";
            this.ck.Size = new System.Drawing.Size(121, 23);
            this.ck.TabIndex = 27;
            // 
            // btl
            // 
            this.btl.FormattingEnabled = true;
            this.btl.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600"});
            this.btl.Location = new System.Drawing.Point(601, 85);
            this.btl.Name = "btl";
            this.btl.Size = new System.Drawing.Size(121, 23);
            this.btl.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(519, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 29;
            this.label1.Text = "串口";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(519, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 30;
            this.label2.Text = "波特率";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(593, 139);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(129, 39);
            this.button1.TabIndex = 31;
            this.button1.Text = "保存串口设置";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(519, 207);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 32;
            this.label3.Text = "条码打印机";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(620, 198);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(341, 23);
            this.comboBox1.TabIndex = 33;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(610, 258);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(162, 35);
            this.button4.TabIndex = 34;
            this.button4.Text = "保存打印机设置";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Fxtsz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 409);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btl);
            this.Controls.Add(this.ck);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.mm);
            this.Controls.Add(this.yhm);
            this.Controls.Add(this.sjk);
            this.Controls.Add(this.fwq);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Fxtsz";
            this.Text = "系统设置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox mm;
        private System.Windows.Forms.TextBox yhm;
        private System.Windows.Forms.TextBox sjk;
        private System.Windows.Forms.TextBox fwq;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox ck;
        private System.Windows.Forms.ComboBox btl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button4;
    }
}