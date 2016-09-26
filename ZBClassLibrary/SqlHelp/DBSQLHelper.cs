using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data ;
using System.Data.SqlClient;
using System.Security.Cryptography ;
using System.IO ;

namespace ZBClassLibrary
{
    public class DBSQLHelper
    {
        private static string conStr_String = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        /// <summary>
        /// 执行一条SQL语句返回它受影响的行数
        /// </summary>
        /// <param name="cmdType">CommandType对象下面的属性</param>
        /// <param name="sqlText">被执行的SQL语句</param>
        /// <param name="cmdParms">SQL语句中的参数及其的赋值</param>
        /// <returns>返回该SQL语句受影响的行数</returns>
        public static int ExecuteNonQuery(CommandType cmdType, string sqlText, params SqlParameter[] cmdParms)
        {
            SqlConnection conn = new SqlConnection(conStr_String);
            SqlCommand cmd = new SqlCommand();

            try
            {
                PrepareCommand(conn, cmd, cmdType, sqlText, cmdParms);
                int sumRow = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return sumRow;
            }
            catch
            {
                conn.Dispose();
                conn.Close();
                throw;
            }
            finally
            {
                conn.Dispose();
                conn.Close();
            }
        }

        public static int ExecuteNonQuery(string sqlText)
        {
            SqlConnection conn = new SqlConnection(conStr_String);
            SqlCommand cmd = new SqlCommand();

            try
            {
                PrepareCommand(conn, cmd, CommandType.Text, sqlText, null);
                int sumRow = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return sumRow;
            }
            catch
            {
                conn.Dispose();
                conn.Close();
                throw;
            }
            finally
            {
                conn.Dispose();
                conn.Close();
            }

        }

        /// <summary>
        /// 执行一条SQL语句返回一个DataReader对象
        /// </summary>
        /// <param name="cmdType">CommandType对象下面的属性</param>
        /// <param name="sqlText">被执行的SQL语句</param>
        /// <param name="cmdParms">SQL语句中的参数及其的赋值</param>
        /// <returns>返回一个DataReader对象</returns>
        public static SqlDataReader ExecuteReader(CommandType cmdType, string sqlText, params SqlParameter[] cmdParms)
        {
            SqlConnection conn = new SqlConnection(conStr_String);
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(conn, cmd, cmdType, sqlText, cmdParms);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return dr;
            }
            catch
            {
                conn.Dispose();
                conn.Close();
                throw;
            }
        }

        public static SqlDataReader ExecuteReader(string sqlText)
        {
            SqlConnection conn = new SqlConnection(conStr_String);
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(conn, cmd, CommandType.Text, sqlText, null);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return dr;
            }
            catch
            {
                conn.Dispose();
                conn.Close();
                throw;
            }
        }


        /// <summary>
        /// 得到个DataSet 的结果集
        /// </summary>
        /// <param name="cmdType">CommandType对象下面的属性</param>
        /// <param name="sqlText">被执行的SQL语句</param>
        /// <param name="cmdParms">SQL语句中的参数及其的赋值</param>
        /// <returns>返回DataSet结果集</returns>
        public static DataSet ExecuteDataSet(CommandType cmdType, string sqlText, params SqlParameter[] cmdParms)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(conStr_String);
            SqlCommand cmd = new SqlCommand();

            PrepareCommand(conn, cmd, cmdType, sqlText, cmdParms);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            cmd.Parameters.Clear();
            return ds;
        }

        public static DataSet ExecuteDataSet(string sqlText)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(conStr_String);
            SqlCommand cmd = new SqlCommand();

            PrepareCommand(conn, cmd, CommandType.Text, sqlText, null);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            cmd.Parameters.Clear();
            return ds;
        }


        /// <summary>
        /// 得到个DataTable 
        /// </summary>
        /// <param name="cmdType">CommandType对象下面的属性</param>
        /// <param name="sqlText">被执行的SQL语句</param>
        /// <param name="cmdParms">SQL语句中的参数及其的赋值</param>
        /// <returns>返回一个DataTable</returns>
        public static DataTable ExecuteDataTable(CommandType cmdType, string sqlText, params SqlParameter[] cmdParms)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(conStr_String);
            SqlCommand cmd = new SqlCommand();

            PrepareCommand(conn, cmd, cmdType, sqlText, cmdParms);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            cmd.Parameters.Clear();
            return dt;
        }

        public static DataTable ExecuteDataTable(string sqlText)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(conStr_String);
            SqlCommand cmd = new SqlCommand();

            PrepareCommand(conn, cmd, CommandType.Text, sqlText, null);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            cmd.Parameters.Clear();
            return dt;
        }


        /// <summary>
        /// 执行一条SQL语句;返回第一行的第一列
        /// </summary>
        /// <param name="cmdType">CommandType对象下面的属性</param>
        /// <param name="sqlText">被执行的SQL语句</param>
        /// <param name="cmdParms">SQL语句中的参数及其的赋值</param>
        /// <returns>返回第一行的第一列</returns>
        public static object ExecuteScalar(CommandType cmdType, string sqlText, params SqlParameter[] cmdParms)
        {
            SqlConnection conn = new SqlConnection(conStr_String);
            SqlCommand cmd = new SqlCommand();

            try
            {
                PrepareCommand(conn, cmd, cmdType, sqlText, cmdParms);
                object row = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return row;
            }
            catch
            {
                conn.Close();
                throw;
            }
            finally
            {
                conn.Close();
            }

        }

        public static object ExecuteScalar(string sqlText)
        {
            SqlConnection conn = new SqlConnection(conStr_String);
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(conn, cmd, CommandType.Text, sqlText, null);
                object row = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return row;
            }
            catch
            {
                conn.Dispose();
                conn.Close();
                throw;
            }
            finally
            {
                conn.Dispose();
                conn.Close();
            }
        }


        /// <summary>
        /// 处理命令对象
        /// </summary>
        /// <param name="conn">连接对象</param>
        /// <param name="cmd">命令对象</param>
        /// <param name="cmdType">CommandType对象下面的属性</param>
        /// <param name="sqlText">被执行的SQL语句</param>
        /// <param name="cmdParms">SQL语句中的参数及其的赋值</param>
        private static void PrepareCommand(SqlConnection conn, SqlCommand cmd, CommandType cmdType, string sqlText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = sqlText;
            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                {
                    cmd.Parameters.Add(parm);
                }
            }
        }
    }
}
