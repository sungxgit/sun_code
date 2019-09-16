using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace hjgxl
{
    public partial class Fuser : Form
    {
        int m_SortModeCol1 = 1;  //用于listView排序控制升序和降序
        String ColumnsText;     //控制被排序的字段
        int flag = -1;           //控制排序字段的序号，用来判断是不是同一个字段
        String tbinfo;        //控制listView显示的表
        SqlDataAdapter tUserTA1, tRoleTA1, tModuleTA1;
        SqlConnection conn;
        DataSet ds = new DataSet();
        Form1 fmain;
        public Fuser(Form1 fmain)
        {
            InitializeComponent();
            conn = new SqlConnection(Globals.connstr);
            this.fmain = fmain;
           // string sql1 = "SELECT  FBarcode, FWork, FQty, FMcd, FMkd, FMhd, FJs, FBc, FJyy, FBz, FUnitID, FICMOID, FItemID, FJlr, FJlrq,FRkd, FSfrk, FSfyw, FICMOBillNo ,FBatchNo FROM  Tgx where 1=2 ";
           //  tUserTA1 = new SqlDataAdapter(sql1, conn);

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            flag = -1;
            try
            {
                if (treeView1.SelectedNode.Tag.ToString() != "")
                {
                    this.ColumnsChang(treeView1.SelectedNode.Tag.ToString());
                    tbinfo = treeView1.SelectedNode.Tag.ToString();
                }
                else
                {
                    tbinfo = "";
                    listView1.Columns.Clear();
                    listView1.Items.Clear();
                    listView1.Columns.Add("名称");
                    listView1.Items.Add("tuser", "用户", 0);
                    listView1.Items.Add("trole", "组", 0);
                    listView1.Items.Add("tmodule", "模块", 0);
                    tbinfo = "top";
                }
            }
            catch { }
        }

        
        private void topmenu(String st)
        {
            /**
            switch (st)
            {
                case "tuser": listView1.ContextMenuStrip = CUMenu;
                    break;
                case "trole": listView1.ContextMenuStrip = CRMenu;
                    break;
                case "tmodule": listView1.ContextMenuStrip = CMMenu;
                    break;
            }*/
            szmm.Visible = false;
            tjmk.Visible = false;
            tjz.Visible = false;
            del.Visible = false;
            attribute.Visible = false;
            line1.Visible = false;
            line2.Visible = false;
            create.Visible = true;

        }

        private void listView1_MouseUp(object sender, MouseEventArgs e)
        {
            listView1.ContextMenuStrip = EMenu;
            if (listView1.SelectedItems.Count != 0)
            {
                szmm.Visible = true;
                tjmk.Visible = true;
                tjz.Visible = true;
              
                line1.Visible = true;
                line2.Visible = true;
                attribute.Visible = true;

                create.Visible = false;
                switch (tbinfo)
                {
                    case "tuser":
                        szmm.Visible = true;
                        tjmk.Visible = false;
                        tjz.Visible = false;
                        del.Visible = false;
                        break;
                    case "trole":
                        del.Visible = true;
                        szmm.Visible = false;
                        tjmk.Visible = false;
                        tjz.Visible = true;
                        break;
                    case "tmodule":
                        szmm.Visible = false;
                        tjmk.Visible = false;
                        tjz.Visible = false;
                        del.Visible = false;
                        attribute.Visible = false;
                        break;
                    default:
                        listView1.ContextMenuStrip = null;
                        break;
                }

            }
            else
            {
                /**
                 szmm.Visible = false;
                 tjmk.Visible = false;
                 tjz.Visible = false;
                 del.Visible = false;
                 attribute.Visible = false;
                 line1.Visible = false;
                 line2.Visible = false;
                 create.Visible = true ;*/
                if (tbinfo == "top")
                {
                    listView1.ContextMenuStrip = null;
                    return;
                }
                topmenu(tbinfo);
            }
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            switch (tbinfo)
            {
                case "tuser":
                    this.ColumnsChang("tuser");
                    break;
                case "trole":
                    this.ColumnsChang("trole");
                    break;
                case "tmodule":
                    this.ColumnsChang("tmodule");
                    break;
            }
        }

        private void create_Click(object sender, EventArgs e)
        {
            switch (tbinfo)
            {
                case "tuser":
                    //同步用户  执行tbuser存储过程
                    SqlCommand sqlcmd = new SqlCommand();
                    sqlcmd.Connection = conn;
                    try
                    {
                        conn.Open();
                    }
                    catch { }
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.CommandText = "tbuser";
                    sqlcmd.ExecuteNonQuery();
                    conn.Close();
                    this.ColumnsChang("tuser");                   
                    //Frm_Cuser cjuser = new Frm_Cuser(this);
                    //cjuser.ShowDialog();
                    break;
                case "trole":
                    Frm_Crole crole = new Frm_Crole(this,0);
                    crole.ShowDialog();
                    break;
                case "tmodule":
                    //Frm_Cmodule module = new Frm_Cmodule(this);
                    //module.ShowDialog();
                    //根据菜单项生成权限模块
                    string sql = "select AppSystem,ModuleName,ModulePage,ModuleMemo from tModule ";
                    //.menuStrip1
                    SqlDataAdapter qx = new SqlDataAdapter(sql, conn);
                    SqlCommandBuilder sb = new SqlCommandBuilder(qx);
                    try
                    {
                        ds.Tables["tModule1"].Clear();
                    }
                    catch { }
                    qx.Fill(ds, "tModule1");
                    DataRow dr;
                    for (int i = 0; i < fmain.menuStrip1.Items.Count; i++)
                    {
                        ToolStripMenuItem tm = (ToolStripMenuItem)fmain.menuStrip1.Items[i];
                        for (int j = 0; j < tm.DropDownItems.Count; j++)
                        {

                            DataRow[] drArr = ds.Tables["tModule1"].Select("ModulePage = '" + tm.DropDownItems[j].Name + "'");
                            if (drArr.Length == 0)
                            {

                                dr = ds.Tables["tModule1"].NewRow();
                                dr["AppSystem"] = "华健车间系统";
                                dr["ModuleName"] = fmain.menuStrip1.Items[i].Text;
                                dr["ModulePage"] = tm.DropDownItems[j].Name;
                                dr["ModuleMemo"] = tm.DropDownItems[j].Text;

                                //  dm1.tUser1.Rows.Add(dr);
                                ds.Tables["tModule1"].Rows.Add(dr);
                            }
                        }
                    }
                    qx.Update(ds.Tables["tModule1"]);
                    this.ColumnsChang("tmodule");
                    break;
            }
        }

        private void del_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("您确定要执行删除操作吗？", "确认",
                       MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                int key = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);

                string sql = "SELECT RoleID AS 编号, RoleName AS 名称, RoleMemo AS 备注 FROM tRole  where roleid = '"+key+"'";
                //  tUserTA1.FillBy(dm1.tUser, key);
                // tRoleTA1.FillBy1(dm1.tRole, key);
                tRoleTA1 = new SqlDataAdapter(sql, conn);
                try
                {
                    ds.Tables["tRole"].Clear();
                }
                catch { }
                tRoleTA1.Fill(ds, "tRole");
                // tUserModuleTA1.Fill(dm1.tUserModule, key, 0);
                //tRoleModuleTA1.FillBy(dm1.tRoleModule, key, 0);
                string sql2 = "SELECT UID , RoleID , ModuleID FROM tRoleModule where RoleID = '"+key+"' or ModuleID = 0";
                SqlDataAdapter tRoleModuleTA1 = new SqlDataAdapter(sql2, conn);
               
                try
                {
                    ds.Tables["tRoleModule"].Clear();
                }
                catch { }
                tRoleModuleTA1.Fill(ds, "tRoleModule");
                string sql1 = "SELECT UID, UserID, RoleID FROM tUserRole where userid = 0 or roleid = '"+key+"'";
                //  tUserRoleTA1.Fill(dm1.tUserRole, 0, key);
                SqlDataAdapter tUserRoleTA1 = new SqlDataAdapter(sql1, conn);
                try
                {
                    ds.Tables["tUserRole"].Clear();
                }
                catch { }
                tUserRoleTA1.Fill(ds, "tUserRole");
                for (int i = 0; i < ds.Tables["tRoleModule"].Rows.Count; i++)
                {
                    ds.Tables["tRoleModule"].Rows[i].Delete();
                }
                for (int i = 0; i < ds.Tables["tUserRole"].Rows.Count; i++)
                {
                    ds.Tables["tUserRole"].Rows[i].Delete();
                }
                try
                {
                    SqlCommandBuilder sb1 = new SqlCommandBuilder(tRoleModuleTA1);
                    tRoleModuleTA1.Update(ds.Tables["tRoleModule"]);
                  
                }
                catch (ArgumentNullException) { }
                try
                {
                    SqlCommandBuilder sb2 = new SqlCommandBuilder(tUserRoleTA1);
                    tUserRoleTA1.Update(ds.Tables["tUserRole"]);
                   
                }
                catch (ArgumentNullException) { }

                ds.Tables["tRole"].Rows[0].Delete();
                SqlCommandBuilder sb = new SqlCommandBuilder(tRoleTA1);
                tRoleTA1.Update(ds.Tables["tRole"]);
              
                this.ColumnsChang("trole");

            }
            else
            {
                return;
            }
            
        }

        private void attribute_Click(object sender, EventArgs e)
        {

            switch (tbinfo)
            {
                case "tuser":
                   Frm_Euser euser = new Frm_Euser(listView1.SelectedItems[0].SubItems[2].Text, int.Parse(listView1.SelectedItems[0].SubItems[0].Text));
                    euser.ShowDialog();
                    this.ColumnsChang("tuser");
                    break;
                case "trole":
                    Frm_Crole role = new Frm_Crole(listView1.SelectedItems[0].SubItems[1].Text, int.Parse(listView1.SelectedItems[0].SubItems[0].Text),this,1);
                    role.ShowDialog();
                    this.ColumnsChang("trole");
                    break;
                case "tmodule":
                    //Frm_Emodule emodule = new Frm_Emodule(listView1.SelectedItems[0].SubItems[2].Text, int.Parse(listView1.SelectedItems[0].SubItems[0].Text));
                  //  emodule.ShowDialog();
                    this.ColumnsChang("tmodule");
                    break;
            }
        }

        private void listView1_ColumnClick_1(object sender, ColumnClickEventArgs e)
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

        private void tjz_Click(object sender, EventArgs e)
        {
            Frm_UserAdd user = new Frm_UserAdd(listView1.SelectedItems[0].SubItems[1].Text, this, int.Parse(listView1.SelectedItems[0].SubItems[0].Text.ToString()));
            user.ShowDialog();

        }

        private void tjmk_Click(object sender, EventArgs e)
        {
            
        }

        private void szmm_Click(object sender, EventArgs e)
        {
            Frm_Epwd epwd = new Frm_Epwd(listView1.SelectedItems[0].SubItems[0].Text);
            epwd.ShowDialog();
        }

        public void ColumnsChang(String st)
        {
            tbinfo = st;
            listView1.Columns.Clear();
            listView1.Items.Clear();
           // DataView dv = new DataView(ds.Tables[st]);
            switch (st)
            {
                case "tuser":
                   // SqlDataAdapter gxjl = new SqlDataAdapter(insertsql, conn);
                  //  gxjl.Fill(ds, "tgx");
                    string sql = "SELECT UserID AS 编号, UserCode AS 工号, UserName AS 姓名, UserMemo AS 备注,Lock AS 锁定, LastLogin AS 上次登录时间 FROM tUser";
                    tUserTA1 = new SqlDataAdapter(sql, conn);
                    try
                    {
                        ds.Tables["tuser"].Clear();
                    }
                    catch { }
                    tUserTA1.Fill(ds, "tuser");
                    //tUserTA1.Fill(dm1.tUser);
                    break;
                case "trole":
                    string sql1 = "SELECT RoleID AS 编号, RoleName AS 名称, RoleMemo AS 备注 FROM tRole";
                    tRoleTA1 = new SqlDataAdapter(sql1, conn);
                    try
                    {
                        ds.Tables["tRole"].Clear();
                    }
                    catch { }
                    tRoleTA1.Fill(ds, "tRole");
                    break;
                case "tmodule":
                    string sql2 = "SELECT ModuleID AS ID, AppSystem AS 系统名称, ModuleName AS 模块名称, ModulePage AS 模块项, ModuleMemo AS 备注 FROM tModule";
                    tModuleTA1 = new SqlDataAdapter(sql2, conn);
                    try
                    {
                        ds.Tables["tmodule"].Clear();
                    }
                    catch { }
                    tModuleTA1.Fill(ds, "tmodule");
                    break;
            }
            DataView dv = new DataView(ds.Tables[st]);
            //填充listview标题栏
            for (int k = 0; k < dv.Table.Columns.Count; k++)
            {
                listView1.Columns.Add(dv.Table.Columns[k].ToString());
            }
            //填充listview
            for (int i = 0; i < dv.Count; i++)
            {
                ListViewItem li = new ListViewItem();
                li.SubItems.Clear();
                li.SubItems[0].Text = dv[i][0].ToString();      //填充第一列             
                for (int j = 1; j < dv.Table.Columns.Count; j++)
                {
                    li.SubItems.Add(dv[i][j].ToString());
                }
                listView1.Items.Add(li);
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
    }
}
