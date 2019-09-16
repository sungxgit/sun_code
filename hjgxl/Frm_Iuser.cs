using System;
using System.Collections;
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
    public partial class Frm_Iuser : Form
    {
        String str;
        int RoleID;
        ArrayList userid = new ArrayList();
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(Globals.connstr);
        public Frm_Iuser()
        {
            InitializeComponent();
        }
        public Frm_Iuser(int roleid)
        {
            InitializeComponent();
            this.RoleID = roleid;
        }

      

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                str = textBox1.Lines[textBox1.Text.Substring(0, textBox1.SelectionStart).Split('\n').Length - 1];//当前行
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("检查字段不能为空", "注意");
                return;
            }
            try
            {
                string sql = "SELECT UserID AS 编号, UserCode AS 工号, UserName AS 姓名, UserMemo AS 备注, Lock AS 锁定, LastLogin AS 上次登录时间 FROM tUser where UserID not in( select  UserID  from tUserRole where RoleID='"+RoleID+ "') and  UserName = '" + str+"'";
                SqlDataAdapter tUserTA1 = new SqlDataAdapter(sql,conn);
                try
                {
                    ds.Tables["tUser"].Clear();
                }
                catch { }

                tUserTA1.Fill(ds, "tUser");
            
               // tUserTA1.FillBy1(dm1.tUser, str);
                DataView dv = new DataView(ds.Tables["tUser"]);
                if (ds.Tables["tUser"].Rows.Count == 0)
                {
                    MessageBox.Show("此用户已在该组中存在", "失败");
                    return;
                }
                userid.Clear();
                userid.Add(dv[0][0]);
                textBox1.Text = str + "(" + dv[0][3].ToString() + ")";


            }
            catch (IndexOutOfRangeException)
            {

                MessageBox.Show("用户不存在", "警告");
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql = "select UID,UserID,RoleID from tUserRole where 1=2";
            SqlDataAdapter tUserRoleTA1 = new SqlDataAdapter(sql, conn);
            SqlCommandBuilder sb = new SqlCommandBuilder(tUserRoleTA1);
            tUserRoleTA1.Fill(ds, "tUserRole");
            for (int i = 0; i < userid.Count; i++)
            {

                DataRow dr = ds.Tables["tUserRole"].NewRow();
                dr["UserID"] = int.Parse(userid[i].ToString());
                dr["RoleID"] = RoleID;
                ds.Tables["tUserRole"].Rows.Add(dr);
            }
            try
            {
                tUserRoleTA1.Update(ds.Tables["tUserRole"]);
               // Globals.SysLogChangeEvent(this.Name, "UID", dm1.tUserRole);
                //dm1.tUserRole.AcceptChanges();
            }
            catch  { }

            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
