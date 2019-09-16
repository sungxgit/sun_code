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
    public partial class Frm_Epwd : Form
    {
        String key;
        SqlConnection conn = new SqlConnection(Globals.connstr);
        SqlDataAdapter tUser1TA;
        DataSet ds = new DataSet();
        SqlCommandBuilder sb;
        public Frm_Epwd()
        {
            InitializeComponent();
        }
        public Frm_Epwd(String key)
        {
            InitializeComponent();
            this.key = key;
            string sql = "SELECT LastLogin, Lock, UserCode, UserID, UserMemo, UserName, UserPWD FROM tUser WHERE (UserID ='"+key+"')";
            tUser1TA = new SqlDataAdapter(sql, conn);
            sb = new SqlCommandBuilder(tUser1TA);
            tUser1TA.Fill(ds, "tUser");
       
            tUser1BindingSource.DataSource = ds;
           // tUser1TA.FillBy(dM.tUser1, int.Parse(key));
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pwd.Text != pwd1.Text)
            {
                MessageBox.Show("密码未被确认。请确保密码和确认密码完全相符。", "警告");
                pwd.Clear();
                pwd1.Clear();
                return;
            }
            ds.Tables["tUser"].Rows[0]["UserPWD"] = FormsAuthentication.HashPasswordForStoringInConfigFile(pwd.Text, "MD5");
            ds.Tables["tUser"].Rows[0].EndEdit();
            tUser1TA.Update(ds.Tables["tUser"]);
          //  Globals.SysLogChangeEvent(this.Name, "UserID", dM.tUser1);
           // dM.tUser1.AcceptChanges();
            this.Close();
        }
    }
}
