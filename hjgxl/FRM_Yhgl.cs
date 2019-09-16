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
using System.Web.Security;

namespace hjgxl
{
    public partial class FRM_Yhgl : Form
    {
        SqlConnection conn = new SqlConnection(Globals.connstr);
        DataSet ds = new DataSet();
        SqlCommandBuilder sb;
        SqlDataAdapter tuser;
        public FRM_Yhgl()
        {
            InitializeComponent();
            string sql = "SELECT LastLogin, Lock, UserCode, UserID, UserMemo, UserName, UserPWD FROM tUser WHERE (userid = '" + Globals.userid + "')";
            tuser = new SqlDataAdapter(sql, conn);
            tuser.Fill(ds, "tuser");
            sb = new SqlCommandBuilder(tuser);
            pwd.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.ds, "tuser.UserPWD", true));
            pwd.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pwd.Text != pwd1.Text | pwd.Text=="")
            {
                MessageBox.Show("密码未被确认。密码不能为空，请确保密码和确认密码完全相符。", "警告");
                pwd.Clear();
                pwd1.Clear();
                return;
            }
            try
            {
                ds.Tables["tuser"].Rows[0]["UserPWD"] = FormsAuthentication.HashPasswordForStoringInConfigFile(pwd.Text, "MD5");
                ds.Tables["tuser"].Rows[0].EndEdit();
                tuser.Update(ds.Tables["tuser"]);
                // Globals.SysLogChangeEvent(this.Name, "编号", dm1.tRole);
                //dm1.tRole.AcceptChanges();

            }
            catch (ArgumentNullException) { }
            this.Close();

            //  RoleMemo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.dm1, "tRole.备注", true));

            // tUser1TA1.FillBy1(dm1.tUser1, Globals.user);
            //  dm1.tUser1[0][3] = FormsAuthentication.HashPasswordForStoringInConfigFile(pwd.Text, "MD5");
            //  dm1.tUser1.Rows[0].EndEdit();
            // tUser1TA1.Update((DM.tUser1DataTable)dm1.tUser1.GetChanges());
            //  Globals.SysLogChangeEvent(this.Name, "UserID", dm1.tUser1);
            // dm1.tUser1.AcceptChanges();
            this.Close();
        }
    }
}
