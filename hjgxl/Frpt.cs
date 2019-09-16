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
    public partial class Frpt : Form
    {
        SqlConnection conn = new SqlConnection(Globals.connstr);
        DataTable Tgx;
        R_T sj = new R_T();


        public Frpt()
        {
            InitializeComponent();
            this.crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
        }
        public Frpt(string dh, Boolean js)
        {
            InitializeComponent();
            ddh.Text = dh;
            jszl.Checked = js;
            this.crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void Frpt_Load(object sender, EventArgs e)
        {
          
        }
    
    

        private void button1_Click(object sender, EventArgs e)
        {
            CrystalReport2 cr = new CrystalReport2();
            if (jszl.Checked)
            {
                string sql1 = "select sum(fqty) /COUNT(fqty) dyzl from  tgx  where  FBatchNo='" + ddh.Text.Trim() + "'and(tgx.FWork = '包装' or tgx.FWork = '制袋') ";
                DataTable dyzl = sj.ds(sql1, "tgx", conn);
               string dgzl= decimal.Parse(dyzl.Rows[0][0].ToString()).ToString("#0.00");



                string sql = "select ''FBatchNo,''fname,''khmc,''FModel,'' hj, '' x1,'' l1,'' x2,'' l2,'' x3,'' l3,'' x4,'' l4,'' x5,'' l5 from tgx where 1=2 ";
                Tgx = sj.ds(sql, "Tgx", conn);
                //sql = "select sum(fqty) from tgx where FBatchNo='" + ddh.Text.Trim() + "'  and (tgx.FWork='包装' or tgx.FWork='制袋') and(tgx.FXh>='" + x1.Text.Trim() + "') and (tgx.FXh<='" + x2.Text.Trim() + "') ";
                //DataTable dt1 = sj.ds(sql, "Tgx1", conn);



                sql = "select * from tgx,v_xdxx1 where tgx.FBatchNo=v_xdxx1.FGMPBatchNo  and tgx.FItemID=v_xdxx1.FItemID and tgx.FBatchNo='" + ddh.Text.Trim() + "'  and (tgx.FWork='包装' or tgx.FWork='制袋') and(tgx.FXh>='" + x1.Text.Trim() + "') and (tgx.FXh<='" + x2.Text.Trim() + "' ) order by fxh ";
                DataTable dt = sj.ds(sql, "Tgx", conn);
                DataRow rk;
                string hjzl = (decimal.Parse(dgzl) * dt.Rows.Count).ToString("#0.00");
                for (int i = 0; i < dt.Rows.Count; i += 5)
                {
                    rk = Tgx.NewRow();
                    rk["x1"] = dgzl;

                    rk["l1"] = dt.Rows[i]["FXh"];
                    rk["fbatchno"] = dt.Rows[i]["fbatchno"];
                    rk["Fname"] = dt.Rows[i]["Fname"];
                    rk["FModel"] = dt.Rows[i]["FModel"];
                    rk["khmc"] = dt.Rows[i]["khmc"];
                    rk["hj"] = hjzl;
                    if (i + 1 < dt.Rows.Count)
                    {
                        rk["x2"] = dgzl;

                        rk["l2"] = dt.Rows[i + 1]["FXh"];

                        if (i + 2 < dt.Rows.Count)
                        {
                            rk["x3"] = dgzl;

                            rk["l3"] = dt.Rows[i + 2]["FXh"];
                            if (i + 3 < dt.Rows.Count)
                            {
                                rk["x4"] = dgzl;

                                rk["l4"] = dt.Rows[i + 3]["FXh"];
                                if (i + 4 < dt.Rows.Count)
                                {
                                    rk["x5"] = dgzl;

                                    rk["l5"] = dt.Rows[i + 4]["FXh"];
                                }
                            }
                        }
                    }

                    Tgx.Rows.Add(rk);

                }


               // CrystalReport2 cr = new CrystalReport2();
                cr.SetDataSource(Tgx);
            
                crystalReportViewer1.ReportSource = cr;
       
             
              
             
             

            }

            else
            {


              
                string sql = "select ''FBatchNo,''fname,''khmc,''FModel,'' hj, '' x1,'' l1,'' x2,'' l2,'' x3,'' l3,'' x4,'' l4,'' x5,'' l5 from tgx where 1=2 ";
                Tgx = sj.ds(sql, "Tgx", conn);
                sql = "select sum(fqty) from tgx where FBatchNo='" + ddh.Text.Trim() + "'  and (tgx.FWork='包装' or tgx.FWork='制袋') and(tgx.FXh>='" + x1.Text.Trim() + "') and (tgx.FXh<='" + x2.Text.Trim() + "') ";
                DataTable dt1 = sj.ds(sql, "Tgx1", conn);



                sql = "select * from tgx,v_xdxx1 where tgx.FBatchNo=v_xdxx1.FGMPBatchNo  and tgx.FItemID=v_xdxx1.FItemID and tgx.FBatchNo='" + ddh.Text.Trim() + "'  and (tgx.FWork='包装' or tgx.FWork='制袋') and(tgx.FXh>='" + x1.Text.Trim() + "') and (tgx.FXh<='" + x2.Text.Trim() + "') order by fxh";
                DataTable dt = sj.ds(sql, "Tgx", conn);
                DataRow rk;

                for (int i = 0; i < dt.Rows.Count; i += 5)
                {
                    rk = Tgx.NewRow();
                    rk["x1"] = dt.Rows[i]["FQty"].ToString();

                    rk["l1"] = dt.Rows[i]["FXh"];
                    rk["fbatchno"] = dt.Rows[i]["fbatchno"];
                    rk["Fname"] = dt.Rows[i]["Fname"];
                    rk["FModel"] = dt.Rows[i]["FModel"];
                    rk["khmc"] = dt.Rows[i]["khmc"];
                    rk["hj"] = dt1.Rows[0][0];
                    if (i + 1 < dt.Rows.Count)
                    {
                        rk["x2"] = dt.Rows[i + 1]["FQty"];

                        rk["l2"] = dt.Rows[i + 1]["FXh"];

                        if (i + 2 < dt.Rows.Count)
                        {
                            rk["x3"] = dt.Rows[i + 2]["FQty"];

                            rk["l3"] = dt.Rows[i + 2]["FXh"];
                            if (i + 3 < dt.Rows.Count)
                            {
                                rk["x4"] = dt.Rows[i + 3]["FQty"];

                                rk["l4"] = dt.Rows[i + 3]["FXh"];
                                if (i + 4 < dt.Rows.Count)
                                {
                                    rk["x5"] = dt.Rows[i + 4]["FQty"];

                                    rk["l5"] = dt.Rows[i + 4]["FXh"];
                                }
                            }
                        }
                    }

                    Tgx.Rows.Add(rk);

                }

              
             

                //ReportDocument rc = new ReportDocument();
                //rc.Load("CrystalReport2.rpt");
                //rc.SetDataSource(Tgx);
                
                //this.crystalReportViewer1.ReportSource = rc;


                cr.SetDataSource(Tgx);

                crystalReportViewer1.ReportSource = cr;


            }
        }

       
    }
}
