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
    public partial class Frm_Dl : Form
    {
        public int flag = 0;
        SqlConnection conn = new SqlConnection(Globals.connstr);
        DataSet ds = new DataSet();
        //  public DM.VUM1DataTable temptb;
        public DataTable temptb;

        

        public Frm_Dl()
        {
            InitializeComponent();
            try
            {
                user.Text = Encrypt.Encrypt.getConfig("config.xml", "user");
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            enter();
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //  Application.Exit();
            this.DialogResult=DialogResult.Cancel;
        }

        private void enter()
        {
            //此处要在项目中添加引用否则不好用ConfigurationManager
            //Globals.saveConfig("13645");
            // string connstr = ConfigurationManager.ConnectionStrings["SAP_VC.Properties.Settings.DDITTESTConnectionString"].ToString();

            // Globals.user = user.Text;
            // Globals.pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(pwd.Text, "MD5");
            //Globals.sysname = "vc";
            //tUser1TA1.FillBy2(dm1.tUser1, Globals.user);
            string sql = "SELECT LastLogin, Lock, UserCode, UserID, UserMemo, UserName, UserPWD FROM tUser WHERE (UserName = '" + user.Text+"')";
            SqlDataAdapter tUser1TA1 = new SqlDataAdapter(sql,conn);
            tUser1TA1.Fill(ds, "tUser");
           // tUser1TA1.FillBy1(dm1.tUser1, Globals.user);
            if (ds.Tables["tUser"].Rows.Count < 1)
            {
                MessageBox.Show("没有此用户", "错误");
                return;
            }
            if (ds.Tables["tUser"].Rows[0]["Lock"].ToString() == "True")
            {
                MessageBox.Show("此用户已被锁定，请联系系统管理员解锁", "注意");
                return;
            }

            if (ds.Tables["tUser"].Rows[0]["UserPWD"].ToString() != FormsAuthentication.HashPasswordForStoringInConfigFile(pwd.Text, "MD5"))
            {
                MessageBox.Show("密码不正确或没有权限操作此系统！", "错误");
                return;
            }
            Globals.userid = ds.Tables["tUser"].Rows[0]["UserID"].ToString();
            Globals.usernm = ds.Tables["tUser"].Rows[0]["UserName"].ToString();
            //temptb = new DataTable();
            sql = "select ModuleID,AppSystem,ModuleName,ModulePage from  tModule  where ModuleID in (select ModuleID from  tUserModule where UserID='"+ ds.Tables["tUser"].Rows[0]["UserID"] + "') or ModuleID in (select ModuleID from  tRoleModule where RoleID in(select RoleID from tUserRole where UserID='"+ds.Tables["tUser"].Rows[0]["UserID"] +"'))";
            SqlDataAdapter vuM1TA1 = new SqlDataAdapter(sql, conn);
            vuM1TA1.Fill(ds, "tModule");
           // vuM1TA1.Fill(dm1.VUM1, Globals.user, Globals.pwd, Globals.sysname);
            //创建一个临时表保存用户所有权限
            
            temptb = ds.Tables["tModule"];
           // vumta1.Fill(dm1.VUM, Globals.user, Globals.pwd, Globals.sysname);
            //解除注释启用
            //解除注释启用

            //if (ds.Tables["tUser"].Rows[0]["UserPWD"].ToString()!= FormsAuthentication.HashPasswordForStoringInConfigFile(pwd.Text, "MD5"))
            //{
            //    MessageBox.Show("密码不正确或没有权限操作此系统！", "错误");
            //    return;
            //}
           // dm1.tUser1[0]["LastLogin"] = DateTime.Now;
           // dm1.tUser1.Rows[0].EndEdit();
           // tUser1TA1.Update((DM.tUser1DataTable)dm1.tUser1.GetChanges());
           // dm1.tUser1.AcceptChanges();

            //筛选过滤相同的权限
            //for (int i = 0; i < dm1.VUM1.Count; i++)
            //{
            //    for (int j = 0; j < dm1.VUM.Count; j++)
            //    {
            //        if (dm1.VUM[j]["UserCode"].ToString() + dm1.VUM[j]["AppSystem"].ToString() + dm1.VUM[j]["ModuleName"].ToString() == dm1.VUM1[i]["UserCode"].ToString() + dm1.VUM1[i]["AppSystem"].ToString() + dm1.VUM1[i]["ModuleName"].ToString())
            //        {
            //            dm1.VUM[j].Delete();
            //            break;
            //        }
            //    }
            //}
            //将权限加到临时表中
            //DataView dv = new DataView(dm1.VUM);
            //过滤删除的部分（重合的权限）
           // dv.RowStateFilter = DataViewRowState.Unchanged;
            //合并权限到临时表中
            //for (int i = 0; i < dv.Count; i++)
            //{
            //    DataRow dr = temptb.NewRow();
            //    dr["UserCode"] = dv[i]["UserCode"].ToString();
            //    dr["AppSystem"] = dv[i]["AppSystem"].ToString();
            //    dr["ModuleName"] = dv[i]["ModuleName"].ToString();
            //    temptb.Rows.Add(dr);
            //}

            //   dm1.VUM1.Rows.Add(dm1.VUM.Rows[0]);
           // Globals.GetComputerInfo();
           // Globals.syslog(this.Name, "登陆系统");
           // IniFile ini = new IniFile(Application.StartupPath + "\\sapconfig.ini");
           // Globals._connectionString = "ASHOST=" + ini.IniReadvalue("sap", "ASHOST") + ";" + "SYSNR=" + ini.IniReadvalue("sap", "SYSNR") + ";" + "CLIENT=" + ini.IniReadvalue("sap", "CLIENT") + ";" + "USER=" + ini.IniReadvalue("sap", "USER") + ";" + "PASSWD=" + Globals.DES_Decrypt(ini.IniReadvalue("sap", "PASSWD"));
            flag = 1;
            Encrypt.Encrypt.saveConfig(user.Text, "config.xml", "user");
            this.Close();
        }

        private void pwd_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                enter();
            }
        }
    }
}
