using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    public class Database
    {

        private SqlConnection con;
        private SqlCommand cmd;

        public void Open()
        {
            con.Open();
        }

        public void Close()
        {
            con.Close();
        }

        public T ExecuteScalar<T>(string command, params SqlParameter[] parameters)
        {
            init(parameters);
            cmd.CommandText = command;
            cmd.CommandType = CommandType.StoredProcedure;
            return (T)cmd.ExecuteScalar();
        }
        public int ExecuteNonQuery(string command, params SqlParameter[] parameters)
        {
            init(parameters);
            cmd.CommandText = command;
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd.ExecuteNonQuery();
        }

        public SqlDataReader ExecuteReader(string command, params SqlParameter[] parameters)
        {
            init(parameters);
            cmd.CommandText = command;
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd.ExecuteReader();
        }

        public Database()
        {
            string config = @"data source=DESKTOP-BD8G1UL\SQLEXPRESS;initial catalog=Internal;trusted_connection=true";
            con = new SqlConnection(config);
            cmd = new SqlCommand();
            cmd.Connection = con;
        }

        public void init(SqlParameter[] parameters)
        {
            cmd.Parameters.Clear();
            if (parameters != null && parameters.Any())
            {
                cmd.Parameters.AddRange(parameters);
            }

        }

        public SqlParameter CreateParameter(string name, string value)
        {
            if (name== "@ProductStartDate")
            {
                DateTime dateTime = DateTime.Parse(value);
                if (dateTime<= DateTime.MinValue)
                {
                    value = "01/01/1970";
                    dateTime = DateTime.Parse(value);
                }
                return new SqlParameter(name, dateTime);
            }
            else
            {
                return new SqlParameter(name, value);
            }
           
        }

        public byte[] GetBytes(SqlDataReader reader, int ordinal)
        {
            byte[] result = null;

            if (!reader.IsDBNull(ordinal))
            {
                long size = reader.GetBytes(ordinal, 0, null, 0, 0); //get the length of data 
                result = new byte[size];
                int bufferSize = 1024;
                long bytesRead = 0;
                int curPos = 0;
                while (bytesRead < size)
                {
                    bytesRead += reader.GetBytes(ordinal, curPos, result, curPos, bufferSize);
                    curPos += bufferSize;
                }
            }

            return result;
        }

    }
}
