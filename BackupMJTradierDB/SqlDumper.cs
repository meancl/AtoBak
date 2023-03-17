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
        string sOriginDB;
        string sBackUpDB;
        string sUpConn;
        string sDownConn;
        string sDownDBConn;
        public string sSqlFile;


        public SqlDumper()
        {
            sOriginDB = "mjtradierdb";
            sBackUpDB = "mjtradierdb_bk";
            sUpConn = $"server=221.149.119.60;port=2023;user=meancl;pwd=1234;database={sOriginDB}";
            sDownConn = $"server=localhost;user=root;pwd=root;database={sBackUpDB}";
            sDownDBConn = "server=localhost;user=root;port=3306;password=root";
            sSqlFile = @"./backup.sql";
        }

        #region mainServer DB download
        public void Backup()
        {
            using (MySqlConnection conn = new MySqlConnection(sUpConn))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mainServer = new MySqlBackup(cmd))
                    {
                        DropDB(sBackUpDB);
                        cmd.Connection = conn;
                        conn.Open();
                        mainServer.ExportToFile(sSqlFile);
                        conn.Close();
                    }
                }
            }
        }
        #endregion

        #region serveServer DB upload
        public void Restore()
        {
            using (MySqlConnection conn = new MySqlConnection(sDownConn))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        CreateDB(sBackUpDB);
                        cmd.Connection = conn;
                        conn.Open();
                        mb.ImportFromFile(sSqlFile);
                        conn.Close();
                    }
                }
            }
        }
        #endregion

        #region DB 생성
        void CreateDB(string sDbName)
        {
            using (var conn = new MySqlConnection(sDownDBConn))
            {
                using (var cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = $"CREATE DATABASE IF NOT EXISTS {sDbName} DEFAULT CHARACTER SET utf8mb4";
                    cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region DB 삭제 
        void DropDB(string sDbName)
        {
            using (var conn = new MySqlConnection(sDownDBConn))
            {
                using (var cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = $"DROP DATABASE IF EXISTS {sDbName}";
                    cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion

    }
}
