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
    public partial class FTmbd : jp
    {
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(Globals.connstr);
        DataTable dt, dt_gxl;
        R_T sj = new R_T();

        public FTmbd()
        {
            InitializeComponent();
        }
        private void clearkj() {
            label6.Text = "";
            gctm.Clear();
            jtmc.Text = "";
            mkd.Clear();
            gx.Clear();
            mhd.Clear();
            mzl.Clear();
            jyy.Clear();
            jlr.Clear();
            bc.Text = "";
            bz.Clear();
            daos.Clear();
            js.Clear();
            zxh.Clear();
            zxjs.Clear();

            label8.Text ="";//物料
            label7.Text = "";//客户名称
            wl.Clear();
          //  jlrq.;
            // wlid.Text = drArr[0]["fitemid"].ToString();//物料id
            wlid.Clear();
            ydnm.Clear();
            rwl.Clear();
            ck.Clear();
            cw.Clear();

            wlbz.Clear();//执行标准  f_104

            wlms.Clear();



        }
        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            clearkj();

            string sql = "select * from  tgx where FBarcode='" + dataGridView2.CurrentRow.Cells["FBarcode"].Value + "'";

            string sql1 = "select FYctm '原料条码',fname '物料',FSycd '使用长度' from  V_ylgx1 where FCptm='" + dataGridView2.CurrentRow.Cells["FBarcode"].Value + "'";
            dataGridView1.DataSource = sj.ds(sql1, "yl", conn);

            dt = sj.ds(sql, "gx", conn);
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            mcd.Text = dt.Rows[0]["fmcd"].ToString();
            comboBox1.Text = dt.Rows[0]["FICMOBillNo"].ToString();
            label6.Text = dt.Rows[0]["FBatchNo"].ToString();
            gctm.Text = dt.Rows[0]["FBarcode"].ToString();
            jtmc.Text = dt.Rows[0]["Fscjt"].ToString();
            mkd.Text = dt.Rows[0]["FMkd"].ToString();
            gx.Text = dt.Rows[0]["FWork"].ToString();
            mhd.Text = dt.Rows[0]["FMhd"].ToString();
            mzl.Text = dt.Rows[0]["FQty"].ToString();
            jyy.Text= dt.Rows[0]["Fjyy"].ToString();
            jlr.Text = dt.Rows[0]["fjlr"].ToString();
            bc.Text = dt.Rows[0]["fbc"].ToString();
            bz.Text = dt.Rows[0]["fbz"].ToString();
            daos.Text= dt.Rows[0]["fds"].ToString();
            js.Text= dt.Rows[0]["fjh"].ToString();
            zxh.Text= dt.Rows[0]["fXh"].ToString();
            zxjs.Text = dt.Rows[0]["Fjs"].ToString();
            jlrq.Text = dt.Rows[0]["FJlrq"].ToString();
            sql = "select FBillNo,FName,khmc,xsdd,fworkshop,fitemid,FNumber,finterid,fqty,FDefaultLoc,FSPID,FGMPBatchNo,FModel,jg,zxbz  from  v_xdxx1  where FBillNo='" + dataGridView2.CurrentRow.Cells["FICMOBillNo"].Value + "'";
            DataTable dt1 = sj.ds(sql, conn);
            try
            {
                wlkd.Text = dt1.Rows[0]["FModel"].ToString();
            }
            catch { }
            //label6.Text = dt1.Rows[0]["FGMPBatchNo"].ToString();//订单号
            label8.Text = dt1.Rows[0]["Fname"].ToString();//物料
            label7.Text = dt1.Rows[0]["khmc"].ToString();//客户名称
            wl.Text = dt1.Rows[0]["fitemid"].ToString();
            
            // wlid.Text = drArr[0]["fitemid"].ToString();//物料id
            wlid.Text = dt1.Rows[0]["fnumber"].ToString().Substring(0, 7);
            ydnm.Text = dt1.Rows[0]["finterid"].ToString();
            rwl.Text = dt1.Rows[0]["fqty"].ToString();
            ck.Text = dt1.Rows[0]["FDefaultLoc"].ToString();
            cw.Text = dt1.Rows[0]["FSPID"].ToString();

            wlbz.Text = dt1.Rows[0]["zxbz"].ToString();//执行标准  f_104
          
            wlms.Text = dt1.Rows[0]["jg"].ToString();

            sql1 = "select FComboBox ,FInteger from v_gx where fname='"+dataGridView2.CurrentRow.Cells["FWork"].Value+"' and fnumber='" + wlid.Text.ToString() + "'";
            
            DataTable trkd = sj.ds(sql1, "rkd", conn);
            sql1 = "select fname from  v_gx  where fnumber='" + wlid.Text.ToString() + "' and FInteger=1+" + trkd.Rows[0][1];
            try
            {
                ngx.Text = sj.ds(sql1, "gx", conn).Rows[0][0].ToString();
            }
            catch { }





            // string cl = "select sum(FMcd) cl from  tgx  where FICMOBillNo='" + comboBox1.SelectedValue + "'  and FWork='印刷'";
            // yscl.Text = sj.ds(cl, "cl", conn).Rows[0][0].ToString();






        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (gx.Text != "分切" & gx.Text != "制袋" & gx.Text != "包装")
            {
                string ptnr = gx.Text + "|" + label6.Text + "|" + label8.Text + "|" + gctm.Text + "|" + jtmc.Text + "|" + jlr.Text + "|" + jlrq.Text + "|" + jyy.Text + "|" + bc.Text + "|" + mcd.Text + "|" + ngx.Text + "|" + wlkd.Text;
                FPt pt = new FPt(ptnr);

            }
            if (gx.Text == "分切")
            {
                string ptnr = label6.Text +"-"+zxh.Text+ "-" + daos.Text + "-" + js.Text + "(" + Globals.userid + ")" +  "|" + gctm.Text + "|" + label8.Text;
                FPt pt = new FPt(ptnr, 1);
            }
            if (gx.Text == "包装")
            {
                string ptnr = label7.Text + "|" + label8.Text + "|" + wlms.Text + "|" + wlbz.Text + "|" + wlkd.Text + "|" + label6.Text + "|" + zxjs.Text + "卷|" + decimal.Parse(mzl.Text).ToString("#0.00") + "kg|" + zxh.Text + "|" + jlrq.Text + "|" + gctm.Text;
                FPt pt = new FPt(ptnr, 2);
            }

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
           // string sql = "select * from  tgx where ( FBarcode like '%"+tj.Text.Trim()+"%' or FBatchNo like '%"+tj.Text.Trim()+ "%') and FJlrq='"+ jlrq.Text+ "' order by id desc";
            string sql = "select * from  tgx where ( FBarcode like '%" + tj.Text.Trim() + "%' or FBatchNo like '%" + tj.Text.Trim() + "%')  order by id desc";

            dt = sj.ds(sql, conn);
            dataGridView2.DataSource = dt;
              
        }
    }
}
