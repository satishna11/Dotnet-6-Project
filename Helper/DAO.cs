using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McavesDataAccess
{

    public static class DAOHelper
    {
        static DAOHelper()
        {

        }
        public static string connectionStr = "Server=localhost;Database=InventoryRe;User Id=SA;Password=MyStrongPass123;TrustServerCertificate=True";
        public static SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection(connectionStr);
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            return con;

        }

        public static int IUD(string sql, SqlParameter[] param, CommandType cmdType)
        {
            using (SqlConnection con = GetConnection())
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = cmdType;
                if (param != null)
                {
                    cmd.Parameters.AddRange(param);
                }
                return cmd.ExecuteNonQuery();
            }
        }
        public static DataTable GetTable(string sql, SqlParameter[] param, CommandType cmdType)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = GetConnection())
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = cmdType;
                if (param != null)
                {
                    cmd.Parameters.AddRange(param);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;

            }
        }

        public static DataSet GetDataSet(string sql, SqlParameter[] param, CommandType cmdType)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = GetConnection())
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = cmdType;
                if (param != null)
                {
                    cmd.Parameters.AddRange(param);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                return ds;

            }
        }

        //public static List<ComplainTypeVM> ExecuteDataReader(string sql, SqlParameter[] param, CommandType cmdType)
        //{

        //    List<ComplainTypeVM> types = new List<ComplainTypeVM>();
        //    using (SqlConnection con = GetConnection())
        //    {
        //        SqlCommand cmd = new SqlCommand(sql, con);
        //        cmd.CommandType = cmdType;
        //        if (param != null)
        //        {
        //            cmd.Parameters.AddRange(param);
        //        }
        //        SqlDataReader rdr = cmd.ExecuteReader();

        //        while (rdr.Read())
        //        {
        //            types.Add(new ComplainTypeVM()
        //            {
        //                ComplainTypeID = rdr.GetInt32(rdr.GetOrdinal("ComplainTypeID")),
        //                ComplainName = rdr.GetString(rdr.GetOrdinal("ComplainName")),
        //                ComplainCode = rdr.GetString(rdr.GetOrdinal("ComplainCode")),
        //            });
        //        }

        //        //rdr.NextResult();
        //        //while (rdr.Read())
        //        //{

        //        //}

        //        //DataTable dt;
        //        //dt.Load(rdr);
        //    }
        //    return types;
        //}

        public static DataTable GetTableWithTVP(string sql, SqlParameter[] param, CommandType cmdType, DataTable ds, string paramName)
        {
            DataTable dt = new DataTable();
            SqlParameter sqlPara;
            int i = 0;
            using (SqlConnection con = GetConnection())
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = cmdType;
                if (param != null)
                {
                    cmd.Parameters.AddRange(param);
                }

                sqlPara = new SqlParameter();
                sqlPara.ParameterName = paramName;
                sqlPara.SqlDbType = SqlDbType.Structured;
                sqlPara.Value = ds;
                cmd.Parameters.Add(sqlPara);



                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;

            }
        }

    }
}
