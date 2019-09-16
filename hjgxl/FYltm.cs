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
    public partial class FYltm : Form
    {
        SqlConnection  conn = new SqlConnection(Globals.connstr);
        DataSet ds=new DataSet();
        string tm, tm1;
        string cd1, cd2;
        string zl1, zl2,rq,rq1,gys1,gys2,ph1,ph2,wl1,wl2,pici1,pici2;
        R_T sj = new R_T();
        DataTable dt,dt1,wgrkd;
        public FYltm()
        {
            InitializeComponent();
            printDocument1.PrinterSettings.PrinterName = Encrypt.Encrypt.getConfig("config.xml", "p1");
            printDocument1.PrintController = new System.Drawing.Printing.StandardPrintController();

            string sql = "select FItemID,fname,FSPGroupID from  t_Stock  ";
            conn = new SqlConnection(Globals.connstr);
           
            comboBox2.DataSource = dt1= sj.ds(sql, "ck", conn);
            comboBox2.DisplayMember = "fname";
            comboBox2.ValueMember = "FSPGroupID"; ;

            try
            {
                string sql1 = "select FSPID, FName,FNumber from  t_StockPlace  where  fname like '"+comboBox3.Text+"%' and FSPGroupID=" + comboBox2.SelectedValue.ToString();
                comboBox3.DataSource = sj.ds(sql1, "cw", conn);
                comboBox3.DisplayMember = "fname";
                comboBox3.ValueMember = "FSPID";
                
            }
            catch { }

        }
        //public SqlConnection Conn
        //{
        //    get
        //    {
        //        return  conn;
        //    }
        //    set {

        //        conn = new SqlConnection(Globals.connstr);
        //    }
        //}




        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            label15.Text = "";
            string sql;
            string wgrk;
            if (textBox1.Text.Trim() == "")
            {
                sql = "select a.FItemID, a.FName,a.fnumber,b.fname hjph,FCoefficient from  t_ICItem a LEFT OUTER JOIN   dbo.t_Item b ON LEFT(a.FNumber, 10) = b.FNumber AND    b.FItemClassID = 4 LEFT OUTER JOIN     t_MeasureUnit ON FProductUnitID = t_MeasureUnit.FItemID   where (a.fnumber like '1.%' and (a.fname like '%" + comboBox1.Text + "%' or a.fnumber like '%" + comboBox1.Text + "%' ))";
            }
            else
            {
                sql = "select a.FItemID, a.FName,a.fnumber,b.fname hjph,FCoefficient from  t_ICItem a LEFT OUTER JOIN   dbo.t_Item b ON LEFT(a.FNumber, 10) = b.FNumber AND    b.FItemClassID = 4  LEFT OUTER JOIN     t_MeasureUnit ON FProductUnitID = t_MeasureUnit.FItemID where  a.FItemID in(select FItemID from ICStockBillEntry  where FInterID in(select FInterID from ICStockBill where FBillNo='" + textBox1.Text + "' ))";

                // sql = "select FItemID, FName ,fnumber from  t_ICItem  where  FItemID in(select FItemID from ICStockBillEntry  where FInterID in(select FInterID from ICStockBill where FBillNo='" + textBox1.Text + "' ))";
                wgrk = "select FItemID,FBatchNo from ICStockBillEntry  where FInterID in(select FInterID from ICStockBill where FBillNo='" + textBox1.Text + "' )  ";

                try
                {
                    wgrkd.Clear();
                }
                catch { }
                wgrkd = sj.ds(wgrk,"rksj",conn);
            }

            // conn.Open();
            try { ds.Tables["wl"].Clear(); } catch { }
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            // ds = new DataSet();
            da.Fill(ds, "wl");
            // DataTable wl;

            comboBox1.DataSource = ds.Tables["wl"];
            comboBox1.DisplayMember = "FName";
            comboBox1.ValueMember = "FItemID";
            //   conn.Close();

        }

       

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
           
            dataGridView1.EndEdit();
            comboBox1.Focus();
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells["cd"].Value == null)
                {
                    MessageBox.Show("必须录入膜使用长度");                    
                    return;
                }
             
            }

           
            if (MessageBox.Show("是否需要保存", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;  //否就退出操作
            }
            try
            {
                conn.Open();
            }
            catch { }
            SqlDataAdapter dtyl = new SqlDataAdapter("select * from tyl  where 1=2", conn);

            dtyl.Fill(ds, "b1");
            //批量写入原料表 
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                DataRow newrow = ds.Tables["b1"].NewRow();
             //   newrow["FItemID"] = comboBox1.SelectedValue;
                newrow["FitemID"]= dataGridView1.Rows[i].Cells["wlnm"].Value.ToString();
                newrow["FBatchNo"] = dataGridView1.Rows[i].Cells["fpc"].Value.ToString();
                newrow["FSyqty"] = newrow["Fqty"] = dataGridView1.Rows[i].Cells["zl"].Value.ToString();
                newrow["FSylength"] = newrow["FLength"] = dataGridView1.Rows[i].Cells["cd"].Value.ToString();
               // newrow["FICMOBillNo"] = rwd.Text.Trim();
            //    if (dataGridView1.Rows[i].Cells["mhd"].Value != null) { 
            //    newrow["Fhd"] = dataGridView1.Rows[i].Cells["mhd"].Value.ToString();
            //    newrow["Fkd"] = dataGridView1.Rows[i].Cells["mkd"].Value.ToString();
            //    newrow["Fmd"] = dataGridView1.Rows[i].Cells["mmd"].Value.ToString();
            //}
                newrow["Fck"] = dataGridView1.Rows[i].Cells["cknm"].Value.ToString();
                newrow["Fcw"] = dataGridView1.Rows[i].Cells["cwnm"].Value.ToString();
                newrow["Frkrq"] = dataGridView1.Rows[i].Cells["frkrq"].Value.ToString();
                newrow["Fscrq"] = dataGridView1.Rows[i].Cells["fscrq"].Value.ToString();
                newrow["Frkdh"]= dataGridView1.Rows[i].Cells["rkdh"].Value.ToString();
                newrow["Fxh"] = dataGridView1.Rows[i].Cells["Fxh"].Value.ToString();
                //if (FSupplyID.Text!="")
                //{
                //    newrow["FSupplyID"] = FSupplyID.Text;
                //}
                if (dataGridView1.Rows[i].Cells["gysid"].Value.ToString()!="") {
                    newrow["FSupplyID"] = dataGridView1.Rows[i].Cells["gysid"].Value.ToString();
                }
                newrow["Fyltm"] = dataGridView1.Rows[i].Cells["barcode"].Value.ToString();
                ds.Tables["b1"].Rows.Add(newrow);

            }
            SqlCommandBuilder sb = new SqlCommandBuilder(dtyl);
            dtyl.Update(ds.Tables["b1"]);
            conn.Close();

            //dataGridView1.EndEdit();
            for (int i = 0; i < dataGridView1.RowCount - 1;)
            {
                tm = dataGridView1.Rows[i].Cells["barcode"].Value.ToString();
                cd1 = dataGridView1.Rows[i].Cells["cd"].Value.ToString();
                wl1= dataGridView1.Rows[i].Cells["wlmc"].Value.ToString();
                zl1 = dataGridView1.Rows[i].Cells["zl"].Value.ToString();
                rq = dataGridView1.Rows[i].Cells["frkrq"].Value.ToString();
                ph1 = dataGridView1.Rows[i].Cells["Fhjph"].Value.ToString();
                gys1 = dataGridView1.Rows[i].Cells["gysmc"].Value.ToString();
                pici1= dataGridView1.Rows[i].Cells["fpc"].Value.ToString();
                tm1 = "";

                if ((dataGridView1.RowCount - 1) > i + 1)
                {
                    tm1 = dataGridView1.Rows[i + 1].Cells["barcode"].Value.ToString();
                    wl2 = dataGridView1.Rows[i+1].Cells["wlmc"].Value.ToString();
                    cd2 = dataGridView1.Rows[i + 1].Cells["cd"].Value.ToString();
                    zl2 = dataGridView1.Rows[i + 1].Cells["zl"].Value.ToString();
                    rq1 = dataGridView1.Rows[i + 1].Cells["frkrq"].Value.ToString();
                    gys2 = dataGridView1.Rows[i + 1].Cells["gysmc"].Value.ToString();
                    ph2 = dataGridView1.Rows[i+1].Cells["Fhjph"].Value.ToString();
                    pici2 = dataGridView1.Rows[i+1].Cells["fpc"].Value.ToString();
                }
                printDocument1.Print();
                i += 2;
            }

            dataGridView1.Rows.Clear();
            gys.Clear();
            FSupplyID.Clear();
            textBox1.Clear();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (pici.Text =="") {
                MessageBox.Show("批次不能为空");
                dataGridView1.EndEdit();
                pici.Focus();
            }
        }

        private void del_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("没有选中任意一行！");
                    return;
                }
                if (MessageBox.Show("是否删除当前行", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return;  //否就退出操作
                }
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);


            }
            catch
            {

                MessageBox.Show("没有任何录入！");
            }
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter & dataGridView1.CurrentCell.ColumnIndex == 1)//是否选择的是第6个单元格
                {
                    if (!Globals.CheckNumber(dataGridView1.CurrentRow.Cells["zl"].Value.ToString().Trim()))
                    {
                        MessageBox.Show("请输入数字");
                        dataGridView1.CurrentRow.Cells["zl"].Value = null;
                        return;
                    }

                    else if (dataGridView1.CurrentRow.Cells["barcode"].Value == null)
                    {
                        dataGridView1.CurrentRow.Cells["barcode"].Value = comboBox1.SelectedValue.ToString().PadLeft(5, '0') + DateTime.Now.Year.ToString().Substring(2, 2) + Globals.get_tmlsh(conn).PadLeft(5, '0');

                        //  dataGridView1.CurrentRow.Cells["cd"].Value = int.Parse(Math.Floor(decimal.Parse(dataGridView1.CurrentRow.Cells["zl"].Value.ToString()) / decimal.Parse(bl.Text) * 1000000).ToString());//计算重量

                        dataGridView1.CurrentRow.Cells["cd"].Value = decimal.Parse(Math.Floor(decimal.Parse(dataGridView1.CurrentRow.Cells["zl"].Value.ToString()) / decimal.Parse(bl.Text)).ToString());//计算重量

                        dataGridView1.CurrentRow.Cells["wlmc"].Value = comboBox1.Text;
                    }
                    DataRow[] drArr = dt1.Select("fname = '" + comboBox2.Text + "'");

                    if (dataGridView1.CurrentRow.Cells["fck"].Value == null)
                    {
                        dataGridView1.CurrentRow.Cells["fck"].Value = comboBox2.Text;
                        dataGridView1.CurrentRow.Cells["fcw"].Value = comboBox3.Text;
                        dataGridView1.CurrentRow.Cells["cknm"].Value = drArr[0]["FItemID"].ToString(); ;
                        dataGridView1.CurrentRow.Cells["cwnm"].Value = comboBox3.SelectedValue;
                        dataGridView1.CurrentRow.Cells["frkrq"].Value = dateTimePicker1.Text;
                        dataGridView1.CurrentRow.Cells["fscrq"].Value = dateTimePicker2.Text;

                        dataGridView1.CurrentRow.Cells["gysmc"].Value = gys.Text;
                        dataGridView1.CurrentRow.Cells["gysid"].Value = FSupplyID.Text;
                        dataGridView1.CurrentRow.Cells["rkdh"].Value = textBox1.Text;
                        dataGridView1.CurrentRow.Cells["Fhjph"].Value = label16.Text;
                        dataGridView1.CurrentRow.Cells["Fxh"].Value = xh.Text.Trim();
                        dataGridView1.CurrentRow.Cells["wlnm"].Value = comboBox1.SelectedValue;
                        dataGridView1.CurrentRow.Cells["fpc"].Value = pici.Text;
                    }
                }
                   
                                             

                if (e.KeyCode == Keys.Enter & dataGridView1.CurrentCell.ColumnIndex == 2)//是否选择的是第6个单元格
                {
                    if (!Globals.CheckNumber(dataGridView1.CurrentRow.Cells["cd"].Value.ToString().Trim()))
                    {
                        MessageBox.Show("请输入数字");
                        dataGridView1.CurrentRow.Cells["cd"].Value = null;
                        return;
                    }

                }

               // dataGridView1.CurrentCell = dataGridView1[1, dataGridView1.CurrentCell.RowIndex];

            }
            catch {
                MessageBox.Show("请选择料号");
            }

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
            if (keyData == Keys.Tab)
            {
                // e.Handled = false;
                return true;
            }

            if (keyData == Keys.Right)
            {
                // e.Handled = false;
                return true;
            }
            if (keyData == Keys.Left)
            {
                // e.Handled = false;
                return true;
            }
            //if (keyData == Keys.Up)
            //{
            //    // e.Handled = false;
            //    return true;
            //}
            //if (keyData == Keys.Down)
            //{
            //    // e.Handled = false;
            //    return true;
            //}
            if (keyData == Keys.Enter)//屏蔽datagridview回车换行
            {
                if (this.dataGridView1.IsCurrentCellInEditMode)
                {
                    dataGridView1.EndEdit();
                    //  dataGridView1.BeginEdit(true);
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);


            //if (keyData == Keys.Enter && dataGridView1.Focused)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    string sql = "select FItemID,FName from  t_Supplier  where fitemid in(select FSupplyID from  ICStockBill  where FBillNo='" + textBox1.Text.Trim() + "') ";
            //    try { dt.Clear(); } catch { }
            //     dt = sj.ds(sql, "gys", conn);
               
            //    // RoleName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.dm1, "tRole.名称", true));
            //    //RoleMemo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.dm1, "tRole.备注", true));
            //    try
            //    {
            //        gys.DataBindings.Clear();
            //        FSupplyID.Clear();
            //        gys.DataBindings.Add(new System.Windows.Forms.Binding("Text", dt, "Fname", true));
            //        FSupplyID.DataBindings.Add(new System.Windows.Forms.Binding("Text", dt, "FItemID", true));
            //    }
            //    catch { }
            //}
        }

       

        private void comboBox3_DropDown(object sender, EventArgs e)
        {
            try
            {
                string sql1 = "select FSPID, FName,FNumber from  t_StockPlace  where  fname like '" + comboBox3.Text + "%' and FSPGroupID=" + comboBox2.SelectedValue.ToString();
                comboBox3.DataSource = sj.ds(sql1, "cw", conn);
                comboBox3.DisplayMember = "fname";
                comboBox3.ValueMember = "FSPID";

            }
            catch { }
        }





        private void textBox1_Leave(object sender, EventArgs e)
        {

            string sql = "select FItemID,FName from  t_Supplier  where fitemid in(select FSupplyID from  ICStockBill  where FBillNo='" + textBox1.Text.Trim() + "') ";
            try { dt.Clear(); } catch { }
            dt = sj.ds(sql, "gys", conn);

            // RoleName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.dm1, "tRole.名称", true));
            //RoleMemo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.dm1, "tRole.备注", true));
            try
            {
                // gys.DataBindings.Remove(new System.Windows.Forms.Binding("Text", dt, "Fname", true));
                // FSupplyID.DataBindings.Remove(new System.Windows.Forms.Binding("Text", dt, "FItemID", true));
                gys.DataBindings.Clear();
                FSupplyID.DataBindings.Clear();
                gys.DataBindings.Add(new System.Windows.Forms.Binding("Text", dt, "Fname", true));
                FSupplyID.DataBindings.Add(new System.Windows.Forms.Binding("Text", dt, "FItemID", true));
            }
            catch { }

        }

      

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
           dataGridView1.EndEdit();
           // comboBox1.Focus();
            try
            {
                decimal hjzl = 0;
                for (int i = 0; i < dataGridView1.SelectedCells.Count; i++)
                {
                    hjzl += decimal.Parse(dataGridView1.SelectedCells[i].Value.ToString());
                }
                label14.Text = hjzl.ToString();
            }
            catch { }
      
            try
            {
                if (comboBox1.Text.Trim() != "")
                {
                    if (dataGridView1.CurrentCell.ColumnIndex == 1)
                    {

                        if (!Globals.CheckNumber(dataGridView1.CurrentRow.Cells["zl"].Value.ToString().Trim()))
                        {
                            MessageBox.Show("请输入数字");
                            dataGridView1.CurrentRow.Cells["zl"].Value = null;
                            return;
                        }

                        else if (dataGridView1.CurrentRow.Cells["barcode"].Value == null)
                        {
                            dataGridView1.CurrentRow.Cells["barcode"].Value = comboBox1.SelectedValue.ToString().PadLeft(5, '0') + DateTime.Now.Year.ToString().Substring(2, 2)+ Globals.get_tmlsh(conn).PadLeft(5,'0');

                            dataGridView1.CurrentRow.Cells["cd"].Value = decimal.Parse(Math.Floor(decimal.Parse(dataGridView1.CurrentRow.Cells["zl"].Value.ToString()) / decimal.Parse(bl.Text) ).ToString());//计算重量

                            dataGridView1.CurrentRow.Cells["wlmc"].Value = comboBox1.Text;
                        }
                        DataRow[] drArr = dt1.Select("fname = '" + comboBox2.Text + "'");
                        if (dataGridView1.CurrentRow.Cells["fck"].Value == null)
                        {
                            dataGridView1.CurrentRow.Cells["fck"].Value = comboBox2.Text;
                            dataGridView1.CurrentRow.Cells["fcw"].Value = comboBox3.Text;
                            dataGridView1.CurrentRow.Cells["cknm"].Value = drArr[0]["FItemID"].ToString(); ;
                            dataGridView1.CurrentRow.Cells["cwnm"].Value = comboBox3.SelectedValue;

                            dataGridView1.CurrentRow.Cells["frkrq"].Value = dateTimePicker1.Text;
                            dataGridView1.CurrentRow.Cells["fscrq"].Value = dateTimePicker2.Text;

                            dataGridView1.CurrentRow.Cells["gysmc"].Value = gys.Text;
                            dataGridView1.CurrentRow.Cells["gysid"].Value = FSupplyID.Text;
                            dataGridView1.CurrentRow.Cells["rkdh"].Value = textBox1.Text;
                            dataGridView1.CurrentRow.Cells["Fhjph"].Value = label16.Text;
                            dataGridView1.CurrentRow.Cells["Fxh"].Value = xh.Text.Trim();
                            dataGridView1.CurrentRow.Cells["wlnm"].Value = comboBox1.SelectedValue;
                            dataGridView1.CurrentRow.Cells["fpc"].Value = pici.Text;
                        }
                    }
                    
                }
                else
                {
                    dataGridView1.CurrentCell = dataGridView1[1, dataGridView1.CurrentCell.RowIndex];
                    MessageBox.Show("先选择物料");
                }
            }
            catch {
            }
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                xh.Clear();
                DataRow[] drArr = ds.Tables["wl"].Select("FItemID = '" + comboBox1.SelectedValue + "'");
                try
                {
                    bl.Text = drArr[0]["FCoefficient"].ToString();
                }
                catch {
                    bl.Clear();

                }

                label15.Text = drArr[0]["FNumber"].ToString();

                    try {
                    label16.Text = drArr[0]["hjph"].ToString().Split('-')[2];
                }
                catch {
                    label16.Text = "";
                }

                  }
            catch {
                label15.Text = "";
            }
            try {
                DataRow[] drArr = wgrkd.Select("FItemID = '" + comboBox1.SelectedValue + "'");
                pici.Text = drArr[0]["fbatchno"].ToString();
            
            } catch { pici.Clear(); }          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            decimal hjzl = 0;
            for (int i = 0; i < dataGridView1.SelectedCells.Count; i++)
            {
                hjzl += decimal.Parse(dataGridView1.SelectedCells[i].Value.ToString());
            }
            label14.Text = hjzl.ToString();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string sql1 = "select FSPID, FName,FNumber from  t_StockPlace  where FSPGroupID=" + comboBox2.SelectedValue.ToString();
                comboBox3.DataSource = sj.ds(sql1, "cw", conn);
                comboBox3.DisplayMember = "fname";
                comboBox3.ValueMember = "FSPID";
            }
            catch { }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];
                if (column is DataGridViewButtonColumn)
                {
                    //这里可以编写你需要的任意关于按钮事件的操作~
                    try
                    {
                        dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                    }
                    catch { }
                }
                //DGV下拉框的取值
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            dataGridView1.EndEdit();
            for (int i = 0; i < dataGridView1.RowCount - 1;)
            {
                tm = dataGridView1.Rows[i].Cells["barcode"].Value.ToString();
                cd1 = dataGridView1.Rows[i].Cells["cd"].Value.ToString();
               
                zl1= dataGridView1.Rows[i].Cells["zl"].Value.ToString();
                rq = dataGridView1.Rows[i].Cells["frkrq"].Value.ToString();
                tm1 = "";
                
              
                if ((dataGridView1.RowCount - 1) >i+1)
                {              
                    tm1 = dataGridView1.Rows[i + 1].Cells["barcode"].Value.ToString();
                    cd2 = dataGridView1.Rows[i+1].Cells["cd"].Value.ToString();
                    zl2 = dataGridView1.Rows[i+1].Cells["zl"].Value.ToString();
                    rq1 = dataGridView1.Rows[i + 1].Cells["frkrq"].Value.ToString();
                }
                printDocument1.Print();
                i += 2;
            }

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            StringFormat sfm = new StringFormat();
            RectangleF fw;
            sfm.Alignment = StringAlignment.Center;
            sfm.LineAlignment = StringAlignment.Center;
            e.Graphics.DrawString(wl1, new Font("黑体", 8), Brushes.Black, 10, 5);
            Image im = barcode128.BarCode.BuildBarCode(tm);
            Rectangle destRect1 = new Rectangle(10, 20, 160, 40);
            e.Graphics.DrawImage(im, destRect1);
            SizeF sf = e.Graphics.MeasureString(tm, new Font("黑体", 15));
            int x = 220 / 2 - (int)sf.Width / 2+10;
            int y = 60;
            e.Graphics.DrawString(tm, new Font("黑体", 10), Brushes.Black, x, y);
            e.Graphics.DrawString("长" + cd1+"m      重"+zl1+"kg", new Font("黑体", 8), Brushes.Black, 10, 80);
            e.Graphics.DrawString("入库日期:" + rq, new Font("黑体", 8), Brushes.Black, 10, 100);
            e.Graphics.DrawString(gys1 , new Font("黑体", 7), Brushes.Black, 10, 120);
            e.Graphics.DrawString(ph1, new Font("黑体", 8), Brushes.Black, 10, 130);
            e.Graphics.DrawString(pici1, new Font("黑体", 8), Brushes.Black, 100, 130);
            if (tm1 != "")
            {

                im = barcode128.BarCode.BuildBarCode(tm1);
                 destRect1 = new Rectangle(210, 20, 160, 40);
                e.Graphics.DrawString(wl2, new Font("黑体", 8), Brushes.Black, 210, 5);
                e.Graphics.DrawImage(im, destRect1);
                 sf = e.Graphics.MeasureString(tm1, new Font("黑体", 15));
                 x = 610 / 2 - (int)sf.Width / 2+10;
                 y = 60;
                e.Graphics.DrawString(tm1, new Font("黑体", 10), Brushes.Black, x, y);
                e.Graphics.DrawString("长" + cd2 + "m      重" + zl2+"kg", new Font("黑体", 8), Brushes.Black, 210, 80);
                e.Graphics.DrawString("入库日期:" +rq1, new Font("黑体", 8), Brushes.Black, 210, 100);
                e.Graphics.DrawString(gys2, new Font("黑体", 7), Brushes.Black, 210, 120);
                e.Graphics.DrawString(ph2, new Font("黑体", 8), Brushes.Black, 220, 130);

                e.Graphics.DrawString(pici2, new Font("黑体", 8), Brushes.Black, 300, 130);
            }
        }
    }

    

}
