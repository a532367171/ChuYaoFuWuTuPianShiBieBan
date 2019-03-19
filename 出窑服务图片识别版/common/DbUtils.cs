using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace 出窑服务图片识别版.common
{
    public static class DbUtils
    {
        static string cs = SqlEasy.connString; //数据库连接字符串


        ///<returns> the id of the inserted object </returns>
        public static int Insert(object o)
        {
            using (var conn = new SqlConnection(cs))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                //cmd.CommandText = "insert " + TableConvention.Resolve(o) + " ("
                //    .InjectFrom(new FieldsBy().IgnoreFields("keyid"), o) + ") values("
                //    .InjectFrom(new FieldsBy().IgnoreFields("keyid").SetFormat("@{0}"), o)
                //    + ") select @@identity";
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        /// <returns>rows affected</returns>
        public static int ExecuteNonQuerySp(string sp)
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sp;
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static int ExecuteNonQuerySp(string sp, string c_cGWid, string c_cGWlineCode, string c_cManufacture)
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    SqlParameter[] sps = new SqlParameter[] { 
                    new SqlParameter("@cGWid",c_cGWid),
                    new SqlParameter("@cGWlineCode",c_cGWlineCode),
                    new SqlParameter("@cManufacture",c_cManufacture)};


                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = sp;

                    //cmd.InjectFrom<SetParamsValues>(parameters);

                    //cmd.Parameters.Add(sps);
                    cmd.Parameters.AddRange(sps);

                    int i = 0;
                    try
                    {
                        conn.Open();
                        i = cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    finally
                    {
                        conn.Close();
                    }
                    return i;
                }
            }
        }

        public static int ExecuteNonQuerySp(string sp, string c_cGWid, string c_cGWlineCode, string c_cGW_text, string c_cManufacture)
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    SqlParameter[] sps = new SqlParameter[] { 
                    new SqlParameter("@cGWid",c_cGWid),
                    new SqlParameter("@cGWlineCode",c_cGWlineCode),
                    new SqlParameter("@cGW_text",c_cGW_text),
                    new SqlParameter("@cManufacture",c_cManufacture)};


                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = sp;

                    //cmd.InjectFrom<SetParamsValues>(parameters);

                    //cmd.Parameters.Add(sps);
                    cmd.Parameters.AddRange(sps);

                    int i = 0;
                    try
                    {
                        conn.Open();
                        i = cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    finally
                    {
                        conn.Close();
                    }
                    return i;
                }
            }
        }

      

    }

}
