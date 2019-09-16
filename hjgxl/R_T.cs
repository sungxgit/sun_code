using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data.SqlClient;
using System.Data;

namespace hjgxl
{
    class R_T
    {
        /// <summary>
        /// 获取datatable
        /// </summary>
        public R_T() { }
        public DataTable ds(string sql, string tbname,SqlConnection conn)
        {
            try
            {
                conn.Open();
            }
            catch { }
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, tbname);
            conn.Close();
            return ds.Tables[tbname];
            

        }
        public DataTable ds(string sql, SqlConnection conn)
        {
            try
            {
                conn.Open();
            }
            catch { }
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "tbname");
            conn.Close();
            return ds.Tables["tbname"];


        }
    }
}
