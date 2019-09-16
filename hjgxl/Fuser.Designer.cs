namespace hjgxl
{
    partial class Fuser
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("用户");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("组");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("模块");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("用户和组", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            this.panel1 = new System.Windows.Forms.Panel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.EMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.szmm = new System.Windows.Forms.ToolStripMenuItem();
            this.tjmk = new System.Windows.Forms.ToolStripMenuItem();
            this.tjz = new System.Windows.Forms.ToolStripMenuItem();
            this.line1 = new System.Windows.Forms.ToolStripSeparator();
            this.create = new System.Windows.Forms.ToolStripMenuItem();
            this.del = new System.Windows.Forms.ToolStripMenuItem();
            this.refresh = new System.Windows.Forms.ToolStripMenuItem();
            this.line2 = new System.Windows.Forms.ToolStripSeparator();
            this.attribute = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.EMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listView1);
            this.panel1.Controls.Add(this.treeView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(819, 532);
            this.panel1.TabIndex = 2;
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(267, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(552, 532);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick_1);
            this.listView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseUp);
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "yh";
            treeNode1.Tag = "tuser";
            treeNode1.Text = "用户";
            treeNode2.Name = "group";
            treeNode2.Tag = "trole";
            treeNode2.Text = "组";
            treeNode3.Name = "节点3";
            treeNode3.Tag = "tmodule";
            treeNode3.Text = "模块";
            treeNode4.Name = "节点0";
            treeNode4.Text = "用户和组";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4});
            this.treeView1.Size = new System.Drawing.Size(267, 532);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // EMenu
            // 
            this.EMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.EMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.szmm,
            this.tjmk,
            this.tjz,
            this.line1,
            this.create,
            this.del,
            this.refresh,
            this.line2,
            this.attribute});
            this.EMenu.Name = "contextMenuStrip1";
            this.EMenu.Size = new System.Drawing.Size(211, 212);
            // 
            // szmm
            // 
            this.szmm.Name = "szmm";
            this.szmm.Size = new System.Drawing.Size(210, 24);
            this.szmm.Text = "设置密码";
            this.szmm.Click += new System.EventHandler(this.szmm_Click);
            // 
            // tjmk
            // 
            this.tjmk.Name = "tjmk";
            this.tjmk.Size = new System.Drawing.Size(210, 24);
            this.tjmk.Text = "添加到模块";
            this.tjmk.Click += new System.EventHandler(this.tjmk_Click);
            // 
            // tjz
            // 
            this.tjz.Name = "tjz";
            this.tjz.Size = new System.Drawing.Size(210, 24);
            this.tjz.Text = "添加到组";
            this.tjz.Click += new System.EventHandler(this.tjz_Click);
            // 
            // line1
            // 
            this.line1.Name = "line1";
            this.line1.Size = new System.Drawing.Size(207, 6);
            // 
            // create
            // 
            this.create.Name = "create";
            this.create.Size = new System.Drawing.Size(210, 24);
            this.create.Text = "创建";
            this.create.Click += new System.EventHandler(this.create_Click);
            // 
            // del
            // 
            this.del.Name = "del";
            this.del.Size = new System.Drawing.Size(210, 24);
            this.del.Text = "删除";
            this.del.Click += new System.EventHandler(this.del_Click);
            // 
            // refresh
            // 
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(210, 24);
            this.refresh.Text = "刷新";
            this.refresh.Click += new System.EventHandler(this.refresh_Click);
            // 
            // line2
            // 
            this.line2.Name = "line2";
            this.line2.Size = new System.Drawing.Size(207, 6);
            // 
            // attribute
            // 
            this.attribute.Name = "attribute";
            this.attribute.Size = new System.Drawing.Size(210, 24);
            this.attribute.Text = "属性";
            this.attribute.Click += new System.EventHandler(this.attribute_Click);
            // 
            // Fuser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 532);
            this.Controls.Add(this.panel1);
            this.Name = "Fuser";
            this.Text = "用户管理";
            this.panel1.ResumeLayout(false);
            this.EMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ContextMenuStrip EMenu;
        private System.Windows.Forms.ToolStripMenuItem szmm;
        private System.Windows.Forms.ToolStripMenuItem tjmk;
        private System.Windows.Forms.ToolStripMenuItem tjz;
        private System.Windows.Forms.ToolStripSeparator line1;
        private System.Windows.Forms.ToolStripMenuItem create;
        private System.Windows.Forms.ToolStripMenuItem del;
        private System.Windows.Forms.ToolStripMenuItem refresh;
        private System.Windows.Forms.ToolStripSeparator line2;
        private System.Windows.Forms.ToolStripMenuItem attribute;
        private System.Windows.Forms.ListView listView1;
    }
}