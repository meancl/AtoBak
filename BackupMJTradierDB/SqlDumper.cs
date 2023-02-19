using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupMJTradierDB
{
    public class SqlDumper
    {
        const string sOriginDB = "mjtradierdb";
        const string sBackUpDB = "mjtradierdb_bk";
        string sUpConn;
        string sDownConn;
        string sDownDBConn;
        string sSqlFile = @"C:\Users\MJ\source\repos\BackupMJTradierDB\BackupMJTradierDB\bin\Debug\backup.sql";


        public SqlDumper()
        {
            sUpConn = $"server=221.149.119.60;port=2023;user=meancl;pwd=1234;database={sOriginDB}";
            sDownConn = $"server=localhost;user=root;pwd=1234;database={sBackUpDB}";
            sDownDBConn = "server=localhost;user=root;port=3306;password=1234"; 
        }
        public void Backup()
        {
            using (MySqlConnection conn = new MySqlConnection(sUpConn))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        mb.ExportToFile(sSqlFile);
                        conn.Close();
                        DropDB();
                    }
                }
            }
        }


        public void Restore()
        {
            using (MySqlConnection conn = new MySqlConnection(sDownConn))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        CreateDB();
                        cmd.Connection = conn;
                        conn.Open();
                        mb.ImportFromFile(sSqlFile);
                        conn.Close();
                    }
                }
            }
        }

        void CreateDB(string sDbName=sBackUpDB)
        {
            using (var conn = new MySqlConnection(sDownDBConn))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = $"CREATE DATABASE IF NOT EXISTS {sDbName} DEFAULT CHARACTER SET utf8mb4";
                cmd.ExecuteNonQuery();
            }
        }

        void DropDB(string sDbName=sBackUpDB)
        {
            using (var conn = new MySqlConnection(sDownDBConn))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = $"DROP DATABASE IF EXISTS {sDbName}";
                cmd.ExecuteNonQuery();
            }
        }

    }
}
