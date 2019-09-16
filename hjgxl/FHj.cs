using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;

namespace hjgxl
{
    public partial class FHj : jp
    {
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(Globals.connstr);
        DataTable dt, dt_ycl, dt_gxl;
        R_T sj = new R_T();
        public FHj()
        {
            InitializeComponent();
            jlr.Text = Globals.usernm;
            string sql = "select fname from  t_SubMessage where FTypeID ='10001'";
            jtmc.DataSource = new R_T().ds(sql, "jt", new SqlConnection(Globals.connstr));
            //// jt.Text = "";
            jtmc.DisplayMember = "FName";
            jtmc.ValueMember = "FName";
            jtmc.SelectedIndex = -1;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataRow[] drArr = dt.Select("FBillNo = '" + comboBox1.SelectedValue + "'");

                label6.Text = drArr[0]["FGMPBatchNo"].ToString();//订单号
                label8.Text = drArr[0]["Fname"].ToString();//物料
                label7.Text = drArr[0]["khmc"].ToString();//客户名称
                wl.Text = drArr[0]["fitemid"].ToString();
                jlrq.Text = DateTime.Now.ToString();
                // wlid.Text = drArr[0]["fitemid"].ToString();//物料id
                wlid.Text = drArr[0]["fnumber"].ToString().Substring(0, 7);
                ydnm.Text = drArr[0]["finterid"].ToString();
                rwl.Text = drArr[0]["fqty"].ToString();
                ck.Text= drArr[0]["FDefaultLoc"].ToString();
                cw.Text = drArr[0]["FSPID"].ToString();

                string cl = "select sum(FMcd) cl from  tgx  where FICMOBillNo='" + comboBox1.SelectedValue + "'  and FWork='分切' and fhj=0";
                yscl.Text = sj.ds(cl, "cl", conn).Rows[0][0].ToString();


                ////string  sql = "select  FName from V_Gx  where  fname like '%涂布%' and FNumber='"+wlid.Text+"'";
                //// comboBox3.DataSource = sj.ds(sql, "gx", conn);
                //// comboBox3.DisplayMember = "fname";
                //// comboBox3.ValueMember = "fname";
                if (!lgxl())
                {
                    MessageBox.Show("领料失败");
                }

                dataGridView2.DataSource = dt_gxl;
                //string sql = "select ID, FItemID, FBatchNo, FRkdh, FYltm, Fqty, FLength, FLength- yycd kycd , FSyqty, FZt, FICMOBillNo, FLy, FName from  v_tyl  where FItemID in( select Fbase2 from v_gx where  fnumber='" + wlid.Text + "' and fbase1='1002') ";
                //try
                //{
                //    dt_ycl.Clear();
                //}
                //catch { }
                //dt_ycl = sj.ds(sql, "ycl", conn);
            }
            catch { }
        }
        public Boolean lgxl()
        {

            try
            {

                //string sql = "select * from  v_kyrklsl  where fitemid in(select FBase3 from  V_Gx where FNumber='" + wlid.Text.ToString() + "'  and FBase3 in(select  fitemID from  v_flrw  where   FICMOBillNo='" + comboBox1.Text + "')) ";
                string sql = "select  FBarcode '条码',FBatchNo '批次',kycd '可用长度' ,FMkd '膜宽',FMhd '膜厚'  from  V_Kygclsl  where FICMOBillNo='" + comboBox1.SelectedValue+"'";
                try
                {
                    dt_gxl.Clear();
                }
                catch { }
                dt_gxl = sj.ds(sql, "dt_gxl", conn);
                dataGridView2.DataSource = dt_gxl;
                string sql1 = "select FComboBox from v_gx where fname='分切' and fnumber='" + wlid.Text.ToString() + "'";
                DataTable trkd = new DataTable();
                trkd = sj.ds(sql1, "rkd", conn);
                rkd.Text = trkd.Rows[0][0].ToString();//入库点

                return true;
            }
            catch
            {

                return false;
            }
        }
        public Boolean yanzheng(string tm, int r)
        {
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                if (i != r)
                {

                    if (dataGridView1.Rows[i].Cells["tm"].Value.ToString() == tm)
                    {
                        MessageBox.Show("请勿重复扫描");
                        return false;
                    }
                }
            }
            return true;

        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter & dataGridView1.CurrentCell.ColumnIndex == 0)//是否选择的是第一个单元格
            {
                try
                {
                    //验证不通过
                    if (!yanzheng(dataGridView1.CurrentRow.Cells["tm"].Value.ToString(), dataGridView1.CurrentCell.RowIndex))
                    {
                        dataGridView1.CurrentRow.Cells["tm"].Value = "";
                        return;
                    }

                    // dataGridView1.CurrentCell 
                    //扫描原料条码  在dt_ycl中 查询
                    if (dt_gxl.Rows.Count != 0)
                    {

                        try
                        {
                            DataRow[] drArr = dt_gxl.Select("条码 = '" + dataGridView1.CurrentCell.Value.ToString().Trim() + "'");

                            dataGridView1.CurrentRow.Cells["kycd"].Value = drArr[0]["可用长度"];//膜长
                          //  dataGridView1.CurrentRow.Cells["sycd1"].Value = drArr[0]["kycd"];//膜长
                            dataGridView1.CurrentCell = dataGridView1[4, dataGridView1.CurrentCell.RowIndex];
                            dataGridView1.CurrentRow.Cells["mk"].Value = drArr[0]["膜宽"];
                            dataGridView1.CurrentRow.Cells["mh"].Value = drArr[0]["膜厚"];
                            //dataGridView1.CurrentRow.Cells[""].Value = drArr[0][""];
                            //dataGridView1.CurrentRow.Cells[""].Value = drArr[0][""];
                            //dataGridView1.CurrentRow.Cells[""].Value = drArr[0][""];
                        }
                        catch
                        {
                            MessageBox.Show("错误的条码，请重新扫描");
                            dataGridView1.CurrentRow.Cells["tm"].Value = "";
                        }
                        return;
                    }
                    else
                    {
                        MessageBox.Show("请先做生产领料单");
                    }
                } 
                catch {
                    MessageBox.Show("错误的条码");
                    return; }
        }
            if (e.KeyCode == Keys.Enter & dataGridView1.CurrentCell.ColumnIndex == 4)//是否选择的是第6个单元格
            {
                if (!Globals.CheckNumber(dataGridView1.CurrentRow.Cells["sycd1"].Value.ToString().Trim()))
                {
                    MessageBox.Show("请输入数字");
                    dataGridView1.CurrentRow.Cells["sycd1"].Value = null;
                    return;
                }
                if (decimal.Parse(dataGridView1.CurrentRow.Cells["sycd1"].Value.ToString().Trim()) > decimal.Parse(dataGridView1.CurrentRow.Cells["kycd"].Value.ToString().Trim()))
                {

                    MessageBox.Show("使用长度不能超过膜长度");
                    dataGridView1.CurrentRow.Cells["sycd1"].Value = null;
                    return;
                }
            }

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

        private void mcd_Click(object sender, EventArgs e)
        {
            try
            {
               // decimal cd = 0;
                int jtsl = 0;
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                   // cd = cd + decimal.Parse(dataGridView1.Rows[i].Cells["sycd1"].Value.ToString());
                    //  dataGridView1.Rows[i].Cells["sycd"].Value 
                    jtsl = i;
                }
                jt.Text = jtsl.ToString();
               // mcd.Text = cd.ToString();
                if (gctm.Text == "")
                {
                    gctm.Text = wl.Text.PadLeft(5, '0') + Globals.GetTimeStamp();
                }
                mkd.Text = dataGridView1.Rows[0].Cells["mk"].Value.ToString();
                mhd.Text = dataGridView1.Rows[0].Cells["mh"].Value.ToString();
            }
            catch { }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells["sycd1"].Value == null)
                {
                    MessageBox.Show("使用长度必须为数字");

                    return;

                }

                if (!Globals.CheckNumber(dataGridView1.Rows[i].Cells["sycd1"].Value.ToString().Trim()))
                {
                    MessageBox.Show("使用长度必须为数字");

                    return;

                }


                if (decimal.Parse(dataGridView1.Rows[i].Cells["sycd1"].Value.ToString().Trim()) > decimal.Parse(dataGridView1.Rows[i].Cells["kycd"].Value.ToString().Trim()))
                {

                    MessageBox.Show("使用长度不能超可用长度");
                    dataGridView1.Rows[i].Cells["sycd1"].Value = null;
                    dataGridView1.Rows[i].Cells["sycd1"].Style.BackColor = Color.Red;

                    return;
                }

            }

            if (MessageBox.Show("是否需要保存", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;  //否就退出操作
            }
            if (!Globals.CheckNumber(mcd.Text.ToString().Trim()))
            {
                MessageBox.Show("膜长度必须为数字");
                mcd.Text = null;
                return;
            }
            if (mkd.Text != "")
            {
                if (!Globals.CheckNumber(mkd.Text.ToString().Trim()))
                {
                    MessageBox.Show("膜宽度必须为数字");
                    mkd.Text = null;
                    return;

                }
            }

            if (dataGridView1.RowCount <= 1 | gctm.Text == "")
            {
                MessageBox.Show("信息不全不能保存");
                return;
            }
            try
            {
                ds.Tables["tgx"].Clear();
            }
            catch { }
            try
            {
                ds.Tables["gxb"].Clear();
            }
            catch { }
            //try
            //{
            //    conn.Open();
            //}
            //catch { }
            // DataSet ds = new DataSet();
            //修改tyl表的 FSylength=FSylength-sycd
            string insertsql = "SELECT  FBarcode, FWork, FQty, FMcd, FMkd, FMhd, FJs, FBc, FJyy, FBz, FUnitID, FICMOID, FItemID, FJlr, FJlrq,FRkd, FSfrk, FSfyw, FICMOBillNo ,FBatchNo,Fjt,FHj FROM  Tgx where 1=2 ";
            SqlDataAdapter gxjl = new SqlDataAdapter(insertsql, conn);
            gxjl.Fill(ds, "tgx");
            DataRow newrowgx = ds.Tables["tgx"].NewRow();
            newrowgx["FBarcode"] = gctm.Text.Trim();
            newrowgx["FWork"] = "分切";
            if (mzl.Text != "")
            {
                newrowgx["fqty"] = mzl.Text.Trim();
            }
            if (mcd.Text != "")
            {
                newrowgx["fmcd"] = mcd.Text.Trim();
            }
            if (mkd.Text != "")
            {
                newrowgx["fmkd"] = mkd.Text.Trim();
            }
            if (mhd.Text != "")
            {
                newrowgx["FMhd"] = mhd.Text.Trim();
            }
            newrowgx["fbc"] = bc.Text;
            newrowgx["FJyy"] = jyy.Text;
            newrowgx["fbz"] = bz.Text;
            newrowgx["FJlr"] = jlr.Text;
            newrowgx["FJlrq"] = jlrq.Text;
            newrowgx["FJt"] = jt.Text;
            newrowgx["FItemID"] = wl.Text;
            newrowgx["FHj"] = true ;
            newrowgx["FRkd"] = (rkd.Text == "1") ? true : false;
            newrowgx["FICMOBillNo"] = comboBox1.Text;
            newrowgx["FBatchNo"] = label6.Text;
            ds.Tables["tgx"].Rows.Add(newrowgx);

            SqlCommandBuilder sb1 = new SqlCommandBuilder(gxjl);
            //gxjl.Update(ds.Tables["tgx"]);

            //插入数据到tycpgx  原料产品关系对照表   FYctm    FSycd   FCptm
            string sql = "select fyctm,fsycd,FCptm from Tylcpgx  where 1=2";

            SqlDataAdapter dtylgx = new SqlDataAdapter(sql, conn);

            dtylgx.Fill(ds, "gxb");
            //批量写入原料与产品关系表
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells["kycd"].Value == null)
                {
                    MessageBox.Show("必须录入膜使用长度");
                    ds.Tables["gxb"].Clear();
                    return;
                }
                DataRow newrow = ds.Tables["gxb"].NewRow();
                newrow["fyctm"] = dataGridView1.Rows[i].Cells["tm"].Value.ToString();


                try
                {
                    if (dataGridView1.Rows[i].Cells["yw"].Value.ToString() == "是")
                    {
                        newrow["fsycd"] = dataGridView1.Rows[i].Cells["kycd"].Value.ToString().Trim();
                    }
                    else
                    {
                        newrow["fsycd"] = dataGridView1.Rows[i].Cells["sycd1"].Value.ToString().Trim();
                    }
                }
                catch
                {
                    newrow["fsycd"] = dataGridView1.Rows[i].Cells["sycd1"].Value.ToString().Trim();
                }
                newrow["fcptm"] = gctm.Text;
                ds.Tables["gxb"].Rows.Add(newrow);

            }
            SqlCommandBuilder sb = new SqlCommandBuilder(dtylgx);
            gxjl.Update(ds.Tables["tgx"]);
            dtylgx.Update(ds.Tables["gxb"]);
            conn.Close();
            //string ptnr = label6.Text + "-" + qds.Text + "-" + i.ToString() + "(" + Globals.userid + ")" + "|" + tmhb.Split('|')[i] + "|" + label8.Text;
            //FPt pt = new FPt(ptnr, 1);
            string ptnr = label6.Text +  "(" + Globals.userid + ")" + "|" + gctm.Text + "|" + label8.Text;
            FPt pt = new FPt(ptnr, 1);
            clearjm();
            lgxl();
            dataGridView1.Rows.Clear();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("任务完成后批量入库", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;  //否就退出操作
            }

            //sqlcmd.Connection.Open();
            try
            {
                // DataSet ds = new DataSet();
                try
                {
                    conn.Open();
                }
                catch { }
                //统计产量
                string cl = "select sum(FMcd) cl from  tgx  where FICMOBillNo='" + comboBox1.Text + "'  and FSfrk=0  and frkd=1";
                SqlDataAdapter sd2 = new SqlDataAdapter(cl, conn);

                try
                {
                    ds.Tables["cl"].Clear();
                }
                catch { }
                sd2.Fill(ds, "cl");
                try
                {
                    if (ds.Tables["cl"].Rows[0][0].ToString() == "")
                    {
                        MessageBox.Show("没有符合入库的数据");
                        conn.Close();
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("没有符合入库的数据");
                    conn.Close();
                    return;
                }
                //获取单号
                //tj tj = new tj();
                //RK ck = new RK(tj);
                //ck.ShowDialog();

                //获取单据号和内码
                ArrayList al = Globals.Return_proc(conn);
                string sql_icstockbill = "select FBillerID,FSelTranType, FBrNo, FInterID, FTranType, FBillNo, FDate, FDeptID  from ICStockBill  where  1=2";

                SqlDataAdapter icstockbill = new SqlDataAdapter(sql_icstockbill, conn);
                try
                {

                    ds.Tables["icstockbill"].Clear();
                    ds.Tables["icstockbillentry"].Clear();
                }
                catch { }

                icstockbill.Fill(ds, "icstockbill");
                DataRow rk = ds.Tables["icstockbill"].NewRow();
                rk["FBillerID"] = 16394;   //制单人
                rk["FSelTranType"] = 85;
                rk["FBrNo"] = 0;
                rk["FInterID"] = al[1];
                rk["FTranType"] = 2;
                rk["FBillNo"] = al[0];
                rk["fdate"] = DateTime.Now.ToString("d");
                rk["FDeptID"] = comboBox2.SelectedValue;
                ds.Tables["icstockbill"].Rows.Add(rk);
                SqlCommandBuilder sb = new SqlCommandBuilder(icstockbill);
                icstockbill.Update(ds.Tables["icstockbill"]);
                string sql_icstockbillentry = "select FChkPassItem,FSourceInterId,FDCStockID,FDCSPID,FBrNo,FEntryID,FUnitID,FQty,FAuxQty,FItemID,FInterID,FQtyMust,FAuxQtyMust,FBatchNo,FSourceTranType,FSourceBillNo,FICMOBillNo,FICMOInterID from  ICStockBillEntry where 1=2";
                SqlDataAdapter icstockbillentry = new SqlDataAdapter(sql_icstockbillentry, conn);
                icstockbillentry.Fill(ds, "icstockbillentry");
                rk = ds.Tables["icstockbillentry"].NewRow();
                rk["FChkPassItem"] = 1058;
                rk["FSourceInterId"] = ydnm.Text;
                rk["FDCStockID"] = ck.Text;
                //rk["FDCStockID"] = tj.ck;//仓库
                rk["FDCSPID"] =cw.Text;//仓位
                rk["FBrNo"] = 0;
                rk["FEntryID"] = 1;
                rk["FUnitID"] = 783;
                rk["FAuxQty"] = rk["FQty"] = ds.Tables["cl"].Rows[0][0];
                rk["FItemID"] = wl.Text;
                rk["FInterID"] = al[1];
                rk["FAuxQtyMust"] = rk["FQtyMust"] = rwl.Text;

                rk["FBatchNo"] = label6.Text;
                rk["FSourceTranType"] = 85;
                rk["FSourceBillNo"] = comboBox1.Text;
                rk["FICMOBillNo"] = comboBox1.Text;
                rk["FICMOInterID"] = ydnm.Text;
                ds.Tables["icstockbillentry"].Rows.Add(rk);
                sb = new SqlCommandBuilder(icstockbillentry);
                icstockbillentry.Update(ds.Tables["icstockbillentry"]);
                //修改入库任务的工序数据入库状态
                Globals.E_proc(conn, comboBox1.Text);

                conn.Close();
                MessageBox.Show("入库完成");
            }
            catch
            {
                MessageBox.Show("入库失败");
            }

        }

        private void gctm_TextChanged(object sender, EventArgs e)
        {

        }

      

        private void comboBox1_DropDown(object sender, EventArgs e)
        {

            string sql = "select * from v_xdxx where FBillNo in(select FICMOBillNo from  V_Flrw where FItemID in(select  FBase3 from V_Gx  where FName='分切'))";
        
            comboBox1.DataSource = dt = sj.ds(sql, "rw", conn);
            comboBox1.DisplayMember = "FBillNo";
            comboBox1.ValueMember = "FBillNo";
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
            if (keyData == Keys.Up)
            {
                // e.Handled = false;
                return true;
            }
            if (keyData == Keys.Down)
            {
                // e.Handled = false;
                return true;
            }
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
    }
}
