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
 
    public partial class Frm_Euser : Form
    {
        int UserID;
        String ucode;
        SqlDataAdapter tUserTA1, tUser1TA1, vUserRoleTA1, vUserModuleTA1, tUserRoleTA1, tUserModuleTA1;
        DataSet dm = new DataSet();
        SqlConnection conn = new SqlConnection(Globals.connstr);
        SqlCommandBuilder sb;


        public Frm_Euser()
        {
            InitializeComponent();
        }
        public Frm_Euser(String title, int UserID)
        {
            InitializeComponent();
            this.Text = title + " 属性";
            this.UserID = UserID;
            string sql = "SELECT LastLogin AS 上次登录时间, Lock AS 锁定, UserCode AS 工号, UserID AS 编号, UserMemo AS 备注, UserName AS 姓名 FROM tUser WHERE (UserID = '"+UserID+"')";
            tUserTA1 = new SqlDataAdapter(sql, conn);
            try
            {
                dm.Tables["tUser"].Clear();
            }
            catch { }
           sb = new SqlCommandBuilder(tUserTA1);
            tUserTA1.Fill(dm, "tUser");
            tUserBindingSource.DataSource = dm;



            // tUserTA1.FillBy(dM.tUser, UserID);

            //this.qxgl = qxgl;
        }

        private void Frm_Euser_Load(object sender, EventArgs e)
        {
            ucode = textBox1.Text;
        }

        private void listView2_MouseUp(object sender, MouseEventArgs e)
        {
            if (listView2.SelectedItems.Count != 0)
            {
                button5.Enabled = true;
            }
            else
            {
                button5.Enabled = false;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("工号和用户名不能为空", "警告");
                return;
            }
            if (ucode != textBox1.Text)
            {
                string sql1 = "SELECT COUNT(*) hj FROM tUser where UserCode='" + textBox1.Text + "'";
                tUser1TA1 = new SqlDataAdapter(sql1, conn);
                try
                {
                    dm.Tables["tUser1"].Clear();
                }
                catch { }
                if (dm.Tables["tUser1"].Rows[0]["hj"].ToString() != "0")
                {
                    MessageBox.Show("此用户已经存在", "失败");
                    return;
                }
            }
            button3.Enabled = false;
            try
            {
                dm.Tables["tUser"].Rows[0].EndEdit();
                // dM.tUser.Rows[0].EndEdit();
               

                tUserTA1.Update(dm.Tables["tUser"]);
                //dm.Tables["tUser"].AcceptChanges();
                //Globals.SysLogChangeEvent(this.Name, "编号", dM.tUser);//写系统日志
                //dM.tUser.AcceptChanges();
                // qxgl.ColumnsChang("tuser");
            }
            catch (ArgumentNullException) { }

            this.Close();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name == "tabPage2")
            {
                this.viewchang();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("您确定要执行删除操作吗？", "确认",
          MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {


                if (listView2.SelectedItems[0].SubItems[1].Tag.ToString() == "role")
                {
                    string sql = "SELECT UID, UserID, RoleID FROM tUserRole WHERE(UID = '" + listView2.SelectedItems[0].SubItems[0].Tag.ToString() + "')";
                    try
                    {
                        dm.Tables["tUserRole"].Clear();
                    }
                    catch { }
                    tUserRoleTA1 = new SqlDataAdapter(sql, conn);
                    tUserRoleTA1.Fill(dm, "tUserRole");
                    SqlCommandBuilder sb1 = new SqlCommandBuilder(tUserRoleTA1);
                    //tUserRoleTA1.FillBy(dM.tUserRole, int.Parse(listView2.SelectedItems[0].SubItems[0].Tag.ToString()));
                    dm.Tables["tUserRole"].Rows[0].Delete();
                    tUserRoleTA1.Update(dm.Tables["tUserRole"]);
                    // Globals.SysLogChangeEvent(this.Name, "UID", dM.tUserRole);
                    // dM.tUserRole.AcceptChanges();
                }
                else
                {
                    string sql = "SELECT ModuleID, UID, UserID FROM tUserModule WHERE (UID = '" + listView2.SelectedItems[0].SubItems[0].Tag.ToString() + "')";
                    try
                    {
                        dm.Tables["tUserModule"].Clear();
                    }
                    catch { }
                    tUserModuleTA1 = new SqlDataAdapter(sql, conn);
                    tUserModuleTA1.Fill(dm, "tUserModule");
                    SqlCommandBuilder sb1 = new SqlCommandBuilder(tUserModuleTA1);

                    // tUserModuleTA1.FillBy(dM.tUserModule, int.Parse(listView2.SelectedItems[0].SubItems[0].Tag.ToString()));
                    dm.Tables["tUserModule"].Rows[0].Delete();
                    tUserModuleTA1.Update(dm.Tables["tUserModule"]);
                    //  Globals.SysLogChangeEvent(this.Name, "UID", dM.tUserModule);
                    // dM.tUserModule.AcceptChanges();
                }

                this.viewchang();
                button5.Enabled = false;
            }
            else { button5.Enabled = false; }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Frm_RoleAdd RoleAdd = new Frm_RoleAdd(UserID);
            RoleAdd.TopMost = true;
            RoleAdd.ShowDialog();
            this.viewchang();
        }
        public void viewchang()
        {
            listView2.Items.Clear();
            listView2.Columns.Clear();
            listView2.Columns.Add("                ");
            listView2.Columns.Add("                ");
            string sql = "SELECT RoleName, RoleMemo, UID, UserID, UserCode, UserName FROM VUserRole WHERE(UserID = '"+UserID+"')";
            try
            {
                dm.Tables["vUserRole"].Clear();
            }
            catch { }
            vUserRoleTA1 = new SqlDataAdapter(sql, conn);
            vUserRoleTA1.Fill(dm, "vUserRole");
            // vUserRoleTA1.Fill(dM.VUserRole, UserID);
            sql = "SELECT ModuleName, ModuleMemo, AppSystem,ModulePage, UID, UserID, UserCode, UserName FROM VUserModule WHERE(UserID ='" + UserID+"')";
            try
            {
                dm.Tables["vUserModule"].Clear();
            }
            catch { }
            vUserModuleTA1 = new SqlDataAdapter(sql, conn);
            vUserModuleTA1.Fill(dm, "vUserModule");
            DataView dv = new DataView(dm.Tables["vUserRole"]);
            for (int i = 0; i < dv.Count; i++)
            {
                ListViewItem li = new ListViewItem();
                li.SubItems.Clear();
                li.SubItems[0].Text = dv[i][0].ToString();
                li.SubItems[0].Tag = dv[i][2].ToString();
                li.SubItems.Add(dv[i][1].ToString());
                li.SubItems[1].Tag = "role";
                listView2.Items.Add(li);
            }
            dv = new DataView(dm.Tables["vUserModule"]);
            for (int i = 0; i < dv.Count; i++)
            {
                ListViewItem li = new ListViewItem();
                li.SubItems.Clear();
                li.SubItems[0].Text = dv[i][3].ToString();
                li.SubItems[0].Tag = dv[i][4].ToString();
                li.SubItems.Add(dv[i][1].ToString());
                li.SubItems[1].Tag = "Module";
                listView2.Items.Add(li);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("工号和用户名不能为空", "警告");
                return;
            }
            if (ucode != textBox1.Text)
            {
                string sql1 = "SELECT COUNT(*) hj FROM tUser where UserCode='"+textBox1.Text+"'";
                tUser1TA1 = new SqlDataAdapter(sql1, conn);
                try
                {
                    dm.Tables["tUser1"].Clear();
                }
                catch { }
                if (dm.Tables["tUser1"].Rows[0]["hj"].ToString()!= "0")
                {
                    MessageBox.Show("此用户已经存在", "失败");
                    return;
                }
            }
            button3.Enabled = false;
            try
            {
                dm.Tables["tUser"].Rows[0].EndEdit();
               // dM.tUser.Rows[0].EndEdit();
                tUserTA1.Update(dm.Tables["tUser"]);
              //  Globals.SysLogChangeEvent(this.Name, "编号", dM.tUser);//写系统日志
               // dM.tUser.AcceptChanges();
                //qxgl.ColumnsChang("tuser");
            }
            catch (ArgumentNullException) { }
        }
    }
}
