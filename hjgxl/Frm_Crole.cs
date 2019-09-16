using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace hjgxl
{
    public partial class Frm_Crole : Form
    {
        int m_SortModeCol1 = 1;
        String ColumnsText;
        int flag = -1;
        String roleid;
        Fuser qxgl;
        SqlConnection conn = new SqlConnection(Globals.connstr);
        DataSet dm1 = new DataSet();
        SqlDataAdapter vRoleModuleTA1, tRoleTA1, tRoleModuleTA1;
        int lx;
        String rname;
        SqlCommandBuilder sb;

        public Frm_Crole()
        {
            InitializeComponent();
        }
        public Frm_Crole(Fuser qxgl,int lx)
        {
            InitializeComponent();
            this.qxgl = qxgl;
            this.lx = lx;
        }
        public Frm_Crole(String title, int roleid,Fuser qxgl,int lx)
        {
            InitializeComponent();
            this.lx = lx;
            this.qxgl = qxgl;
            this.Text = title + "组--权限";
           // RoleName.Text = title;
          
           
            this.roleid = roleid.ToString();
            //button3.Visible = false;
            button1.Enabled = true;
            //tRoleTA1.FillBy1(dm1.tRole, roleid);
           
            string sql = "SELECT RoleID AS 编号, RoleName AS 名称, RoleMemo AS 备注 FROM tRole  where roleid ='"+roleid+"'  ";
            tRoleTA1 = new SqlDataAdapter(sql, conn);
            try
            {
                dm1.Tables["tRole"].Clear();
            }
            catch { }
            tRoleTA1.Fill(dm1, "tRole");

          
            sb = new SqlCommandBuilder(tRoleTA1);
          
            RoleName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.dm1, "tRole.名称", true));
            RoleMemo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.dm1, "tRole.备注", true));

                // RoleName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "名称", true));
                // RoleMemo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "备注", true));
                // RoleMemo.Text = dm1.Tables["tRole"].Rows[0]["备注"].ToString();

            this.viewchang();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Frm_ModuleAdd Module = new Frm_ModuleAdd(int.Parse(roleid));
            Module.ShowDialog();
            this.viewchang();

        }
        public void viewchang()
        {
            listView1.Items.Clear();
            listView1.Columns.Clear();
            listView1.Columns.Add("模块项");
            listView1.Columns.Add("模块名称");
            listView1.Columns.Add("备     注");
            string sql = "SELECT UID, AppSystem, ModuleName, ModuleMemo, RoleName, RoleMemo,ModulePage FROM VRoleModule WHERE(RoleID ='" + int.Parse(roleid)+"') OR (ModuleID = 0)";
            vRoleModuleTA1 = new SqlDataAdapter(sql, conn);
            try
            {
                dm1.Tables["VRoleModule"].Clear();
            }
            catch { }
            vRoleModuleTA1.Fill(dm1, "VRoleModule");
            // vRoleModuleTA1.Fill(dm1.VRoleModule, int.Parse(roleid), 0);

            DataView dv = new DataView(dm1.Tables["VRoleModule"]);
            for (int i = 0; i < dv.Count; i++)
            {
                ListViewItem li = new ListViewItem();
                li.SubItems[0].Text = dv[i][6].ToString();
                li.SubItems[0].Tag = dv[i][0].ToString();                 //用来做删除的关键字
                li.SubItems.Add(dv[i][2].ToString());
                li.SubItems.Add(dv[i][3].ToString());
                listView1.Items.Add(li);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                button2.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
            }
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (flag != e.Column)
            {//判断是不是同一个标题
                if (flag != -1)
                {//判断是不是第一次
                    listView1.Columns[flag].Text = ColumnsText;
                }
                flag = e.Column;
            }
            else
            {
                listView1.Columns[flag].Text = ColumnsText;
            }
            ListViewSorter sorter = new ListViewSorter(e.Column, m_SortModeCol1);
            this.listView1.ListViewItemSorter = sorter;
            ColumnsText = listView1.Columns[e.Column].Text;
            if (m_SortModeCol1 != 1)
            {
                listView1.Columns[e.Column].Text = listView1.Columns[e.Column].Text + "↓";
                listView1.Columns[e.Column].Width = listView1.Columns[e.Column].Text.Length + 100;
            }
            else
            {
                listView1.Columns[e.Column].Text = listView1.Columns[e.Column].Text + "↑";
                listView1.Columns[e.Column].Width = listView1.Columns[e.Column].Text.Length + 100;
            }
            this.m_SortModeCol1 = -m_SortModeCol1;
        }

        private void Frm_Crole_Load(object sender, EventArgs e)
        {
            rname = RoleName.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (RoleName.Text == "")
            {
                MessageBox.Show("组名不能为空", "警告");
                return;
            }
            string sql = "SELECT COUNT(*) tj   FROM tRole WHERE(RoleName = '" + RoleName.Text + "')";
            try
            {
                dm1.Tables["tRole1"].Clear();
            }
            catch { }
            SqlDataAdapter t1 = new SqlDataAdapter(sql, conn);
            t1 = new SqlDataAdapter(sql, conn);
            t1.Fill(dm1, "tRole1");

            if (dm1.Tables["tRole1"].Rows[0]["tj"].ToString() != "0")
            {
                MessageBox.Show("该组已存在请更换名称", "失败");
                return;
            }
            if (lx == 0)
            {
               
                string sql1 = "SELECT RoleName,RoleMemo  FROM tRole where 1=2 ";
                try
                {
                    dm1.Tables["tRole"].Clear();
                }
                catch { }
                tRoleTA1 = new SqlDataAdapter(sql1, conn);
                tRoleTA1.Fill(dm1, "tRole");
                DataRow dr = dm1.Tables["tRole"].NewRow();

                dr["RoleName"] = RoleName.Text;
                dr["RoleMemo"] = RoleMemo.Text;
                //  dm1.tUser1.Rows.Add(dr);
                dm1.Tables["tRole"].Rows.Add(dr);
                SqlCommandBuilder sb1 = new SqlCommandBuilder(tRoleTA1);
                tRoleTA1.Update(dm1.Tables["tRole"]);

                string sql2 = "SELECT RoleID AS 编号, RoleName AS 名称, RoleMemo AS 备注 FROM tRole where RoleName ='" + RoleName.Text + "'";
                try
                {
                    dm1.Tables["tRole"].Clear();
                }
                catch { }
                tRoleTA1 = new SqlDataAdapter(sql2, conn);
                tRoleTA1.Fill(dm1, "tRole");
                roleid = dm1.Tables["tRole"].Rows[0]["编号"].ToString();
                qxgl.ColumnsChang("trole");
                button1.Enabled = true;
                button3.Enabled = false;
            }
            else
            {
             //修改                  
               

                try
                {
                    dm1.Tables["tRole"].Rows[0].EndEdit();
                    tRoleTA1.Update(dm1.Tables["tRole"]);
                   // Globals.SysLogChangeEvent(this.Name, "编号", dm1.tRole);
                    //dm1.tRole.AcceptChanges();

                }
                catch (ArgumentNullException) { }
                this.Close();

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("您确定要执行删除操作吗？", "确认",
        MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string sql = "SELECT UID , RoleID , ModuleID FROM tRoleModule where uid = '" + int.Parse(listView1.SelectedItems[0].SubItems[0].Tag.ToString()) + "'";
                tRoleModuleTA1 = new SqlDataAdapter(sql, conn);
                try
                {
                    dm1.Tables["tRoleModule"].Clear();
                }
                catch { }
                tRoleModuleTA1.Fill(dm1, "tRoleModule");
                dm1.Tables["tRoleModule"].Rows[0].Delete();
                SqlCommandBuilder sb = new SqlCommandBuilder(tRoleModuleTA1);
                    tRoleModuleTA1.Update(dm1.Tables["tRoleModule"]);              
             
            }

            this.viewchang();
            button2.Enabled = false;
        }
    }
}
