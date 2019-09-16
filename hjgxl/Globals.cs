using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Collections;
using System.Data;

namespace hjgxl
{

    class Globals
    {
        public static string connstr = Encrypt.Encrypt.Decode(Encrypt.Encrypt.getConfig("config.xml", "sqlserver"), "1", "2");
        public static string userid;
        public static string usernm;
        public static string ckh = Encrypt.Encrypt.getConfig("config.xml", "com");
        public static string btl = Encrypt.Encrypt.getConfig("config.xml", "baudrate");
        public static bool CheckNumber(string number)
        {
            if (string.IsNullOrEmpty(number))
            {
                return false;
            }
            Regex regex = new Regex(@"^\d+(\.\d+)?$");
            if (regex.IsMatch(number))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static ArrayList Return_proc(SqlConnection conn)
        {
            ArrayList al = new ArrayList();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = conn;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "p_BM_GetBillNo";
            sqlcmd.Parameters.Add(new SqlParameter("@ClassType", SqlDbType.Int));
            sqlcmd.Parameters["@ClassType"].Value = "2";
            sqlcmd.Parameters.Add(new SqlParameter("@BillNo", SqlDbType.VarChar, 200));
            sqlcmd.Parameters["@BillNo"].Direction = ParameterDirection.Output;
            sqlcmd.ExecuteNonQuery();
            string rkdh = sqlcmd.Parameters["@BillNo"].Value.ToString();
            //获取id
            al.Add(rkdh);
            sqlcmd.CommandText = "GetICMaxNum";
            sqlcmd.Parameters.Clear();
            sqlcmd.Parameters.Add(new SqlParameter("@TableName", SqlDbType.VarChar, 50));
            sqlcmd.Parameters["@TableName"].Value = "icstockbill";
            sqlcmd.Parameters.Add(new SqlParameter("@FInterID", SqlDbType.Int));
            sqlcmd.Parameters["@FInterID"].Direction = ParameterDirection.Output;
            sqlcmd.ExecuteNonQuery();
            string finterid = sqlcmd.Parameters["@FInterID"].Value.ToString();
            al.Add(finterid);
            return al;

        }
        public static ArrayList Return_proc(SqlConnection conn, string tp)
        {
            ArrayList al = new ArrayList();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = conn;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "p_BM_GetBillNo";
            sqlcmd.Parameters.Add(new SqlParameter("@ClassType", SqlDbType.Int));
            sqlcmd.Parameters["@ClassType"].Value = tp;
            sqlcmd.Parameters.Add(new SqlParameter("@BillNo", SqlDbType.VarChar, 200));
            sqlcmd.Parameters["@BillNo"].Direction = ParameterDirection.Output;
            sqlcmd.ExecuteNonQuery();
            string rkdh = sqlcmd.Parameters["@BillNo"].Value.ToString();
            //获取id
            al.Add(rkdh);
            sqlcmd.CommandText = "GetICMaxNum";
            sqlcmd.Parameters.Clear();
            sqlcmd.Parameters.Add(new SqlParameter("@TableName", SqlDbType.VarChar, 50));
            sqlcmd.Parameters["@TableName"].Value = "icstockbill";
            sqlcmd.Parameters.Add(new SqlParameter("@FInterID", SqlDbType.Int));
            sqlcmd.Parameters["@FInterID"].Direction = ParameterDirection.Output;
            sqlcmd.ExecuteNonQuery();
            string finterid = sqlcmd.Parameters["@FInterID"].Value.ToString();
            al.Add(finterid);
            return al;

        }
        public static string get_tmlsh(SqlConnection conn)
        {
            //try
            //{
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = conn;
            try { conn.Open(); }
            catch { }
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "p_lsh";
            sqlcmd.Parameters.Add(new SqlParameter("@lsh", SqlDbType.NVarChar, 5));
            sqlcmd.Parameters["@lsh"].Direction = ParameterDirection.Output;
            sqlcmd.Parameters.Add(new SqlParameter("@lx", SqlDbType.NVarChar, 10));
            sqlcmd.Parameters["@lx"].Value = "";
            sqlcmd.ExecuteNonQuery();
            return sqlcmd.Parameters["@lsh"].Value.ToString();
            //}
            //catch {

            //    return "";

            //}



        }

        public static void E_proc_closeicmo(SqlConnection conn, String tj)
        {
            try { conn.Open(); } catch { }
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = conn;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "gbrwd";
            sqlcmd.Parameters.Clear();
            sqlcmd.Parameters.Add(new SqlParameter("@BillNo", SqlDbType.VarChar, 50));
            sqlcmd.Parameters["@BillNo"].Value = tj;
            sqlcmd.ExecuteNonQuery();
        }
        public static void E_proc(SqlConnection conn,String tj)
        {
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = conn;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "updaterkzt";
            sqlcmd.Parameters.Clear();
            sqlcmd.Parameters.Add(new SqlParameter("@BillNo", SqlDbType.VarChar, 50));
            sqlcmd.Parameters["@BillNo"].Value = tj;
            sqlcmd.ExecuteNonQuery();

            sqlcmd.CommandText = "CheckInventory";
            sqlcmd.Parameters.Clear();
            sqlcmd.ExecuteNonQuery();
        }
        public static void E_proc_1(SqlConnection conn, String tj)
        {
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = conn;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "updaterkzt1";
            sqlcmd.Parameters.Clear();
            sqlcmd.Parameters.Add(new SqlParameter("@BillNo", SqlDbType.VarChar, 500));
            sqlcmd.Parameters["@BillNo"].Value = tj;
            sqlcmd.ExecuteNonQuery();
            sqlcmd.CommandText = "CheckInventory";
            sqlcmd.Parameters.Clear();
            sqlcmd.ExecuteNonQuery();
        }

        public static void delyl(SqlConnection conn, String tj)
        {
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = conn;
            try
            {
                sqlcmd.Connection.Open();
            }catch{ }
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "delycl ";
            sqlcmd.Parameters.Clear();
            sqlcmd.Parameters.Add(new SqlParameter("@BillNo", SqlDbType.VarChar, 500));
            sqlcmd.Parameters["@BillNo"].Value = tj;
            sqlcmd.ExecuteNonQuery();
        }


        public static string GetTimeStamp()
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(2019, 1, 1, 0, 0, 0, 0));
            DateTime nowTime = DateTime.Now;
            long unixTime = (long)System.Math.Round((nowTime - startTime).TotalMilliseconds, MidpointRounding.AwayFromZero);
            return unixTime.ToString();
        }
        //计算重量
        public static int zljs(decimal cd,decimal kd,decimal hd,decimal md){
            return int.Parse(Math.Floor(cd / 1000000 * kd * hd * md).ToString());
       }
    }
}