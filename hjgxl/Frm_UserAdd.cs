using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace hjgxl
{
    public partial class Frm_UserAdd : Form
    {
        int m_SortModeCol1 = 1;
       // String ColumnsText;
        int flag = -1;
        int roleid;
        Fuser qxgl;
        SqlConnection conn = new SqlConnection(Globals.connstr);
        SqlDataAdapter tRoleTA1;
        DataSet ds = new DataSet();
        public Frm_UserAdd()
        {
            InitializeComponent();
        }

        public Frm_UserAdd(String title, Fuser qxgl, int roleid)
        {
            InitializeComponent();
            this.qxgl = qxgl;
            this.roleid = roleid;
            this.Text = title + "组--添加用户";
            string sql = "SELECT RoleID AS 编号, RoleName AS 名称, RoleMemo AS 备注 FROM tRole  where roleid = '"+roleid+"'";
            tRoleTA1 = new SqlDataAdapter(sql, conn);
            tRoleTA1.Fill(ds, "tRole");
            textBox1.Text = ds.Tables["tRole"].Rows[0]["名称"].ToString() ;
            textBox2.Text = ds.Tables["tRole"].Rows[0]["备注"].ToString();
            // tRoleTA1.Fill(ds, "tRole")
            //tRoleTA1.FillBy1(dm1.tRole, roleid);
            this.viewchang();
        }
        public void viewchang()
        {

            listView1.Items.Clear();
            listView1.Columns.Clear();
            listView1.Columns.Add("工  号");
            listView1.Columns.Add("姓  名");
            string sql = "SELECT RoleName, RoleMemo, UID, UserID, UserCode, UserName FROM VUserRole WHERE(RoleID = '"+roleid+"')";
            SqlDataAdapter vUserRoleTA1 = new SqlDataAdapter(sql, conn);

            try
            {
                ds.Tables["vUserRole"].Clear();
            }
            catch { }
            vUserRoleTA1.Fill(ds, "vUserRole");

            // vUserRoleTA1.FillBy(dm1.VUserRole, roleid);
            for (int i = 0; i < ds.Tables["VUserRole"].Rows.Count; i++)
            {
                ListViewItem li = new ListViewItem();
                li.SubItems[0].Text = ds.Tables["VUserRole"].Rows[i][4].ToString();
                li.SubItems[0].Tag = ds.Tables["VUserRole"].Rows[i][2].ToString();                 //用来做删除的关键字
                li.SubItems.Add(ds.Tables["VUserRole"].Rows[i][5].ToString());
                listView1.Items.Add(li);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("您确定要执行删除操作吗？", "确认",
       MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string sql = "select UID,UserID,RoleID from tUserRole where uid = '" + listView1.SelectedItems[0].SubItems[0].Tag.ToString()+"'";
                SqlDataAdapter tUserRoleTA1 = new SqlDataAdapter(sql,conn);
                tUserRoleTA1.Fill(ds, "tUserRole");
                //tRoleModuleTA1.Fill(dm1.tRoleModule, int.Parse(listView1.SelectedItems[0].SubItems[0].Tag.ToString()));
                SqlCommandBuilder sb = new SqlCommandBuilder(tUserRoleTA1);
                ds.Tables["tUserRole"].Rows[0].Delete();
                tUserRoleTA1.Update(ds.Tables["tUserRole"]);
                //Globals.SysLogChangeEvent(this.Name, "UID", dm1.tRoleModule);
               // dm1.tRoleModule.AcceptChanges();
            }

            this.viewchang();
           // button5.Enabled = false;
        }

        private void listView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                button4.Enabled = true;
            }
            else
            {
                button4.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Frm_Iuser iuser = new Frm_Iuser(roleid);
            iuser.ShowDialog();
            this.viewchang();
        }
    }
}
