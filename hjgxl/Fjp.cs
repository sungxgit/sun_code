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
using System.Collections;

namespace hjgxl
{
    public partial class Fjp : jp
    {
        SqlConnection   conn = new SqlConnection(Globals.connstr);
        DataSet ds = new DataSet();
        R_T sj = new R_T();
        DataTable dt, dt_ycl, dt_gxl;
        public Fjp()
        {
            InitializeComponent();
            jlr.Text = Globals.usernm;
            panel3.Visible = false;
            string sql = "select fname from  t_SubMessage where FTypeID ='10001'";
            jtmc.DataSource = new R_T().ds(sql, "jt", new SqlConnection(Globals.connstr));
            //// jt.Text = "";
            jtmc.DisplayMember = "FName";
            jtmc.ValueMember = "FName";
            jtmc.SelectedIndex = -1;
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            //根据车间内码查询                                                                    
            //   string sql = "select FBillNo,FName,khmc,xsdd,fworkshop,fitemid,FNumber from  v_xdxx where fworkshop='4554' and  fbillno like '%" + comboBox1.Text + "%'";
            // string sql = "select FBillNo,FName,khmc,xsdd,fworkshop,fitemid,FNumber,finterid,fqty from  v_xdxx where fworkshop='"+comboBox2.SelectedValue+"' and  fbillno like '%" + comboBox1.Text + "%' and LEFT(FNumber,7) in (select  FNumber from V_Gx  where FName='检品')";
            string sql = "select FBillNo,FName,khmc,xsdd,fworkshop,fitemid,FNumber,finterid,fqty,FDefaultLoc,FSPID,FGMPBatchNo,FModel from  v_xdxx1 where FBillNo in(select FICMOBillNo from  V_Flrw)  and fworkshop='" + comboBox2.SelectedValue + "' and (fbillno like '%"+comboBox1.Text+ "%' or FGMPBatchNo like '%" + comboBox1.Text + "%')";

            comboBox1.DataSource = dt = sj.ds(sql, "rw", conn);
            comboBox1.DisplayMember = "FBillNo";
            comboBox1.ValueMember = "FBillNo";
            //DateTime.UtcNow
        }
        //保存
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
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
            //DataSet ds = new DataSet();
            //修改tyl表的 FSylength=FSylength-sycd
            string insertsql = "SELECT  FBarcode, FWork, FQty, FMcd, FMkd, FMhd, FJs, FBc, FJyy, FBz, FUnitID, FICMOID, FItemID, FJlr, FJlrq,FRkd, FSfrk, FSfyw, FICMOBillNo ,FBatchNo,fscjt FROM  Tgx where 1=2 ";
            SqlDataAdapter gxjl = new SqlDataAdapter(insertsql, conn);
            gxjl.Fill(ds, "tgx");
            DataRow newrowgx = ds.Tables["tgx"].NewRow();
            newrowgx["FBarcode"] = gctm.Text.Trim();
            newrowgx["FWork"] = "检品";
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
            newrowgx["Fscjt"] = jtmc.Text;
            newrowgx["FItemID"] = wl.Text;
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
                        newrow["fsycd"] = dataGridView1.Rows[i].Cells["kycd"].Value.ToString();
                    }
                    else
                    {
                        newrow["fsycd"] = dataGridView1.Rows[i].Cells["sycd1"].Value.ToString();
                    }
                }
                catch { newrow["fsycd"] = dataGridView1.Rows[i].Cells["sycd1"].Value.ToString(); }
                newrow["fcptm"] = gctm.Text;
                ds.Tables["gxb"].Rows.Add(newrow);

            }
            SqlCommandBuilder sb = new SqlCommandBuilder(dtylgx);
            gxjl.Update(ds.Tables["tgx"]);
            dtylgx.Update(ds.Tables["gxb"]);
            conn.Close();
            string ptnr = "检品|" + label6.Text + "|" + label8.Text + "|" + gctm.Text + "|" + jtmc.Text + "|" + Globals.usernm + "|" + jlrq.Text + "|" + jyy.Text + "|" + bc.Text + "|" + mcd.Text+"|"+ngx.Text+"|"+wlkd.Text;
            FPt pt = new FPt(ptnr);
            try
            {
                yscl.Text = (decimal.Parse(yscl.Text.Trim()) + decimal.Parse(mcd.Text.Trim())).ToString();
            }
            catch {
                yscl.Text = (decimal.Parse(mcd.Text.Trim())).ToString();

            }
            clearjm();
            dataGridView1.Rows.Clear();
            if (!lgxl())
            {
                MessageBox.Show("领料失败");
            }
        }
        //入库
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
                string sql_icstockbill = "select FBillerID,FSelTranType, FBrNo, FInterID, FTranType, FBillNo, FDate,FStatus, FDeptID,FCheckerID,FCheckDate   from ICStockBill  where  1=2";

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
                //rk["FCheckerID"] = 16394;
                //rk["FStatus"] = 1;
                //rk["FCheckDate"] = DateTime.Now.ToString("d");
                rk["FSelTranType"] = 85;
                rk["FBrNo"] = 0;
                rk["FInterID"] = al[1];
                rk["FTranType"] = 2;
                rk["FBillNo"] = al[0];
                rk["fdate"] = DateTime.Now.ToString("d");
                rk["FDeptID"] = comboBox2.SelectedValue;
                      //审核标志
                ds.Tables["icstockbill"].Rows.Add(rk);
                SqlCommandBuilder sb = new SqlCommandBuilder(icstockbill);
                icstockbill.Update(ds.Tables["icstockbill"]);
                string sql_icstockbillentry = "select FChkPassItem,FSourceInterId,FDCStockID,FDCSPID,FBrNo,FEntryID,FUnitID,FQty,FAuxQty,FItemID,FInterID,FQtyMust,FAuxQtyMust,FBatchNo,FSourceTranType,FSourceBillNo,FICMOBillNo,FICMOInterID from  ICStockBillEntry where 1=2";
                SqlDataAdapter icstockbillentry = new SqlDataAdapter(sql_icstockbillentry, conn);
                icstockbillentry.Fill(ds, "icstockbillentry");
                rk = ds.Tables["icstockbillentry"].NewRow();
                rk["FChkPassItem"] = 1058;
                rk["FSourceInterId"] = ydnm.Text;
                rk["FDCStockID"] =ck.Text;
                //rk["FDCStockID"] = tj.ck;//仓库
                rk["FDCSPID"] = cw.Text;//仓位
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
                MessageBox.Show("入库完成。");
            }
            catch
            {
                MessageBox.Show("入库失败");
            }

        }



        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                DataRow[] drArr = dt.Select("FBillNo = '" + comboBox1.SelectedValue + "'");
                wlkd.Text = drArr[0]["FModel"].ToString();
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

                string cl = "select sum(FMcd) cl from  tgx  where FICMOBillNo='" + comboBox1.SelectedValue + "'  and FWork='检品'";
                yscl.Text = sj.ds(cl, "cl", conn).Rows[0][0].ToString();
                cbdx.Text = rt_cbdx(comboBox1.SelectedValue.ToString());  //获取成本对象
                if (!lgxl())
                {
                    MessageBox.Show("领料失败");
                }
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

        public Boolean lyl()
        {
            try
            {
                string sql = "select ID, FItemID, FBatchNo, FRkdh, FYltm, Fqty, FLength, FLength- yycd kycd , FSyqty, FZt, FICMOBillNo, FLy, FName from  v_tyl  where FItemID in (select  fitemID from  v_flrw  where   FICMOBillNo='" + comboBox1.SelectedValue + "')  and FLength- yycd>0  ";

                try
                {
                    dt_ycl.Clear();
                }
                catch { }
                dt_ycl = sj.ds(sql, "ycl", conn);
                string sql1 = "select FComboBox ,FInteger from v_gx where fname='检品' and fnumber='" + wlid.Text.ToString() + "'";
                DataTable trkd = new DataTable();
                trkd = sj.ds(sql1, "rkd", conn);
                rkd.Text = trkd.Rows[0][0].ToString();
                sql1 = "select fname from  v_gx  where fnumber='" + wlid.Text.ToString() + "' and FInteger=1+" + trkd.Rows[0][1];
                ngx.Text = sj.ds(sql1, "gx", conn).Rows[0][0].ToString();
                dataGridView3.DataSource = dt_ycl;
                return true;
            }
            catch
            {

                return false;
            }
        }

        private void comboBox2_DropDown(object sender, EventArgs e)
        {
            try
            {
                dt_gxl.Clear();
            }
            catch { }
            clearjm();
            comboBox1.SelectedIndex = -1;
            dataGridView1.Rows.Clear();

            string sql = "select FItemID,FName from  t_Department where fname like '%印刷%'";
             
            comboBox2.DataSource = sj.ds(sql, "cj", conn);
            comboBox2.DisplayMember = "FName";
            comboBox2.ValueMember = "FItemID";
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
                }
                catch {
                    MessageBox.Show("条码错误");
                    return;
                }

                // dataGridView1.CurrentCell 
                //扫描原料条码  在dt_ycl中 查询
                if (dt_gxl != null)
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
            }
            if (e.KeyCode == Keys.Enter & dataGridView1.CurrentCell.ColumnIndex == 4)//是否选择的是第6个单元格
            {
                try
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
                catch {
                    MessageBox.Show("请输入正确的长度");
                }
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

        private void mcd_Click(object sender, EventArgs e)
        {
            try
            {
                decimal cd = 0;
                //for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                //{
                //    cd = cd + decimal.Parse(dataGridView1.Rows[i].Cells["sycd1"].Value.ToString());
                //    //  dataGridView1.Rows[i].Cells["sycd"].Value 



                //}
                //mcd.Text = cd.ToString();
                if (gctm.Text == "")
                {
                    gctm.Text = wl.Text.PadLeft(5, '0') + Globals.GetTimeStamp();
                }
                mkd.Text = dataGridView1.Rows[0].Cells["mk"].Value.ToString();
                mhd.Text = dataGridView1.Rows[0].Cells["mh"].Value.ToString();
            }
            catch { }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("退料将关闭任务单", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;  //否就退出操作
            }
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = conn;
            //sqlcmd.Connection.Open();
            try
            {
                // DataSet ds = new DataSet();
                try
                {
                    conn.Open();
                }
                catch { }
                //统计材料用量
                //string ylsyl = "select sum(FSycd) from  Tylcpgx  where FCptm in(select FBarcode from  Tgx  where FICMOBillNo ='"+comboBox1.Text+"' and FWork='印刷')";
                // string ylsyl = "select sum( fsycd) * ISNULL(fcoefficient , 1) as yl,fitemid,fcoefficient from  V_ylgx where FCptm in(select FBarcode from  Tgx  where FICMOBillNo ='" + comboBox1.Text + "' and FWork='印刷')  group by fitemid,coefficient";
                //领用量
                // string ly = "select sum(fqty)lyl ,FItemID from  v_flrw where FICMOBillNo='"+comboBox1.Text+"' group by FItemID";
                //string ylsyl = "select 0-lyl+yl tll,tb1.FItemID,FBatchNo from (select sum( fsycd) * ISNULL(fcoefficient , 1) as yl,fitemid,fcoefficient,FBatchNo from  V_ylgx where FCptm in(select FBarcode from  Tgx  where FICMOBillNo ='" + comboBox1.Text + "' and FWork='印刷' and FHj=0)  group by fitemid,fcoefficient,FBatchNo)tb1  left join(select sum(fqty)lyl ,FItemID from  v_flrw where FICMOBillNo='" + comboBox1.Text + "' group by FItemID )tb2 on tb1.fitemid= tb2.FItemID  where tb1.FItemID is not null";
                string ylsyl = "select 0-lyl+yl tll,tb1.FItemID,tb1.FBatchNo from (select sum( fsycd) * ISNULL(fcoefficient , 1) as yl,fitemid,fcoefficient,FBatchNo  from  V_ylgx where FCptm in(select FBarcode from  Tgx  where FICMOBillNo ='" + comboBox1.Text + "' and FWork='印刷')  group by fitemid,fcoefficient,FBatchNo)tb1  left join(select sum(fqty)lyl ,FItemID,FBatchNo from  v_flrw where FICMOBillNo='" + comboBox1.Text + "' group by FItemID,FBatchNo ) tb2 on tb1.fitemid= tb2.FItemID  and tb1.Fbatchno=tb2.FBatchNo  where tb1.fitemid  is not null and 0-lyl+yl<0";
                string blql = "select 0-lyl+yl tll,tb1.FItemID,tb1.FBatchNo from (select sum( fsycd) * ISNULL(fcoefficient , 1) as yl,fitemid,fcoefficient,FBatchNo  from  V_ylgx where FCptm in(select FBarcode from  Tgx  where FICMOBillNo ='" + comboBox1.Text + "' and FWork='印刷')  group by fitemid,fcoefficient,FBatchNo)tb1  left join(select sum(fqty)lyl ,FItemID,FBatchNo from  v_flrw where FICMOBillNo='" + comboBox1.Text + "' group by FItemID,FBatchNo ) tb2 on tb1.fitemid= tb2.FItemID  and tb1.Fbatchno=tb2.FBatchNo  where tb1.fitemid  is not null and 0-lyl+yl>0";
                tlbl(ylsyl, conn, -1, "退料");
                tlbl(blql, conn, 1, "补料");
                Globals.E_proc_closeicmo(conn, comboBox1.Text);
                /***************************************************/



            }
            catch { }
            string sql = "select FBillNo,FName,khmc,xsdd,fworkshop,fitemid,FNumber,finterid,fqty,FDefaultLoc,FSPID,FGMPBatchNo,FModel from  v_xdxx where FBillNo in(select FICMOBillNo from  V_Flrw)  and fworkshop='" + comboBox2.SelectedValue + "'";
            comboBox1.DataSource = null;
            comboBox1.DataSource = dt = sj.ds(sql, "rw", conn);
            comboBox1.DisplayMember = "FBillNo";
            comboBox1.ValueMember = "FBillNo";
           
        }

      

        public Boolean lgxl()
        {

            try
            {

                //where cast(FItemID as nvarchar(15)) +cast(FBatchNo as nvarchar(15))  in (select cast(FItemID as nvarchar(15)) + cast(FBatchNo as nvarchar(15))
                string sql = "select  FBarcode '条码',FBatchNo '批次',kycd '可用长度' ,FMkd '膜宽',FMhd '膜厚'  from  V_Kygclsl  where fitemid in(select FBase3 from  V_Gx where FNumber='" + wlid.Text.ToString() + "' and FName='检品' ) and FICMOBillNo='"+comboBox1.SelectedText+"'";

                try
                {
                    dt_gxl.Clear();
                }
                catch { }
                dt_gxl = sj.ds(sql, "dt_gxl", conn);
                dataGridView3.DataSource = dt_gxl;
                string sql1 = "select FComboBox ,FInteger from v_gx where fname='检品' and fnumber='" + wlid.Text.ToString() + "'";
                DataTable trkd = new DataTable();
                trkd = sj.ds(sql1, "rkd", conn);
                rkd.Text = trkd.Rows[0][0].ToString();
                sql1 = "select fname from  v_gx  where fnumber='" + wlid.Text.ToString() + "' and FInteger=1+" + trkd.Rows[0][1];
                ngx.Text = sj.ds(sql1, "gx", conn).Rows[0][0].ToString();
                return true;
            }
            catch
            {

                return false;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

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
        public string rt_cbdx(string rw)
        {
            string sql = "select FCostOBJID from  v_flrw   where FICMOBillNo='" + rw + "'";

            try
            {
                return sj.ds(sql, "cbdx", conn).Rows[0][0].ToString();
            }
            catch
            {
                return "0";
            }
        }
        public void tlbl(string sql,SqlConnection conn,int zt,string msg) {
            try { conn.Open(); } catch { }
            SqlDataAdapter sd2 = new SqlDataAdapter(sql, conn);

            try
            {
                ds.Tables["ylsyl"].Clear();
            }
            catch { }
            sd2.Fill(ds, "ylsyl");
            try
            {   //与领用做比对
                if (ds.Tables["ylsyl"].Rows[0][0].ToString() == "")
                {
                    MessageBox.Show("没有符合"+msg+"的数据");
                    conn.Close();
                    return;
                }


            }
            catch
            {
                MessageBox.Show("没有符合"+msg+"的数据");
                conn.Close();
                return;
            }
            tj tj = new tj();
            RK ck = new RK(tj);
            ck.ShowDialog();



            /***************************************************/

            ArrayList al = Globals.Return_proc(conn, "24");
            string sql_icstockbill = "select FBillerID,FSelTranType, FBrNo, FInterID, FTranType, FBillNo, FDate, FDeptID,Frob,FPurposeID  from ICStockBill  where  1=2";

            SqlDataAdapter icstockbill = new SqlDataAdapter(sql_icstockbill, conn);
            try
            {

                ds.Tables["icstockbill"].Clear();
                ds.Tables["icstockbillentry"].Clear();
            }
            catch { }

            icstockbill.Fill(ds, "icstockbill");
            DataRow rk = ds.Tables["icstockbill"].NewRow();
            rk["FPurposeID"] = 12000;
            rk["FBillerID"] = 16394;   //制单人
            rk["FSelTranType"] = 85;
            rk["FBrNo"] = 0;
            rk["FInterID"] = al[1];
            rk["FTranType"] = 24;
            rk["FBillNo"] = al[0];
            rk["fdate"] = DateTime.Now.ToString("d");
            rk["FDeptID"] = comboBox2.SelectedValue;
            rk["Frob"] = zt;
            ds.Tables["icstockbill"].Rows.Add(rk);
            SqlCommandBuilder sb = new SqlCommandBuilder(icstockbill);


            string sql_icstockbillentry = "select FChkPassItem,FSourceInterId,FDCStockID,FSCStockID, FDCSPID,FBrNo,FEntryID,FUnitID,FQty,FAuxQty,FItemID,FInterID,FQtyMust,FAuxQtyMust,FBatchNo,FSourceTranType,FSourceBillNo,FICMOBillNo,FICMOInterID,FCostOBJID from  ICStockBillEntry where 1=2";
            SqlDataAdapter icstockbillentry = new SqlDataAdapter(sql_icstockbillentry, conn);
            icstockbillentry.Fill(ds, "icstockbillentry");
            sb = new SqlCommandBuilder(icstockbillentry);
            for (int i = 0; i < ds.Tables["ylsyl"].Rows.Count; i++)
            {
                rk = ds.Tables["icstockbillentry"].NewRow();
                rk["FChkPassItem"] = 1058;
                rk["FSourceInterId"] = ydnm.Text;
                rk["FSCStockID"] = tj.ck;//仓库
                if (tj.cw != "0")
                {
                    rk["FDCSPID"] = tj.cw;//仓位
                }
                rk["FCostOBJID"] = cbdx.Text;

                rk["FBrNo"] = 0;
                rk["FEntryID"] = i + 1;
                rk["FUnitID"] = 259;
                rk["FAuxQty"] = rk["FQty"] = ds.Tables["ylsyl"].Rows[i][0];
                rk["FItemID"] = ds.Tables["ylsyl"].Rows[i][1];
                rk["FInterID"] = al[1];
                rk["FAuxQtyMust"] = rk["FQtyMust"] = ds.Tables["ylsyl"].Rows[i][0];
                rk["FBatchNo"] = ds.Tables["ylsyl"].Rows[i][2];
                //rk["FBatchNo"] = label6.Text;
                rk["FSourceTranType"] = 85;
                rk["FSourceBillNo"] = comboBox1.Text;
                rk["FICMOBillNo"] = comboBox1.Text;
                rk["FICMOInterID"] = ydnm.Text;
                ds.Tables["icstockbillentry"].Rows.Add(rk);
            }
            icstockbill.Update(ds.Tables["icstockbill"]);
            icstockbillentry.Update(ds.Tables["icstockbillentry"]);
            //修改入库任务的工序数据入库状态

            MessageBox.Show(msg+"完成。");
            conn.Close();

        }
    }
}
